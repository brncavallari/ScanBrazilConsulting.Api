using Infrastructure.Data.Query.Queries.v1.WorkTimer.GetAllWorkTimersImported;

namespace API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class WorkTimerImportedController(
    IMediator _mediator) : ControllerBase
{
    [HttpPost("file/upload")]
    [DisableRequestSizeLimit]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Upload([FromForm] UploadWorkTimerImportedCommand uploadWorkTimerImportedCommand)
    {
        try
        {
            await _mediator.Send(uploadWorkTimerImportedCommand);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetAllWorkTimersImportedQueryResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllWorkTimersImported()
    {
        try
        {
            var result = await _mediator.Send(new GetAllWorkTimersImportedQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteWorkTimerImported(string id)
    {
        try
        {
            var command = new DeleteWorkTimerImportedCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}