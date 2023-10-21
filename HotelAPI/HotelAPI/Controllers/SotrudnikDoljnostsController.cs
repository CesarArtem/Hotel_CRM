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
    public class SotrudnikDoljnostsController : ControllerBase
    {
        private readonly HotelContext _context;

        public SotrudnikDoljnostsController(HotelContext context)
        {
            _context = context;
        }

        // GET: api/SotrudnikDoljnosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SotrudnikDoljnost>>> GetSotrudnikDoljnost()
        {
            return await _context.SotrudnikDoljnost.ToListAsync();
        }

        // GET: api/SotrudnikDoljnosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SotrudnikDoljnost>> GetSotrudnikDoljnost(int id)
        {
            var sotrudnikDoljnost = await _context.SotrudnikDoljnost.FindAsync(id);

            if (sotrudnikDoljnost == null)
            {
                return NotFound();
            }

            return sotrudnikDoljnost;
        }

        // PUT: api/SotrudnikDoljnosts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSotrudnikDoljnost(int id, SotrudnikDoljnost sotrudnikDoljnost)
        {
            if (id != sotrudnikDoljnost.IdSotrudnikDoljnost)
            {
                return BadRequest();
            }

            _context.Entry(sotrudnikDoljnost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SotrudnikDoljnostExists(id))
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

        // POST: api/SotrudnikDoljnosts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SotrudnikDoljnost>> PostSotrudnikDoljnost(SotrudnikDoljnost sotrudnikDoljnost)
        {
            _context.SotrudnikDoljnost.Add(sotrudnikDoljnost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSotrudnikDoljnost", new { id = sotrudnikDoljnost.IdSotrudnikDoljnost }, sotrudnikDoljnost);
        }

        // DELETE: api/SotrudnikDoljnosts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SotrudnikDoljnost>> DeleteSotrudnikDoljnost(int id)
        {
            var sotrudnikDoljnost = await _context.SotrudnikDoljnost.FindAsync(id);
            if (sotrudnikDoljnost == null)
            {
                return NotFound();
            }

            _context.SotrudnikDoljnost.Remove(sotrudnikDoljnost);
            await _context.SaveChangesAsync();

            return sotrudnikDoljnost;
        }

        private bool SotrudnikDoljnostExists(int id)
        {
            return _context.SotrudnikDoljnost.Any(e => e.IdSotrudnikDoljnost == id);
        }
    }
}
