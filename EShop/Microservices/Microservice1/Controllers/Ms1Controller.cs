using Microsoft.AspNetCore.Mvc;

namespace Microservice1.Controllers;

[ApiController]
[Route("controller")]
public class Ms1Controller : ControllerBase{

    private readonly ILogger<Ms1Controller> _logger;
    
    public Ms1Controller(ILogger<Ms1Controller> logger){
        _logger=logger;
    }

    [HttpGet]
    public ActionResult<string> Get(){
        return "Microservice 1";
    }


}