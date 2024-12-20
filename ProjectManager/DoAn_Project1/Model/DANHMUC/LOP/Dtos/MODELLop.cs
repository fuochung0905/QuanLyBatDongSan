using Model.BASE;

namespace Model.DANHMUC.LOP.Dtos
{
    public class MODELLop : MODELBase
    {
        public Guid Id { get; set; }

        public string TenGoi { get; set; } = null!;

        public string TenVietTat { get; set; } = null!;
    }
}
