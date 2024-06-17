using Microsoft.EntityFrameworkCore;
using CRUDWithWebAPI.Models;


//DATA ACCESS LAYER
// This class is responsible for interacting with the database.
namespace CRUDWithWebAPI.DAL
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
