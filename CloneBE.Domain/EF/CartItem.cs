using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneBE.Domain.EF
{
    public  class CartItem
    {
        public int CartitemId { get; set; }

        public double price { get; set; }
        public int quanlity { get; set; }
        public int Productid { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }  

        public Product Product { get; set; }

    }
}
