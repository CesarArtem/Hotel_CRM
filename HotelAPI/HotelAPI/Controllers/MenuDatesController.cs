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
    public class MenuDatesController : ControllerBase
    {
        private readonly HotelContext _context;

        public MenuDatesController(HotelContext context)
        {
            _context = context;
        }

        // GET: api/MenuDates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuDate>>> GetMenuDate()
        {
            return await _context.MenuDate.ToListAsync();
        }

        // GET: api/MenuDates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuDate>> GetMenuDate(int id)
        {
            var menuDate = await _context.MenuDate.FindAsync(id);

            if (menuDate == null)
            {
                return NotFound();
            }

            return menuDate;
        }

        // PUT: api/MenuDates/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuDate(int id, MenuDate menuDate)
        {
            if (id != menuDate.IdMenuDate)
            {
                return BadRequest();
            }

            _context.Entry(menuDate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuDateExists(id))
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

        // POST: api/MenuDates
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MenuDate>> PostMenuDate(MenuDate menuDate)
        {
            _context.MenuDate.Add(menuDate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenuDate", new { id = menuDate.IdMenuDate }, menuDate);
        }

        // DELETE: api/MenuDates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MenuDate>> DeleteMenuDate(int id)
        {
            var menuDate = await _context.MenuDate.FindAsync(id);
            if (menuDate == null)
            {
                return NotFound();
            }

            _context.MenuDate.Remove(menuDate);
            await _context.SaveChangesAsync();

            return menuDate;
        }

        private bool MenuDateExists(int id)
        {
            return _context.MenuDate.Any(e => e.IdMenuDate == id);
        }
    }
}
