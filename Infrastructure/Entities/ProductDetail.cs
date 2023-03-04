using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class ProductDetail
    {
        public int Id{ get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }= new Product();
        
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public DateTime NSX { get; set; }
        public DateTime HSD { get; set; }
        public string  Size { get; set; }
        public string Color { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        

    }
}
