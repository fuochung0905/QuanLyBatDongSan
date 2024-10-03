using System;
using System.Collections.Generic;

namespace Entity.DBContent;

public partial class LOPHOC
{
    public Guid Id { get; set; }

    public string? TenLop { get; set; }

    public int? SiSo { get; set; }

    public string? TenGiaoVien { get; set; }

    public DateTime? NgayTao { get; set; }

    public DateTime? NgayXoa { get; set; }

    public string? NguoiTao { get; set; }

    public string? NguoiXoa { get; set; }

    public bool? IsActived { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? NgaySua { get; set; }

    public string? NguoiSua { get; set; }
}
