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
        [HttpPost]
        public async Task<IActionResult> PostScore([FromBody] Score score)
        {
            // Validate input
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid request data.",
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            if (string.IsNullOrWhiteSpace(score.playername))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Player name cannot be empty."
                });
            }

            if (score.points < 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Points must be a non-negative number."
                });
            }

            // Insert or upsert
            var existingScore = await context.Scores
                .FirstOrDefaultAsync(s => s.playername == score.playername);

            if (existingScore != null)
            {
                existingScore.points = score.points;
                existingScore.createdat = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                context.Scores.Update(existingScore);
            }
            else
            {
                score.createdat = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                context.Scores.Add(score);
            }

            await context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = existingScore != null ? "Score updated successfully." : "Score inserted successfully.",
                data = existingScore ?? score
            });
        }


        [HttpGet("top/{count}")]
        public async Task<IActionResult> GetTopScores(int count)
        {
            var topScores = await context.Scores
                .OrderByDescending(s => s.points)
                .Take(count)
                .ToListAsync();

            return Ok(topScores);
        }
    }
}
