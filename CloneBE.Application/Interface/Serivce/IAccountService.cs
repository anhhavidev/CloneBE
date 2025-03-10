using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Application.DTO;
using CloneBE.Application.DTO.Request;
using CloneBE.Application.Helper;
using Microsoft.AspNetCore.Identity;


namespace CloneBE.Application.Interface.Serivce
{
    public interface IAccountService
    {
        Task<AuthRespone?> SignInAsync(SignInModel model);
        Task<IdentityResult> SignUpAsync(SignUpModel model);
        Task<bool> SendForgotPasswordEmail(string email);
        Task<bool> ResetPassword(ResetPasswordModel model);
        Task<string?> VerifyOTPAndGenerateResetToken(string email, string otp);
    }

}
