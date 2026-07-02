using ECommerce_Solution.Models;
using Microsoft.EntityFrameworkCore;


namespace ECommerce_Solution
{
   
    public class ECommerceContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost;Database=ECommerceDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }



    }
}

// database address (connection string) =>
//   " server = localhost;  Database=ECommerceDB;  username=your_name; password=your_password; tusted"






