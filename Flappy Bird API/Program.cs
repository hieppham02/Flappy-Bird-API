using Flappy_Bird_API.Database;
using Microsoft.EntityFrameworkCore;
using System;

namespace Flappy_Bird_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                dbContext.Database.EnsureCreated();
                
                if (dbContext.Database.CanConnect())
                {
                    Console.WriteLine("✅ Kết nối SQLite thành công!");
                }
                else
                {
                    Console.WriteLine("❌ Kết nối SQLite thất bại!");
                }
            }
            
            app.UseHttpsRedirection();
            app.UseAuthorization();       
            app.MapControllers();

            app.Run();
        }
    }
}
