# FOUNDATION PLAN - SHOP ĐỒNG HỒ

## 1. TỔNG QUAN DỰ ÁN

### Công nghệ sử dụng
- **Framework**: ASP.NET MVC 5.2.9
- **Platform**: .NET Framework 4.7.2
- **Frontend**: Bootstrap 5 + jQuery 3.7.0
- **Database**: SQL Server LocalDB / SQL Server Express
- **ORM**: Entity Framework 6.x

### Mục tiêu
Xây dựng website bán đồng hồ đơn giản, đầy đủ tính năng cơ bản cho sinh viên:
- Quản lý sản phẩm đồng hồ
- Giỏ hàng và đặt hàng
- Quản lý tài khoản khách hàng
- Trang quản trị admin

---

## 2. CẤU TRÚC DATABASE

### 2.1. Bảng Categories (Danh mục)
```sql
Id (int, PK, Identity)
Name (nvarchar(100), NOT NULL)
Description (nvarchar(500))
CreatedDate (datetime)
```

### 2.2. Bảng Products (Sản phẩm)
```sql
Id (int, PK, Identity)
Name (nvarchar(200), NOT NULL)
CategoryId (int, FK -> Categories)
Brand (nvarchar(100))
Price (decimal(18,2), NOT NULL)
OriginalPrice (decimal(18,2))
Image (nvarchar(255))
Stock (int, DEFAULT 0)
Description (nvarchar(max))
IsActive (bit, DEFAULT 1)
CreatedDate (datetime)
```

### 2.3. Bảng Users (Người dùng)
```sql
Id (int, PK, Identity)
Username (nvarchar(50), UNIQUE, NOT NULL)
PasswordHash (nvarchar(255), NOT NULL)
Email (nvarchar(100), UNIQUE, NOT NULL)
FullName (nvarchar(100))
Phone (nvarchar(20))
Address (nvarchar(255))
Role (nvarchar(20), DEFAULT 'Customer')
IsActive (bit, DEFAULT 1)
CreatedDate (datetime)
```

### 2.4. Bảng Orders (Đơn hàng)
```sql
Id (int, PK, Identity)
UserId (int, FK -> Users, NULL)
CustomerName (nvarchar(100), NOT NULL)
CustomerEmail (nvarchar(100))
CustomerPhone (nvarchar(20), NOT NULL)
CustomerAddress (nvarchar(255), NOT NULL)
OrderDate (datetime, NOT NULL)
Status (nvarchar(50), DEFAULT 'Pending')
TotalAmount (decimal(18,2), NOT NULL)
Note (nvarchar(500))
```

### 2.5. Bảng OrderDetails (Chi tiết đơn hàng)
```sql
Id (int, PK, Identity)
OrderId (int, FK -> Orders)
ProductId (int, FK -> Products)
ProductName (nvarchar(200))
Quantity (int, NOT NULL)
Price (decimal(18,2), NOT NULL)
```

---

## 3. CẤU TRÚC MODELS

### 3.1. Models/Entities/
```
Category.cs
Product.cs
User.cs
Order.cs
OrderDetail.cs
ShopDbContext.cs
```

### 3.2. Models/ViewModels/
```
ProductViewModel.cs
ProductDetailViewModel.cs
CartItemViewModel.cs
CheckoutViewModel.cs
OrderViewModel.cs
LoginViewModel.cs
RegisterViewModel.cs
AdminProductViewModel.cs
```

### 3.3. Models/Repositories/
```
IRepository.cs
Repository.cs
ProductRepository.cs
CategoryRepository.cs
OrderRepository.cs
UserRepository.cs
```

---

## 4. CẤU TRÚC CONTROLLERS

### 4.1. HomeController
- `Index()`: Trang chủ - hiển thị sản phẩm nổi bật
- `About()`: Giới thiệu shop
- `Contact()`: Liên hệ

### 4.2. ProductController
- `Index(categoryId, brand, minPrice, maxPrice, page)`: Danh sách sản phẩm có lọc
- `Detail(id)`: Chi tiết sản phẩm
- `Search(keyword)`: Tìm kiếm sản phẩm

### 4.3. CartController
- `Index()`: Xem giỏ hàng
- `AddToCart(productId, quantity)`: Thêm vào giỏ
- `UpdateCart(productId, quantity)`: Cập nhật số lượng
- `RemoveFromCart(productId)`: Xóa khỏi giỏ
- `Checkout()`: Trang thanh toán

