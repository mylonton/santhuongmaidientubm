using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SanThuongMaiDienTu.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(200)]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm")]
        [Range(0, (double)decimal.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Giá bán (VND)")]
        public decimal Price { get; set; }

        [Display(Name = "Mô tả sản phẩm")]
        public string? Description { get; set; }

        [Display(Name = "Link ảnh sản phẩm")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng tồn kho")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng không được âm")]
        [Display(Name = "Số lượng tồn")]
        public int StockQuantity { get; set; }

      
        [Display(Name = "Thông số kỹ thuật/Hiệu năng")]
        public string? Specification { get; set; } 

        // Liên kết với danh mục (Category)
        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}