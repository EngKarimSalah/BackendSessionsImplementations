using FirstWebApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApp
{
    public class ProjectContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }   
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
        }
    }
}
