using Flappy_Bird_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Flappy_Bird_API.Database
{
    public class ScoreDbContext : DbContext
    {
        public ScoreDbContext(DbContextOptions<ScoreDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Score>().ToTable("SCORE");
        }
        public DbSet<Models.Score> Score { get; set; }
    }
}
