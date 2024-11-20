using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clothing_storeAPI.Models;
using Clothing_storeAPI.Models.DTO;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Authorization;

namespace Clothing_storeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly Context _context;

        public ProductController(Context context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Getproducts(int page = 1,
            int pageSize = 10,
            string? name = null,
            float? minPrice = null,
            float? maxPrice = null)
        {
            // Lấy danh sách sản phẩm
            var productsQuery = _context.Products.AsQueryable();

            // Tìm kiếm theo tên sản phẩm
            if (!string.IsNullOrEmpty(name))
            {
                productsQuery = productsQuery.Where(p => p.productName.Contains(name));
            }

            // Lọc theo khoảng giá
            if (minPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.price <= maxPrice.Value);
            }

            // Tính tổng số sản phẩm sau khi lọc
            int totalItems = await productsQuery.CountAsync();

            // Thực hiện phân trang
            var products = await productsQuery.Include(p=> p.Category)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Trả về kết quả với tổng số sản phẩm và danh sách sản phẩm phân trang
            return Ok(new
            {
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize,
                Products = products
            });
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
       
        public async Task<IActionResult> PutProduct(int id, [FromForm] ProductDTO productDTO)
        {
            //if (id != productDTO.id)
            //{
            //    return BadRequest();
            //}
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            //cap nhat
            product.code = productDTO.code;
            product.productName = productDTO.productName;
            product.price = productDTO.price;
            product.desciption = productDTO.desciption;
            product.categoryId = productDTO.categoryId;
        

            // Kiểm tra xem có ảnh được tải lên không
            if (productDTO.image != null)
            {
                string fileName = productDTO.image.FileName;
                string filePath = Path.Combine("wwwroot/images", fileName);

                // Lưu ảnh vào thư mục
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await productDTO.image.CopyToAsync(stream);
                }

                // Xóa ảnh cũ (nếu có)
                if (!string.IsNullOrEmpty(product.image))
                {
                    var oldImagePath = Path.Combine("wwwroot/images", product.image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                // Lưu đường dẫn ảnh vào thuộc tính image của sản phẩm
                product.image = fileName;
            }

            // Cập nhật trạng thái sản phẩm trong cơ sở dữ liệu
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
    
        public async Task<ActionResult<Product>> PostProduct([FromForm] ProductDTO productDTO)
        {

            // Kiểm tra xem có ảnh được tải lên không
            string imagePath = null;
            if (productDTO.image != null)
            {              
                string fileName = productDTO.image.FileName;
                string filePath = Path.Combine("wwwroot/images", fileName);

                // Lưu ảnh vào thư mục
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await productDTO.image.CopyToAsync(stream);
                }
                // Lưu đường dẫn ảnh vào thuộc tính image của sản phẩm
                imagePath = fileName;
            }
            // Chuyển DTO thành Product model
            var product = new Product
            {
                code = productDTO.code,
                productName = productDTO.productName,
                price = productDTO.price,
                desciption = productDTO.desciption,
                categoryId= productDTO.categoryId = 1,
                image = imagePath

            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.id }, product);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        //tìm kiem theo ten , khoang gia, phan trang
        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] SearchproductDTO request)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(request.productName))
            {
                query = query = query.Where(p => p.productName.ToLower().Contains(request.productName.ToLower()));
            }

            if (request.MinPrice.HasValue)
            {
                query = query.Where(p => p.price >= request.MinPrice.Value);
            }

            if (request.MaxPrice.HasValue)
            {
                query = query.Where(p => p.price <= request.MaxPrice.Value);
            }

            var totalItems = await query.CountAsync();
            var products = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var result = new
            {
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)request.PageSize),
                CurrentPage = request.PageNumber,
                Products = products
            };

            return Ok(result);
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
