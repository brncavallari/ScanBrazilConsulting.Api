using Domain.Commands.v1.UserTimer.Create;
using Infrastructure.Data.Query.Queries.v1.UserTimer.GetAllUserTimer;
using Infrastructure.Data.Query.Queries.v1.UserTimer.GetUserTimerByEmail;

namespace API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Authorize]
public class UserTimerController(
    IMediator _mediator) : ControllerBase
{
    [HttpGet("byEmail")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByEmail()
    {
        try
        {
            var getUserTimerByEmailQueryResponse = await _mediator.Send(new GetUserTimerByEmailQuery());
            return Ok(getUserTimerByEmailQueryResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update([FromBody] UpdateUserTimerCommand updateUserTimerCommand)
    {
        await _mediator.Send(updateUserTimerCommand);
        return Ok();
    }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUserTimerQuery getAllUserInformationQuery)
    {
        try
        {
            var getAllUsersResponse = await _mediator.Send(getAllUserInformationQuery);
            return Ok(getAllUsersResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserTimerCommand createUserTimerCommand)
    {
        try
        {
            await _mediator.Send(createUserTimerCommand);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}