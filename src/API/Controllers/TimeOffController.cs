using Domain.Commands.v1.TimeOff.Approve;
using Domain.Commands.v1.TimeOff.Create;
using Domain.Commands.v1.TimeOff.Reject;
using Infrastructure.Data.Query.Queries.v1.TimeoOff.GetAllTimeOff;
using Infrastructure.Data.Query.Queries.v1.TimeoOff.GetTimeOffByProtocol;

namespace API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Authorize]
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

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllTimeOffQuery getAllTimeOffQuery)
    {
        var getAllTimeOffResponse = await _mediator.Send(getAllTimeOffQuery);
        return Ok(getAllTimeOffResponse);
    }

    [HttpGet("{protocol}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByProtocol(string protocol)
    {
        var getTimeOffByProtocolResponse = await _mediator.Send(new GetTimeOffByProtocolQuery(protocol));
        return Ok(getTimeOffByProtocolResponse);
    }

    [HttpPost("{protocol}/approve")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Approve(string protocol, [FromBody] ApproveTimeOffCommand approveTimeOffCommand)
    {
        approveTimeOffCommand.Protocol = protocol;
        await _mediator.Send(approveTimeOffCommand);

        return Ok();
    }

    [HttpPost("{protocol}/reject")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Reject(string protocol,[FromBody] RejectTimeOffCommand rejectTimeOffCommand)
    {
        rejectTimeOffCommand.Protocol = protocol;
        await _mediator.Send(rejectTimeOffCommand);

        return Ok();
    }
}
