using Microsoft.AspNetCore.Mvc;

namespace Microservice2.Controllers;

[ApiController]
[Route("controller")]
public class Ms2Controller : ControllerBase{

    private readonly ILogger<Ms2Controller> _logger;
    
    public Ms2Controller(ILogger<Ms2Controller> logger){
        _logger=logger;
    }

    [HttpGet]
    public async Task<ActionResult<string>> Get(){
        HttpClient httpClient=new HttpClient();
        var response=await httpClient.GetAsync("http://localhost:5034/controller");
        var res=await response.Content.ReadAsStringAsync();
        Console.WriteLine(res);
        return Ok("Microservice 2 reading content of "+res);
    }
}