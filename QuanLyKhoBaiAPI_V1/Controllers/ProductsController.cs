using Microsoft.AspNetCore.Mvc;
using QuanLyKhoBaiAPI_V1.Data;
using QuanLyKhoBaiAPI_V1.Models;

namespace QuanLyKhoBaiAPI_V1.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_db.Products.ToList());
        }

        [HttpPost]
        public IActionResult Create(Product p)
        {
            _db.Products.Add(p);
            _db.SaveChanges();
            return Ok(p);
        }
    }
}
