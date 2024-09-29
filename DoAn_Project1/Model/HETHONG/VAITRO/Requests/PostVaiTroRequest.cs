using Model.BASE;

namespace Model.HETHONG.VAITRO.Requests
{
    public class PostVaiTroRequest : BaseRequest
    {
        public Guid? Id { get; set; }
        public string TenGoi { get; set; }

    }
}
