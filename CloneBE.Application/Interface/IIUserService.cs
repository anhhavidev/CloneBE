using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Application.DTO.Request;
using CloneBE.Domain.EF;

namespace CloneBE.Application.Interface
{
    public interface IIUserService
    {
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
        Task<AppUser?> GetCurrentUserAsync(string userId);
        Task<bool> UpdateCurrentUserAsync(string userId, UpdateUserModel model);
    }
}
