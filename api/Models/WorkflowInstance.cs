
namespace api.Models;

public class WorkflowInstance
{
    public string Id { get; set; } = string.Empty;
    public string DefinitionId { get; set; } = string.Empty;
    public string CurrentStateId { get; set; } = string.Empty;
    public List<ActionHistory> History { get; set; } = new();
}

public class ActionHistory
{
    public string ActionId { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string FromState { get; set; } = string.Empty;
    public string ToState { get; set; } = string.Empty;
}
