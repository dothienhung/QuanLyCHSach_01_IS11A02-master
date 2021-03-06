CREATE DATABASE QuanLyCuaHangSach;
CREATE TABLE Admin
(
	User_name nvarchar (50) not null PRIMARY KEY,
	Pass_word nvarchar (50) not null
);
insert into admin (User_name, Pass_word)
values ('ND01', '12345678');

CREATE TABLE LinhVuc
(
   MaLinhVuc nvarchar (10) not null PRIMARY KEY,
   TenLinhVuc nvarchar (50) not null
);
CREATE TABLE NgonNgu
(
   MaNgonNgu nvarchar (10) not null PRIMARY KEY,
   TenNgonNgu nvarchar (50) not null
);
CREATE TABLE CongViec
(
   MaCongViec nvarchar (10) not null PRIMARY KEY,
   TenCongViec nvarchar (50) not null
);
CREATE TABLE NhaCungCap
(
   MaNhaCC nvarchar (10) not null PRIMARY KEY,
   TenNhaCC nvarchar (50) not null,
   DiaChi nvarchar (50), 
   DienThoai varchar (10)
);
CREATE TABLE KhachHang
(
   MaKhach nvarchar (10) not null PRIMARY KEY,
   TenKhach nvarchar (50),
   DiaChi nvarchar (50),
   DienThoai varchar (10)
);
CREATE TABLE NhaXuatBan
(
   MaNXB nvarchar (10) not null PRIMARY KEY,
   TenNXB nvarchar (50) not null,
   DiaChi nvarchar (50),
   DienThoai varchar (10)
);
CREATE TABLE TacGia
(
   MaTG nvarchar (10) not null PRIMARY KEY,
   TenTG nvarchar (50) not null,
   NgaySinh datetime,
   GioiTinh nvarchar (10),
   DiaChi nvarchar (50)
);
CREATE TABLE LoaiSach
(
   MaLoaiSach nvarchar (10) not null PRIMARY KEY,
   TenLoaiSach nvarchar (100) not null
);
CREATE TABLE NhanVien
(
   MaNV nvarchar (10) not null PRIMARY KEY,
   TenNV nvarchar (50),
   DienThoai varchar (10),
   DiaChi nvarchar (50),
   MaCongViec nvarchar (10),
   FOREIGN KEY (MaCongViec) REFERENCES CongViec (MaCongViec)
);
CREATE TABLE HoaDonNhap
(
   SoHDN nvarchar (20) not null PRIMARY KEY,
   NgayNhap datetime,
   MaNhaCC nvarchar (10),
   MaNV nvarchar (10),
   TongTien float,
   FOREIGN KEY (MaNhaCC) REFERENCES NhaCungCap (MaNhaCC),
   FOREIGN KEY (MaNV) REFERENCES NhanVien (MaNV)
);
CREATE TABLE HoaDonBan
(
   SoHDB nvarchar (50) not null PRIMARY KEY,
   MaNV nvarchar (10) not null,
   NgayBan datetime,
   MaKhach nvarchar (10),
   TongTien float,
   FOREIGN KEY (MaNV) REFERENCES NhanVien (MaNV),
   FOREIGN KEY (MaKhach) REFERENCES KhachHang (MaKhach)
);
CREATE TABLE KhoSach
(
   MaSach nvarchar (10) not null PRIMARY KEY,
   TenSach nvarchar (100) not null,
   SoLuong int,
   DonGiaNhap float,
   DonGiaBan float,
   MaLoaiSach nvarchar (10) not null,
   MaTG nvarchar (10) not null,
   MaNXB nvarchar (10) not null,
   MaLinhVuc nvarchar (10) not null,
   MaNgonNgu nvarchar (10) not null,
   Anh nvarchar (200),
   SoTrang int,
   CONSTRAINT check_don_gia_ban CHECK (DonGiaBan >= DonGiaNhap),
   FOREIGN KEY (MaLoaiSach) REFERENCES LoaiSach (MaLoaiSach),
   FOREIGN KEY (MaTG) REFERENCES TacGia (MaTG),
   FOREIGN KEY (MaNXB) REFERENCES NhaXuatBan (MaNXB),
   FOREIGN KEY (MaLinhVuc) REFERENCES LinhVuc (MaLinhVuc),
   FOREIGN KEY (MaNgonNgu) REFERENCES NgonNgu (MaNgonNgu)
);
CREATE TABLE ChiTietHDN
(
   SoHDN nvarchar (20) not null PRIMARY KEY,
   MaSach nvarchar (10) not null,
   SoLuongNhap int,
   DonGiaNhap float,
   KhuyenMai float,
   ThanhTien float,
   CONSTRAINT FK_MasachHDN FOREIGN KEY (MaSach) REFERENCES KhoSach (MaSach),
   CONSTRAINT fk_chitietHDN FOREIGN KEY (SoHDN) REFERENCES HoaDonNhap (SoHDN)
);

