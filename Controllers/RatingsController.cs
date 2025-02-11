using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CreationHub.Models;
using CreationHub.Models.NicePartUsage;

namespace CreationHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly CreationHubContext _context;

        public RatingsController(CreationHubContext context)
        {
            _context = context;
        }

        // GET: api/Rating
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingDto>>> GetRatings()
        {
            return await _context.Ratings
                .Select(entity => RatingToDto(entity))
                .ToListAsync();
        }

        // GET: api/Rating/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RatingDto>> GetRating(long id)
        {
            var Rating = await _context.Ratings.FindAsync(id);

            if (Rating == null)
            {
                return NotFound();
            }

            return RatingToDto(Rating);
        }

        // PUT: api/Rating/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating(long id, RatingDto ratingDto)
        {
            if (id != ratingDto.Id)
            {
                return BadRequest();
            }

            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            rating.Creativity = ratingDto.Creativity;
            rating.Uniqueness = ratingDto.Uniqueness;
            rating.NicePartUsageId = ratingDto.NicePartUsageId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
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

        // POST: api/Rating
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RatingDto>> PostRating(RatingDto ratingDto)
        {
            var nicePartUsage = await _context.NicePartUsages.FindAsync(ratingDto.NicePartUsageId);
            if (nicePartUsage == null)
            {
                return NotFound();
            }
            
            var rating = new Rating
            {
                Creativity = ratingDto.Creativity,
                Uniqueness = ratingDto.Uniqueness,
                NicePartUsageId = ratingDto.NicePartUsageId,
            };

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRating), new { id = rating.Id }, rating);
        }

        // DELETE: api/Rating/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(long id)
        {
            var Rating = await _context.Ratings.FindAsync(id);
            if (Rating == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(Rating);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static RatingDto RatingToDto(Rating rating) => 
            new RatingDto
            {
                Id = rating.Id,
                Creativity = rating.Creativity,
                Uniqueness = rating.Uniqueness,
                NicePartUsageId = rating.NicePartUsageId
            };

        private bool RatingExists(long id)
        {
            return _context.Ratings.Any(e => e.Id == id);
        }
        
        private bool NicePartUsageExists(long id)
        {
            return _context.NicePartUsages.Any(e => e.Id == id);
        }
    }
}
