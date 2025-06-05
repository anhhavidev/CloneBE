using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.EF;
using CloneBE.Domain.InterfaceRepo;
using CloneBE.Infraction.Presistences;
using Microsoft.EntityFrameworkCore;

namespace CloneBE.Infraction.Repo
{
    public class UserRepository :IUserRepository

    {

        private readonly Databasese db;

        public UserRepository(Databasese databasese)
        {

            db = databasese;
        }
        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await db.Users.ToListAsync(); // Lấy toàn bộ danh sách người dùng từ cơ sở dữ liệu
        }

        public async Task<AppUser> GetUserByIdAsync(string userId)
        {
            return await db.Users.FindAsync(userId); // Lấy người dùng theo userId
        }

       

        public async Task<bool> UpdateCurrentUserAsync(AppUser user)
        {
            db.Users.Update(user);
            return await db.SaveChangesAsync() > 0;
        }
    }
}