CREATE TABLE ChiTietHDB
(
   SoHDB nvarchar (50) not null PRIMARY KEY,
   MaSach nvarchar (10) not null,
   SoLuong int,
   SoLuongBan int,
   KhuyenMai float,
   ThanhTien float,
   CONSTRAINT fk_MaSach FOREIGN KEY (MaSach) REFERENCES KhoSach (MaSach),
   CONSTRAINT check_so_luong_ban CHECK (SoLuongBan <= SoLuong),
   CONSTRAINT fk_chitietHDB FOREIGN KEY (SoHDB) REFERENCES HoaDonBan (SoHDB)
);
drop table MatSach
CREATE TABLE MatSach
(
   MaLanMat nvarchar (10) not null PRIMARY KEY,
   MaSach nvarchar (10) not null,
   SoLuong int,
   NgayMat datetime,
   SoLuongMat int,
   FOREIGN KEY (MaSach) REFERENCES KhoSach (MaSach)
);   
INSERT LinhVuc
VALUES ('LV01', N'Trinh thám');
INSERT LinhVuc
VALUES ('LV02', N'Truyện teen');
INSERT LinhVuc
VALUES ('LV03', N'Truyện cổ tích');
INSERT LinhVuc
VALUES ('LV04', N'Ngôn tình');
INSERT LinhVuc
VALUES ('LV05', N'Lịch sử');

INSERT INTO NgonNgu
VALUES ('NN01', N'Tiếng Việt');
INSERT INTO NgonNgu
VALUES ('NN02', N'Tiếng Trung');
INSERT INTO NgonNgu
VALUES ('NN03', N'Tiếng Nga');
INSERT INTO NgonNgu
VALUES ('NN04', N'Tiếng Nhật');
INSERT INTO NgonNgu
VALUES ('NN05', N'Tiếng Anh');

INSERT INTO CongViec
VALUES ('MCV01', N'Bán hàng');
INSERT INTO CongViec
VALUES ('MCV02', N'Thu ngân');
INSERT INTO CongViec
VALUES ('MCV03', N'Kiểm hàng');
INSERT INTO CongViec
VALUES ('MCV04', N'Quản lý');
INSERT INTO CongViec
VALUES ('MCV05', N'Kế toán');

INSERT INTO NhaCungCap
VALUES ('NCC01', N'Phúc Minh', N'Cầu Giấy', '0986367237');
INSERT INTO NhaCungCap
VALUES ('NCC02', N'Cổ Nguyệt', N'Đống Đa', '0626896858');
INSERT INTO NhaCungCap
VALUES ('NCC03', N'Asbooks', N'Cầu Giấy', '0985865747');
INSERT INTO NhaCungCap
VALUES ('NCC04', N'Nhã Nam', N'Ba Đình', '0923676548');
INSERT INTO NhaCungCap
VALUES ('NCC05', N'Quảng Văn', N'Thanh Xuân', '0986357961');

