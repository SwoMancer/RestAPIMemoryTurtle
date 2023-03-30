using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPTurtelMemory.Models;
using ASPTurtelMemory.Models.Light;

namespace ASPTurtelMemory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurtlesController : ControllerBase
    {
        private readonly TurtleoverwatchContext _context;

        public TurtlesController(TurtleoverwatchContext context)
        {
            _context = context;
        }
        /*
        // GET: api/Turtles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turtle>>> GetTurtles()
        {
          if (_context.Turtles == null)
          {
              return NotFound();
          }
            return await _context.Turtles.ToListAsync();
        }
        */
        // GET: api/Turtles/5
        [HttpGet("world/{world_id}")]
        public async Task<ActionResult<IEnumerable<Turtle>>> GetTurtles(int world_id)
        {
            if (_context.Turtles == null)
            {
                return NotFound();
            }
            return await _context.Turtles.Where(i => i.WorldId == world_id).ToListAsync();
        }

        // GET: api/Turtles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Turtle>> GetTurtle(int id)
        {
          if (_context.Turtles == null)
          {
              return NotFound();
          }
            var turtle = await _context.Turtles.FindAsync(id);

            if (turtle == null)
            {
                return NotFound();
            }

            return turtle;
        }

        // PUT: api/Turtles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTurtle(int id, LightTurtle lightTurtle)
        {
            /*
            if (id != turtle.Id)
            {
                return BadRequest();
            }
            */
            if (_context.Turtles == null)
                return NotFound();

            var tempTurtle = await _context.Turtles.FindAsync(id);

            if (tempTurtle == null)
                return NotFound();
            
            Turtle turtle = lightTurtle.EditTurtle(tempTurtle);

            _context.Entry(turtle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TurtleExists(id))
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

        // POST: api/Turtles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Turtle>> PostTurtle(Turtle turtle)
        {
          if (_context.Turtles == null)
          {
              return Problem("Entity set 'TurtleoverwatchContext.Turtles'  is null.");
          }
            _context.Turtles.Add(turtle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTurtle", new { id = turtle.Id }, turtle);
        }

        // DELETE: api/Turtles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurtle(int id)
        {
            if (_context.Turtles == null)
            {
                return NotFound();
            }
            var turtle = await _context.Turtles.FindAsync(id);
            if (turtle == null)
            {
                return NotFound();
            }

            _context.Turtles.Remove(turtle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TurtleExists(int id)
        {
            return (_context.Turtles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
