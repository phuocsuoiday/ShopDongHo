# HƯỚNG DẪN ADD FILES VÀO PROJECT

## Files đã tạo nhưng chưa có trong Solution Explorer

### CÁCH 1: Add từng file thủ công (Khuyến nghị)

1. **Mở Visual Studio**
2. **Trong Solution Explorer**, click chuột phải vào folder tương ứng
3. Chọn **Add → Existing Item...**
4. Browse đến folder và chọn file cần add
5. Click **Add**

---

## DANH SÁCH FILES CẦN ADD

### 📂 Controllers (5 files)
```
ShopDongHo/Controllers/
├── AccountController.cs      ✅
├── AdminController.cs         ✅
├── CartController.cs          ✅
├── OrderController.cs         ✅
└── ProductController.cs       ✅
```

**Cách add:**
1. Click phải vào folder **Controllers** trong Solution Explorer
2. Add → Existing Item
3. Chọn: AccountController.cs, AdminController.cs, CartController.cs, OrderController.cs, ProductController.cs
4. Click Add

---

### 📂 Models/Entities (6 files)
```
ShopDongHo/Models/Entities/
├── Category.cs              ✅
├── Order.cs                 ✅
├── OrderDetail.cs           ✅
├── Product.cs               ✅
├── ShopDbContext.cs         ✅
├── ShopDbInitializer.cs     ✅
└── User.cs                  ✅
```

