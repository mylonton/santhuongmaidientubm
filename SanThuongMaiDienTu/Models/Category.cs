using System.ComponentModel.DataAnnotations;

namespace SanThuongMaiDienTu.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(100)]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        // Mối quan hệ: Một danh mục có nhiều sản phẩm
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}