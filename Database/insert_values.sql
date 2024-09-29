INSERT INTO PHANQUYEN(Id, VaiTroId, ControllerName, IsXem, IsCapNhat, IsXoa, IsDuyet, IsThongKe )
VALUES ('C8106755-6A52-432C-9324-05077F3FCF70','F8ED5B61-4E04-4AE5-B84D-B21C42D4F7B3', 'vaitrocontroller', 1, 1, 1, 1, 1);

INSERT INTO PHANQUYEN_NHOMQUYEN(Id, TenGoi, Sort, Icon, IsActived)
VALUES ('A6700DBB-A254-491D-839F-415F5D7D04B8', 'Hệ thống', 999, '<i class="fas fa-cogs nav-icon"></i>', 1); 

INSERT INTO SYS_MENU(ControllerName, Controller, Action, TenGoi, NhomQuyenId, Sort, CoXem, CoThem, CoCapNhat, CoXoa, CoDuyet, CoThongKe, IsActived, IsShowMenu)
VALUES ('vaitrocontroller', 'vaitro', 'index', 'Vai trò', 'A6700DBB-A254-491D-839F-415F5D7D04B8', 1, 1, 1, 1, 1, 1, 1, 1, 1);

INSERT INTO TAIKHOAN(Id, UserName, TinhId, VaiTroId, DonViId, PhongBanId, SoDienThoai, Email, HoLot, Ten, NgaySinh, GioiTinh, MatKhau, MatKhauSalt, NgayTao, NguoiTao, IsActived, IsDeleted)
VALUES ('2BF29773-6531-45FA-9E90-E8438AFEF7A7', 
N'admin', 
'836F499B-7CC1-45F8-AADC-87428155297B', 
'F8ED5B61-4E04-4AE5-B84D-B21C42D4F7B3', 
'06F58867-F9A1-4F3D-84A9-7C78352ED824', 
'F57439EA-AA13-4B8A-A306-7624C8D7E888', 
N'0399333643', 
'phuochungnguyen.work@gmail.com', 
N'Nguyễn Phước', 
N'Hùng', 
'2012-06-18 10:34:09', 1, 
N'k+sPGkzOIWE0v5YOjVrXhQ==', 
N'PtbCx2KbE96fB2otlnTCeQ==', 
'2022-12-16 08:41:59.580', 
N'GOD', 
1,
0);

INSERT INTO VAITRO(Id, TenGoi, NgayTao, NguoiTao, IsActived, IsDeleted)
VALUES ('F8ED5B61-4E04-4AE5-B84D-B21C42D4F7B3', N'Quản trị', '2022-06-22 12:14:29', N'GOD', 1, 0);