using Model.BASE;

namespace Model.DANHMUC.GIAIDOAN.Requests
{
    public class PostGiaiDoanRequest : BaseRequest
    {
        public Guid Id { get; set; }

        public string Ma { get; set; } = null!;

        public string TenGoi { get; set; } = null!;
    }
}
