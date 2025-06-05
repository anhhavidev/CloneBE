using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Application.DTO.Request;
using FluentValidation;

namespace CloneBE.Application.FluentVaLIdation
{
    public class SignUpModelValidator : AbstractValidator<SignUpModel>
    {
        public SignUpModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email không được để trống")
                .EmailAddress().WithMessage("Email không hợp lệ");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống")
                .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 ký tự")
                .Matches(@"[A-Z]+").WithMessage("Mật khẩu phải có ít nhất 1 chữ hoa")
                .Matches(@"[a-z]+").WithMessage("Mật khẩu phải có ít nhất 1 chữ thường")
                .Matches(@"\d+").WithMessage("Mật khẩu phải có ít nhất 1 số");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Xác nhận mật khẩu không khớp");

            RuleFor(x => x.phoneNumber)
                .NotEmpty().WithMessage("Số điện thoại không được để trống")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Số điện thoại không hợp lệ");
        }
    }
}
