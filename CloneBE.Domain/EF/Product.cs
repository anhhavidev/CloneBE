using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneBE.Domain.EF
{
    public class Product
    {
        public int ProductId {  get; set; }
        public string? Name {  get; set; }
        public string? Description { get; set; }
        public int ?stock { get; set; }
     
       
        public double? Price {  get; set; }
        public string? LinkImagesPath { get; set; }
        public int? CategoryId { get; set; }
        public Category category { get; set; }

        public List<CartItem> cartItems { get; set; }   
        

    }
}
