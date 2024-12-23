using System;
using System.Collections.Generic;

namespace Entity.DBContent;

public partial class QLCONGVIEC
{
    public Guid Id { get; set; }

    public string TenCongViec { get; set; } = null!;

    public int TrangThaiCongViec { get; set; }

    public string NguoiThucHien { get; set; } = null!;

    public string NguoiKiemTra { get; set; } = null!;

    public string? AssignTo { get; set; }

    public string? MoTa { get; set; }

    public string? GhiChu { get; set; }

    public DateTime ExpectedStartDate { get; set; }

    public DateTime ExpectedEndDate { get; set; }

    public int? ExpectedTime { get; set; }

    public DateTime? ActualStartDate { get; set; }

    public DateTime? ActualEndDate { get; set; }

    public int? ActualTime { get; set; }

    public string? KetQuaCongViec { get; set; }

    public string? HuongDanNhanh { get; set; }

    public string? NguoiTao { get; set; }

    public DateTime? NgaySua { get; set; }

    public string? NguoiSua { get; set; }

    public DateTime? NgayXoa { get; set; }

    public string? NguoiXoa { get; set; }

    public bool? IsActived { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid? ParentId { get; set; }
}
