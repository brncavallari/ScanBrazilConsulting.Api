namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    [Authorize]
    public class UserTimerController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] UserTimerCommand command)
        {
            await _mediator.Send(command);
            return Ok("deu bom");
        }
    }
}