using Microsoft.AspNetCore.Mvc;
using JwtAuthApi.Models;
using JwtAuthApi.Services;

namespace JwtAuthApi.Controllers;

[ApiController]
[Route("[controller]")]
public class JwtAuthApiController : ControllerBase{

    private readonly IJwtService? _jwtService;

    public JwtAuthApiController(IJwtService jwtService){
        _jwtService=jwtService;
    }
    
    [HttpPost]
    public IActionResult ValidateUser(User user){
        
        if(user.Username=="admin"&&user.Password=="1234"){
            var token= _jwtService?.GenerateJwtToken(user.Username);
            var dt= _jwtService?.DecodeJwtToken(token.Trim(), "admin123admin123admin123admin0123");

            return Ok(new {EncToken=token, DecToken=dt});
        }
        return Unauthorized("User not found!");
    }
}