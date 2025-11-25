using Domain.Commands.v1.TimeOff.Create;
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

    //[HttpPost("{protocol}/approve")]
    //public async Task<IActionResult> Approve(string protocol, [FromBody] ApproveTimeOffCommand approveTimeOffCommand)
    //{
    //    var command = new ApproveTimeOffCommand { Protocol = protocol, Description = approveTimeOffCommand.Description };
    //    var result = await _mediator.Send(command);

    //    return Ok(result);
    //}

    //[HttpPost("{protocol}/reject")]
    //public async Task<IActionResult> Reject(string protocol, [FromBody] RejectTimeOffCommand rejectTimeOffCommand)
    //{
    //    var command = new RejectTimeOffCommand
    //    {
    //        Protocol = protocol,
    //        Description = rejectTimeOffCommand.Description
    //    };

    //    var result = await _mediator.Send(command);

    //    return Ok(result);
    //}
}
