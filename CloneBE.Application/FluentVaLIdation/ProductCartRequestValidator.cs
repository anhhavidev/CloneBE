using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Application.DTO.Request;
using FluentValidation;

namespace CloneBE.Application.FluentVaLIdation
{
    public class ProductCartRequestValidator : AbstractValidator<ProductCartRequest>
    {
        public ProductCartRequestValidator()
        {
            RuleFor(x => x.productId)
                .GreaterThan(0).WithMessage("ProductId phải lớn hơn 0");

            RuleFor(x => x.quantity)
                .GreaterThan(0).WithMessage("Số lượng phải lớn hơn 0")
                .LessThanOrEqualTo(100).WithMessage("Không được thêm quá 100 sản phẩm một lần");
        }
    }
}
