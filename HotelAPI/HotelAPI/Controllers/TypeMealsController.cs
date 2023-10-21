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
    public class TypeMealsController : ControllerBase
    {
        private readonly HotelContext _context;

        public TypeMealsController(HotelContext context)
        {
            _context = context;
        }

        // GET: api/TypeMeals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeMeal>>> GetTypeMeal()
        {
            return await _context.TypeMeal.ToListAsync();
        }

        // GET: api/TypeMeals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeMeal>> GetTypeMeal(int id)
        {
            var typeMeal = await _context.TypeMeal.FindAsync(id);

            if (typeMeal == null)
            {
                return NotFound();
            }

            return typeMeal;
        }

        // PUT: api/TypeMeals/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeMeal(int id, TypeMeal typeMeal)
        {
            if (id != typeMeal.IdType)
            {
                return BadRequest();
            }

            _context.Entry(typeMeal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeMealExists(id))
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

        // POST: api/TypeMeals
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TypeMeal>> PostTypeMeal(TypeMeal typeMeal)
        {
            _context.TypeMeal.Add(typeMeal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeMeal", new { id = typeMeal.IdType }, typeMeal);
        }

        // DELETE: api/TypeMeals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TypeMeal>> DeleteTypeMeal(int id)
        {
            var typeMeal = await _context.TypeMeal.FindAsync(id);
            if (typeMeal == null)
            {
                return NotFound();
            }

            _context.TypeMeal.Remove(typeMeal);
            await _context.SaveChangesAsync();

            return typeMeal;
        }

        private bool TypeMealExists(int id)
        {
            return _context.TypeMeal.Any(e => e.IdType == id);
        }
    }
}
