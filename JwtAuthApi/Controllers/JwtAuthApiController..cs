using Microsoft.AspNetCore.Mvc;
using JwtAuthApi.Models;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace JwtAuthApi.Controllers;

[ApiController]
[Route("[controller]")]
public class JwtAuthApiController : ControllerBase{
    
    [HttpPost]
    public IActionResult ValidateUser(User user){
        
        if(user.Username=="admin"&&user.Password=="1234"){
            var token = GenerateJwtToken(user.Username);
            return Ok(new {Token=token});
        }
        return Unauthorized("User not found!");
    }

    private string GenerateJwtToken(string username){
        var key=Encoding.ASCII.GetBytes("admin123admin123admin123admin0123");
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}