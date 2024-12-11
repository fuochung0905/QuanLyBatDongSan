using FluentValidation;
using Model.BASE;
using System.ComponentModel.DataAnnotations;

namespace Model.HETHONG.VAITRO.Requests
{
    public class PostVaiTroRequest : BaseRequest
    {
        public Guid? Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tên gọi bắt buộc nhập")]
        public string? TenGoi { get; set; }
    }
    public class PostVaiTroRequestValidator : AbstractValidator<PostVaiTroRequest>
    {
        public PostVaiTroRequestValidator()
        {
            RuleFor(r => r.TenGoi).NotEmpty().WithMessage("Tên gọi không được rỗng");
        }
    }
}