### 4.4. OrderController
- `Create(CheckoutViewModel)`: Tạo đơn hàng
- `Confirm(orderId)`: Xác nhận đơn hàng
- `History()`: Lịch sử đơn hàng (cần login)
- `Detail(id)`: Chi tiết đơn hàng

### 4.5. AccountController
- `Login()`: Đăng nhập
- `Register()`: Đăng ký
- `Logout()`: Đăng xuất
- `Profile()`: Thông tin cá nhân
- `UpdateProfile()`: Cập nhật thông tin

### 4.6. AdminController (Authorize Role = Admin)
- `Dashboard()`: Tổng quan
- `Products()`: Danh sách sản phẩm
- `CreateProduct()`: Thêm sản phẩm
- `EditProduct(id)`: Sửa sản phẩm
- `DeleteProduct(id)`: Xóa sản phẩm
- `Categories()`: Quản lý danh mục
- `Orders()`: Quản lý đơn hàng
- `UpdateOrderStatus(orderId, status)`: Cập nhật trạng thái đơn

---

## 5. CẤU TRÚC VIEWS

### 5.1. Views/Home/
- `Index.cshtml`: Trang chủ
- `About.cshtml`: Giới thiệu
- `Contact.cshtml`: Liên hệ

### 5.2. Views/Product/
- `Index.cshtml`: Danh sách sản phẩm + filter sidebar
- `Detail.cshtml`: Chi tiết sản phẩm
- `Search.cshtml`: Kết quả tìm kiếm

### 5.3. Views/Cart/
- `Index.cshtml`: Giỏ hàng
- `Checkout.cshtml`: Thanh toán

### 5.4. Views/Order/
- `Confirm.cshtml`: Xác nhận đơn hàng
- `History.cshtml`: Lịch sử đơn hàng
- `Detail.cshtml`: Chi tiết đơn hàng

### 5.5. Views/Account/
- `Login.cshtml`: Đăng nhập
- `Register.cshtml`: Đăng ký
- `Profile.cshtml`: Thông tin cá nhân

### 5.6. Views/Admin/
- `Dashboard.cshtml`: Tổng quan
- `Products.cshtml`: Danh sách sản phẩm
- `ProductForm.cshtml`: Form thêm/sửa sản phẩm
- `Categories.cshtml`: Quản lý danh mục
- `Orders.cshtml`: Quản lý đơn hàng
- `OrderDetail.cshtml`: Chi tiết đơn hàng

### 5.7. Views/Shared/
- `_Layout.cshtml`: Layout chính
- `_AdminLayout.cshtml`: Layout admin
- `_ProductCard.cshtml`: Partial view card sản phẩm
- `_Pagination.cshtml`: Phân trang
- `_CartSummary.cshtml`: Tóm tắt giỏ hàng
- `Error.cshtml`: Trang lỗi

---

## 6. TÍNH NĂNG CHÍNH

### 6.1. Khách hàng
- ✅ Xem danh sách đồng hồ theo danh mục
- ✅ Lọc theo thương hiệu, khoảng giá
- ✅ Tìm kiếm sản phẩm
- ✅ Xem chi tiết sản phẩm
- ✅ Thêm vào giỏ hàng
- ✅ Cập nhật giỏ hàng (số lượng, xóa)
- ✅ Đặt hàng (không cần đăng nhập)
- ✅ Đăng ký tài khoản
- ✅ Đăng nhập và xem lịch sử đơn hàng

### 6.2. Admin
- ✅ Quản lý sản phẩm (CRUD)
- ✅ Quản lý danh mục (CRUD)
- ✅ Quản lý đơn hàng
- ✅ Cập nhật trạng thái đơn hàng
- ✅ Xem thống kê tổng quan

---

## 7. PACKAGES CẦN CÀI ĐẶT

```
Install-Package EntityFramework -Version 6.4.4
Install-Package BCrypt.Net-Next -Version 4.0.3
Install-Package PagedList.Mvc -Version 4.5.0
```

---

## 8. CẤU TRÚC THỦ MỤC BỔ SUNG

```
ShopDongHo/
├── Content/
│   ├── css/
│   ├── images/
│   │   ├── products/      (hình ảnh sản phẩm)
│   │   └── banners/       (banner trang chủ)
├── Helpers/
│   ├── AuthHelper.cs      (xử lý authentication)
│   ├── SessionHelper.cs   (quản lý session giỏ hàng)
│   └── ImageHelper.cs     (upload/xử lý hình ảnh)
├── Filters/
│   ├── AuthorizeRoleAttribute.cs
│   └── AdminAuthorizeAttribute.cs
└── App_Data/
    └── (database files)
```

