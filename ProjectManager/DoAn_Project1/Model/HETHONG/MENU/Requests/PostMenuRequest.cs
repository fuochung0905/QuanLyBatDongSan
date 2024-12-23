using FluentValidation;
using Model.BASE;
using System.ComponentModel.DataAnnotations;

namespace Model.HETHONG.MENU.Requests
{
    public class PostMenuRequest : BaseRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "ControllerName bắt buộc nhập")]
        public string? ControllerName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Controller bắt buộc nhập")]
        public string? Controller { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Action bắt buộc nhập")]
        public string? Action { get; set; } = "index";
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tên gọi bắt buộc nhập")]
        public string? TenGoi { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nhóm quyền bắt buộc nhập")]
        public Guid? NhomQuyenId { get; set; }
        public bool CoXem { get; set; } = false;
        public bool CoThem { get; set; } = false;
        public bool CoCapNhat { get; set; } = false;
        public bool CoXoa { get; set; } = false;
        public bool CoDuyet { get; set; } = false;
        public bool CoThongKe { get; set; } = false;
        public bool IsShowMenu { get; set; } = true;
    }
    public class PostMenuRequestValidator : AbstractValidator<PostMenuRequest>
    {
        public PostMenuRequestValidator()
        {
            RuleFor(r => r.ControllerName).NotEmpty().WithMessage("ControllerName không được rỗng");
            RuleFor(r => r.Controller).NotEmpty().WithMessage("Controller không được rỗng");
            RuleFor(r => r.Action).NotEmpty().WithMessage("Action không được rỗng");
            RuleFor(r => r.TenGoi).NotEmpty().WithMessage("Tên gọi không được rỗng");
            RuleFor(r => r.NhomQuyenId).NotNull().WithMessage("Nhóm quyền không được rỗng");
        }
    }
}
