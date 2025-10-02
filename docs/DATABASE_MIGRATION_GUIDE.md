# HƯỚNG DẪN KẾT NỐI DATABASE VÀ MIGRATIONS

## ✅ ĐÃ CẤU HÌNH

### 1. Connection String
- ✅ Đã cập nhật `Web.config` kết nối đến SQL Server localhost
- ✅ Database sẽ tên là: **ShopDongHo**
- ✅ Sử dụng Integrated Security (Windows Authentication)

```xml
<connectionStrings>
  <add name="ShopDongHoConnection"
       connectionString="Data Source=localhost;Initial Catalog=ShopDongHo;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### 2. Entity Framework Migrations
- ✅ Đã tạo `Migrations/Configuration.cs` với seed data
- ✅ Đã cập nhật `ShopDbContext` để dùng Migrations

---

## 🚀 CÁCH CHẠY MIGRATIONS

### Bước 1: Mở Package Manager Console

Trong Visual Studio:
1. **Tools** → **NuGet Package Manager** → **Package Manager Console**
2. Hoặc nhấn phím tắt: **Ctrl+Q** → Gõ "Package Manager Console"

### Bước 2: Kiểm tra Default Project

Ở dropdown **"Default project:"** trong Package Manager Console, chọn: **ShopDongHo**

### Bước 3: Enable Migrations (Nếu chưa có)

```powershell
Enable-Migrations
```

**Lưu ý:** Có thể bỏ qua bước này vì đã tạo sẵn `Migrations/Configuration.cs`

### Bước 4: Tạo Migration đầu tiên

```powershell
Add-Migration InitialCreate
```

**Kết quả:** Sẽ tạo file migration trong folder `Migrations/` với timestamp, ví dụ:
```
202410021234567_InitialCreate.cs
```

File này chứa code để tạo tất cả các bảng: Categories, Products, Users, Orders, OrderDetails

### Bước 5: Cập nhật Database

```powershell
Update-Database
```

**Lệnh này sẽ:**
1. Tạo database **ShopDongHo** trên SQL Server localhost (nếu chưa có)
2. Tạo tất cả các bảng theo schema đã define
3. Chạy Seed() method để insert data mẫu:
   - 4 Categories
   - 12 Products
   - 2 Users (admin/customer)

### Bước 6: Kiểm tra Database

1. Mở **SQL Server Management Studio** (SSMS)
2. Kết nối đến: **localhost** (hoặc **.\SQLEXPRESS** nếu dùng SQL Express)
3. Refresh **Databases** → Sẽ thấy database **ShopDongHo**
4. Expand **Tables** → Sẽ thấy:
   - dbo.Categories
   - dbo.Products
   - dbo.Users
   - dbo.Orders
   - dbo.OrderDetails
   - dbo.__MigrationHistory (bảng tracking migrations của EF)

---

## 📋 COMMANDS TỔNG HỢP

### Tạo Migration mới
```powershell
Add-Migration <TênMigration>
```
Ví dụ: `Add-Migration AddProductImageField`

### Cập nhật Database
```powershell
Update-Database
```

### Rollback về migration trước
```powershell
Update-Database -TargetMigration <TênMigration>
```

### Xem danh sách migrations
```powershell
Get-Migrations
```

### Tạo SQL script từ migration
```powershell
Update-Database -Script
```

### Xóa database và tạo lại từ đầu
```powershell
Update-Database -TargetMigration:$InitialDatabase
Update-Database
```

---

## 🔍 KIỂM TRA KẾT NỐI

### Test Connection trong Code

Thêm đoạn code này vào `HomeController.cs` → `Index()` action (tạm thời):

```csharp
using (var db = new ShopDbContext())
{
    var count = db.Products.Count();
    ViewBag.ProductCount = count;
}
```

Sau đó trong `Views/Home/Index.cshtml`, thêm:
```html
<p>Số sản phẩm: @ViewBag.ProductCount</p>
```

Chạy app (F5), nếu thấy "Số sản phẩm: 12" → Kết nối thành công!

---

## 🐛 TROUBLESHOOTING

### Lỗi: "A network-related or instance-specific error"
**Nguyên nhân:** Không kết nối được SQL Server

**Giải pháp:**
1. Kiểm tra SQL Server đã chạy:
   - Services → Tìm "SQL Server" → Đảm bảo đang Running
2. Thử đổi connection string:
   ```xml
   Data Source=.\SQLEXPRESS;Initial Catalog=ShopDongHo;...
   ```
   hoặc
   ```xml
   Data Source=(local);Initial Catalog=ShopDongHo;...
   ```

### Lỗi: "Login failed for user"
**Nguyên nhân:** Vấn đề authentication

**Giải pháp:**
1. Nếu dùng SQL Server Authentication, thay đổi connection string:
   ```xml
   Data Source=localhost;Initial Catalog=ShopDongHo;User ID=sa;Password=yourpassword;MultipleActiveResultSets=True;TrustServerCertificate=True
   ```

### Lỗi: "Could not find a part of the path"
**Nguyên nhân:** Migrations folder chưa được include vào project

**Giải pháp:**
1. Solution Explorer → Show All Files
2. Click phải Migrations folder → Include In Project

### Lỗi: "Unable to generate an explicit migration"
**Nguyên nhân:** Có thay đổi model nhưng chưa tạo migration

**Giải pháp:**
```powershell
Add-Migration <TênMigration>
Update-Database
```

### Database đã tồn tại, muốn tạo lại
**Cách 1: Xóa database trong SSMS**
1. SSMS → Click phải database **ShopDongHo** → Delete
2. Chạy lại: `Update-Database`

**Cách 2: Xóa trong Package Manager Console**
```powershell
Drop-Database
Update-Database
```

### Muốn reset data seed
1. Xóa tất cả data trong bảng
2. Xóa bảng `__MigrationHistory`
3. Chạy: `Update-Database -Force`

---

## 📊 DATA MẪU ĐÃ SEED

### Categories (4)
1. Đồng hồ nam
2. Đồng hồ nữ
3. Đồng hồ thể thao
4. Đồng hồ thông minh

### Products (12)
- **Đồng hồ nam:** Casio MTP-VD01D, Citizen NH8350, Seiko 5 SNKL23, Orient FAC00009N0
- **Đồng hồ nữ:** Casio LTP-V005D, Citizen EW2470, Seiko SUR634P1
- **Đồng hồ thể thao:** G-Shock GA-2100, G-Shock DW-5600E, Citizen Eco-Drive BN0150
- **Smartwatch:** Apple Watch Series 9, Samsung Galaxy Watch 6

### Users (2)
```
Admin Account:
- Username: admin
- Password: admin123
- Email: admin@shopdongho.com
- Role: Admin

