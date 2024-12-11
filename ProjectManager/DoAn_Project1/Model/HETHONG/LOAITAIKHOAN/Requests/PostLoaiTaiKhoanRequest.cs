using Model.BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.HETHONG.LOAITAIKHOAN.Requests
{
    public class PostLoaiTaiKhoanRequest : BaseRequest
    {
        public Guid Id { get; set; }
        public string? Ma { get; set; }

        public string TenGoi { get; set; }
    }
}
