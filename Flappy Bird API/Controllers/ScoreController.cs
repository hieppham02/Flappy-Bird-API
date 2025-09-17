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
        private readonly DataContext context;
        public ScoreController(DataContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostScore([FromBody] Score score)
        {
            context.Scores.Add(score);
            await context.SaveChangesAsync();
            return Ok(score);
        }
       
        [HttpGet("top/{count}")]
        public async Task<IActionResult> GetTopScores(int count)
        {
            var topScores = await context.Scores
                .OrderByDescending(s => s.Points)
                .Take(count)
                .ToListAsync();

            return Ok(topScores);
        }
    }
}
