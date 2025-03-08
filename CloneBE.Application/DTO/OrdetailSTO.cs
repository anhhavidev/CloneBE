using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneBE.Application.DTO
{
    public class OrdetailSTO
    {
         public int ProductId { get; set; }
        public int Quantity { get; set; }

       
        public double UnitPrice { get; set; }
    }
}
