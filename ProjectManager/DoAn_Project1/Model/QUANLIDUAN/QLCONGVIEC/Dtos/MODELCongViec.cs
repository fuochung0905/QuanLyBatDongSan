using Model.BASE;

namespace Model.QUANLIDUAN.QLCONGVIEC.Dtos
{
    public class MODELCongViec : MODELBase
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
    }
}
