using SanThuongMaiDienTu.Models;
using System.Collections.Generic;

namespace SanThuongMaiDienTu.ViewModels
{
    public class ProductListViewModel
    {
        // Danh sách sản phẩm để hiển thị trên lưới (Index)
        public IEnumerable<Product> Products { get; set; } = new List<Product>();

        // Danh sách các danh mục để hiển thị ở Sidebar hoặc Menu
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();

        // ID danh mục hiện tại đang được lọc
        public int? CurrentCategoryId { get; set; }
        
        // Tên danh mục hiện tại (Để hiện tiêu đề như: "Sản phẩm Điện tử")
        public string? CurrentCategoryName { get; set; }

        // Lưu từ khóa tìm kiếm để giữ lại chữ trong ô input sau khi Search
        public string? SearchString { get; set; }
    }
}