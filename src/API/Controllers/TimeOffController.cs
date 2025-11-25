using Domain.Commands.v1.TimeOff.Create;

namespace API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
//[Authorize]
public class TimeOffController(
    IMediator _mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<IActionResult> Post([FromBody] CreateTimeOffCommand createTimeOffCommand)
    {
        await _mediator.Send(createTimeOffCommand);
        return Ok();
    }
}
