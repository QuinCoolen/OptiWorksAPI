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
        public async Task<ActionResult<IEnumerable<World>>> GetWorlds()
        {
            return await _context.Worlds.ToListAsync();
        }

        // GET: api/Worlds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<World>> GetWorld(int id)
        {
            var world = await _context.Worlds.FindAsync(id);

            if (world == null)
            {
                return NotFound();
            }

            return world;
        }

        // PUT: api/Worlds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorld(int id, World world)
        {
            if (id != world.Id)
            {
                return BadRequest();
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
        public async Task<ActionResult<World>> PostWorld(World world)
        {
            _context.Worlds.Add(world);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorld", new { id = world.Id }, world);
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
