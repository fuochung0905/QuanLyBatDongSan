using Model.BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DANHMUC.GIAIDOAN.Dtos
{
    public class MODELGiaiDoan : MODELBase
    {
        public Guid Id { get; set; }

        public string Ma { get; set; } = null!;

        public string TenGoi { get; set; } = null!;
    }
}
