using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.EF;

namespace CloneBE.Application.DTO
{
    public class OrderDTO
    {
        
        public int OrderId { get; set; }

       

      
        public double TotalAmount { get; set; }  // Tổng tiền đơn hàng

      
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;  // Ngày đặt hàng

       
        public string Status { get; set; } = "Pending";  // Trạng thái đơn hàng

      
        public string ShippingAddress { get; set; }  // Địa chỉ giao hàng

     
        public string PhoneNumber { get; set; }  // Số điện thoại nhận hàng

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
      
        public ICollection<OrdetailSTO> OrderDetails { get; set; }
    }

}

