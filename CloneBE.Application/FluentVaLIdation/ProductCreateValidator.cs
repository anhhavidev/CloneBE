using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Application.DTO;
using CloneBE.Application.DTO.Request;
using FluentValidation;

namespace CloneBE.Application.FluentVaLIdation
{
    public  class ProductCreateValidator :  AbstractValidator<ProductRequest>
    {
        public ProductCreateValidator() {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .Length(3, 100).WithMessage("Product name must be between 3 and 100 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.");

            RuleFor(X => X.Description).Length(1, 200).WithMessage("dESCRIPTION MUST BE  > 100 AND < 200");
            RuleFor(X => X.linkimages).NotEmpty();



        }
    }
}
