using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneBE.Application.DTO.Request
{
    public class ProductCartRequest
    {
        //[Range(1, int.MaxValue, ErrorMessage = "ProductId must be > 0")]
        public int productId { get; set; }
        //[Range(1, int.MaxValue, ErrorMessage = "Quantity must be > 0")]
        public int quantity { get; set; }
    }
}
