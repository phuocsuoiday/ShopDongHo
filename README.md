# 🕐 Shop Đồng Hồ - ASP.NET MVC

Hệ thống website bán đồng hồ trực tuyến sử dụng ASP.NET MVC 5, Entity Framework 6.

## 📋 Tính năng

### Khách hàng
- ✅ Xem danh sách sản phẩm với filter (danh mục, thương hiệu, giá)
- ✅ Tìm kiếm sản phẩm
- ✅ Xem chi tiết sản phẩm
- ✅ Giỏ hàng (thêm, sửa, xóa)
- ✅ Đặt hàng (có/không cần đăng nhập)
- ✅ Đăng ký/Đăng nhập
- ✅ Xem lịch sử đơn hàng

### Admin
- ✅ Dashboard tổng quan
- ✅ Quản lý sản phẩm (CRUD)
- ✅ Quản lý danh mục (CRUD)
- ✅ Quản lý đơn hàng
- ✅ Cập nhật trạng thái đơn hàng
- ✅ Xem danh sách khách hàng

## 🛠️ Công nghệ

- **Framework:** ASP.NET MVC 5.2.9
- **Platform:** .NET Framework 4.7.2
- **Database:** SQL Server
- **ORM:** Entity Framework 6.4.4
- **Frontend:** Bootstrap 5, jQuery 3.7.0
- **Authentication:** Session-based với BCrypt password hashing

## 📦 Cài đặt

### 1. Clone repository
```bash
git clone https://github.com/phuocsuoiday/ShopDongHo.git
cd ShopDongHo
```

### 2. Restore NuGet Packages
```
Tools → NuGet Package Manager → Restore Packages
```

Hoặc Package Manager Console:
```powershell
Update-Package -reinstall
```

### 3. Cấu hình Database

Mở `Web.config`, cập nhật connection string:
```xml
<connectionStrings>
  <add name="ShopDongHoConnection"
       connectionString="Data Source=localhost;Initial Catalog=ShopDongHo;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### 4. Chạy Migrations

Package Manager Console:
```powershell
Enable-Migrations
Add-Migration InitialCreate
Update-Database
```

### 5. Chạy Application
```
Press F5 hoặc click Start
```

## 🔑 Test Accounts

```
Admin:
- Username: admin
- Password: admin123

Customer:
- Username: customer
- Password: customer123
```

## 📊 Database Schema

- **Categories** - Danh mục sản phẩm
- **Products** - Sản phẩm đồng hồ
- **Users** - Người dùng
- **Orders** - Đơn hàng
- **OrderDetails** - Chi tiết đơn hàng

## 📁 Cấu trúc Project

```
ShopDongHo/
├── Controllers/         # MVC Controllers
├── Models/
│   ├── Entities/       # Database entities
│   ├── ViewModels/     # View models
│   └── Repositories/   # Repository pattern
├── Views/              # Razor views
├── Helpers/            # Helper classes
├── Filters/            # Authorization filters
├── Content/            # CSS, images
├── Scripts/            # JavaScript files
└── docs/               # Documentation
```

## 📚 Documentation

Chi tiết xem trong folder `docs/`:
- `FOUNDATION_PLAN.md` - Kiến trúc tổng thể
- `IMPLEMENTATION_STATUS.md` - Trạng thái triển khai
- `DATABASE_MIGRATION_GUIDE.md` - Hướng dẫn database
- `QUICK_START.md` - Hướng dẫn nhanh

## 🚀 Deployment

### SQL Server
1. Tạo database `ShopDongHo`
2. Chạy migrations hoặc script SQL trong `docs/CREATE_DATABASE_MANUAL.sql`
3. Cập nhật connection string trong `Web.config`

### IIS
1. Publish project (Build → Publish)
2. Tạo Application Pool (.NET Framework v4.0)
3. Deploy files đến IIS
4. Cấu hình connection string

## 🤝 Contributing

1. Fork repository
2. Tạo branch mới (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. Open Pull Request

## 📝 License

This project is licensed under the MIT License.

## 👤 Author

**Phước**
- GitHub: [@phuocsuoiday](https://github.com/phuocsuoiday)

## 🙏 Acknowledgments

- Bootstrap 5
- Entity Framework
- BCrypt.Net
- jQuery
