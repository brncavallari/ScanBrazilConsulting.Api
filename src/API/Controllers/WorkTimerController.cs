using Domain.Commands.v1.WorkTimer.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class WorkTimerController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Create([FromBody] CreateWorkTimerCommand createWorkTimerCommand)
    {
        await _mediator.Send(createWorkTimerCommand);
        return Ok("deu bom");
    }
}
