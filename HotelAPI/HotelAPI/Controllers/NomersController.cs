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
    public class NomersController : ControllerBase
    {
        private readonly HotelContext _context;

        public NomersController(HotelContext context)
        {
            _context = context;
        }

        // GET: api/Nomers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nomer>>> GetNomer()
        {
            return await _context.Nomer.ToListAsync();
        }

        // GET: api/Nomers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nomer>> GetNomer(int id)
        {
            var nomer = await _context.Nomer.FindAsync(id);

            if (nomer == null)
            {
                return NotFound();
            }

            return nomer;
        }

        // PUT: api/Nomers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNomer(int id, Nomer nomer)
        {
            if (id != nomer.IdNomer)
            {
                return BadRequest();
            }

            _context.Entry(nomer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NomerExists(id))
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

        // POST: api/Nomers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Nomer>> PostNomer(Nomer nomer)
        {
            _context.Nomer.Add(nomer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNomer", new { id = nomer.IdNomer }, nomer);
        }

        // DELETE: api/Nomers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Nomer>> DeleteNomer(int id)
        {
            var nomer = await _context.Nomer.FindAsync(id);
            if (nomer == null)
            {
                return NotFound();
            }

            _context.Nomer.Remove(nomer);
            await _context.SaveChangesAsync();

            return nomer;
        }

        private bool NomerExists(int id)
        {
            return _context.Nomer.Any(e => e.IdNomer == id);
        }
    }
}
