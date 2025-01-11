using ASP_NET_MVC_Shop_Example.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_MVC_Shop_Example.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}