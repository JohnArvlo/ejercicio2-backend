using caso2_solucion.application.Users.Commands.Login;
using caso2_solucion.application.Users.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace caso2_solucion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator) => _mediator = mediator;

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var token = await _mediator.Send(command);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { id });
        }
    }
}
