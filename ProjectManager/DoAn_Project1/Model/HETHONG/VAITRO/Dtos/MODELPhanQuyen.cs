using Model.BASE;

namespace Model.HETHONG.VAITRO.Dtos
{
    public class MODELPhanQuyen : MODELBase
    {
        public Guid vaiTroId { get; set; }
        public List<MODELCombobox> nhomQuyen { get; set; } = new List<MODELCombobox>();
    }
}
