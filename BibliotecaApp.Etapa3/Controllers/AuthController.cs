using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BibliotecaApp.Etapa3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IConfiguration configuration) : ControllerBase
{
    [HttpPost("token")]
    public IActionResult CreateToken([FromQuery] string username = "demo-user", [FromQuery] string role = "Reader")
    {
        var key = configuration["Jwt:Key"] ?? "super-secret-key-for-stage3-demo";
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddHours(2), signingCredentials: credentials);

        return Ok(new { access_token = new JwtSecurityTokenHandler().WriteToken(token), expires_in = 7200 });
    }
}
