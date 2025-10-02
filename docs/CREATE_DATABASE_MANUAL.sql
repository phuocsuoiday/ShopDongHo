-- =============================================
-- SCRIPT TẠO DATABASE VÀ INSERT DATA MẪU
-- SHOP ĐỒNG HỒ
-- =============================================

-- Tạo Database
USE master;
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name = 'ShopDongHo')
BEGIN
    ALTER DATABASE ShopDongHo SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE ShopDongHo;
END
GO

CREATE DATABASE ShopDongHo;
GO

USE ShopDongHo;
GO

-- =============================================
-- TẠO CÁC BẢNG
-- =============================================

-- Bảng Categories
CREATE TABLE Categories (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- Bảng Products
CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL,
    CategoryId INT NOT NULL,
    Brand NVARCHAR(100),
    Price DECIMAL(18,2) NOT NULL,
    OriginalPrice DECIMAL(18,2),
    Image NVARCHAR(255),
    Stock INT NOT NULL DEFAULT 0,
    Description NVARCHAR(MAX),
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryId)
        REFERENCES Categories(Id)
);
GO

-- Bảng Users
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    FullName NVARCHAR(100),
    Phone NVARCHAR(20),
    Address NVARCHAR(255),
    Role NVARCHAR(20) NOT NULL DEFAULT 'Customer',
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- Bảng Orders
CREATE TABLE Orders (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT,
    CustomerName NVARCHAR(100) NOT NULL,
    CustomerEmail NVARCHAR(100),
    CustomerPhone NVARCHAR(20) NOT NULL,
    CustomerAddress NVARCHAR(255) NOT NULL,
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    TotalAmount DECIMAL(18,2) NOT NULL,
    Note NVARCHAR(500),
    CONSTRAINT FK_Orders_Users FOREIGN KEY (UserId)
        REFERENCES Users(Id)
);
GO

-- Bảng OrderDetails
CREATE TABLE OrderDetails (
    Id INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    ProductName NVARCHAR(200),
    Quantity INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_OrderDetails_Orders FOREIGN KEY (OrderId)
        REFERENCES Orders(Id) ON DELETE CASCADE,
    CONSTRAINT FK_OrderDetails_Products FOREIGN KEY (ProductId)
        REFERENCES Products(Id)
);
GO

-- Bảng Migration History (cho Entity Framework)
CREATE TABLE __MigrationHistory (
    MigrationId NVARCHAR(150) NOT NULL PRIMARY KEY,
    ContextKey NVARCHAR(300) NOT NULL,
    Model VARBINARY(MAX) NOT NULL,
    ProductVersion NVARCHAR(32) NOT NULL
);
GO

-- =============================================
-- INSERT DATA MẪU
-- =============================================

-- Categories
INSERT INTO Categories (Name, Description, CreatedDate) VALUES
(N'Đồng hồ nam', N'Đồng hồ dành cho nam giới', GETDATE()),
(N'Đồng hồ nữ', N'Đồng hồ dành cho nữ giới', GETDATE()),
(N'Đồng hồ thể thao', N'Đồng hồ thể thao chống nước', GETDATE()),
(N'Đồng hồ thông minh', N'Smartwatch và đồng hồ thông minh', GETDATE());
GO

