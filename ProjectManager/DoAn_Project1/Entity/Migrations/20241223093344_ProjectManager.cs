using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entity.Migrations
{
    /// <inheritdoc />
    public partial class ProjectManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LoaiTaiKhoanId",
                table: "TAIKHOAN",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LopId",
                table: "TAIKHOAN",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsThem",
                table: "PHANQUYEN",
                type: "bit",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VAITRO",
                table: "VAITRO",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CHUYENTRANGTHAI",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrangThaiNguonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrangThaiDichId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenTrangThai = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
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
                name: "GIAIDOAN",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ma = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenGoi = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguoiSua = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguoiXoa = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActived = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GIAIDOAN", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KHOA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenGoi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TenVietTat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                    table.PrimaryKey("PK_KHOA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LOAITAIKHOAN",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenGoi = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguoiSua = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguoiXoa = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActived = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Ma = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOAITAIKHOAN", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LOP",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenGoi = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TenVietTat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguoiSua = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguoiXoa = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActived = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOP", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QL_DUAN",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenVietTat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenGoi = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    KhoaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LopId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_QL_DUAN", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QLCONGVIEC",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenCongViec = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TrangThaiCongViec = table.Column<int>(type: "int", nullable: false),
                    NguoiThucHien = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NguoiKiemTra = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AssignTo = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ExpectedStartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpectedEndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpectedTime = table.Column<int>(type: "int", nullable: true),
                    ActualStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ActualEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ActualTime = table.Column<int>(type: "int", nullable: true),
                    KetQuaCongViec = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    HuongDanNhanh = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    NguoiTao = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguoiSua = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguoiXoa = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActived = table.Column<bool>(type: "bit", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QLCONGVIEC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TRANGTHAICONGVIEC",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenGoi = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
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
                    table.PrimaryKey("PK_TRANGTHAICONGVIEC", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CHUYENTRANGTHAI");

            migrationBuilder.DropTable(
                name: "GIAIDOAN");

            migrationBuilder.DropTable(
                name: "KHOA");

            migrationBuilder.DropTable(
                name: "LOAITAIKHOAN");

            migrationBuilder.DropTable(
                name: "LOP");

            migrationBuilder.DropTable(
                name: "QL_DUAN");

            migrationBuilder.DropTable(
                name: "QLCONGVIEC");

            migrationBuilder.DropTable(
                name: "TRANGTHAICONGVIEC");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VAITRO",
                table: "VAITRO");

            migrationBuilder.DropColumn(
                name: "LoaiTaiKhoanId",
                table: "TAIKHOAN");

            migrationBuilder.DropColumn(
                name: "LopId",
                table: "TAIKHOAN");

            migrationBuilder.DropColumn(
                name: "IsThem",
                table: "PHANQUYEN");
        }
    }
}