---

## 9. SESSION & AUTHENTICATION

### 9.1. Shopping Cart Session
```csharp
Session["Cart"] = List<CartItemViewModel>
{
    ProductId,
    ProductName,
    Image,
    Price,
    Quantity
}
```

### 9.2. User Authentication
```csharp
Session["UserId"] = user.Id
Session["Username"] = user.Username
Session["UserRole"] = user.Role
```

---

## 10. TRẠNG THÁI ĐơN HÀNG

```
- Pending: Chờ xác nhận
- Confirmed: Đã xác nhận
- Shipping: Đang giao hàng
- Completed: Hoàn thành
- Cancelled: Đã hủy
```

---

## 11. FLOW CHÍNH

### 11.1. Flow mua hàng
1. Khách xem sản phẩm → Thêm vào giỏ
2. Xem giỏ hàng → Cập nhật số lượng
3. Checkout → Điền thông tin
4. Xác nhận đơn hàng → Lưu DB + Clear Cart
5. Hiển thị thông báo thành công

### 11.2. Flow quản lý (Admin)
1. Đăng nhập admin
2. Dashboard → Xem tổng quan
3. Quản lý sản phẩm/danh mục/đơn hàng
4. CRUD operations
5. Cập nhật trạng thái đơn hàng

---

## 12. VALIDATION RULES

### Products
- Name: Required, MaxLength(200)
- Price: Required, Range(0, 999999999)
- Stock: Required, Range(0, 99999)

### Users
- Username: Required, MinLength(3), MaxLength(50)
- Password: Required, MinLength(6)
- Email: Required, EmailAddress
- Phone: Required, Phone format

### Orders
- CustomerName: Required, MaxLength(100)
- CustomerPhone: Required
- CustomerAddress: Required, MaxLength(255)

---

## 13. RESPONSIVE DESIGN

- Mobile: < 768px (1 column)
- Tablet: 768px - 991px (2 columns)
- Desktop: ≥ 992px (3-4 columns)

Bootstrap Grid System đảm bảo responsive tự động

---

## 14. SECURITY

- Password hashing: BCrypt
- SQL Injection: Entity Framework (parameterized queries)
- XSS: Razor auto-encode output
- CSRF: AntiForgeryToken cho POST forms
- Authorization: Custom AuthorizeAttribute filter

---

## 15. LỘ TRÌNH TRIỂN KHAI

### Phase 1: Foundation (Tuần 1)
1. Cài đặt Entity Framework
2. Tạo Models/Entities
3. Tạo DbContext + Migrations
4. Seed data mẫu

### Phase 2: Product Module (Tuần 2)
1. ProductController + Views
2. Hiển thị danh sách sản phẩm
3. Chi tiết sản phẩm
4. Tìm kiếm & lọc

### Phase 3: Cart & Order (Tuần 3)
1. CartController + Session
2. Checkout flow
3. OrderController
4. Xác nhận đơn hàng

### Phase 4: User Management (Tuần 4)
1. AccountController
2. Login/Register
3. Profile management
4. Order history

### Phase 5: Admin Panel (Tuần 5)
1. AdminController + Authorization
2. Product CRUD
3. Category CRUD
4. Order management
5. Dashboard

### Phase 6: Polish & Testing (Tuần 6)
1. UI/UX improvements
2. Validation & error handling
3. Testing các chức năng
4. Deploy (nếu cần)

---

## 16. DATA SEED MẪU

### Categories
- Đồng hồ nam
- Đồng hồ nữ
- Đồng hồ thể thao
- Đồng hồ thông minh

### Brands
- Casio
- Citizen
- Seiko
- Orient
- Tissot

### Sample Products (10-15 sản phẩm)

### Admin Account
- Username: admin
- Password: admin123
- Role: Admin

---

## KẾT LUẬN

Foundation plan này cung cấp:
- ✅ Cấu trúc rõ ràng, dễ hiểu
- ✅ Đầy đủ tính năng cơ bản
- ✅ Phù hợp với project sinh viên
- ✅ Dễ mở rộng sau này
- ✅ Tuân thủ best practices ASP.NET MVC

Chỉ cần follow plan từng bước, project sẽ hoàn thành đúng deadline và chất lượng tốt.
