using Domain.Models.Dto.ImportInvoice;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.ImportInvoiceDto
{
	public class ImportInvoiceDto
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
        //public SupplierDto? Supplier { get; set; }
        public ICollection<ImportInvoiceDetailsDto>? ImportInvoiceDetails { get; set; }
    }
}
