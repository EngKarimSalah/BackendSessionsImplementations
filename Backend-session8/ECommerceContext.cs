using ECommerce_Solution.Models;

namespace ECommerce_Solution
{
    // In-memory storage context — holds one List<T> for every entity in the system.
    // Note: OrderItem is included as a separate list because it is a full entity
    // (the bridge class that resolves the Order <-> Product many-to-many relationship
    // and carries the quantity attribute).
    public class ECommerceContext
    {
        public List<User>      Users      { get; set; }
        public List<Category>  Categories { get; set; }
        public List<Product>   Products   { get; set; }
        public List<Order>     Orders     { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<Review>    Reviews    { get; set; }
    }
}






//public DbSet<User> Users { get; set; }
//public DbSet<Category> Categories { get; set; }
//public DbSet<Product> Products { get; set; }
//public DbSet<Order> Orders { get; set; }
//public DbSet<OrderItem> OrderItems { get; set; }
//public DbSet<Review> Reviews { get; set; }

//protected override void OnConfiguring(DbContextOptionsBuilder options)
//{
//    options.UseSqlServer("Server=localhost;Database=ECommerceDB;Trusted_Connection=True;TrustServerCertificate=True;");
//}



