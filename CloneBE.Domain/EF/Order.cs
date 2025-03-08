using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneBE.Domain.EF
{
    public class Order
    {
            [Key]
            public int OrderId { get; set; }

            [Required]
            public string UserId { get; set; }  // ID người đặt hàng

            [Required]
            public double TotalAmount { get; set; }  // Tổng tiền đơn hàng

            [Required]
            public DateTime OrderDate { get; set; } = DateTime.UtcNow;  // Ngày đặt hàng

            [Required]
            [MaxLength(50)]
            public string Status { get; set; } = "Pending";  // Trạng thái đơn hàng

            [Required]
            [MaxLength(255)]
            public string ShippingAddress { get; set; }  // Địa chỉ giao hàng

            [Required]
            [MaxLength(15)]
            public string PhoneNumber { get; set; }  // Số điện thoại nhận hàng

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("UserId")]                            
            
            public AppUser AppUser { get; set; }
            public ICollection<OrderDetail> OrderDetails { get; set; }
        }

    }

