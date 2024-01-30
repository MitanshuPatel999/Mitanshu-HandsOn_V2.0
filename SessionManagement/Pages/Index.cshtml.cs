using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SessionManagement.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public const string SKeyName="_Name";
    public const string SKeyAge="_Age";
    public const string SKeyTime="_Time";
    [BindProperty]
    public Customer Customer{get;set;}

    [BindProperty]
    public int Amount{get;set;}
    [TempData]
    public string TempMessage{get;set;}

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnPostSubmitForm()
    {
        if(HttpContext.Session.GetString(SKeyAge)==null || HttpContext.Session.GetString(SKeyAge)==""){
            HttpContext.Session.SetString(SKeyName,"Session One");
            // HttpContext.Session.SetString(SKeyName,"Session Two");
            HttpContext.Session.SetInt32(SKeyAge,20);
        }

        if (HttpContext.Session.Get(SKeyTime) == default)
            {
                HttpContext.Session.SetString(SKeyTime, DateTime.Now.ToString());
            }

        var name=HttpContext.Session.GetString(SKeyName);
        var age=HttpContext.Session.GetInt32(SKeyAge);
        var time=HttpContext.Session.GetString(SKeyTime);

        _logger.LogInformation($"Session Name : {name}", name);
        _logger.LogInformation($"Session Age : {age}", age);
        _logger.LogInformation($"Session Time : {time}", time);
        _logger.LogInformation($"Amount(Rs.) : {Amount}");
        TempMessage=$"{Customer.Id} => {Customer.Name}";

        return RedirectToPage();
    }
  
}

public class Customer{
    public int Id{get;set;}
    
    public string? Name{get;set;}
}

