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
    public class MenuDishesController : ControllerBase
    {
        private readonly HotelContext _context;

        public MenuDishesController(HotelContext context)
        {
            _context = context;
        }

        // GET: api/MenuDishes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuDish>>> GetMenuDish()
        {
            return await _context.MenuDish.ToListAsync();
        }

        // GET: api/MenuDishes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuDish>> GetMenuDish(int id)
        {
            var menuDish = await _context.MenuDish.FindAsync(id);

            if (menuDish == null)
            {
                return NotFound();
            }

            return menuDish;
        }

        // PUT: api/MenuDishes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuDish(int id, MenuDish menuDish)
        {
            if (id != menuDish.IdMenuDish)
            {
                return BadRequest();
            }

            _context.Entry(menuDish).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuDishExists(id))
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

        // POST: api/MenuDishes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MenuDish>> PostMenuDish(MenuDish menuDish)
        {
            _context.MenuDish.Add(menuDish);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenuDish", new { id = menuDish.IdMenuDish }, menuDish);
        }

        // DELETE: api/MenuDishes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MenuDish>> DeleteMenuDish(int id)
        {
            var menuDish = await _context.MenuDish.FindAsync(id);
            if (menuDish == null)
            {
                return NotFound();
            }

            _context.MenuDish.Remove(menuDish);
            await _context.SaveChangesAsync();

            return menuDish;
        }

        private bool MenuDishExists(int id)
        {
            return _context.MenuDish.Any(e => e.IdMenuDish == id);
        }
    }
}
