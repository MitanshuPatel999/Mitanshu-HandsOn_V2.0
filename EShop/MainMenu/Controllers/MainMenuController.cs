using Microsoft.AspNetCore.Mvc;
using MainMenu.Models;
using MainMenu.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

[ApiController]
[Route("MMcontroller")]
public class MainMenuController : ControllerBase{

    private readonly MainMenuContext _context;

    public MainMenuController(MainMenuContext context){
        _context=context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(){
        if(_context.Products==null) return Problem();

        return await _context.Products.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id){
        if(_context.Products==null) return Problem("Something is wrong!");

        var prod= await _context.Products.FindAsync(id);
        
        if(prod==null){
            return NotFound("No Product Found");
        }

        return prod;
    }

    [HttpPost]
    public async Task<ActionResult> PostProduct(Product prod){
        if(_context.Products==null) return Problem("Someting is wrong!");

        _context.Products.Add(prod);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProduct",new{id=prod.Id},prod);

        // return prod;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutProduct(int id, Product prod){
        if(_context.Products==null) return Problem("Someting is wrong!");

        if(id!=prod.Id) return BadRequest();

        _context.Entry(prod).State=EntityState.Modified;
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetProduct",new{id=prod.Id},prod);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DelProduct(int id){
        if(_context.Products==null) return Problem("Someting is wrong!");

        var prod=await _context.Products.FindAsync(id);

        if(prod==null){
            return NotFound("No product found for removing!");
        }

        _context.Products.Remove(prod);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("addtocart/{id}")]
    public async Task<ActionResult> AddToCart(int id){
        if(_context.Products==null) return Problem("Someting is wrong!");
        
        var currentProd=await _context.Products.FindAsync(id);
        
        if(currentProd==null) return Problem("No product present for adding it into cart!");

        HttpClient httpClient=new HttpClient();

        var response=await httpClient.GetAsync($"http://localhost:5169/Ccontroller/{id}");
        var res= await response.Content.ReadAsStringAsync();
        Console.WriteLine(res);
       



        if (res == null || res == "")
        {
            Console.WriteLine("################## No Product in Cart ##################");
            HttpResponseMessage response2 = await httpClient.PostAsJsonAsync("http://localhost:5169/Ccontroller",new{productId=currentProd.Id,currentProd.ProductName,currentProd.Price,quantity=1});
            response2.EnsureSuccessStatusCode();
            return Ok("Product added to cart!");
        }else{
            Console.WriteLine("################## Product in Cart ##################");
            JObject json=JObject.Parse(res);
            int quantity=(int)json["quantity"];
            int cId=(int)json["id"];
            HttpResponseMessage response3 = await httpClient.PutAsJsonAsync($"http://localhost:5169/Ccontroller/{cId}",new{id=cId,productId=currentProd.Id,currentProd.ProductName,currentProd.Price,quantity=quantity+1});
            response3.EnsureSuccessStatusCode();
            return Ok("Product quantity increased!");
        }
    }

    public class CartItem{
        public int Id{get;set;}
        public int ProductId{get;set;}
        public string? ProductName{get;set;}
        public int Price{get;set;}
        public int Quantity{get;set;
    }
}

}

