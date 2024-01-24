using Microsoft.AspNetCore.Mvc;
using Cart.Models;
using Cart.Data;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("Ccontroller")]
public class MainMenuController : ControllerBase{

    private readonly CartContext _context;

    public MainMenuController(CartContext context){
        _context=context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CartItem>>> GetProducts(){
        if(_context.CartProducts==null) return Problem();

        return await _context.CartProducts.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CartItem>> GetProduct(int id){
        if(_context.CartProducts==null) return Problem("Something is wrong!");

        var prod= await _context.CartProducts.FirstOrDefaultAsync(p=>p.ProductId==id);
        
        if(prod==null){
            return NotFound("");
        }

        return prod;
    }

    [HttpPost]
    public async Task<ActionResult> PostProduct(CartItem prod){
        if(_context.CartProducts==null) return Problem("Someting is wrong!");
        // var item=_context.CartProducts.FindAsync(prod.Id);
        
        _context.CartProducts.Add(prod);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProduct",new{id=prod.Id},prod);

        // return prod;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutProduct(int id, CartItem prod){
        if(_context.CartProducts==null) return Problem("Someting is wrong!");

        if(id!=prod.Id) return BadRequest();

        _context.Entry(prod).State=EntityState.Modified;
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetProduct",new{id=prod.Id},prod);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DelProduct(int id){
        if(_context.CartProducts==null) return Problem("Someting is wrong!");

        var prod=await _context.CartProducts.FindAsync(id);

        if(prod==null){
            return NotFound("No product found for removing!");
        }

        _context.CartProducts.Remove(prod);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}