Customer Account:
- Username: customer
- Password: customer123
- Email: customer@example.com
- Role: Customer
```

---

## 🔄 QUY TRÌNH THAY ĐỔI MODEL

Khi thay đổi Entity (thêm/sửa/xóa property):

1. **Sửa Model** (ví dụ: thêm field mới vào Product.cs)
   ```csharp
   public string SerialNumber { get; set; }
   ```

2. **Tạo Migration**
   ```powershell
   Add-Migration AddSerialNumberToProduct
   ```

3. **Xem code migration** (optional) - kiểm tra trong file migration vừa tạo

4. **Cập nhật Database**
   ```powershell
   Update-Database
   ```

5. **Kiểm tra** trong SSMS → Table có thêm column mới

---

## ⚠️ LƯU Ý QUAN TRỌNG

1. **KHÔNG XÓA** folder `Migrations/` sau khi đã chạy migrations
2. **KHÔNG SỬA** các file migration đã chạy rồi
3. Mỗi lần thay đổi model → Tạo migration mới
4. Test trên development database trước khi deploy production
5. Backup database trước khi chạy migration phức tạp
6. Commit migrations vào Git để team sync được

---

## ✅ CHECKLIST SAU KHI SETUP

- [ ] Connection string đã đúng trong Web.config
- [ ] SQL Server đang chạy
- [ ] Package Manager Console mở được
- [ ] `Enable-Migrations` chạy thành công (hoặc đã có Migrations/Configuration.cs)
- [ ] `Add-Migration InitialCreate` tạo file migration
- [ ] `Update-Database` chạy không lỗi
- [ ] Database **ShopDongHo** đã tồn tại trong SSMS
- [ ] Tất cả bảng đã được tạo (5 bảng + __MigrationHistory)
- [ ] Data mẫu đã được insert (12 products, 4 categories, 2 users)
- [ ] Chạy app (F5) không bị lỗi database connection

---

## 📞 HỖ TRỢ

Nếu gặp lỗi khác, cung cấp:
1. Error message đầy đủ
2. Connection string đang dùng
3. SQL Server version (SSMS → Help → About)
4. Screenshot Package Manager Console

Happy Coding! 🚀
