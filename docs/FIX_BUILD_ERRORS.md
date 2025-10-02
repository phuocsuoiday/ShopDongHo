# FIX BUILD ERRORS - CHECKLIST

## Bước 1: Xem lỗi cụ thể

1. **View** → **Error List** (hoặc Ctrl+\, E)
2. Screenshot hoặc copy toàn bộ errors
3. Gửi cho tôi để fix

---

## Bước 2: Build và xem lỗi

1. **Build** → **Build Solution** (Ctrl+Shift+B)
2. Xem **Error List** tab ở dưới
3. Double-click vào error để jump đến file lỗi

---

## Các lỗi thường gặp và cách fix:

### ❌ Lỗi 1: "The type or namespace name 'xxx' could not be found"

**Nguyên nhân:** Files chưa được include vào project

**Fix:**
1. Solution Explorer → Click **Show All Files** (icon folder với dấu chấm)
2. Click phải các folders/files màu xám → **Include In Project**
3. Build lại

---

### ❌ Lỗi 2: "Could not load file or assembly 'EntityFramework'"

**Nguyên nhân:** Package chưa được cài hoặc restore

**Fix:**
```
Tools → NuGet Package Manager → Manage NuGet Packages for Solution
→ Tab "Installed" → Restore nếu có warning
```

Hoặc Package Manager Console:
```powershell
Update-Package -reinstall
```

---

### ❌ Lỗi 3: "The name 'BCrypt' does not exist in the current context"

**Nguyên nhân:** Package BCrypt.Net-Next chưa cài

**Fix:**
```powershell
Install-Package BCrypt.Net-Next -Version 4.0.3
```

---

### ❌ Lỗi 4: Files Migrations/Configuration.cs lỗi

**Nguyên nhân:** File chưa include hoặc namespace sai

**Fix:**
1. Include file Migrations/Configuration.cs vào project
2. Build lại

---

### ❌ Lỗi 5: "Could not load file Microsoft.AspNetCore.Http"

**Nguyên nhân:** AdminProductViewModel dùng sai namespace

**Fix:** Đã sửa rồi, nhưng nếu vẫn lỗi:
- Xóa dòng `using Microsoft.AspNetCore.Http;` trong AdminProductViewModel.cs
- Thay bằng `using System.Web;`

---

## Quick Fix Commands:

### 1. Clean và Rebuild
```
Build → Clean Solution
Build → Rebuild Solution
```

### 2. Restore Packages
```powershell
# Package Manager Console
Update-Package -reinstall
```

### 3. Delete bin/obj folders
1. Đóng Visual Studio
2. Xóa thư mục:
   - `ShopDongHo/bin`
   - `ShopDongHo/obj`
3. Mở lại VS
4. Rebuild

---

## Checklist Include Files:

### Controllers ✅
- [ ] AccountController.cs
- [ ] AdminController.cs
- [ ] CartController.cs
- [ ] OrderController.cs
- [ ] ProductController.cs

### Models/Entities ✅
- [ ] Category.cs
- [ ] Product.cs
- [ ] User.cs
- [ ] Order.cs
- [ ] OrderDetail.cs
- [ ] ShopDbContext.cs
- [ ] ShopDbInitializer.cs (optional, có thể xóa)

### Models/ViewModels ✅
- [ ] ProductViewModel.cs
- [ ] CartItemViewModel.cs
- [ ] CheckoutViewModel.cs
- [ ] LoginViewModel.cs
- [ ] RegisterViewModel.cs
- [ ] AdminProductViewModel.cs

### Models/Repositories ✅
- [ ] IRepository.cs
- [ ] Repository.cs
- [ ] ProductRepository.cs
- [ ] CategoryRepository.cs
- [ ] OrderRepository.cs
- [ ] UserRepository.cs

### Helpers ✅
- [ ] AuthHelper.cs
- [ ] SessionHelper.cs
- [ ] ImageHelper.cs

### Filters ✅
- [ ] AuthorizeRoleAttribute.cs
- [ ] AdminAuthorizeAttribute.cs

### Migrations ✅
- [ ] Configuration.cs

---

## NẾU VẪN LỖI:

Gửi cho tôi:
1. Screenshot Error List (toàn bộ errors)
2. Hoặc copy text errors
3. Tôi sẽ fix cụ thể

---

## Command kiểm tra nhanh:

```powershell
# Xem packages đã cài
Get-Package

# Phải thấy:
# - EntityFramework 6.4.4
# - BCrypt.Net-Next 4.0.3
# - PagedList.Mvc 4.5.0
```
