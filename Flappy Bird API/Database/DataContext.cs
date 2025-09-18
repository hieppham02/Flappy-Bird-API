using Flappy_Bird_API.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Flappy_Bird_API.Database
{
    public class DataContext : DbContext
    {

        protected readonly IConfiguration Configuration;

        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<Models.Score> Score { get; set; }
    }
}
