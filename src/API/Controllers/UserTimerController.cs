using Domain.Commands.v1.UserTimer.CreateOrUpdate;
using Domain.Commands.v1.UserTimer.Delete;
using Domain.Commands.v1.UserTimer.UpdateHours;
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
    public async Task<IActionResult> Update([FromBody] UpdateHoursCommand updateHoursCommand)
    {
        await _mediator.Send(updateHoursCommand);
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
    public async Task<IActionResult> CreateOrUpdate([FromBody] CreateOrUpdateCommand createOrUpdateCommand)
    {
        try
        {
            await _mediator.Send(createOrUpdateCommand);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteById(string id)
    {
        try
        {
            await _mediator.Send(new DeleteCommand(id));
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}