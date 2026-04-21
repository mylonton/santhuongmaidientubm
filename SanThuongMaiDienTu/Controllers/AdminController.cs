using Microsoft.AspNetCore.Mvc;
using SanThuongMaiDienTu.Data;
using System;
using System.Linq;

namespace SanThuongMaiDienTu.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = _context.Order
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            // 💰 Tổng doanh thu (chỉ tính đơn đã giao)
            ViewBag.TotalRevenue = _context.Order
                .Where(o => o.IsDelivered)
                .Sum(o => (decimal?)o.TotalAmount) ?? 0;

            // 🛒 Tổng đơn hàng
            ViewBag.TotalOrders = _context.Order.Count();

            // 📦 Tổng sản phẩm
            ViewBag.TotalProducts = _context.Products.Count();

            // 📦 Tổng tồn kho
            ViewBag.TotalStock = _context.Products
                .Sum(p => (int?)p.StockQuantity) ?? 0;

            // 📊 BIỂU ĐỒ (FIX LỖI EF Ở ĐÂY)
            var chartData = _context.Order
                .Where(o => o.IsDelivered)
                .ToList() // 👈 QUAN TRỌNG: đưa về RAM để tránh lỗi EF
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new
                {
                    Date = g.Key.ToString("dd/MM"),
                    Total = g.Sum(x => x.TotalAmount)
                })
                .OrderBy(x => x.Date)
                .Take(7)
                .ToList();

            ViewBag.ChartLabels = chartData.Select(x => x.Date).ToList();
            ViewBag.ChartData = chartData.Select(x => x.Total).ToList();

            return View(orders);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id)
        {
            var order = _context.Order.Find(id);
            if (order != null)
            {
                order.IsDelivered = !order.IsDelivered;
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

