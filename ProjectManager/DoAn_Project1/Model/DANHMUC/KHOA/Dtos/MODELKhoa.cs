using Model.BASE;

namespace Model.DANHMUC.KHOA.Dtos
{
    public class MODELKhoa : MODELBase
    {
        public Guid Id { get; set; }

        public string TenGoi { get; set; } = null!;

        public string TenVietTat { get; set; } = null!;
    }
}
