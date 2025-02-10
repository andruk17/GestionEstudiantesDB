using GestionEstudiantesDB.Models;
using GestionEstudiantesDB.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionEstudiantesDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (user.Username == "admin" && user.Password == "admin2025")
            {
                var token = _jwtService.GenerateToken(user.Username, "Admin");
                return Ok(new { Token = token });
            }

            return Unauthorized("Credenciales inválidas");
        }
    }
}
