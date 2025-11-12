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
}