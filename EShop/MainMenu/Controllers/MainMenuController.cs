using Microsoft.AspNetCore.Mvc;
using MainMenu.Models;
using MainMenu.Data;
using Microsoft.EntityFrameworkCore;

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

}

