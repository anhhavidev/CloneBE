using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CloneBE.Application.DTO.Request
{
    public class ProductRequest
    {
        //public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int stock { get; set; }
        private double _price;
        public double Price
        {
            get
            { return _price; }

            set
            {
                _price = value;
            }
        }
        public IFormFile LinkImagesPath { get; set; }  // Thêm trường này để nhận file ảnh
     
        public int CategoryId { get; set; }
    }
}
