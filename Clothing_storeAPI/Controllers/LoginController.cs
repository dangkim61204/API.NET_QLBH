using Clothing_storeAPI.Models;
using Clothing_storeAPI.Models.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Clothing_storeAPI.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost]

        public IActionResult Index([FromBody] LoginRequest request)
        {
            // Mã hóa password
            var md5pass = Utilitie.MDH5Hash(request.password);

            // Kiểm tra người dùng trong db
            var acc = _context.Accounts.FirstOrDefault(x => x.userName == request.userName && x.password == md5pass);
            if (acc != null)
            {
                if (acc.role == "Admin")
                {
                    var identity = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, acc.userName),
                        new Claim(ClaimTypes.GivenName, acc.fullName),
                        new Claim(ClaimTypes.Role, acc.role)
                    };

                    // Lấy thông tin từ cấu hình
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


                    // Tạo token
                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        claims: identity,
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: creds);

                    
                    // Trả về token dưới dạng JSON
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        status_code = 200
                    });
                }
            }
            // Trường hợp đăng nhập không thành công
            return Unauthorized(new { message = "Tên đăng nhập hoặc mật khẩu không đúng." });
        }
    }
}
