using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptiWorksAPI.Models;

namespace OptiWorksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttractionsController : ControllerBase
    {
        private readonly OwContext _context;

        public AttractionsController(OwContext context)
        {
            _context = context;
        }

        // GET: api/Attractions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttractionDto>>> GetAttractions()
        {
            var attractions = await _context.Attractions
                .Select(a => new AttractionDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    MaxRiders = a.MaxRiders,
                    IsOpen = a.IsOpen,
                    WorldId = a.WorldId
                })
                .ToListAsync();

            return Ok(attractions);
        }

        // GET: api/Attractions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttractionDto>> GetAttraction(int id)
        {
            var attraction = await _context.Attractions
                .Select(a => new AttractionDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    MaxRiders = a.MaxRiders,
                    IsOpen = a.IsOpen,
                    WorldId = a.WorldId
                })
                .FirstOrDefaultAsync(a => a.Id == id);

            if (attraction == null)
            {
                return NotFound();
            }

            return attraction;
        }

        // PUT: api/Attractions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttraction(int id, AttractionDto attractionDto)
        {
            if (id != attractionDto.Id)
            {
                return BadRequest();
            }

            var attraction = await _context.Attractions.FindAsync(id);
            if (attraction == null)
            {
                return NotFound();
            }

            attraction.Name = attractionDto.Name;
            attraction.MaxRiders = attractionDto.MaxRiders;
            attraction.IsOpen = attractionDto.IsOpen;

            var world = await _context.Worlds.FindAsync(attractionDto.WorldId);
            if (world == null)
            {
                return BadRequest("Invalid WorldId");
            }

            attraction.World = world;

            _context.Entry(attraction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttractionExists(id))
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

        // POST: api/Attractions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AttractionDto>> PostAttraction(AttractionDto attractionDto)
        {
            var world = await _context.Worlds.FindAsync(attractionDto.WorldId);
            if (world == null)
            {
                return BadRequest("Invalid WorldId");
            }

            var attraction = new Attraction(attractionDto.Name, attractionDto.MaxRiders)
            {
                IsOpen = attractionDto.IsOpen,
                World = world
            };

            _context.Attractions.Add(attraction);
            await _context.SaveChangesAsync();

            attractionDto.Id = attraction.Id;

            return CreatedAtAction(nameof(GetAttraction), new { id = attractionDto.Id }, attractionDto);
        }

        // DELETE: api/Attractions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttraction(int id)
        {
            var attraction = await _context.Attractions.FindAsync(id);
            if (attraction == null)
            {
                return NotFound();
            }

            _context.Attractions.Remove(attraction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttractionExists(int id)
        {
            return _context.Attractions.Any(e => e.Id == id);
        }
    }
}
