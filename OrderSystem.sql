IF EXISTS (SELECT * FROM sys.databases WHERE name = 'Don_hang')
BEGIN
    DROP DATABASE Don_hang;
END;
GO

-- Tạo mới database
CREATE DATABASE Don_hang;
GO

USE Don_hang;
GO

-- Bảng Products
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY,
    TenSanPham NVARCHAR(100) NOT NULL, -- Tên sản phẩm
    Gia DECIMAL(18, 2) NOT NULL CHECK (Gia >= 0), -- Giá sản phẩm không âm
    SoLuongTon INT NOT NULL CHECK (SoLuongTon >= 0) -- Số lượng tồn không âm
);

-- Bảng Orders
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY,
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(), -- Ngày tạo
    TrangThai NVARCHAR(50) NOT NULL,             -- Trạng thái đơn hàng
    TongTien DECIMAL(18, 2) NOT NULL DEFAULT 0 CHECK (TongTien >= 0) -- Tổng tiền không âm
);

-- Bảng OrderItems
CREATE TABLE OrderItems (
    OrderItemID INT PRIMARY KEY IDENTITY,
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    SoLuong INT NOT NULL CHECK (SoLuong > 0),  -- Số lượng sản phẩm lớn hơn 0
    Gia DECIMAL(18, 2) NOT NULL CHECK (Gia >= 0), -- Giá không âm
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- Thêm trigger tự động cập nhật TongTien trong Orders khi có thay đổi ở OrderItems
/*    CREATE TRIGGER trg_UpdateTongTien
    ON OrderItems
    AFTER INSERT, UPDATE, DELETE
    AS
    BEGIN
        UPDATE Orders
        SET TongTien = (
            SELECT SUM(SoLuong * Gia)
            FROM OrderItems
            WHERE OrderItems.OrderID = Orders.OrderID
        )
        WHERE Orders.OrderID IN (
            SELECT DISTINCT OrderID FROM Inserted
            UNION
            SELECT DISTINCT OrderID FROM Deleted
        );
    END;
    GO
*/

-- Thêm dữ liệu mẫu cho bảng Products
INSERT INTO Products (TenSanPham, Gia, SoLuongTon) VALUES
('Áo Thun', 150000, 100),
('Quần Jean', 250000, 50),
('Giày Sneaker', 600000, 30),
('Ba Lô', 350000, 20),
('Mũ Lưỡi Trai', 120000, 80);

-- Thêm dữ liệu mẫu cho bảng Orders
INSERT INTO Orders (NgayTao, TrangThai) VALUES
(GETDATE(), 'Đang xử lý'),
(GETDATE(), 'Hoàn thành'),
(GETDATE(), 'Đã hủy'),
(GETDATE(), 'Đang vận chuyển');

-- Thêm dữ liệu mẫu cho bảng OrderItems
INSERT INTO OrderItems (OrderID, ProductID, SoLuong, Gia) VALUES
(1, 1, 2, 150000),  -- Đơn hàng 1: 2 áo thun
(1, 3, 1, 600000),  -- Đơn hàng 1: 1 giày sneaker
(2, 2, 1, 250000),  -- Đơn hàng 2: 1 quần jean
(2, 4, 1, 350000),  -- Đơn hàng 2: 1 ba lô
(3, 1, 1, 150000),  -- Đơn hàng 3: 1 áo thun
(4, 5, 2, 120000);  -- Đơn hàng 4: 2 mũ lưỡi trai

-- Cập nhật TongTien cho các đơn hàng hiện có (nếu chưa có trigger)
UPDATE Orders
SET TongTien = (
    SELECT SUM(SoLuong * Gia)
    FROM OrderItems
    WHERE OrderItems.OrderID = Orders.OrderID
);
GO
