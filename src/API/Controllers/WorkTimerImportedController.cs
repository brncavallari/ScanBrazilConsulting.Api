namespace API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class WorkTimerImportedController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("file/upload")]
    [DisableRequestSizeLimit]
    public async Task<IActionResult> Upload([FromForm] UploadWorkTimerImportedCommand command)
    {
        await _mediator.Send(command);
        return Ok("deu bom");
    }
}