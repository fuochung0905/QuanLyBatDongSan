using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entity.DBContent;

public partial class DoAnProject1Context : DbContext
{
    public DoAnProject1Context(DbContextOptions<DoAnProject1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<CHUYENTRANGTHAI> CHUYENTRANGTHAIs { get; set; }

    public virtual DbSet<KHOA> KHOAs { get; set; }

    public virtual DbSet<LOAITAIKHOAN> LOAITAIKHOANs { get; set; }

    public virtual DbSet<LOP> LOPs { get; set; }

    public virtual DbSet<PHANQUYEN> PHANQUYENs { get; set; }

    public virtual DbSet<PHANQUYEN_NHOMQUYEN> PHANQUYEN_NHOMQUYENs { get; set; }

    public virtual DbSet<QLCONGVIEC> QLCONGVIECs { get; set; }

    public virtual DbSet<QL_DUAN> QL_DUANs { get; set; }

    public virtual DbSet<SYS_MENU> SYS_MENUs { get; set; }

    public virtual DbSet<TAIKHOAN> TAIKHOANs { get; set; }

    public virtual DbSet<TRANGTHAICONGVIEC> TRANGTHAICONGVIECs { get; set; }

    public virtual DbSet<VAITRO> VAITROs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CHUYENTRANGTHAI>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CHUYENTRANGTHAI");

            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayXoa).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua).HasMaxLength(256);
            entity.Property(e => e.NguoiTao).HasMaxLength(256);
            entity.Property(e => e.NguoiXoa).HasMaxLength(256);
            entity.Property(e => e.TenTrangThai).HasMaxLength(256);
        });

        modelBuilder.Entity<KHOA>(entity =>
        {
            entity.ToTable("KHOA");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayXoa).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua).HasMaxLength(256);
            entity.Property(e => e.NguoiTao).HasMaxLength(256);
            entity.Property(e => e.NguoiXoa).HasMaxLength(256);
            entity.Property(e => e.TenGoi).HasMaxLength(200);
            entity.Property(e => e.TenVietTat).HasMaxLength(50);
        });

        modelBuilder.Entity<LOAITAIKHOAN>(entity =>
        {
            entity.ToTable("LOAITAIKHOAN");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Ma).HasMaxLength(50);
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayXoa).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua).HasMaxLength(256);
            entity.Property(e => e.NguoiTao).HasMaxLength(256);
            entity.Property(e => e.NguoiXoa).HasMaxLength(256);
            entity.Property(e => e.TenGoi).HasMaxLength(256);
        });

        modelBuilder.Entity<LOP>(entity =>
        {
            entity.ToTable("LOP");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayXoa).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua).HasMaxLength(256);
            entity.Property(e => e.NguoiTao).HasMaxLength(256);
            entity.Property(e => e.NguoiXoa).HasMaxLength(256);
            entity.Property(e => e.TenGoi).HasMaxLength(256);
            entity.Property(e => e.TenVietTat).HasMaxLength(50);
        });

        modelBuilder.Entity<PHANQUYEN>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PHANQUYEN");

            entity.Property(e => e.ControllerName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PHANQUYEN_NHOMQUYEN>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PHANQUYEN_NHOMQUYEN");

            entity.Property(e => e.Icon)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenGoi).HasMaxLength(500);
        });

        modelBuilder.Entity<QLCONGVIEC>(entity =>
        {
            entity.ToTable("QLCONGVIEC");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ActualEndDate).HasColumnType("datetime");
            entity.Property(e => e.ActualStartDate).HasColumnType("datetime");
            entity.Property(e => e.AssignTo).HasMaxLength(256);
            entity.Property(e => e.ExpectedEndDate).HasColumnType("datetime");
            entity.Property(e => e.ExpectedStartDate).HasColumnType("datetime");
            entity.Property(e => e.GhiChu).HasMaxLength(1000);
            entity.Property(e => e.HuongDanNhanh).HasMaxLength(1000);
            entity.Property(e => e.KetQuaCongViec).HasMaxLength(1000);
            entity.Property(e => e.MoTa).HasMaxLength(1000);
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayXoa).HasColumnType("datetime");
            entity.Property(e => e.NguoiKiemTra).HasMaxLength(256);
            entity.Property(e => e.NguoiSua).HasMaxLength(256);
            entity.Property(e => e.NguoiTao).HasMaxLength(256);
            entity.Property(e => e.NguoiThucHien).HasMaxLength(256);
            entity.Property(e => e.NguoiXoa).HasMaxLength(256);
            entity.Property(e => e.TenCongViec).HasMaxLength(500);
        });

        modelBuilder.Entity<QL_DUAN>(entity =>
        {
            entity.ToTable("QL_DUAN");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayXoa).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua).HasMaxLength(256);
            entity.Property(e => e.NguoiTao).HasMaxLength(256);
            entity.Property(e => e.NguoiXoa).HasMaxLength(256);
            entity.Property(e => e.TenGoi).HasMaxLength(256);
            entity.Property(e => e.TenVietTat).HasMaxLength(50);
        });

        modelBuilder.Entity<SYS_MENU>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SYS_MENU");

            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Controller)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ControllerName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenGoi).HasMaxLength(500);
        });

        modelBuilder.Entity<TAIKHOAN>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TAIKHOAN");

            entity.Property(e => e.AnhDaiDien).HasMaxLength(500);
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.HoLot).HasMaxLength(200);
            entity.Property(e => e.MatKhau).HasMaxLength(500);
            entity.Property(e => e.MatKhauSalt).HasMaxLength(100);
            entity.Property(e => e.NgaySinh).HasColumnType("datetime");
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayXoa).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua).HasMaxLength(256);
            entity.Property(e => e.NguoiTao).HasMaxLength(256);
            entity.Property(e => e.NguoiXoa).HasMaxLength(256);
            entity.Property(e => e.SoDienThoai).HasMaxLength(12);
            entity.Property(e => e.Ten).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<TRANGTHAICONGVIEC>(entity =>
        {
            entity.ToTable("TRANGTHAICONGVIEC");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayXoa).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua).HasMaxLength(256);
            entity.Property(e => e.NguoiTao).HasMaxLength(256);
            entity.Property(e => e.NguoiXoa).HasMaxLength(256);
            entity.Property(e => e.TenGoi).HasMaxLength(256);
        });

        modelBuilder.Entity<VAITRO>(entity =>
        {
            entity.ToTable("VAITRO");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayXoa).HasColumnType("datetime");
            entity.Property(e => e.NguoSua).HasMaxLength(256);
            entity.Property(e => e.NguoiTao).HasMaxLength(256);
            entity.Property(e => e.NguoiXoa).HasMaxLength(256);
            entity.Property(e => e.TenGoi).HasMaxLength(256);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
