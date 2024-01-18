using Microsoft.EntityFrameworkCore;
using Login .Models;

namespace Login.Data{
    public class MyDBContext : DbContext{
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options){
        }

        public DbSet<User> Users { get; set; }
    }
}