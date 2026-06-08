using AbitElit.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
namespace AbitElit.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var token = authService.Authenticate(request);

            if (token == null)
            {
                return Unauthorized("Невірний логін або пароль.");
            }

            return Ok(new { Token = token }); //повертає токен на клієнт
        }
    }
}