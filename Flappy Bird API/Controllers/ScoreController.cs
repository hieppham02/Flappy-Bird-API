using Flappy_Bird_API.Database;
using Flappy_Bird_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Flappy_Bird_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreController : Controller
    {
        private readonly ScoreDbContext context;
        public ScoreController(ScoreDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostScore([FromBody] Score score)
        {
            context.Score.Add(score);
            await context.SaveChangesAsync();
            return Ok(score);
        }
       
        [HttpGet("top/{count}")]
        public async Task<IActionResult> GetTopScores(int count)
        {
            var topScores = await context.Score
                .OrderByDescending(s => s.Points)
                .Take(count)
                .ToListAsync();

            return Ok(topScores);
        }
    }
}
