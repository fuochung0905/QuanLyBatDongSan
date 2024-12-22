using Model.BASE;

namespace Model.QUANLIDUAN.TRANGTHAICONGVIEC.Requests
{
    public class PostTrangThaiCongViecRequest : BaseRequest
    {
        public Guid Id { get; set; }

        public string? TenGoi { get; set; }
    }
}
