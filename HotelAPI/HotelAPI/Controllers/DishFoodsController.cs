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
    public class DishFoodsController : ControllerBase
    {
        private readonly HotelContext _context;

        public DishFoodsController(HotelContext context)
        {
            _context = context;
        }

        // GET: api/DishFoods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishFood>>> GetDishFood()
        {
            return await _context.DishFood.ToListAsync();
        }

        // GET: api/DishFoods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DishFood>> GetDishFood(int id)
        {
            var dishFood = await _context.DishFood.FindAsync(id);

            if (dishFood == null)
            {
                return NotFound();
            }

            return dishFood;
        }

        // PUT: api/DishFoods/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDishFood(int id, DishFood dishFood)
        {
            if (id != dishFood.IdDishFood)
            {
                return BadRequest();
            }

            _context.Entry(dishFood).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishFoodExists(id))
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

        // POST: api/DishFoods
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DishFood>> PostDishFood(DishFood dishFood)
        {
            _context.DishFood.Add(dishFood);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDishFood", new { id = dishFood.IdDishFood }, dishFood);
        }

        // DELETE: api/DishFoods/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DishFood>> DeleteDishFood(int id)
        {
            var dishFood = await _context.DishFood.FindAsync(id);
            if (dishFood == null)
            {
                return NotFound();
            }

            _context.DishFood.Remove(dishFood);
            await _context.SaveChangesAsync();

            return dishFood;
        }

        private bool DishFoodExists(int id)
        {
            return _context.DishFood.Any(e => e.IdDishFood == id);
        }
    }
}
