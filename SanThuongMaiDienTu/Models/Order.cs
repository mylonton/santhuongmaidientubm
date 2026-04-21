using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SanThuongMaiDienTu.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now; // Tự động lấy giờ hiện tại

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        // --- THÔNG TIN KHÁCH HÀNG ---
        
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ nhận hàng")]
        [StringLength(500)]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không đúng định dạng")]
        [StringLength(15)]
        public string Phone { get; set; } = string.Empty;

        // --- TRẠNG THÁI GIAO HÀNG 
        [Display(Name = "Trạng thái giao hàng")]
        public bool IsDelivered { get; set; } = false; // Mặc định là chưa giao

        // --- QUAN HỆ DỮ LIỆU ---
        
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}