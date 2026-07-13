
using FirstWebApp.Repositories;
using FirstWebApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //services container ( place to register program services ) / dependency injection container
            var builder = WebApplication.CreateBuilder(args); //start line of service container

            // Add services to the container.

            //1-register context ( make an object )
            builder.Services.AddDbContext<ProjectContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddScoped<ProductRepo>();
            //builder.Services.AddScoped<CategoryRepo>();
            //builder.Services.AddScoped<UserRepo>();
            //builder.Services.AddScoped<ReviewRepo>();

            builder.Services.AddScoped<ProductService>();
            //builder.Services.AddScoped<CategoryService>();
            //builder.Services.AddScoped<UserService>();
            //builder.Services.AddScoped<ReveiwService>();


            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build(); //end line of service container
            ////////////////////////////////////////////////////////////////////////////






            // Configure the HTTP request pipeline / middleware pipline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection(); //middleware

            app.UseAuthorization(); //middleware


            app.MapControllers(); //middleware
            //////////////////////////////////





            //run application
            app.Run();
        }
    }
}
