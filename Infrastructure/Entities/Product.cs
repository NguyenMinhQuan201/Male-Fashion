using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Product
    {
        public int IdProduct { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } // tong so luong (hieu la kho)
        public string? Img { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int IdCategory { get; set; }
        public virtual Category? Categories { get; set; }
        public virtual ICollection<Promotion>? Promotion { get; set; }
        public ICollection<OrderDetails>? OrderDetails { get; set; }
        public ICollection<ProductDetails>? ProductDetails { get; set; }
        public ICollection<ImportInvoiceDetails>? ImportInvoiceDetails { get; set; }


    }
}
