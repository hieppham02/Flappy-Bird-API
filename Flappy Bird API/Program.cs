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

            builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            
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

                if (dbContext.Database.CanConnect())
                {
                    Console.WriteLine("✅ Kết nối thành công!");
                }
                else
                {
                    Console.WriteLine("❌ Kết nối thất bại!");
                }
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();       
            app.MapControllers();

            app.Run();
        }
    }
}
