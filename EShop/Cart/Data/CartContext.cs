using Microsoft.EntityFrameworkCore;
using Cart.Models;

namespace Cart.Data  
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options) : base(options) { }
        public DbSet<CartItem> CartProducts { get; set; }
    }
}