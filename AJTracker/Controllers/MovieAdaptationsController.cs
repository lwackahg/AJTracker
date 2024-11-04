using AJTracker.Data;
using AJTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AJTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieAdaptationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MovieAdaptationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MovieAdaptations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieAdaptation>>> GetMovieAdaptations()
        {
            return await _context.MovieAdaptations.ToListAsync();
        }

        // GET: api/MovieAdaptations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieAdaptation>> GetMovieAdaptation(int id)
        {
            var movieAdaptation = await _context.MovieAdaptations.FindAsync(id);
            if (movieAdaptation == null)
            {
                return NotFound();
            }
            return movieAdaptation;
        }

        // POST: api/MovieAdaptations
        [HttpPost]
        public async Task<ActionResult<MovieAdaptation>> PostMovieAdaptation(MovieAdaptation movieAdaptation)
        {
            _context.MovieAdaptations.Add(movieAdaptation);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetMovieAdaptation", new { id = movieAdaptation.Id }, movieAdaptation);
        }

        // PUT: api/MovieAdaptations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieAdaptation(int id, MovieAdaptation movieAdaptation)
        {
            if (id != movieAdaptation.Id)
            {
                return BadRequest();
            }

            _context.Entry(movieAdaptation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieAdaptationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/MovieAdaptations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieAdaptation(int id)
        {
            var movieAdaptation = await _context.MovieAdaptations.FindAsync(id);
            if (movieAdaptation == null)
            {
                return NotFound();
            }

            _context.MovieAdaptations.Remove(movieAdaptation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieAdaptationExists(int id)
        {
            return _context.MovieAdaptations.Any(e => e.Id == id);
        }
    }
}