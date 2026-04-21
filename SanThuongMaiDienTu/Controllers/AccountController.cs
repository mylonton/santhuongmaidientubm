using Microsoft.AspNetCore.Mvc; // Thiếu cái này nên nó không hiểu Controller là gì

namespace SanThuongMaiDienTu.Controllers
{
    // Phải kế thừa từ Controller
    public class AccountController : Controller 
    {
        // Trang Đăng nhập (GET)
        [HttpGet]
        public IActionResult Login() 
        {
            return View();
        }

        // Trang Đăng ký (GET)
        [HttpGet]
        public IActionResult Register() 
        {
            return View();
        }

        // Xử lý logic Đăng nhập (POST)
        [HttpPost]
        public IActionResult Login(string username, string password) 
        {
            if (username == "admin" && password == "123") 
            {
                // Lưu trạng thái đăng nhập vào Session để các trang khác biết
                HttpContext.Session.SetString("UserRole", "Admin");
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Sai tài khoản hoặc mật khẩu!";
            return View();
        }

        // Đăng xuất
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}