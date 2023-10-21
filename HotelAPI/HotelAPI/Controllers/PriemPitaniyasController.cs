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
    public class PriemPitaniyasController : ControllerBase
    {
        private readonly HotelContext _context;

        public PriemPitaniyasController(HotelContext context)
        {
            _context = context;
        }

        // GET: api/PriemPitaniyas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriemPitaniya>>> GetPriemPitaniya()
        {
            return await _context.PriemPitaniya.ToListAsync();
        }

        // GET: api/PriemPitaniyas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PriemPitaniya>> GetPriemPitaniya(int id)
        {
            var priemPitaniya = await _context.PriemPitaniya.FindAsync(id);

            if (priemPitaniya == null)
            {
                return NotFound();
            }

            return priemPitaniya;
        }

        // PUT: api/PriemPitaniyas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPriemPitaniya(int id, PriemPitaniya priemPitaniya)
        {
            if (id != priemPitaniya.IdPriem)
            {
                return BadRequest();
            }

            _context.Entry(priemPitaniya).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriemPitaniyaExists(id))
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

        // POST: api/PriemPitaniyas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PriemPitaniya>> PostPriemPitaniya(PriemPitaniya priemPitaniya)
        {
            _context.PriemPitaniya.Add(priemPitaniya);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPriemPitaniya", new { id = priemPitaniya.IdPriem }, priemPitaniya);
        }

        // DELETE: api/PriemPitaniyas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PriemPitaniya>> DeletePriemPitaniya(int id)
        {
            var priemPitaniya = await _context.PriemPitaniya.FindAsync(id);
            if (priemPitaniya == null)
            {
                return NotFound();
            }

            _context.PriemPitaniya.Remove(priemPitaniya);
            await _context.SaveChangesAsync();

            return priemPitaniya;
        }

        private bool PriemPitaniyaExists(int id)
        {
            return _context.PriemPitaniya.Any(e => e.IdPriem == id);
        }
    }
}
