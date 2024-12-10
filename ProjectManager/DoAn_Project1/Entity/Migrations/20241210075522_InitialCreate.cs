using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entity.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PHANQUYEN",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VaiTroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ControllerName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    IsXem = table.Column<bool>(type: "bit", nullable: false),
                    IsCapNhat = table.Column<bool>(type: "bit", nullable: false),
                    IsXoa = table.Column<bool>(type: "bit", nullable: false),
                    IsDuyet = table.Column<bool>(type: "bit", nullable: false),
                    IsThongKe = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "PHANQUYEN_NHOMQUYEN",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenGoi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    Icon = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsActived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SYS_MENU",
                columns: table => new
                {
                    ControllerName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Controller = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Action = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TenGoi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NhomQuyenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: true),
                    CoXem = table.Column<bool>(type: "bit", nullable: false),
                    CoThem = table.Column<bool>(type: "bit", nullable: false),
                    CoCapNhat = table.Column<bool>(type: "bit", nullable: false),
                    CoXoa = table.Column<bool>(type: "bit", nullable: false),
                    CoDuyet = table.Column<bool>(type: "bit", nullable: false),
                    CoThongKe = table.Column<bool>(type: "bit", nullable: false),
                    IsActived = table.Column<bool>(type: "bit", nullable: false),
                    IsShowMenu = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TAIKHOAN",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TinhId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HuyenId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    XaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VaiTroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DonViId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PhongBanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NguoiQuanLyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: true),
                    HoLot = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    GioiTinh = table.Column<int>(type: "int", nullable: false),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MatKhauSalt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguoiSua = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguoiXoa = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActived = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "VAITRO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenGoi = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguoSua = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguoiXoa = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActived = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PHANQUYEN");

            migrationBuilder.DropTable(
                name: "PHANQUYEN_NHOMQUYEN");

            migrationBuilder.DropTable(
                name: "SYS_MENU");

            migrationBuilder.DropTable(
                name: "TAIKHOAN");

            migrationBuilder.DropTable(
                name: "VAITRO");
        }
    }
}
