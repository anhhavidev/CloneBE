using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using CloneBE.Application.DTO;
using CloneBE.Application.Helper;
using CloneBE.Domain.InterfaceRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using CloneBE.Domain.EF;
using CloneBE.Application.DTO.Request;
using Microsoft.AspNetCore.Http;
// config k lỗi

namespace CloneBE.Application.Interface.Serivce
{
    public class AccountService : IAccountService
    {
        private readonly IAcountRepository accountRepository;
        private readonly IConfiguration configuration;
        private readonly ISendMailService mailService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IOTPService otpService;

        public AccountService(IAcountRepository accountRepository, IConfiguration configuration, ISendMailService mailService, IHttpContextAccessor httpContextAccessor, IOTPService otpService)
        {
            this.accountRepository = accountRepository;
            this.configuration = configuration;
            this.mailService = mailService;
            this.httpContextAccessor = httpContextAccessor;
            this.otpService = otpService;
        }

        public async Task<AuthRespone?> SignInAsync(SignInModel model)
        {
            var user = await accountRepository.GetUserByEmailAsync(model.Email);
            if (user == null || !await accountRepository.CheckPasswordAsync(user, model.Password))
            {
                return null;
            }

            var accessToken = await GenerateAccessTokenAsync(user);

            return new AuthRespone { AccessToken = accessToken };
        }

        private async Task<string> GenerateAccessTokenAsync(AppUser user)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, user.Email),
        new Claim("username", user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    };
            var roles = await accountRepository.GetUserRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddMinutes(20),
                claims: claims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var user = new AppUser
            {
                PhoneNumber = model.phoneNumber,
                Email = model.Email,
                UserName = model.Email
            };

            var result = await accountRepository.CreateUserAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (!await accountRepository.RoleExistsAsync(AppRole.Customer))
                {
                    await accountRepository.CreateRoleAsync(AppRole.Customer);
                }

                await accountRepository.AddUserToRoleAsync(user, AppRole.Customer);
            }

            return result;
        }

        public async Task<bool> SendForgotPasswordEmail(string email)
        {
            var user = await accountRepository.GetUserByEmailAsync(email);
            if (user == null) return false;

            var otpCode = otpService.GenerateOTP(email);
            var mailContent = new MailContent
            {
                To = email,
                Subject = "Xác thực đặt lại mật khẩu",
                Body = $"<p>Mã OTP của bạn là: <strong>{otpCode}</strong>. Mã này có hiệu lực trong 5 phút.</p>"
            };

            await mailService.SendMail(mailContent);
            return true;
        }
        public async Task<string?> VerifyOTPAndGenerateResetToken(string email, string otp)
        {
            var isValidOTP = otpService.ValidateOTP(email, otp);
            if (!isValidOTP) return null;

            var user = await accountRepository.GetUserByEmailAsync(email);
            if (user == null) return null;

            var token = await accountRepository.GeneratePasswordResetTokenAsync(user);
            return token;
        }

        public  async Task<bool> ResetPassword(ResetPasswordModel model)
        {
            var user = await accountRepository.GetUserByEmailAsync(model.Email);
            if (user == null) return false;

            var result = await accountRepository.ResetPasswordAsync(user, model.Token, model.NewPassword);
            return result.Succeeded;
        
    }
    }
}
