using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.Product
{
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        /*public Product Product { get; set; } = new Product();*/
        /*public int Size { get; set; } 
        public int Color { get; set; } */
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public DateTime NSX { get; set; }
        public DateTime HSD { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int Status { get; set; }
        /*public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;*/
    }
}
