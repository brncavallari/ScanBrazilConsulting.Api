using Domain.Commands.v1.UserTimer.Update;
using Infrastructure.Data.Query.Queries.v1.GetUserTimerByEmail;

namespace API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Authorize]
public class UserTimerController(
    IMediator _mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<IActionResult> Create([FromBody] UserTimerCommand command)
    {
        await _mediator.Send(command);
        return Ok("deu bom");
    }


    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get([FromQuery] GetUserTimerByEmailQuery getWorkTimerByEmailQuery)
    {
        try
        {
            var getUserTimerByEmailQueryResponse = await _mediator.Send(getWorkTimerByEmailQuery);
            return Ok(getUserTimerByEmailQueryResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update([FromBody] UpdateUserTimerCommand command)
    {
        await _mediator.Send(command);
        return Ok("deu bom");
    }
}