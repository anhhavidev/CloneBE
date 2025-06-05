using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Application.DTO.Request;
using FluentValidation;

namespace CloneBE.Application.FluentVaLIdation
{
    public class OrderRequestValidator : AbstractValidator<OrderRequest>
    {
        public OrderRequestValidator()
        {
            // 📱 Kiểm tra số điện thoại bằng regex
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Số điện thoại không được để trống.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Số điện thoại không hợp lệ (10-15 chữ số, có thể bắt đầu bằng +).");

            // 🏠 Kiểm tra địa chỉ
            RuleFor(x => x.ShippingAddress)
                .NotEmpty().WithMessage("Địa chỉ giao hàng không được để trống.")
                .MinimumLength(10).WithMessage("Địa chỉ phải dài ít nhất 10 ký tự.");
        }
    }
}
