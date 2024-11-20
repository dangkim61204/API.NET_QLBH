using Clothing_storeAPI.Models;
using Clothing_storeAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clothing_storeAPI.User.Controllers
{
    [Route("api/user/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
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

        [HttpGet]
        public async Task<IActionResult> getAllProduct(int page = 1,
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
            var products = await productsQuery.Include(p => p.Category)
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

        //tìm kiem theo ten , khoang gia, phan trang
        [HttpGet("search")]
        public async Task<IActionResult> SearchName([FromQuery] SearchproductDTO request)
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
    }

    
}