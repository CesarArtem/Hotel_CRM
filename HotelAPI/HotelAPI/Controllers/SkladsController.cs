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
    public class SkladsController : ControllerBase
    {
        private readonly HotelContext _context;

        public SkladsController(HotelContext context)
        {
            _context = context;
        }

        // GET: api/Sklads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sklad>>> GetSklad()
        {
            return await _context.Sklad.ToListAsync();
        }

        // GET: api/Sklads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sklad>> GetSklad(int id)
        {
            var sklad = await _context.Sklad.FindAsync(id);

            if (sklad == null)
            {
                return NotFound();
            }

            return sklad;
        }

        // PUT: api/Sklads/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSklad(int id, Sklad sklad)
        {
            if (id != sklad.IdSklad)
            {
                return BadRequest();
            }

            _context.Entry(sklad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkladExists(id))
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

        // POST: api/Sklads
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Sklad>> PostSklad(Sklad sklad)
        {
            _context.Sklad.Add(sklad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSklad", new { id = sklad.IdSklad }, sklad);
        }

        // DELETE: api/Sklads/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sklad>> DeleteSklad(int id)
        {
            var sklad = await _context.Sklad.FindAsync(id);
            if (sklad == null)
            {
                return NotFound();
            }

            _context.Sklad.Remove(sklad);
            await _context.SaveChangesAsync();

            return sklad;
        }

        private bool SkladExists(int id)
        {
            return _context.Sklad.Any(e => e.IdSklad == id);
        }
    }
}
