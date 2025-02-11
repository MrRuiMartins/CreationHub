using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CreationHub.Models;

namespace CreationHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NicePartUsageController : ControllerBase
    {
        private readonly NicePartUsageContext _context;

        public NicePartUsageController(NicePartUsageContext context)
        {
            _context = context;
        }

        // GET: api/NicePartUsage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NicePartUsageDTO>>> GetNicePartUsages()
        {
            return await _context.NicePartUsages
                .Select(entity => NicePartUsageToDTO(entity))
                .ToListAsync();
        }

        // GET: api/NicePartUsage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NicePartUsageDTO>> GetNicePartUsage(long id)
        {
            var nicePartUsage = await _context.NicePartUsages.FindAsync(id);

            if (nicePartUsage == null)
            {
                return NotFound();
            }

            return NicePartUsageToDTO(nicePartUsage);
        }

        // PUT: api/NicePartUsage/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNicePartUsage(long id, NicePartUsageDTO nicePartUsageDto)
        {
            if (id != nicePartUsageDto.Id)
            {
                return BadRequest();
            }

            var nicePartUsage = await _context.NicePartUsages.FindAsync(id);
            if (nicePartUsage == null)
            {
                return NotFound();
            }

            nicePartUsage.Title = nicePartUsageDto.Title;
            nicePartUsage.Description = nicePartUsage.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NicePartUsageExists(id))
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

        // POST: api/NicePartUsage
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NicePartUsageDTO>> PostNicePartUsage(NicePartUsageDTO nicePartUsageDto)
        {
            var nicePartUsage = new NicePartUsage
            {
                Title = nicePartUsageDto.Title,
                Description = nicePartUsageDto.Description
            };

            _context.NicePartUsages.Add(nicePartUsage);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNicePartUsage), new { id = nicePartUsage.Id }, nicePartUsage);
        }

        // DELETE: api/NicePartUsage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNicePartUsage(long id)
        {
            var nicePartUsage = await _context.NicePartUsages.FindAsync(id);
            if (nicePartUsage == null)
            {
                return NotFound();
            }

            _context.NicePartUsages.Remove(nicePartUsage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static NicePartUsageDTO NicePartUsageToDTO(NicePartUsage nicePartUsage) => 
            new NicePartUsageDTO
            {
                Id = nicePartUsage.Id,
                Title = nicePartUsage.Title,
                Description = nicePartUsage.Description,
            };

        private bool NicePartUsageExists(long id)
        {
            return _context.NicePartUsages.Any(e => e.Id == id);
        }
    }
}
