using System;
using System.Collections.Generic;

namespace Entity.DBContent;

public partial class LOP
{
    public Guid Id { get; set; }

    public string TenGoi { get; set; } = null!;

    public string TenVietTat { get; set; } = null!;

    public string NguoiTao { get; set; } = null!;

    public DateTime NgayTao { get; set; }

    public DateTime? NgaySua { get; set; }

    public string? NguoiSua { get; set; }

    public DateTime? NgayXoa { get; set; }

    public string? NguoiXoa { get; set; }

    public bool IsActived { get; set; }

    public bool IsDeleted { get; set; }
}
