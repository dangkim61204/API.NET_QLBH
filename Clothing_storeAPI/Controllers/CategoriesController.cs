using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clothing_storeAPI.Models;
using Asp.Versioning;
using Clothing_storeAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Clothing_storeAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly Context _context;

        public CategoriesController(Context context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Getcategories()
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        
        public async Task<IActionResult> PutCategory(int id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.categoryId)
            {
                return BadRequest();
            }
            var category = await _context.Categories.FindAsync(id);
            //cap nhat
            category.categoryName = categoryDTO.categoryName;
            category.status = categoryDTO.status;
            
            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        
        public async Task<ActionResult<Category>> PostCategory(CategoryDTO categoryDTO)
        {
          if (_context.Categories == null)
          {
              return Problem("Entity set 'Context.categories'  is null.");
          }
            var category = new Category
            {
                categoryName = categoryDTO.categoryName,
                status = categoryDTO.status
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.categoryId }, categoryDTO);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories
                                 .Include(c => c.Products)
                                 .FirstOrDefaultAsync(c => c.categoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            // Kiểm tra nếu danh mục có sản phẩm liên kết
            if (category.Products.Any())
            {
                return BadRequest("Không thể xóa danh mục vì vẫn còn sản phẩm liên kết.");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.categoryId == id)).GetValueOrDefault();
        }
    }
}
