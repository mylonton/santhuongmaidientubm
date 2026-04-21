using System;

namespace SanThuongMaiDienTu.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        
        public string Image { get; set; } = "/images/no-image.png"; 
        
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        
        // Thuộc tính tính tổng tiền của một dòng sản phẩm
        public decimal Total => Price * Quantity;
    }
}