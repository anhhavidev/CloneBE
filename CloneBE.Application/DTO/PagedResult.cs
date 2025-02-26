using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneBE.Application.DTO
{
    public class PagedResult<T> where T : class
    {
        public IEnumerable<T> Items { get; set; } = new List<T>(); // Danh sách kết quả
        public int TotalCount { get; set; } // Tổng số bản ghi
        public int PageSize { get; set; }   // Số lượng trên mỗi trang
        public int CurrentPage { get; set; } // Trang hiện tại
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize); // Tổng số trang
    }
}
