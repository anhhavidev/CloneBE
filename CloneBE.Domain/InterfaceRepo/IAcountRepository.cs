using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CloneBE.Domain.InterfaceRepo
{
    public interface IAcountRepository
    {

        Task<IdentityUser?> GetUserByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(IdentityUser user, string password); // kiem tra xem mk nhap vao no đung voi mk cua tk nay k 
        Task<IdentityResult> CreateUserAsync(IdentityUser user, string password); // tạo người dùng với mk được mã hóa 
        Task<IList<string>> GetUserRolesAsync(IdentityUser user); // lấy ds vai trò ng dùng 
        Task<bool> RoleExistsAsync(string roleName); // kiểm tra xem có vai trò đấy trong hệ thống k 
        Task<IdentityResult> CreateRoleAsync(string roleName); // tạo mới 1 vai trò 
        Task AddUserToRoleAsync(IdentityUser user, string roleName); // gán người dùng với vai trò cụ thể
    }

}
