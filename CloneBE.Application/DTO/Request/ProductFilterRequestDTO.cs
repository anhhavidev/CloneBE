using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneBE.Application.DTO.Request
{
    public class ProductFilterRequestDTO
    {
        public string? SearchTerm { get; set; }
        public string? Category { get; set; }
        public double? MinPrice { get; set; } // ✅ Thêm lọc theo giá tối thiểu
        public double? MaxPrice { get; set; } // ✅ Thêm lọc theo giá tối đa
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 3;
    }

}
