using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clothing_storeAPI.Models;
using Clothing_storeAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Clothing_storeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly Context _context;

        public OrdersController(Context context)
        {
            _context = context;
        }
        // Lấy danh sách tất cả các đơn hàng, bao gồm chi tiết đơn hàng (OrderDetails).
        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            return await _context.Orders.Include(o => o.OrderDetails).ToListAsync();
        }

        //Lấy thông tin chi tiết một đơn hàng theo id.

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var orderDt = await _context.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.id == id);

            if (orderDt == null)
            {
                return NotFound();
            }

            return orderDt;
        }

        //Cập nhật thông tin của một đơn hàng đã có
        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutOrder(int id, [FromForm] OrderDTO orderDTO)
        //{
        //    if (id != orderDTO.OrderId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(orderDTO).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!OrderExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //Tạo một đơn hàng mới và lưu vào cơ sở dữ liệu
        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromForm] OrderDTO request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Dữ liệu không hợp lệ.");
                }
                if (string.IsNullOrWhiteSpace(request.userName))
                {
                    return BadRequest("UserName là bắt buộc.");
                }

            
                var order = new Order
                {
                    code = Guid.NewGuid().ToString("N").Substring(0, 8),
                    userName = request.userName,    
                    address = request.address,
                    phone = request.phone,
                    active = request.active,
                    orderDate = request.orderDate == default ? DateTime.UtcNow : request.orderDate,

                };

                _context.Orders.Add(order);
               
                await _context.SaveChangesAsync();

                //var orderDetail = order.id;
                foreach (var item in request.cartItems)
                {
                    Console.WriteLine($"Item ID: {item.id}");
                    var orderDetail = new OrderDetail
                    {
                        //code = Guid.NewGuid().ToString("N").Substring(0, 8),
                        orderId = order.id,
                        productId = item.id,//id sp
                        price = item.price,                       
                        quantity = item.quantity,
                    };
                    _context.OrderDetails.Add(orderDetail);

                    await _context.SaveChangesAsync();
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi chi tiết
                throw new Exception("Lỗi khi tạo đơn hàng");
                //return StatusCode(500, "Đã xảy ra lỗi khi xử lý yêu cầu.");
            }
        }

        //Xóa một đơn hàng theo id.
        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.id == id);
        }
    }

    //private bool OrderExists(int id)
    //{
    //    return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
    //}

}
