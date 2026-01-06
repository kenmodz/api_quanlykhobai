namespace QuanLyKhoBaiAPI_V1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }

    // DTO để nhận dữ liệu từ màn hình Login
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
