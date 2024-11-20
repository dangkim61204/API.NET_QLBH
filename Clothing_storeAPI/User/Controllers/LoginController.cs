using Clothing_storeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Clothing_storeAPI.Models.Request;

namespace Clothing_storeAPI.User.Controllers
{
    [Route("api/user/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public LoginController(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost()]
        public IActionResult Index([FromBody] LoginRequest request)
        {
            // Mã hóa mật khẩu (MD5)
            var md5pass = Utilitie.MDH5Hash(request.password);

            // Kiểm tra người dùng trong DB
            var acc = _context.Accounts.FirstOrDefault(x => x.email == request.email && x.password == md5pass);
            if (acc != null)
            {
                // Tạo danh tính (ClaimsIdentity)
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, acc.userName),
                    new Claim(ClaimTypes.Email, acc.email),
                    new Claim(ClaimTypes.GivenName, acc.fullName),
                    new Claim(ClaimTypes.Role, acc.role)
                };

                //// Tạo JWT token
                //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //var token = new JwtSecurityToken(
                //    issuer: _configuration["Jwt:Issuer"],
                //    audience: _configuration["Jwt:Audience"],
                //    claims: claims,
                //    expires: DateTime.Now.AddHours(1),
                //    signingCredentials: creds
                //);

                //var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new { message = "Đăng nhập thành công" });
            }
            else
            {
                return Ok(new { message = "Email hoặc mật khẩu không chính xác." });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            // Hủy đăng nhập (JWT không cần hủy cookie)
            return Ok(new { message = "Đã đăng xuất." });
        }
    }
}
