using Model.BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DANHMUC.KHOA.Requests
{
    public class PostKhoanRequest : BaseRequest
    {
        public Guid Id { get; set; }

        public string TenGoi { get; set; } = null!;

        public string TenVietTat { get; set; } = null!;
    }
}
