using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneBE.Application.DTO.Request
{
    public class ProductCartRequest
    {
        public int productId { get; set; }
        public int quantity { get; set; }
    }
}
