using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Services;

namespace WorkflowEngine.Controllers;

[ApiController]
[Route("api/workflow")]
public class WorkflowController(IWorkflowService service) : ControllerBase
{
    [HttpPost("definitions")]
    public ActionResult<WorkflowDefinition> Create(WorkflowDefinition d)
    {
        try { return Ok(service.CreateDefinition(d)); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet("definitions/{id}")]
    public ActionResult<WorkflowDefinition> GetDef(string id)
    {
        var def = service.GetDefinition(id);
        return def == null ? NotFound() : Ok(def);
    }

    [HttpPost("instances")]
    public ActionResult<WorkflowInstance> Start([FromBody] StartInstanceRequest r)
    {
        try { return Ok(service.StartInstance(r.DefinitionId)); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpPost("instances/{id}/actions/{actionId}")]
    public ActionResult<WorkflowInstance> Exec(string id, string actionId)
    {
        try { return Ok(service.ExecuteAction(id, actionId)); }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [HttpGet("instances/{id}")]
    public ActionResult<WorkflowInstance> GetInst(string id)
    {
        var inst = service.GetInstance(id);
        return inst == null ? NotFound() : Ok(inst);
    }
}

public class StartInstanceRequest
{
    public string DefinitionId { get; set; } = string.Empty;
}
