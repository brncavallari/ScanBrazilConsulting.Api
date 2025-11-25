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
            var token = Request.Headers.Authorization.ToString();

            if (token is null) return Unauthorized();

            var getUserTimerByEmailQueryResponse = await _mediator.Send(new GetUserTimerByEmailQuery(token));
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
    public async Task<IActionResult> GetAll([FromQuery] GetAllUserInformationQuery getAllUserInformationQuery)
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
}