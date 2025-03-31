using Microsoft.AspNetCore.Mvc;
using WebApiPerson.Models;
using WebApiPerson.Services;

namespace WebApiPerson.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin user)
        {
            var token = _authService.Authenticate(user);
            if (token == null)
                return Unauthorized(new { message = "Credenciales incorrectas" });

            return Ok(new { token });
        }
    }
}
