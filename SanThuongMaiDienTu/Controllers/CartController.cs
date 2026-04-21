using Microsoft.AspNetCore.Mvc;
using SanThuongMaiDienTu.Data;
using SanThuongMaiDienTu.Models;
using Newtonsoft.Json;

namespace SanThuongMaiDienTu.Controllers
{
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string CART_KEY = "Cart";

        public CartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Trang danh sách giỏ hàng
        public IActionResult Index()
        {
            return View(GetCartItems());
        }

        // 2. Thêm sản phẩm vào giỏ
        [HttpPost]
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            var cart = GetCartItems();
            var item = cart.FirstOrDefault(p => p.ProductId == id);

            if (item == null)
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = quantity,
                    Image = product.ImageUrl ?? ""
                });
            }
            else
            {
                item.Quantity += quantity;
            }

            SaveCartSession(cart);
            return RedirectToAction("Index");
        }

        // 3. Cập nhật số lượng (+/-)
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            var cart = GetCartItems();
            var item = cart.FirstOrDefault(p => p.ProductId == id);

            if (item != null)
            {
                if (quantity > 0) item.Quantity = quantity;
                else cart.Remove(item);
            }

            SaveCartSession(cart);
            return RedirectToAction("Index");
        }

        // 4. Xóa sản phẩm khỏi giỏ
        public IActionResult Remove(int id)
        {
            var cart = GetCartItems();
            var item = cart.FirstOrDefault(p => p.ProductId == id);
            if (item != null) cart.Remove(item);

            SaveCartSession(cart);
            return RedirectToAction("Index");
        }

        // 5. Trang nhập thông tin thanh toán (GET)
        public IActionResult Checkout()
        {
            var cart = GetCartItems();
            if (cart.Count == 0) return RedirectToAction("Index");
            return View(cart);
        }

        // 6. Xử lý đặt hàng và lưu Database (POST)
        [HttpPost]
        public IActionResult ProcessOrder(string address, string phone)
        {
            var cart = GetCartItems();
            if (cart.Count == 0) return RedirectToAction("Index");

            try
            {
                // Tạo đơn hàng mới
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    
                    Address = address, 
                    Phone = phone,
                    TotalAmount = cart.Sum(i => i.Price * i.Quantity),
                    OrderItems = cart.Select(i => new OrderItem
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        Price = i.Price
                    }).ToList()
                };

                _context.Order.Add(order);
                _context.SaveChanges();

                // Xóa giỏ hàng sau khi thành công
                HttpContext.Session.Remove(CART_KEY);

                TempData["Success"] = "Đặt hàng thành công! Mã đơn của bạn là: #" + order.Id;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi khi lưu đơn hàng: " + ex.Message);
                return View("Checkout", cart);
            }
        }

        // --- Hàm hỗ trợ Session ---
        private List<CartItem> GetCartItems()
        {
            var sessionData = HttpContext.Session.GetString(CART_KEY);
            return string.IsNullOrEmpty(sessionData)
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(sessionData)!;
        }

        private void SaveCartSession(List<CartItem> cart)
        {
            HttpContext.Session.SetString(CART_KEY, JsonConvert.SerializeObject(cart));
        }
    }
}