using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SanThuongMaiDienTu.Models;

namespace SanThuongMaiDienTu.ViewComponents
{
    public class CartBadgeViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var sessionData = HttpContext.Session.GetString("Cart");
            var cart = string.IsNullOrEmpty(sessionData)
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(sessionData);

            // Đếm tổng số lượng sản phẩm trong giỏ
            int count = cart.Sum(i => i.Quantity);
            return View(count);
        }
    }
}