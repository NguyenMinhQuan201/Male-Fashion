using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Supplier // nha cung cap
    {
        public int IdSupplier { get; set; }
        public string? Name { get; set; }
        public int Phone { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public ICollection<Product>? Product { get; set; }
        public ICollection<ImportInvoice>? ImportInvoice { get; set; }
        public bool IsEnable { get; set; }
    }
}
