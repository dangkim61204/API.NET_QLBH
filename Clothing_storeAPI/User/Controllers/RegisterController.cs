using Clothing_storeAPI.Models;
using Clothing_storeAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Clothing_storeAPI.User.Controllers
{
    [Route("api/user/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly Context _context;

        public RegisterController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Index(AccountDTO request)
        {
            // Kiểm tra nếu tài khoản đã tồn tại
            var existingAccount = await _context.Accounts
                .FirstOrDefaultAsync(acc => acc.userName == request.userName || acc.email == request.email);

            if (existingAccount != null)
            {
                return BadRequest("Tài khoản đã tồn tại.");
            }

            // Tạo tài khoản mới
            var acc = new Account
            {
                accountId= Guid.NewGuid().ToString("N").Substring(0, 5),
                userName = request.userName,
                fullName = request.fullName,
                email = request.email,
                //phone = request.phone,
                address = request.address,
                //gender = request.gender,
                password = Utilitie.MDH5Hash(request.password), // Giả sử sử dụng MD5Hash cho mật khẩu
                role = "User", // Gán vai trò mặc định là "User"
            };

            // Lưu tài khoản vào cơ sở dữ liệu
            await _context.Accounts.AddAsync(acc);
            await _context.SaveChangesAsync();

            return Ok(acc);  // Trả về tài khoản mới đăng ký
        }
      
    }
}
