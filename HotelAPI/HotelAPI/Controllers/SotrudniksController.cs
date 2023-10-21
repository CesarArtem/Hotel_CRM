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
    public class SotrudniksController : ControllerBase
    {
        private readonly HotelContext _context;

        public SotrudniksController(HotelContext context)
        {
            _context = context;
        }

        // GET: api/Sotrudniks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sotrudnik>>> GetSotrudnik()
        {
            return await _context.Sotrudnik.ToListAsync();
        }

        // GET: api/Sotrudniks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sotrudnik>> GetSotrudnik(int id)
        {
            var sotrudnik = await _context.Sotrudnik.FindAsync(id);

            if (sotrudnik == null)
            {
                return NotFound();
            }

            return sotrudnik;
        }

        // PUT: api/Sotrudniks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSotrudnik(int id, Sotrudnik sotrudnik)
        {
            if (id != sotrudnik.IdSotrudnik)
            {
                return BadRequest();
            }

            _context.Entry(sotrudnik).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SotrudnikExists(id))
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

        // POST: api/Sotrudniks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Sotrudnik>> PostSotrudnik(Sotrudnik sotrudnik)
        {
            _context.Sotrudnik.Add(sotrudnik);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSotrudnik", new { id = sotrudnik.IdSotrudnik }, sotrudnik);
        }

        // DELETE: api/Sotrudniks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sotrudnik>> DeleteSotrudnik(int id)
        {
            var sotrudnik = await _context.Sotrudnik.FindAsync(id);
            if (sotrudnik == null)
            {
                return NotFound();
            }

            _context.Sotrudnik.Remove(sotrudnik);
            await _context.SaveChangesAsync();

            return sotrudnik;
        }

        private bool SotrudnikExists(int id)
        {
            return _context.Sotrudnik.Any(e => e.IdSotrudnik == id);
        }
    }
}
