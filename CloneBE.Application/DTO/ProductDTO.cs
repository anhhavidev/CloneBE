using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneBE.Application.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string LinkImagesPath { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
       
    }
}
