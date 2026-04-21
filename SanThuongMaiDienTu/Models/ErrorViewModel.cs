namespace SanThuongMaiDienTu.Models
{
    public class ErrorViewModel
    {
        // Thuộc tính để chứa mã định danh lỗi
        public string? RequestId { get; set; }

        // Kiểm tra xem có hiển thị RequestId hay không
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}