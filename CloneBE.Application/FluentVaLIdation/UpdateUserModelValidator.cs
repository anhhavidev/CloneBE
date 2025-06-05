using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Application.DTO.Request;
using FluentValidation;

namespace CloneBE.Application.FluentVaLIdation
{
    public class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
    {
        public UpdateUserModelValidator()
        {
            RuleFor(x => x.FullName)
               
                .MaximumLength(100).WithMessage("FullName không được vượt quá 100 ký tự");

            RuleFor(x => x.PhoneNumber)
              
                .Matches(@"^\+?\d{10,15}$").WithMessage("PhoneNumber không hợp lệ");

            RuleFor(x => x.Address)
              
                .MaximumLength(200).WithMessage("Address không được vượt quá 200 ký tự");
        }
    }
}