**Cách add:**
1. Click phải vào folder **Models** → New Folder → Đặt tên "Entities"
2. Click phải vào folder **Entities** → Add → Existing Item
3. Browse đến `ShopDongHo\Models\Entities\`
4. Chọn tất cả 7 files .cs
5. Click Add

---

### 📂 Models/ViewModels (6 files)
```
ShopDongHo/Models/ViewModels/
├── AdminProductViewModel.cs   ✅
├── CartItemViewModel.cs       ✅
├── CheckoutViewModel.cs       ✅
├── LoginViewModel.cs          ✅
├── ProductViewModel.cs        ✅
└── RegisterViewModel.cs       ✅
```

**Cách add:**
1. Click phải vào folder **Models** → New Folder → "ViewModels"
2. Click phải vào **ViewModels** → Add → Existing Item
3. Chọn tất cả 6 files
4. Click Add

---

### 📂 Models/Repositories (6 files)
```
ShopDongHo/Models/Repositories/
├── CategoryRepository.cs      ✅
├── IRepository.cs             ✅
├── OrderRepository.cs         ✅
├── ProductRepository.cs       ✅
├── Repository.cs              ✅
└── UserRepository.cs          ✅
```

**Cách add:**
1. Click phải vào **Models** → New Folder → "Repositories"
2. Add → Existing Item → Chọn 6 files
3. Click Add

---

### 📂 Helpers (3 files)
```
ShopDongHo/Helpers/
├── AuthHelper.cs              ✅
├── ImageHelper.cs             ✅
└── SessionHelper.cs           ✅
```

**Cách add:**
1. Click phải vào project root **ShopDongHo** → New Folder → "Helpers"
2. Click phải vào **Helpers** → Add → Existing Item
3. Chọn 3 files
4. Click Add

---

### 📂 Filters (2 files)
```
ShopDongHo/Filters/
├── AdminAuthorizeAttribute.cs  ✅
└── AuthorizeRoleAttribute.cs   ✅
```

**Cách add:**
1. Click phải vào project root → New Folder → "Filters"
2. Add → Existing Item → Chọn 2 files
3. Click Add

---

### 📂 Views/Product (2 files)
```
ShopDongHo/Views/Product/
├── Index.cshtml               ✅
└── Detail.cshtml              ✅
```

**Cách add:**
1. Click phải vào **Views** → New Folder → "Product"
2. Add → Existing Item → Chọn 2 files

---

### 📂 Views/Cart (2 files)
```
ShopDongHo/Views/Cart/
├── Index.cshtml               ✅
└── Checkout.cshtml            ✅
```

**Cách add:**
1. Click phải vào **Views** → New Folder → "Cart"
2. Add → Existing Item → Chọn 2 files

---

### 📂 Views/Account (2 files)
```
ShopDongHo/Views/Account/
├── Login.cshtml               ✅
└── Register.cshtml            ✅
```

**Cách add:**
1. Click phải vào **Views** → New Folder → "Account"
2. Add → Existing Item → Chọn 2 files

---

### 📂 Views/Order (1 file)
```
ShopDongHo/Views/Order/
└── Confirm.cshtml             ✅
```

**Cách add:**
1. Click phải vào **Views** → New Folder → "Order"
2. Add → Existing Item → Chọn file

---

### 📂 Views/Shared (3 files partial views)
```
ShopDongHo/Views/Shared/
├── _ProductCard.cshtml        ✅
├── _Pagination.cshtml         ✅
└── _CartSummary.cshtml        ✅
```

**Cách add:**
1. Click phải vào **Views/Shared** (đã có sẵn)
2. Add → Existing Item → Chọn 3 files

---

## CÁCH 2: Reload Project (Đơn giản hơn)

1. **Đóng Visual Studio**
2. **Xóa folders** `.vs`, `bin`, `obj` trong thư mục solution
3. **Mở lại** file `.sln`
4. Visual Studio có thể tự detect và hỏi có muốn include files mới không
5. Click **Show All Files** ở đầu Solution Explorer
6. Click phải vào từng file/folder màu xám → **Include In Project**

---

## CÁCH 3: Unload/Reload Project

1. Trong Solution Explorer, click phải vào project **ShopDongHo**
2. Chọn **Unload Project**
3. Click phải lại → Chọn **Reload Project**
4. Click **Show All Files** button (icon ở trên Solution Explorer)
5. Các files/folders mới sẽ hiện màu xám/dotted
6. Click phải từng item → **Include In Project**

---

## SAU KHI ADD XONG

1. **Build Solution** (Ctrl+Shift+B)
2. **Kiểm tra lỗi** trong Error List
3. **Chạy app** (F5)

---

## LƯU Ý QUAN TRỌNG

- Files **phải được include** vào project thì mới compile được
- **Show All Files** button giúp xem files chưa được include (màu xám)
- Nếu file màu trắng = đã include, màu xám = chưa include
- Có thể select multiple files (Ctrl+Click) rồi Include In Project cùng lúc

---

## NẾU GẶP LỖI COMPILE

### Lỗi: "The type or namespace name 'xxx' could not be found"
→ File chưa được include vào project
→ Hoặc thiếu using statement

### Lỗi: "Entity Framework provider not found"
→ Restore NuGet packages
→ Tools → NuGet Package Manager → Restore

### Lỗi: "Could not load file or assembly 'EntityFramework'"
→ Rebuild solution
→ Clean Solution → Rebuild

---

## KIỂM TRA

Sau khi add xong, Solution Explorer phải có cấu trúc:

```
ShopDongHo
├── App_Start/
├── Content/
├── Controllers/
│   ├── HomeController.cs
│   ├── ProductController.cs
│   ├── CartController.cs
│   ├── AccountController.cs
│   ├── OrderController.cs
│   └── AdminController.cs
├── Filters/
│   ├── AdminAuthorizeAttribute.cs
│   └── AuthorizeRoleAttribute.cs
├── Helpers/
│   ├── AuthHelper.cs
│   ├── ImageHelper.cs
│   └── SessionHelper.cs
├── Models/
│   ├── Entities/
│   │   ├── Category.cs
│   │   ├── Product.cs
│   │   ├── User.cs
│   │   ├── Order.cs
│   │   ├── OrderDetail.cs
│   │   ├── ShopDbContext.cs
│   │   └── ShopDbInitializer.cs
│   ├── ViewModels/
│   │   ├── ProductViewModel.cs
│   │   ├── CartItemViewModel.cs
│   │   ├── CheckoutViewModel.cs
│   │   ├── LoginViewModel.cs
│   │   ├── RegisterViewModel.cs
│   │   └── AdminProductViewModel.cs
│   └── Repositories/
│       ├── IRepository.cs
│       ├── Repository.cs
│       ├── ProductRepository.cs
│       ├── CategoryRepository.cs
│       ├── OrderRepository.cs
│       └── UserRepository.cs
├── Scripts/
├── Views/
│   ├── Account/
│   │   ├── Login.cshtml
│   │   └── Register.cshtml
│   ├── Cart/
│   │   ├── Index.cshtml
│   │   └── Checkout.cshtml
│   ├── Home/
│   │   ├── Index.cshtml
│   │   ├── About.cshtml
│   │   └── Contact.cshtml
│   ├── Order/
│   │   └── Confirm.cshtml
│   ├── Product/
│   │   ├── Index.cshtml
│   │   └── Detail.cshtml
│   └── Shared/
│       ├── _Layout.cshtml
│       ├── _AdminLayout.cshtml
│       ├── _ProductCard.cshtml
│       ├── _Pagination.cshtml
│       ├── _CartSummary.cshtml
│       └── Error.cshtml
├── Global.asax
└── Web.config
```

Sau đó **Build** (Ctrl+Shift+B) để kiểm tra! ✅
