using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class ImportInvoice // hoa don nhap
    {
        public int IdImportInvoice { get; set; } // id
        public string? Name { get; set; }
        public decimal SumPrice { get; set; }
        public string? Address { get; set; }
        public int Phone { get; set; }
        public string? Note { get; set; } // ghi
        public string? Status { get; set; } // trang thai cua don hang
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string? CreatAtBy { get; set; } // duoc nhap boi ai
        public int IdSupplier { get; set; } // nha phan phoi
        public virtual Supplier? Supplier { get; set; }
        
        public ICollection<ImportInvoiceDetails>? ImportInvoiceDetails { get; set; }
    }
}