INSERT INTO KhachHang
VALUES ('MK01', N'Nguyễn Thị Oanh', N'Kim Mã', '0973138713');
INSERT INTO KhachHang
VALUES ('MK02', N'Nguyễn Trà My', N'Đống Đa', '0986623138');
INSERT INTO KhachHang
VALUES ('MK03', N'Tạ Hữu Dương', N'Thanh Xuân', '0923138138');
INSERT INTO KhachHang
VALUES ('MK04', N'Nguyễn Văn Thắng', N'Hà Đông', '0813817389');
INSERT INTO KhachHang
VALUES ('MK05', N'Nguyễn Thị Thương', N'Phương Mai', '0987868313');

INSERT INTO NhaXuatBan
VALUES ('NXB01', N'NXB Trẻ', N'Đống Đa', '0987868317');
INSERT INTO NhaXuatBan
VALUES ('NXB02', N'NXB Tuổi Trẻ', N'Thanh Xuân', '0898828313');
INSERT INTO NhaXuatBan
VALUES ('NXB03', N'NXB Thanh Niên', N'Hà Đông', '0173918313');
INSERT INTO NhaXuatBan
VALUES ('NXB04', N'NXB Học Trò', N'Thanh Xuân', '0183018210');
INSERT INTO NhaXuatBan
VALUES ('NXB05', N'NXB Thiếu Niên', N'Cầu Giấy', '0183011312');

INSERT INTO TacGia
VALUES ('TG01', N'Nguyễn Nhật Ánh', '1980-08-07', N'Nam', N'Cầu Giấy');
INSERT INTO TacGia
VALUES ('TG02', N'Nguyễn Minh Hà', '1989-09-12', N'Nữ', N'Biên Giang');
INSERT INTO TacGia
VALUES ('TG03', N'Phí Minh Hiếu', '1967-11-12', N'Nam', N'Khánh Hòa');
INSERT INTO TacGia
VALUES ('TG04', N'Lê Oanh', '1989-11-11', N'Nữ', N'Hà Nội');

INSERT INTO LoaiSach
VALUES ('LS01', N'Thiếu nhi');
INSERT INTO LoaiSach
VALUES ('LS02', N'Truyện');
INSERT INTO LoaiSach
VALUES ('LS03', N'Sách tham khảo');
INSERT INTO LoaiSach
VALUES ('LS04', N'Sách giáo khoa');

INSERT INTO NhanVien
VALUES ('NV01', N'Nguyễn Tru Trà', '0131803108', N'Hà Đông', 'MCV02');
INSERT INTO NhanVien
VALUES ('NV02', N'Phạm Diệu Thúy', '0917310210', N'Thanh Xuân', 'MCV01');
INSERT INTO NhanVien
VALUES ('NV03', N'Trần Huy Hoàng', '0183012910', N'Đống Đa', 'MCV03');
INSERT INTO NhanVien
VALUES ('NV04', N'Lê Thanh Bình', '0138191131', N'Phương Mai', 'MCV04');

INSERT INTO KhoSach
VALUES ('MS01', N'Tôi thấy hoa vàng trên cỏ xanh', 12, 40000, 44000, 'LS02', 'TG01', 'NXB01', 'LV02', 'NN01', 'jgwwj.jpg', 300);
INSERT INTO KhoSach
VALUES ('MS02', N'Mưu sát', 24, 50000, 55000, 'LS02', 'TG03', 'NXB02', 'LV01', 'NN01', 'jshiwkr.jpg', 650);
INSERT INTO KhoSach
VALUES ('MS03', N'Harry Potter', 23, 100000, 110000, 'LS02', 'TG04', 'NXB02', 'LV02', 'NN05', 'jsgfi232.png', 700);

INSERT INTO MatSach
VALUES ('LM01', 'MS01', 12, '2018-09-11', 1);
INSERT INTO MatSach
VALUES ('LM02', 'MS02', 24, '2019-12-11', 2);
INSERT INTO MatSach
VALUES ('LM03', 'MS03', 23, '2020-06-06', 3);





