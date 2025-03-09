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
// config k lỗi

namespace CloneBE.Application.Interface.Serivce
{
    public class AccountService : IAccountService
    {
        private readonly IAcountRepository accountRepository;
        private readonly IConfiguration configuration;
        private readonly IEmailService emailService;

        public AccountService(IAcountRepository accountRepository, IConfiguration configuration, IEmailService  emailService)
        {
            this.accountRepository = accountRepository;
            this.configuration = configuration;
            this.emailService = emailService;
        }

        public async Task<AuthRespone?> SignInAsync(SignInModel model)
        {
            var user = await accountRepository.GetUserByEmailAsync(model.Email);
            if (user == null || !await accountRepository.CheckPasswordAsync(user, model.Password))
            {
                return null;
            }

            var accessToken = GenerateAccessToken(user);

            return new AuthRespone { AccessToken = accessToken };
        }

        private string GenerateAccessToken(AppUser user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("username", user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

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

        public async Task<string> ForgotPasswordAsync(string email, string requestScheme, string requestHost)
        {
            var user = await accountRepository.GetUserByEmailAsync(email);
            if (user == null)
                throw new ArgumentException("Không tìm thấy tài khoản!");

            var token = await accountRepository.GenerateResetTokenAsync(user);

            // Tạo link đặt lại mật khẩu
            var resetLink = $"{requestScheme}://{requestHost}/reset-password?email={email}&token={Uri.EscapeDataString(token)}";

            // Gửi email
            await emailService.SendEmailAsync(user.Email, "Đặt lại mật khẩu", $"Click vào link để đặt lại mật khẩu: <a href='{resetLink}'>Reset Password</a>");

            return "Vui lòng kiểm tra email để đặt lại mật khẩu!";
        }
    }
}
