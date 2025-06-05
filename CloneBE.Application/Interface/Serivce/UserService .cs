using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Application.DTO.Request;
using CloneBE.Domain.EF;
using CloneBE.Domain.InterfaceRepo;
using Microsoft.EntityFrameworkCore;

namespace CloneBE.Application.Interface.Serivce
{
    public class UserService : IIUserService
    {
        private readonly IUnitOfWork1 unitOfWork1;

        public UserService(IUnitOfWork1 unitOfWork1) {
            this.unitOfWork1 = unitOfWork1;
        }
        public async  Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            var users = await unitOfWork1.UserRepository.GetAllUsersAsync();
            return users;
        }

        public async Task<AppUser?> GetCurrentUserAsync(string userId)
        {
            return await unitOfWork1.UserRepository.GetUserByIdAsync(userId);
        }

        public async Task<bool> UpdateCurrentUserAsync(string userId, UpdateUserModel model)
        {
            var user = await unitOfWork1.UserRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            // Cập nhật các trường thông tin cá nhân
            user.FullName = model.FullName;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;

            return await unitOfWork1.UserRepository.UpdateCurrentUserAsync(user);
        }
    }
}
