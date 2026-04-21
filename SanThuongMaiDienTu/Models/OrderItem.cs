using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SanThuongMaiDienTu.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }
        
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }

        [Required]
        public int ProductId { get; set; }
        
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }

        [Display(Name = "Số lượng")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải ít nhất là 1")]
        public int Quantity { get; set; }

        [Display(Name = "Giá bán")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } 
    }
}