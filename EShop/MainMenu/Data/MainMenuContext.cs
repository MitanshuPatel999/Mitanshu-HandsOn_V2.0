using Microsoft.EntityFrameworkCore;
using MainMenu.Models;

namespace MainMenu.Data  
{
    public class MainMenuContext : DbContext
    {
        public MainMenuContext(DbContextOptions<MainMenuContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}