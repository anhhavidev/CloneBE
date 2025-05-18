using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CloneBE.Application.DTO.Request
{
    public class ProductResponse
    {
        public int ProductId { get; set; } // Tự tăng, trả về sau khi lưu DB
        public string Name { get; set; }
        public string Description { get; set; }
        public int stock { get; set; }
        public double Price { get; set; }
        public string LinkImagesPath { get; set; } // Đường dẫn ảnh đã lưu
        public int CategoryId { get; set; }
    }
}
