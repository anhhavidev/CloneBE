using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CloneBE.Domain.EF
{
    public  class AppUser :IdentityUser<string>
    {
       public int CartId { get; set; }
        public Cart Cart { get; set; }

    }
}
