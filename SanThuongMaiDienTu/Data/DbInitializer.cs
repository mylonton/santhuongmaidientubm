using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SanThuongMaiDienTu.Models;

namespace SanThuongMaiDienTu.Data
{
    public static class DbInitializer
    {
        // Fix: Đổi tên thành Initialize (không async) để Program.cs gọi dễ hơn, 
        // hoặc giữ Async nhưng phải gọi đúng cách. Ở đây tôi dùng cách an toàn nhất cho ông.
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            context.Database.Migrate();

            // 1. Tạo Danh Mục
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "Điện tử" },
                    new Category { Name = "Thời trang" },
                    new Category { Name = "Sách & Quà tặng" },
                    new Category { Name = "Gia dụng" }
                );
                context.SaveChanges();
            }

            // 2. Tạo 21 SẢN PHẨM (Đã thêm StockQuantity = 100)
            if (!context.Products.Any())
            {
                var products = new List<Product>
                {
                    // FIX: Tôi thêm StockQuantity = 100 cho mỗi món để không bị báo HẾT HÀNG
                    new Product { Name = "iPhone 15 Pro Max 256GB Titanium", Price = 34990000, StockQuantity = 100, Description = "Chip A17 Pro mạnh mẽ nhất, camera 48MP zoom quang 5x.", ImageUrl = "https://media-cdn.bnn.in.th/332492/iPhone_15_Pro_Max_Natural_Titanium_6-square_medium.jpg", CategoryId = 1 },
                    new Product { Name = "MacBook Air M2 13 inch 2023", Price = 26500000, StockQuantity = 100, Description = "Siêu mỏng nhẹ, hiệu năng vượt trội với chip M2.", ImageUrl = "https://macfinder.co.uk/wp-content/uploads/2023/03/img-MacBook-Air-13-Inch-35786.jpg", CategoryId = 1 },
                    new Product { Name = "Tai nghe Sony WH-1000XM5 Black", Price = 8450000, StockQuantity = 100, Description = "Chống ồn đỉnh cao thế giới, chất âm trung thực.", ImageUrl = "https://m.media-amazon.com/images/I/41M7sHrx90L.jpg", CategoryId = 1 },
                    new Product { Name = "Apple Watch Series 9 GPS", Price = 10290000, StockQuantity = 100, Description = "Theo dõi sức khỏe thông minh, màn hình luôn bật.", ImageUrl = "https://tse2.mm.bing.net/th/id/OIP.slSR8oe_IbyDxcydRIZLzAHaHa?rs=1&pid=ImgDetMain&o=7&rm=3", CategoryId = 1 },
                    new Product { Name = "Bàn phím cơ Keychron K2V2", Price = 2250000, StockQuantity = 100, Description = "Gõ phím cực sướng cho coder và gamer.", ImageUrl = "https://m.media-amazon.com/images/I/61OS8Ptfl2L._SX522_.jpg", CategoryId = 1 },
                    new Product { Name = "Chuột Logitech MX Master 3S", Price = 2490000, StockQuantity = 100, Description = "Chuột tốt nhất cho dân thiết kế và văn phòng.", ImageUrl = "https://p.turbosquid.com/ts-thumb/IV/YQOJ87/C9/00logitechmxmaster3mouseblack00thumbnail/jpg/1687936064/1920x1080/fit_q87/cce99e5608a9430146e1887a7a6ed5d71749acc6/00logitechmxmaster3mouseblack00thumbnail.jpg", CategoryId = 1 },

                    new Product { Name = "Giày Sneaker Nike Air Force 1 '07", Price = 3200000, StockQuantity = 100, Description = "Phong cách kinh điển, năng động.", ImageUrl = "https://myshoes.vn/image/cache/catalog/2025/nike/nike5/giay-nike-air-force-1-07-nu-trang-04-1600x1600.jpg", CategoryId = 2 },
                    new Product { Name = "Áo Hoodie Essentials Gray FOG", Price = 1550000, StockQuantity = 100, Description = "Chất nỉ dày dặn, form rộng thời thượng.", ImageUrl = "https://down-th.img.susercontent.com/file/th-11134207-7rash-m3ngb63lqdp73f", CategoryId = 2 },
                    new Product { Name = "Balo Mark Ryden Anti-Theft MR9031", Price = 1150000, StockQuantity = 100, Description = "Chống trộm thông minh, có cổng sạc USB.", ImageUrl = "https://www.gntpremium.com/wp-content/uploads/2023/07/MR9031-5-1024x1024.jpg", CategoryId = 2 },
                    new Product { Name = "Kính mát Ray-Ban Aviator Classic RB3025", Price = 4250000, StockQuantity = 100, Description = "Thiết kế phi công cổ điển, chống tia UV.", ImageUrl = "https://tse2.mm.bing.net/th/id/OIP.MLJlVeOqZBIagnWapmzAGAHaHa?rs=1&pid=ImgDetMain&o=7&rm=3", CategoryId = 2 },
                    new Product { Name = "Đồng hồ Seiko Presage SRPD37J1 Cocktail", Price = 12500000, StockQuantity = 100, Description = "Đồng hồ cơ Nhật Bản đẳng cấp.", ImageUrl = "https://tse3.mm.bing.net/th/id/OIP.I7DH6UBsZ2I4T95ZWcLH0AHaHa?w=670&h=670&rs=1&pid=ImgDetMain&o=7&rm=3", CategoryId = 2 },

                    new Product { Name = "Sách Còn Chút Gì Để Nhớ ", Price = 120000, StockQuantity = 100, Description = "Từ nhà văn Nguyễn Nhật Ánh.", ImageUrl = "https://www.netabooks.vn/Data/Sites/1/Product/50023/1con-chut-gi-de-nho-phien-ban-mau-dac-biet.jpg", CategoryId = 3 },
                    new Product { Name = "Nước hoa YSL", Price = 180000, StockQuantity = 100, Description = "Nước hoa cao cấo, cho người bạn đồng hành.", ImageUrl = "https://product.hstatic.net/1000025647/product/yves-saint-laurent-libre-50ml_8d098915b8b24705a47631afa90c5240_1024x1024.png", CategoryId = 3 },
                    new Product { Name = "Bộ xếp hình Lego Creator Expert Fiat 500", Price = 2550000, StockQuantity = 100, Description = "Mô hình sáng tạo cực chi tiết.", ImageUrl = "https://m.media-amazon.com/images/I/91TqoZg-SlL._AC_SL1500_.jpg", CategoryId = 3 },
                    new Product { Name = "Cốc giữ nhiệt Yeti Rambler 20oz", Price = 850000, StockQuantity = 100, Description = "Giữ lạnh suốt 24 tiếng, giữ nóng 12 tiếng.", ImageUrl = "https://www.winwinstore.vn/wp-content/uploads/2021/12/coc-giu-nhiet-yeti-travel-mug-20oz-navy-1.jpeg", CategoryId = 3 },
                    new Product { Name = "Nến thơm tinh dầu Agaya cao cấp", Price = 450000, StockQuantity = 100, Description = "Hương thơm dịu nhẹ, giúp thư giãn.", ImageUrl = "https://nenthomagaya.com/wp-content/uploads/2021/11/nen-thom-agaya-candle-cup-thu-duc-72.jpeg", CategoryId = 3 },

                    new Product { Name = "Máy pha Cà phê mini Delonghi EC230.BK", Price = 4800000, StockQuantity = 100, Description = "Pha Espresso chuẩn vị Ý tại nhà.", ImageUrl = "https://tse3.mm.bing.net/th/id/OIP.lZi6Ntn1UUvjEtZl6gFhsAHaHa?w=1280&h=1280&rs=1&pid=ImgDetMain&o=7&rm=3", CategoryId = 4 },
                    new Product { Name = "Nồi chiên không dầu Philips HD9270/90", Price = 3250000, StockQuantity = 100, Description = "Nấu ăn ngon, giảm 80% dầu mỡ.", ImageUrl = "https://tse1.mm.bing.net/th/id/OIP.O7xF3gaRhyr4vSTzBIXSJAHaHa?rs=1&pid=ImgDetMain&o=7&rm=3", CategoryId = 4 },
                    new Product { Name = "Robot hút bụi Xiaomi Vacuum-Mop 2 Ultra", Price = 6900000, StockQuantity = 100, Description = "Lực hút mạnh, tự động dọn dẹp.", ImageUrl = "https://bizweb.dktcdn.net/100/450/414/files/xiaomi-mi-robot-vacuum-mop-2-ultra-gps.jpg?v=1652603439284", CategoryId = 4 },
                    new Product { Name = "Đèn bàn thông minh Xiaomi Mi Smart Desk Lamp Pro", Price = 650000, StockQuantity = 100, Description = "Bảo vệ mắt khi làm việc, điều chỉnh ánh sáng.", ImageUrl = "https://smarthomekit.vn/wp-content/uploads/2019/08/den-ban-thong-minh-Xiaomi-LED-Table-Lamp-3-768x768.jpg", CategoryId = 4 },
                    new Product { Name = "Bộ dao bếp Nhật Bản Global G-2 G-5 G-9 G-46", Price = 1850000, StockQuantity = 100, Description = "Thép không gỉ siêu bén, thiết kế sang trọng.", ImageUrl = "https://cutleryandmore.com/cdn/shop/products/48837_5febe161-71c0-45ab-9801-7e2e40356215_3000x.jpg?v=1665001861", CategoryId = 4 }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }

            // 3. Seed Quyền Admin
            // Lưu ý: Đoạn Async trong hàm Void phải gọi .GetAwaiter().GetResult() để tránh treo máy
            if (!roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult()) 
                roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();

            var adminEmail = "admin@bm.com";
            if (userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult() == null)
            {
                var admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                userManager.CreateAsync(admin, "Admin@123").GetAwaiter().GetResult();
                userManager.AddToRoleAsync(admin, "Admin").GetAwaiter().GetResult();
            }
        }
    }
}