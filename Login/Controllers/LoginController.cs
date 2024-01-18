using Microsoft.AspNetCore.Mvc;
using Login.Models;
using Login.Data;

namespace Login.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase{
    
    private readonly MyDBContext _context;
    public LoginController(MyDBContext context){
        _context=context;
    } 

    [HttpPost("ValidateUser", Name = "ValidateUser")]
    public IActionResult ValidateUser(User user){
        var isExists = _context.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

        if(isExists!=null){
            return Ok($"User is authorized having id={isExists.Id}");
        }
        return Unauthorized("User not found!");
    }

    [HttpPost("CreateUser", Name = "CreateUser")]
    public IActionResult CreateUser(User newUser){

        if (_context.Users == null)
        {
            return Problem("Entity set is null.");
        }
        else if (newUser == null)
        {
            return BadRequest("Entity is null.");
        }
        else if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        else
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Ok("User created!");
        }

    }

}