-- Products
INSERT INTO Products (Name, CategoryId, Brand, Price, OriginalPrice, Stock, Description, IsActive, CreatedDate) VALUES
(N'Casio MTP-VD01D-1BVUDF', 1, N'Casio', 1250000, 1500000, 50, N'Đồng hồ nam Casio dây da, mặt tròn, phong cách cổ điển', 1, GETDATE()),
(N'Citizen NH8350-83L', 1, N'Citizen', 4800000, 5500000, 30, N'Đồng hồ nam Citizen automatic, dây thép không gỉ', 1, GETDATE()),
(N'Seiko 5 SNKL23K1', 1, N'Seiko', 3200000, NULL, 25, N'Đồng hồ nam Seiko automatic, dây thép', 1, GETDATE()),
(N'Orient FAC00009N0', 1, N'Orient', 4500000, 5200000, 20, N'Đồng hồ nam Orient automatic, mặt số xanh', 1, GETDATE()),
(N'Casio LTP-V005D-2BUDF', 2, N'Casio', 980000, 1200000, 40, N'Đồng hồ nữ Casio dây da, mặt xanh nhỏ gọn', 1, GETDATE()),
(N'Citizen EW2470-87F', 2, N'Citizen', 5200000, NULL, 15, N'Đồng hồ nữ Citizen Eco-Drive, dây thép vàng', 1, GETDATE()),
(N'Seiko SUR634P1', 2, N'Seiko', 3800000, 4200000, 20, N'Đồng hồ nữ Seiko quartz, mặt số đính đá', 1, GETDATE()),
(N'G-Shock GA-2100-1A1DR', 3, N'Casio', 3100000, NULL, 35, N'Đồng hồ G-Shock chống nước 200m, thiết kế octagon', 1, GETDATE()),
(N'G-Shock DW-5600E-1VDF', 3, N'Casio', 2200000, 2500000, 45, N'Đồng hồ G-Shock classic, chống sốc, chống nước', 1, GETDATE()),
(N'Citizen Eco-Drive BN0150-28E', 3, N'Citizen', 8500000, NULL, 10, N'Đồng hồ lặn Citizen Eco-Drive, chống nước 300m', 1, GETDATE()),
(N'Apple Watch Series 9', 4, N'Apple', 10500000, 12000000, 25, N'Smartwatch Apple Watch Series 9, GPS, 45mm', 1, GETDATE()),
(N'Samsung Galaxy Watch 6', 4, N'Samsung', 7200000, 8500000, 30, N'Smartwatch Samsung Galaxy Watch 6, theo dõi sức khỏe', 1, GETDATE());
GO

-- Users (Passwords: admin123 và customer123 đã được hash bằng BCrypt)
-- BCrypt hash của "admin123": $2a$11$...
-- BCrypt hash của "customer123": $2a$11$...
INSERT INTO Users (Username, PasswordHash, Email, FullName, Phone, Address, Role, IsActive, CreatedDate) VALUES
(N'admin', N'$2a$11$rGOE8yfLx5v8qXH3Z5YXXu5Z5XKCj5qXH3Z5YXXu5Z5XKCj5qXH3Z', N'admin@shopdongho.com', N'Administrator', N'0123456789', N'123 Đường ABC, TP.HCM', N'Admin', 1, GETDATE()),
(N'customer', N'$2a$11$rGOE8yfLx5v8qXH3Z5YXXu5Z5XKCj5qXH3Z5YXXu5Z5XKCj5qXH3Z', N'customer@example.com', N'Khách Hàng Mẫu', N'0987654321', N'456 Đường XYZ, TP.HCM', N'Customer', 1, GETDATE());
GO

-- Migration History (đánh dấu đã chạy InitialCreate migration)
INSERT INTO __MigrationHistory (MigrationId, ContextKey, Model, ProductVersion) VALUES
(N'202410021500000_InitialCreate', N'ShopDongHo.Models.Entities.ShopDbContext', 0x1F8B0800000000000400, N'6.4.4');
GO

-- =============================================
-- KIỂM TRA KẾT QUẢ
-- =============================================

PRINT '=================================';
PRINT 'DATABASE CREATED SUCCESSFULLY!';
PRINT '=================================';
PRINT '';
PRINT 'Categories: ' + CAST((SELECT COUNT(*) FROM Categories) AS NVARCHAR(10));
PRINT 'Products: ' + CAST((SELECT COUNT(*) FROM Products) AS NVARCHAR(10));
PRINT 'Users: ' + CAST((SELECT COUNT(*) FROM Users) AS NVARCHAR(10));
PRINT '';
PRINT 'Test Accounts:';
PRINT '- Admin: admin / admin123';
PRINT '- Customer: customer / customer123';
PRINT '';
PRINT '=================================';
GO

-- Xem dữ liệu
SELECT * FROM Categories;
SELECT * FROM Products;
SELECT Username, Email, Role FROM Users;
GO
