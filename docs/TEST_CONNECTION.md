# TEST DATABASE CONNECTION

## 🔍 3 CÁCH TEST CONNECTION

### CÁCH 1: Dùng SQL Script (Nhanh nhất)

1. **Mở SSMS** → Kết nối localhost
2. **File → Open:** `TestConnection.sql` (trong thư mục root)
3. **Execute** (F5)

**Kết quả mong đợi:**
```
=================================
DATABASE STATISTICS
=================================
Categories: 4
Products: 12
Users: 2
Orders: 0
OrderDetails: 0
=================================
```

---

### CÁCH 2: Dùng Test Page (Recommended)

1. **Copy file** `TestDbConnection.aspx` vào folder `ShopDongHo/`
2. **Chạy app** (F5)
3. **Truy cập:** `https://localhost:44376/TestDbConnection.aspx`

**Kết quả:**
- ✅ Green boxes = Success
- ❌ Red boxes = Error
- Hiển thị danh sách Categories, Products, Users

---

### CÁCH 3: Trong Code (HomeController)

**File:** `Controllers/HomeController.cs`

Thêm code vào action `Index()`:

```csharp
public ActionResult Index()
{
    try
    {
        using (var db = new ShopDbContext())
        {
            ViewBag.CategoryCount = db.Categories.Count();
            ViewBag.ProductCount = db.Products.Count();
            ViewBag.UserCount = db.Users.Count();
            ViewBag.ConnectionStatus = "✅ Connected";
        }
    }
    catch (Exception ex)
    {
        ViewBag.ConnectionStatus = "❌ Error: " + ex.Message;
    }

    return View();
}
```

**File:** `Views/Home/Index.cshtml`

Thêm vào đầu trang:

```html
@if (ViewBag.ConnectionStatus != null)
{
    <div class="alert alert-info">
        <h4>Database Connection Test:</h4>
        <p>@ViewBag.ConnectionStatus</p>
        @if (ViewBag.CategoryCount != null)
        {
            <ul>
                <li>Categories: @ViewBag.CategoryCount</li>
                <li>Products: @ViewBag.ProductCount</li>
                <li>Users: @ViewBag.UserCount</li>
            </ul>
        }
    </div>
}
```

**Chạy app (F5)** → Xem trang chủ

---

## 🐛 TROUBLESHOOTING

### Lỗi: "A network-related or instance-specific error"

**Check 1: SQL Server đang chạy?**
```
Windows + R → services.msc
Tìm "SQL Server" → Phải có Status = Running
```

**Check 2: Connection String đúng chưa?**

Mở `Web.config`, kiểm tra:
```xml
<connectionStrings>
  <add name="ShopDongHoConnection"
       connectionString="Data Source=localhost;Initial Catalog=ShopDongHo;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

Thử đổi `localhost` thành:
- `.\SQLEXPRESS` (nếu dùng SQL Express)
- `(local)`
- `.` (dấu chấm)

**Check 3: Database tồn tại?**

Trong SSMS:
```sql
SELECT name FROM sys.databases WHERE name = 'ShopDongHo';
```

Phải trả về 1 row.

---

### Lỗi: "Login failed for user"

**Solution 1: Dùng SQL Authentication**

Đổi connection string:
```xml
connectionString="Data Source=localhost;Initial Catalog=ShopDongHo;User ID=sa;Password=yourpassword;MultipleActiveResultSets=True;TrustServerCertificate=True"
```

**Solution 2: Grant quyền cho Windows User**

SSMS → Security → Logins → [Your User] → Properties → Server Roles → Check "sysadmin"

---

### Lỗi: "Could not load file or assembly 'EntityFramework'"

**Solution:**
```
Tools → NuGet Package Manager → Package Manager Console

Update-Package EntityFramework -reinstall
```

---

## ✅ CHECKLIST THÀNH CÔNG

Khi test connection thành công, bạn sẽ thấy:

- [ ] Categories: 4 records
- [ ] Products: 12 records
- [ ] Users: 2 records (admin, customer)
- [ ] Orders: 0 records (chưa có ai đặt hàng)
- [ ] OrderDetails: 0 records

Sample data:
- [ ] Đồng hồ nam, nữ, thể thao, thông minh (4 categories)
- [ ] 12 sản phẩm từ Casio, Citizen, Seiko, Orient, Apple, Samsung
- [ ] 2 users: admin/admin123, customer/customer123

---

## 🎯 SAU KHI TEST THÀNH CÔNG

1. **Xóa file test:** `TestDbConnection.aspx` (nếu đã test xong)
2. **Chạy app:** https://localhost:44376/
3. **Test các trang:**
   - Trang chủ: /
   - Sản phẩm: /Product
   - Đăng nhập: /Account/Login (admin/admin123)

4. **Nếu trang Product hiển thị 12 sản phẩm** → ✅ SUCCESS!

---

## 📊 EXPECTED RESULTS

### SQL Script Output:
```
TABLE_NAME               TABLE_TYPE
------------------------ ----------
Categories              BASE TABLE
OrderDetails            BASE TABLE
Orders                  BASE TABLE
Products                BASE TABLE
Users                   BASE TABLE
__MigrationHistory      BASE TABLE

DATABASE STATISTICS
===================
Categories: 4
Products: 12
Users: 2
Orders: 0
OrderDetails: 0
```

### Test Page Output:
- ✅ SQL Connection: SUCCESS
- ✅ Entity Framework Connection: SUCCESS
- 📊 Categories: 4 | Products: 12 | Users: 2 | Orders: 0
- Table với danh sách Categories (4 rows)
- Table với danh sách Products (12 rows)
- Table với danh sách Users (2 rows)

### Code Test Output:
```
Database Connection Test:
✅ Connected
• Categories: 4
• Products: 12
• Users: 2
```

---

Chọn 1 trong 3 cách test, gửi kết quả cho tôi! 🚀
