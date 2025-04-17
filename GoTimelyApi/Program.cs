using GoTimelyAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GoTimelyAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting application...");

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GoTimely API", Version = "v1" });
            });

            // CORS configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://127.0.0.1:5500")  // Your frontend server address
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // EF Core database setup
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            Console.WriteLine("Application built, now running...");

            // Enable Swagger only in Development
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // CORS middleware configuration
            app.UseCors("AllowFrontend");  // Enabling CORS with the 'AllowFrontend' policy

            app.UseRouting();
            app.UseAuthorization();

            // Map controllers
            app.MapControllers();

            // Test database connection
            try
            {
                Console.WriteLine("Testing database connection...");

                using (var scope = app.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    context.Database.EnsureCreated();  // Or use Migrations if needed
                    Console.WriteLine("Database connection test passed successfully.");
                    app.Logger.LogInformation("Database connection test passed successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database connection failed: {ex.Message}");
                app.Logger.LogError($"Database connection failed: {ex.Message}");
            }

            app.Run();
        }
    }
}
