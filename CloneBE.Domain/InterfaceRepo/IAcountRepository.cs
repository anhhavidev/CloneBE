using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.EF;
using Microsoft.AspNetCore.Identity;

namespace CloneBE.Domain.InterfaceRepo
{
    public interface IAcountRepository
    {

        Task<AppUser?> GetUserByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(AppUser user, string password); // kiem tra xem mk nhap vao no đung voi mk cua tk nay k 
        Task<IdentityResult> CreateUserAsync(AppUser user, string password); // tạo người dùng với mk được mã hóa 
        Task<IList<string>> GetUserRolesAsync(AppUser user); // lấy ds vai trò ng dùng 
        Task<bool> RoleExistsAsync(string roleName); // kiểm tra xem có vai trò đấy trong hệ thống k 
        Task<IdentityResult> CreateRoleAsync(string roleName); // tạo mới 1 vai trò 
        Task AddUserToRoleAsync(AppUser user, string roleName); // gán người dùng với vai trò cụ thể

        //
        Task<string> GeneratePasswordResetTokenAsync(AppUser user);
        Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword);
    }

}
