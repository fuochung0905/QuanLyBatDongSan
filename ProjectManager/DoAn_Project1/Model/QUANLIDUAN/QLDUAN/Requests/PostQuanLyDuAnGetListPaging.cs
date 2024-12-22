using Model.BASE;

namespace Model.QUANLIDUAN.QLDUAN.Requests
{
    public class PostQuanLyDuAnGetListPaging : GetListPagingRequest
    {
        public Guid? KhoaId { get; set; }
        public Guid? LopId { get; set; }
    }
}
