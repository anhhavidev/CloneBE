using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.EF;

namespace CloneBE.Domain.Model
{
    public class ProductFilterParameters
    {
        public string? SearchTerm { get; set; }
        public string? Category { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

}
