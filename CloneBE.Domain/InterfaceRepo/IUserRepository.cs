using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.EF;

namespace CloneBE.Domain.InterfaceRepo
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
        Task<AppUser> GetUserByIdAsync(string userId);
        Task<bool> UpdateCurrentUserAsync(AppUser user);

    }
}
