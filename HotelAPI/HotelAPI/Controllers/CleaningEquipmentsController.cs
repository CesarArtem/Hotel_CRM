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
    public class CleaningEquipmentsController : ControllerBase
    {
        private readonly HotelContext _context;

        public CleaningEquipmentsController(HotelContext context)
        {
            _context = context;
        }

        // GET: api/CleaningEquipments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CleaningEquipment>>> GetCleaningEquipment()
        {
            return await _context.CleaningEquipment.ToListAsync();
        }

        // GET: api/CleaningEquipments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CleaningEquipment>> GetCleaningEquipment(int id)
        {
            var cleaningEquipment = await _context.CleaningEquipment.FindAsync(id);

            if (cleaningEquipment == null)
            {
                return NotFound();
            }

            return cleaningEquipment;
        }

        // PUT: api/CleaningEquipments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCleaningEquipment(int id, CleaningEquipment cleaningEquipment)
        {
            if (id != cleaningEquipment.IdCleaningEquipment)
            {
                return BadRequest();
            }

            _context.Entry(cleaningEquipment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CleaningEquipmentExists(id))
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

        // POST: api/CleaningEquipments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CleaningEquipment>> PostCleaningEquipment(CleaningEquipment cleaningEquipment)
        {
            _context.CleaningEquipment.Add(cleaningEquipment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCleaningEquipment", new { id = cleaningEquipment.IdCleaningEquipment }, cleaningEquipment);
        }

        // DELETE: api/CleaningEquipments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CleaningEquipment>> DeleteCleaningEquipment(int id)
        {
            var cleaningEquipment = await _context.CleaningEquipment.FindAsync(id);
            if (cleaningEquipment == null)
            {
                return NotFound();
            }

            _context.CleaningEquipment.Remove(cleaningEquipment);
            await _context.SaveChangesAsync();

            return cleaningEquipment;
        }

        private bool CleaningEquipmentExists(int id)
        {
            return _context.CleaningEquipment.Any(e => e.IdCleaningEquipment == id);
        }
    }
}
