using System.Linq;
using QuanLyKhoBaiAPI_V1.Data;
using QuanLyKhoBaiAPI_V1.Models;

namespace QuanLyKhoBaiAPI_V1.Services
{
    public class InventoryService
    {
        private readonly AppDbContext _db;

        public InventoryService(AppDbContext db)
        {
            _db = db;
        }

        public void Import(int productId, int warehouseId, int qty)
        {
            var inv = _db.Inventories
                .FirstOrDefault(x => x.ProductId == productId && x.WarehouseId == warehouseId);

            if (inv == null)
            {
                inv = new Inventory
                {
                    ProductId = productId,
                    WarehouseId = warehouseId,
                    Quantity = qty
                };
                _db.Inventories.Add(inv);
            }
            else
            {
                inv.Quantity += qty;
            }

            _db.SaveChanges();
        }

        public bool Export(int productId, int warehouseId, int qty)
        {
            var inv = _db.Inventories
                .FirstOrDefault(x => x.ProductId == productId && x.WarehouseId == warehouseId);

            if (inv == null || inv.Quantity < qty)
                return false;

            inv.Quantity -= qty;
            _db.SaveChanges();
            return true;
        }
    }
}
