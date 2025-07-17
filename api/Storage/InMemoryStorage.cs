using api.Models;

namespace api.Storage;

public class InMemoryStorage
{
    private readonly Dictionary<string, WorkflowDefinition> _definitions = new();
    private readonly Dictionary<string, WorkflowInstance> _instances = new();

    public void SaveDefinition(WorkflowDefinition d) => _definitions[d.Id] = d;
    public WorkflowDefinition? GetDefinition(string id) => _definitions.TryGetValue(id, out var d) ? d : null;
    public void SaveInstance(WorkflowInstance i) => _instances[i.Id] = i;
    public WorkflowInstance? GetInstance(string id) => _instances.TryGetValue(id, out var i) ? i : null;
}
