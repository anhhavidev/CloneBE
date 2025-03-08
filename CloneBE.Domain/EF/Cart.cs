using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CloneBE.Domain.EF
{
    public  class Cart
    {
        public int CartId { get; set; }
        public List<CartItem>cartItems { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
