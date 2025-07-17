using api.Models;
using api.Storage;

namespace api.Services;

public class WorkflowService : IWorkflowService
{
    private readonly InMemoryStorage _store;

    public WorkflowService(InMemoryStorage store) => _store = store;

    public WorkflowDefinition CreateDefinition(WorkflowDefinition d)
    {
        if (d.States.Count(s => s.IsInitial) != 1)
            throw new ArgumentException("Must have exactly one initial state");

        if (d.States.Select(s => s.Id).Distinct().Count() != d.States.Count)
            throw new ArgumentException("Duplicate state IDs");

        if (d.Actions.Select(a => a.Id).Distinct().Count() != d.Actions.Count)
            throw new ArgumentException("Duplicate action IDs");

        var stateIds = d.States.Select(s => s.Id).ToHashSet();

        foreach (var a in d.Actions)
        {
            if (!stateIds.Contains(a.ToState) || a.FromStates.Any(f => !stateIds.Contains(f)))
                throw new ArgumentException("Invalid state reference in action");
        }

        _store.SaveDefinition(d);
        return d;
    }

    public WorkflowDefinition? GetDefinition(string id) => _store.GetDefinition(id);

    public WorkflowInstance StartInstance(string defId)
    {
        var def = _store.GetDefinition(defId) ?? throw new ArgumentException("Invalid definition");
        var init = def.States.First(s => s.IsInitial);

        var inst = new WorkflowInstance
        {
            Id = Guid.NewGuid().ToString(),
            DefinitionId = defId,
            CurrentStateId = init.Id
        };

        _store.SaveInstance(inst);
        return inst;
    }

    public WorkflowInstance ExecuteAction(string instanceId, string actionId)
    {
        var inst = _store.GetInstance(instanceId) ?? throw new ArgumentException("Instance not found");
        var def = _store.GetDefinition(inst.DefinitionId) ?? throw new InvalidOperationException("Definition not found");

        var action = def.Actions.FirstOrDefault(a => a.Id == actionId && a.Enabled);
        if (action == null || !action.FromStates.Contains(inst.CurrentStateId))
            throw new InvalidOperationException("Invalid action");

        var to = def.States.FirstOrDefault(s => s.Id == action.ToState && s.Enabled);
        var from = def.States.First(s => s.Id == inst.CurrentStateId);
        if (from.IsFinal || to == null)
            throw new InvalidOperationException("Transition not allowed");

        inst.History.Add(new ActionHistory { ActionId = actionId, FromState = inst.CurrentStateId, ToState = to.Id });
        inst.CurrentStateId = to.Id;

        _store.SaveInstance(inst);
        return inst;
    }

    public WorkflowInstance? GetInstance(string id) => _store.GetInstance(id);
}
