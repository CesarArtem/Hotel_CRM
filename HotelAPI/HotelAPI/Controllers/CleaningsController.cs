using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelAPI.Models;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CleaningsController : ControllerBase
    {
        private readonly HotelContext _context;

        public CleaningsController(HotelContext context)
        {
            _context = context;
        }

        // GET: api/Cleanings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cleaning>>> GetCleaning()
        {
            return await _context.Cleaning.ToListAsync();
        }

        // GET: api/Cleanings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cleaning>> GetCleaning(int id)
        {
            var cleaning = await _context.Cleaning.FindAsync(id);

            if (cleaning == null)
            {
                return NotFound();
            }

            return cleaning;
        }

        // PUT: api/Cleanings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCleaning(int id, Cleaning cleaning)
        {
            if (id != cleaning.IdCleaning)
            {
                return BadRequest();
            }

            _context.Entry(cleaning).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CleaningExists(id))
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

        // POST: api/Cleanings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cleaning>> PostCleaning(Cleaning cleaning)
        {
            _context.Cleaning.Add(cleaning);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCleaning", new { id = cleaning.IdCleaning }, cleaning);
        }

        // DELETE: api/Cleanings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cleaning>> DeleteCleaning(int id)
        {
            var cleaning = await _context.Cleaning.FindAsync(id);
            if (cleaning == null)
            {
                return NotFound();
            }

            _context.Cleaning.Remove(cleaning);
            await _context.SaveChangesAsync();

            return cleaning;
        }

        private bool CleaningExists(int id)
        {
            return _context.Cleaning.Any(e => e.IdCleaning == id);
        }
    }
}
