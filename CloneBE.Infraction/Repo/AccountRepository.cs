using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.EF;
using CloneBE.Domain.InterfaceRepo;
using Microsoft.AspNetCore.Identity;

namespace CloneBE.Infraction.Repo
{
    public class AccountRepository : IAcountRepository
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppIdentityRole> roleManager;

        public AccountRepository(UserManager<AppUser> userManager, RoleManager<AppIdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<AppUser?> GetUserByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> CreateUserAsync(AppUser user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        public async Task<IList<string>> GetUserRolesAsync(AppUser user)
        {
            return await userManager.GetRolesAsync(user);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await roleManager.RoleExistsAsync(roleName);
        }

        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            return await roleManager.CreateAsync(new AppIdentityRole(roleName));
        }

        public async Task AddUserToRoleAsync(AppUser user, string roleName)
        {
            await userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<string> GenerateResetTokenAsync(AppUser user)
        {
            return await userManager.GeneratePasswordResetTokenAsync(user);
        }
    }

}
