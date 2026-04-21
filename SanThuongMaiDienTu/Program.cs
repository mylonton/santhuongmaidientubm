using Microsoft.EntityFrameworkCore;
using SanThuongMaiDienTu.Data;
using SanThuongMaiDienTu.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Cấu hình Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Cấu hình Session & Cache (Rất quan trọng cho Giỏ hàng)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = ".BMDigital.Session"; // Đặt tên riêng cho Cookie của mình
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// 3. Khởi tạo dữ liệu (Seeding Data)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try 
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated(); // Đảm bảo DB đã được tạo

        // Tự động bơm hàng nếu kho trống - Chỉ dùng khi đang phát triển
        var products = context.Products.ToList();
        if (products.Any(p => p.StockQuantity <= 0)) 
        {
            foreach (var p in products.Where(x => x.StockQuantity <= 0)) 
            {
                p.StockQuantity = 100; 
            }
            context.SaveChanges();
        }
    } 
    catch (Exception ex) 
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Có lỗi xảy ra khi nạp dữ liệu Database.");
    }
}

// 4. Cấu hình Middleware Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Cho phép đọc file CSS, JS từ wwwroot

app.UseRouting();

// Kích hoạt Session TRƯỚC Authorization
app.UseSession(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();