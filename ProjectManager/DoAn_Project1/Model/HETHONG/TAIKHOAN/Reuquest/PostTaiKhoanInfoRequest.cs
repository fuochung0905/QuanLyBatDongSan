using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.HETHONG.TAIKHOAN.Reuquest
{
    public class PostTaiKhoanInfoRequest
    {
        public Guid? Id { get; set; }
        //Nhập số điện thoại cần 10 số
        [RegularExpression("^[Z0-9]{10}$", ErrorMessage = "Nhập 10 chữ (số)")]
        [Required(ErrorMessage = "Số điện thoại không được rỗng")]
        public string SoDienThoai { get; set; } = null!;
        [Required(ErrorMessage = "Email không được rỗng")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Họ không được rỗng")]
        public string? HoLot { get; set; } = null!;
        [Required(ErrorMessage = "Tên không được rỗng")]
        public string? Ten { get; set; } = null!;
        [Required(ErrorMessage = "Ngày sinh không được rỗng")]
        public DateTime? NgaySinh { get; set; }
        public int? GioiTinh { get; set; } = 0;
        public string? AnhDaiDien { get; set; }
        public string? FolderUpload { get; } = Guid.NewGuid().ToString();
    }

    public class PostTaiKhoanInfoRequestValidator : AbstractValidator<PostTaiKhoanInfoRequest>
    {
        public PostTaiKhoanInfoRequestValidator()
        {
            RuleFor(r => r.Email).NotNull().WithMessage("Email không được rỗng");
            RuleFor(r => r.SoDienThoai).NotNull().WithMessage("Số điện thoại không được rỗng");
            RuleFor(r => r.SoDienThoai).Length(10, 10).WithMessage("Nhập 10 chữ (số)");
            RuleFor(r => r.Ten).NotEmpty().WithMessage("Tên không được rỗng");
            RuleFor(r => r.HoLot).NotEmpty().WithMessage("Họ không được rỗng");
            RuleFor(r => r.NgaySinh).NotNull().WithMessage("Ngày sinh không được rỗng");
        }
    }
}
