using api.Models;

namespace api.Services;

public interface IWorkflowService
{
    WorkflowDefinition CreateDefinition(WorkflowDefinition d);
    WorkflowInstance StartInstance(string definitionId);
    WorkflowInstance ExecuteAction(string instanceId, string actionId);
    WorkflowInstance? GetInstance(string id);
    WorkflowDefinition? GetDefinition(string id);
}
