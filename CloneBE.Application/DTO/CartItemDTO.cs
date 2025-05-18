using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneBE.Application.DTO
{
    public  class CartItemDTO
    {
        public int CartitemId { get; set; }

        public double price { get; set; }
        public int quanlity { get; set; }
        public int Productid { get; set; }
        public int CartId { get; set; }
    }
}
