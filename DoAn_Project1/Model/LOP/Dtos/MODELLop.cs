using Model.BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.HETHONG.LOP.Dtos
{
    public class MODELLop : MODELBase
    {
        public Guid? Id { get; set; }
        public string? TenLop {  get; set; }
        public bool? IsDeleted { get; set; }
    }
}
