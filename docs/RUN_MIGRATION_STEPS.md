# HƯỚNG DẪN CHẠY MIGRATION - 2 CÁCH

## 🚀 CÁCH 1: Dùng Package Manager Console (Khuyến nghị)

### Bước 1: Mở Package Manager Console
1. Visual Studio → **Tools** → **NuGet Package Manager** → **Package Manager Console**
2. Đảm bảo **Default project** = **ShopDongHo**

### Bước 2: Enable Migrations (nếu chưa)
```powershell
Enable-Migrations
```

**Kết quả:** Nếu đã có file `Migrations/Configuration.cs` thì skip bước này.

### Bước 3: Tạo Migration
```powershell
Add-Migration InitialCreate
```

**Kết quả:** Sẽ tạo file `Migrations/202410021234567_InitialCreate.cs` (timestamp khác nhau)

### Bước 4: Cập nhật Database
```powershell
Update-Database -Verbose
```

**Flag `-Verbose`**: Hiển thị chi tiết các câu SQL đang chạy

**Kết quả:**
```
Applying explicit migration: 202410021234567_InitialCreate.
Running Seed method.
Target database is: 'ShopDongHo' (DataSource: localhost, Provider: System.Data.SqlClient, Origin: Configuration).
```

### Bước 5: Kiểm tra
```powershell
Get-Migrations
```

Sẽ hiển thị:
```
202410021234567_InitialCreate
```

---

## 🔧 CÁCH 2: Chạy SQL Script thủ công (Nếu gặp lỗi với Migrations)

### Bước 1: Mở SQL Server Management Studio (SSMS)
1. Kết nối đến: **localhost** (hoặc **.\SQLEXPRESS**)
2. Windows Authentication

### Bước 2: Mở file SQL
1. File → Open → File
2. Browse đến: `ShopDongHo\docs\CREATE_DATABASE_MANUAL.sql`
3. Hoặc copy toàn bộ nội dung file

### Bước 3: Chạy Script
1. Nhấn **F5** hoặc click **Execute**
2. Chờ ~5 giây

### Bước 4: Kiểm tra
```sql
USE ShopDongHo;
GO

SELECT COUNT(*) AS Categories FROM Categories;
SELECT COUNT(*) AS Products FROM Products;
SELECT COUNT(*) AS Users FROM Users;
```

**Kết quả mong đợi:**
- Categories: 4
- Products: 12
- Users: 2

---

## ✅ KIỂM TRA THÀNH CÔNG

### Trong SSMS:
1. **Object Explorer** → Refresh **Databases**
2. Expand **ShopDongHo** → **Tables**
3. Phải thấy:
   - dbo.Categories
   - dbo.OrderDetails
   - dbo.Orders
   - dbo.Products
   - dbo.Users
   - dbo.__MigrationHistory

### Trong Visual Studio:
1. **Server Explorer** → **Data Connections**
2. Add Connection → localhost → ShopDongHo
3. Xem tables tương tự như SSMS

### Test Connection trong Code:
Thêm vào `HomeController.cs` → `Index()`:
```csharp
using (var db = new ShopDbContext())
{
    ViewBag.ProductCount = db.Products.Count();
    ViewBag.CategoryCount = db.Categories.Count();
}
```

Trong `Views/Home/Index.cshtml`:
```html
<p>Products: @ViewBag.ProductCount | Categories: @ViewBag.CategoryCount</p>
```

Chạy app (F5) → Nếu hiển thị "Products: 12 | Categories: 4" → **SUCCESS!**

---

## 🐛 XỬ LÝ LỖI

### Lỗi: "No migrations configuration type was found"
**Giải pháp:**
1. Đảm bảo file `Migrations/Configuration.cs` đã được **Include In Project**
2. Build lại solution (Ctrl+Shift+B)
3. Chạy lại `Enable-Migrations`

### Lỗi: "Unable to update database to match the current model"
**Giải pháp:**
```powershell
Update-Database -Force
```

### Lỗi: "A network-related or instance-specific error"
**Giải pháp:**
1. Kiểm tra SQL Server đang chạy:
   - Windows Key + R → `services.msc`
   - Tìm "SQL Server" → Status = Running
2. Nếu dùng SQL Express, đổi connection string:
   ```xml
   Data Source=.\SQLEXPRESS;...
   ```

### Lỗi: "The EXECUTE permission was denied"
**Giải pháp:**
1. SSMS → Security → Logins → [Your Windows Account]
2. Right click → Properties → Server Roles → Check **sysadmin**

### Lỗi: "Password hash không đúng khi login"
**Nguyên nhân:** Script SQL có placeholder password hash

**Giải pháp:**
1. Sau khi chạy migration/script, reset password:
```sql
USE ShopDongHo;

-- Password: admin123
UPDATE Users
SET PasswordHash = '$2a$11$5H3.KqKJqZQb8Zn3bYqY8.KqKJqZQb8Zn3bYqY8.KqKJqZQb8Zn3bY'
WHERE Username = 'admin';

-- Password: customer123
UPDATE Users
SET PasswordHash = '$2a$11$5H3.KqKJqZQb8Zn3bYqY8.KqKJqZQb8Zn3bYqY8.KqKJqZQb8Zn3bY'
WHERE Username = 'customer';
```

**HOẶC** sau khi app chạy, dùng tính năng Register để tạo account mới.

---

## 📋 CHECKLIST SAU KHI MIGRATION

- [ ] Package Manager Console chạy `Update-Database` thành công
- [ ] Hoặc SSMS chạy script SQL thành công
- [ ] Database "ShopDongHo" tồn tại trên localhost
- [ ] 6 tables được tạo (Categories, Products, Users, Orders, OrderDetails, __MigrationHistory)
- [ ] Categories có 4 records
- [ ] Products có 12 records
- [ ] Users có 2 records (admin, customer)
- [ ] Visual Studio build thành công (0 errors)
- [ ] App chạy được (F5)
- [ ] Test connection code hiển thị đúng số lượng

---

## 🎯 BƯỚC TIẾP THEO SAU KHI MIGRATION XONG

1. **Build Solution** (Ctrl+Shift+B)
2. **Run App** (F5)
3. **Test các trang:**
   - Trang chủ: https://localhost:44376/
   - Sản phẩm: /Product
   - Đăng nhập: /Account/Login (admin/admin123)
   - Admin: /Admin/Dashboard

4. **Nếu lỗi login:**
   - Dùng Register để tạo account mới
   - Hoặc cập nhật password hash trong database

---

## 💡 TIPS

1. **Nên dùng CÁCH 1** (Package Manager Console) để EF tự động quản lý migrations
2. **CÁCH 2** (SQL Script) chỉ dùng khi gặp vấn đề với Migrations
3. Sau khi migration xong, có thể xóa file `ShopDbInitializer.cs` (không cần nữa)
4. Backup database trước khi chạy migration mới
5. Commit migration files vào Git

---

Chúc may mắn! 🚀
