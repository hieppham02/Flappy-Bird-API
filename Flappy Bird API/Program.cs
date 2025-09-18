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
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            string connectionString;
            if (!string.IsNullOrEmpty(databaseUrl))
            {
                // Convert URL -> Npgsql connection string
                var uri = new Uri(databaseUrl);
                var userInfo = uri.UserInfo.Split(':');
                connectionString =
                    $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";
            }
            else
            {
                connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            }

            builder.Services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(connectionString));
            // 

            builder.Services.AddControllers();

            builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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
