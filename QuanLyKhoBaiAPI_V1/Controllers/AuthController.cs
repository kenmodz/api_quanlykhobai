using Microsoft.AspNetCore.Mvc;
using QuanLyKhoBaiAPI_V1.Data;
using QuanLyKhoBaiAPI_V1.Models; // Đảm bảo bạn đã có AppDbContext và Model User
using System.Linq;

namespace QuanLyKhoBaiAPI_V1.Controllers
{
    [Route("api/[controller]")] // Đường dẫn sẽ là: api/Auth
    [ApiController] // Đánh dấu đây là API, không phải Web giao diện
    public class AuthController : ControllerBase // Đổi từ Controller sang ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // API Đăng nhập
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Tìm user trong database khớp cả User và Pass
            var user = _context.Users.FirstOrDefault(u =>
                u.Username == request.Username && u.Password == request.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Tài khoản hoặc mật khẩu không chính xác!" });
            }

            return Ok(new
            {
                message = "Đăng nhập thành công!",
                user = user.Username,
                fullName = user.FullName
            });
        }
    }

    // Lớp tạm để hứng dữ liệu từ phía Client gửi lên
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}