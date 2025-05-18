using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CloneBE.Application.DTO.Request
{
    public class ProductUpdateRequest
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? stock { get; set; }
        public double? Price { get; set; }
        public IFormFile? LinkImages { get; set; }
        public int? CategoryId { get; set; }
    }

}
