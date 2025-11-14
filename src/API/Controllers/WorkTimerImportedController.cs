using Infrastructure.Data.Query.Queries.v1.GetAllWorkTimersImported;

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

    [HttpGet()]
    [ProducesResponseType(typeof(List<GetAllWorkTimersImportedQueryResponse>), (int)HttpStatusCode.OK)]
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
}