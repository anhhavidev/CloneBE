using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CloneBE.Domain.EF
{
    public class AppIdentityRole : IdentityRole<string>
    {
        public AppIdentityRole() : base() { } // Constructor mặc định
// hasdata chỉ dùng constructer mặc định 
        public AppIdentityRole(string roleName) : base(roleName)
        {

        }
    }
}
