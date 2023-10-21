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
    public class DoljnostsController : ControllerBase
    {
        private readonly HotelContext _context;

        public DoljnostsController(HotelContext context)
        {
            _context = context;
        }

        // GET: api/Doljnosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doljnost>>> GetDoljnost()
        {
            return await _context.Doljnost.ToListAsync();
        }

        // GET: api/Doljnosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doljnost>> GetDoljnost(int id)
        {
            var doljnost = await _context.Doljnost.FindAsync(id);

            if (doljnost == null)
            {
                return NotFound();
            }

            return doljnost;
        }

        // PUT: api/Doljnosts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoljnost(int id, Doljnost doljnost)
        {
            if (id != doljnost.IdDoljnost)
            {
                return BadRequest();
            }

            _context.Entry(doljnost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoljnostExists(id))
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

        // POST: api/Doljnosts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Doljnost>> PostDoljnost(Doljnost doljnost)
        {
            _context.Doljnost.Add(doljnost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoljnost", new { id = doljnost.IdDoljnost }, doljnost);
        }

        // DELETE: api/Doljnosts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Doljnost>> DeleteDoljnost(int id)
        {
            var doljnost = await _context.Doljnost.FindAsync(id);
            if (doljnost == null)
            {
                return NotFound();
            }

            _context.Doljnost.Remove(doljnost);
            await _context.SaveChangesAsync();

            return doljnost;
        }

        private bool DoljnostExists(int id)
        {
            return _context.Doljnost.Any(e => e.IdDoljnost == id);
        }
    }
}
