using Model.BASE;

namespace Model.DANHMUC.LOP.Requests
{
    public class PostLopRequest : BaseRequest
    {
        public Guid Id { get; set; }

        public string TenGoi { get; set; } = null!;

        public string TenVietTat { get; set; } = null!;
    }
}
