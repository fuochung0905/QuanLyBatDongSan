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

    public virtual DbSet<LOPHOC> LOPHOCs { get; set; }

    public virtual DbSet<PHANQUYEN> PHANQUYENs { get; set; }

    public virtual DbSet<PHANQUYEN_NHOMQUYEN> PHANQUYEN_NHOMQUYENs { get; set; }

    public virtual DbSet<SYS_MENU> SYS_MENUs { get; set; }

    public virtual DbSet<TAIKHOAN> TAIKHOANs { get; set; }

    public virtual DbSet<VAITRO> VAITROs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LOPHOC>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LOPHOC__3214EC0796E2FDA2");

            entity.ToTable("LOPHOC");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayXoa).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua).HasMaxLength(100);
            entity.Property(e => e.NguoiTao).HasMaxLength(100);
            entity.Property(e => e.NguoiXoa).HasMaxLength(100);
            entity.Property(e => e.TenGiaoVien).HasMaxLength(100);
            entity.Property(e => e.TenLop).HasMaxLength(100);
        });

        modelBuilder.Entity<PHANQUYEN>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PHANQUYE__3214EC07BCC829C6");

            entity.ToTable("PHANQUYEN");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ControllerName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PHANQUYEN_NHOMQUYEN>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PHANQUYE__3214EC07677B1ACA");

            entity.ToTable("PHANQUYEN_NHOMQUYEN");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Icon)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenGoi).HasMaxLength(500);
        });

        modelBuilder.Entity<SYS_MENU>(entity =>
        {
            entity.HasKey(e => e.ControllerName).HasName("PK__SYS_MENU__B652C7A3D2A267B7");

            entity.ToTable("SYS_MENU");

            entity.Property(e => e.ControllerName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Controller)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TenGoi).HasMaxLength(500);
        });

        modelBuilder.Entity<TAIKHOAN>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TAIKHOAN__3214EC075AD19BB7");

            entity.ToTable("TAIKHOAN");

            entity.Property(e => e.Id).ValueGeneratedNever();
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

        modelBuilder.Entity<VAITRO>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VAITRO__3214EC07E8507F7F");

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
