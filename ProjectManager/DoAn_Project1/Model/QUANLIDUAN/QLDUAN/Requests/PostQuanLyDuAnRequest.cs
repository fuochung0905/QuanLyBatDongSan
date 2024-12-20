using Model.BASE;

namespace Model.QUANLIDUAN.QLDUAN.Requests
{
    public class PostQuanLyDuAnRequest : BaseRequest
    {
        public Guid Id { get; set; }

        public string TenVietTat { get; set; } = null!;

        public string TenGoi { get; set; } = null!;

        public Guid KhoaId { get; set; }

        public Guid LopId { get; set; }
    }
}
