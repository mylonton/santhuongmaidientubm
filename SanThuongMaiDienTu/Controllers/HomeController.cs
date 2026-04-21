using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanThuongMaiDienTu.Data;
using SanThuongMaiDienTu.Models;

namespace SanThuongMaiDienTu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // 🏠 TRANG CHỦ + SEARCH + FILTER
        public async Task<IActionResult> Index(string query, int? categoryId)
        {
            // Sidebar danh mục
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.CurrentCategoryId = categoryId;

            var productsQuery = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            // 🔎 LỌC THEO DANH MỤC
            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId);
            }

            // 🔍 SEARCH
            if (!string.IsNullOrWhiteSpace(query))
            {
                ViewData["SearchQuery"] = query;

                var searchResult = productsQuery.Where(p =>
                    p.Name.Contains(query) ||
                    (p.Description != null && p.Description.Contains(query))
                );

                if (!await searchResult.AnyAsync())
                {
                    TempData["SearchError"] = "Sản phẩm này chưa có trên hệ thống";
                    return View(await productsQuery.ToListAsync());
                }

                productsQuery = searchResult;
            }

            return View(await productsQuery.ToListAsync());
        }

        // ✅ FIX QUAN TRỌNG: CHI TIẾT SẢN PHẨM
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // 🔒 PRIVACY
        public IActionResult Privacy()
        {
            return View();
        }

        // ❌ ERROR
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}

