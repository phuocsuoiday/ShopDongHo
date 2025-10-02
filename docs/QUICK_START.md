# 🚀 QUICK START - SHOP ĐỒNG HỒ

## TL;DR - Chạy nhanh trong 5 phút

### 1. Add Files vào Project ✅
```
1. Visual Studio → Solution Explorer
2. Click "Show All Files" button (icon folder với dấu chấm)
3. Click phải các folders/files màu xám → "Include In Project"
   - Controllers/*.cs (5 files)
   - Models/Entities/*.cs (7 files)
   - Models/ViewModels/*.cs (6 files)
   - Models/Repositories/*.cs (6 files)
   - Helpers/*.cs (3 files)
   - Filters/*.cs (2 files)
   - Migrations/*.cs (1 file)
   - Views/**/*.cshtml (tất cả views)
4. Build Solution (Ctrl+Shift+B)
```

### 2. Restore NuGet Packages ✅
```
Tools → NuGet Package Manager → Manage NuGet Packages for Solution
→ Click "Restore" nếu có báo thiếu packages

Hoặc Package Manager Console:
Update-Package -reinstall
```

Packages cần có:
- EntityFramework 6.4.4
- BCrypt.Net-Next 4.0.3
- PagedList.Mvc 4.5.0

### 3. Tạo Database ✅
```
Tools → NuGet Package Manager → Package Manager Console

Chạy lần lượt:
1. Enable-Migrations          (nếu chưa có Migrations)
2. Add-Migration InitialCreate
3. Update-Database

Xong! Database "ShopDongHo" đã được tạo trên localhost
```

### 4. Chạy App ✅
```
Press F5 (hoặc click Start)

Truy cập: https://localhost:44376/
```

### 5. Test Login ✅
```
Admin:
- Username: admin
- Password: admin123

Customer:
- Username: customer
- Password: customer123
```

---

## 📁 FILES CẦN INCLUDE VÀO PROJECT

### Controllers (5 files)
- AccountController.cs
- AdminController.cs
- CartController.cs
- OrderController.cs
- ProductController.cs

### Models/Entities (7 files)
- Category.cs
- Product.cs
- User.cs
- Order.cs
- OrderDetail.cs
- ShopDbContext.cs
- ShopDbInitializer.cs

### Models/ViewModels (6 files)
- ProductViewModel.cs
- CartItemViewModel.cs
- CheckoutViewModel.cs
- LoginViewModel.cs
- RegisterViewModel.cs
- AdminProductViewModel.cs

### Models/Repositories (6 files)
- IRepository.cs
- Repository.cs
- ProductRepository.cs
- CategoryRepository.cs
- OrderRepository.cs
- UserRepository.cs

### Helpers (3 files)
- AuthHelper.cs
- SessionHelper.cs
- ImageHelper.cs

### Filters (2 files)
- AuthorizeRoleAttribute.cs
- AdminAuthorizeAttribute.cs

### Migrations (1 file)
- Configuration.cs

### Views (tất cả .cshtml files)
- Views/Product/Index.cshtml, Detail.cshtml
- Views/Cart/Index.cshtml, Checkout.cshtml
- Views/Account/Login.cshtml, Register.cshtml
- Views/Order/Confirm.cshtml
- Views/Shared/_ProductCard.cshtml, _Pagination.cshtml, _CartSummary.cshtml, _AdminLayout.cshtml

---

## ⚡ COMMANDS CHEAT SHEET

### Package Manager Console
```powershell
# Migrations
Enable-Migrations
Add-Migration InitialCreate
Update-Database
Get-Migrations

# NuGet
Update-Package -reinstall
Install-Package EntityFramework -Version 6.4.4
Install-Package BCrypt.Net-Next -Version 4.0.3
Install-Package PagedList.Mvc -Version 4.5.0
```

### Build
```
Ctrl+Shift+B  - Build Solution
Ctrl+F5       - Run without debugging
F5            - Run with debugging
```

---

## 🎯 CÁC TRANG CHÍNH

### User Pages
- **Trang chủ**: /
- **Sản phẩm**: /Product
- **Chi tiết SP**: /Product/Detail/1
- **Giỏ hàng**: /Cart
- **Thanh toán**: /Cart/Checkout
- **Đăng nhập**: /Account/Login
- **Đăng ký**: /Account/Register

### Admin Pages
- **Dashboard**: /Admin/Dashboard
- **Sản phẩm**: /Admin/Products
- **Thêm SP**: /Admin/CreateProduct
- **Danh mục**: /Admin/Categories
- **Đơn hàng**: /Admin/Orders

---

## 🔧 CONNECTION STRING

Đã cấu hình trong `Web.config`:
```xml
Data Source=localhost;
Initial Catalog=ShopDongHo;
Integrated Security=True;
MultipleActiveResultSets=True;
TrustServerCertificate=True
```

Nếu dùng SQL Express:
```xml
Data Source=.\SQLEXPRESS;...
```

---

## 📊 DATA MẪU

### Products: 12 sản phẩm
- 4 đồng hồ nam (Casio, Citizen, Seiko, Orient)
- 3 đồng hồ nữ (Casio, Citizen, Seiko)
- 3 đồng hồ thể thao (G-Shock x2, Citizen)
- 2 smartwatch (Apple Watch, Galaxy Watch)

### Categories: 4 danh mục
- Đồng hồ nam
- Đồng hồ nữ
- Đồng hồ thể thao
- Đồng hồ thông minh

### Users: 2 accounts
- admin / admin123 (Role: Admin)
- customer / customer123 (Role: Customer)

---

## ❗ COMMON ERRORS

### "Could not load file EntityFramework"
→ Restore NuGet packages

### "Login failed for user"
→ Kiểm tra SQL Server đang chạy
→ Thử đổi `localhost` → `.\SQLEXPRESS` trong connection string

### Files không có trong Solution Explorer
→ Click "Show All Files" → Include In Project

### Database không tạo được
→ Kiểm tra SQL Server Service đang running
→ Chạy lại `Update-Database -Force`

---

## 📚 DOCUMENTATION

Chi tiết xem các files:
- `docs/FOUNDATION_PLAN.md` - Kiến trúc tổng thể
- `docs/IMPLEMENTATION_STATUS.md` - Trạng thái triển khai
- `docs/DATABASE_MIGRATION_GUIDE.md` - Hướng dẫn database
- `docs/ADD_FILES_TO_PROJECT.md` - Hướng dẫn add files

---

## ✅ CHECKLIST HOÀN THÀNH

- [ ] Visual Studio đã mở solution
- [ ] Đã include tất cả files vào project
- [ ] Build thành công (0 errors)
- [ ] Đã restore NuGet packages
- [ ] SQL Server đang chạy
- [ ] Đã chạy migrations (Update-Database)
- [ ] Database ShopDongHo đã tồn tại
- [ ] App chạy được (F5)
- [ ] Đăng nhập admin thành công
- [ ] Xem được danh sách sản phẩm

---

Nếu tất cả checklist ✅ → **DONE!** Bắt đầu code thôi! 🎉
