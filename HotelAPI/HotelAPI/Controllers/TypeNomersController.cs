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
    public class TypeNomersController : ControllerBase
    {
        private readonly HotelContext _context;

        public TypeNomersController(HotelContext context)
        {
            _context = context;
        }

        // GET: api/TypeNomers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeNomer>>> GetTypeNomer()
        {
            return await _context.TypeNomer.ToListAsync();
        }

        // GET: api/TypeNomers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeNomer>> GetTypeNomer(int id)
        {
            var typeNomer = await _context.TypeNomer.FindAsync(id);

            if (typeNomer == null)
            {
                return NotFound();
            }

            return typeNomer;
        }

        // PUT: api/TypeNomers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeNomer(int id, TypeNomer typeNomer)
        {
            if (id != typeNomer.IdType)
            {
                return BadRequest();
            }

            _context.Entry(typeNomer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeNomerExists(id))
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

        // POST: api/TypeNomers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TypeNomer>> PostTypeNomer(TypeNomer typeNomer)
        {
            _context.TypeNomer.Add(typeNomer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeNomer", new { id = typeNomer.IdType }, typeNomer);
        }

        // DELETE: api/TypeNomers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TypeNomer>> DeleteTypeNomer(int id)
        {
            var typeNomer = await _context.TypeNomer.FindAsync(id);
            if (typeNomer == null)
            {
                return NotFound();
            }

            _context.TypeNomer.Remove(typeNomer);
            await _context.SaveChangesAsync();

            return typeNomer;
        }

        private bool TypeNomerExists(int id)
        {
            return _context.TypeNomer.Any(e => e.IdType == id);
        }
    }
}
