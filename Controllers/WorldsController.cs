using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptiWorksAPI.Models;

namespace OptiWorksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorldsController : ControllerBase
    {
        private readonly OwContext _context;

        public WorldsController(OwContext context)
        {
            _context = context;
        }

        // GET: api/Worlds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorldDto>>> GetWorlds()
        {
            var worlds = await _context.Worlds
                .Include(w => w.Attractions)
                .ToListAsync();

            var worldDtos = worlds.Select(w => new WorldDto
            {
                Id = w.Id,
                Name = w.Name,
                Attractions = w.Attractions.Select(a => new AttractionDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    MaxRiders = a.MaxRiders,
                    IsOpen = a.IsOpen,
                    WorldId = w.Id
                }).ToList()
            }).ToList();

            return Ok(worldDtos);
        }

        // GET: api/Worlds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorldDto>> GetWorld(int id)
        {
            var world = await _context.Worlds
                .Include(w => w.Attractions)
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();
            
            var worldDto = new WorldDto
            {
                Id = world.Id,
                Name = world.Name,
                Attractions = world.Attractions.Select(a => new AttractionDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    MaxRiders = a.MaxRiders,
                    IsOpen = a.IsOpen,
                    WorldId = world.Id
                }).ToList()
            };

            if (worldDto == null)
            {
                return NotFound();
            }

            return worldDto;
        }

        // PUT: api/Worlds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorld(int id, WorldDto worldDto)
        {
            if (id != worldDto.Id)
            {
                return BadRequest();
            }

            var world = await _context.Worlds.Include(w => w.Attractions).FirstOrDefaultAsync(w => w.Id == id);
            if (world == null)
            {
                return NotFound();
            }

            world.Name = worldDto.Name;

            // Updating attractions
            foreach (var attractionDto in worldDto.Attractions)
            {
                var attraction = world.Attractions.FirstOrDefault(a => a.Id == attractionDto.Id);
                if (attraction != null)
                {
                    attraction.Name = attractionDto.Name;
                    attraction.MaxRiders = attractionDto.MaxRiders;
                    attraction.IsOpen = attractionDto.IsOpen;
                }
            }

            _context.Entry(world).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorldExists(id))
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

        // POST: api/Worlds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorldDto>> PostWorld(WorldDto worldDto)
        {
            var world = new World(worldDto.Name);

            foreach (var attractionDto in worldDto.Attractions)
            {
                var attraction = new Attraction(attractionDto.Name, attractionDto.MaxRiders)
                {
                    IsOpen = attractionDto.IsOpen,
                    World = world
                };
                world.Attractions.Add(attraction);
            }

            _context.Worlds.Add(world);
            await _context.SaveChangesAsync();

            worldDto.Id = world.Id;

            return CreatedAtAction(nameof(GetWorld), new { id = worldDto.Id }, worldDto);
        }

        // DELETE: api/Worlds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorld(int id)
        {
            var world = await _context.Worlds.FindAsync(id);
            if (world == null)
            {
                return NotFound();
            }

            _context.Worlds.Remove(world);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorldExists(int id)
        {
            return _context.Worlds.Any(e => e.Id == id);
        }
    }
}
