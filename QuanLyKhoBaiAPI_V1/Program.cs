using Microsoft.EntityFrameworkCore;
using QuanLyKhoBaiAPI_V1.Data;
using QuanLyKhoBaiAPI_V1.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. ĐĂNG KÝ DỊCH VỤ CORS (BẮT BUỘC ĐỂ KẾT NỐI VỚI HTML)
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<InventoryService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 2. KÍCH HOẠT CORS (PHẢI NẰM TRƯỚC APP.MAPCONTROLLERS)
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Lưu ý: Nếu bạn chạy local để test với HTML, có thể tạm comment dòng HttpsRedirection 
// để tránh lỗi chứng chỉ SSL không hợp lệ
// app.UseHttpsRedirection(); 

app.UseAuthorization();
app.MapControllers();
app.Run();