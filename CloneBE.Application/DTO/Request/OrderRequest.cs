using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneBE.Application.DTO.Request
{
    public  class OrderRequest
    {
        public string ShippingAddress { get; set; }  // Địa chỉ giao hàng

       
        public string PhoneNumber { get; set; }  // Số điện thoại nhận hàng
    }
}
