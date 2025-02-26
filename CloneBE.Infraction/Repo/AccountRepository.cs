using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.InterfaceRepo;
using Microsoft.AspNetCore.Identity;

namespace CloneBE.Infraction.Repo
{
    public class AccountRepository : IAcountRepository
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IdentityUser?> GetUserByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<bool> CheckPasswordAsync(IdentityUser user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> CreateUserAsync(IdentityUser user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        public async Task<IList<string>> GetUserRolesAsync(IdentityUser user)
        {
            return await userManager.GetRolesAsync(user);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await roleManager.RoleExistsAsync(roleName);
        }

        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            return await roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public async Task AddUserToRoleAsync(IdentityUser user, string roleName)
        {
            await userManager.AddToRoleAsync(user, roleName);
        }
    }

}
