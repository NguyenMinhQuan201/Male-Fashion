using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.ImportInvoice
{
	public class ImportInvoiceDetailsDto
	{
        public int IdImportInvoice { get; set; }
        public int IdProduct { get; set; } // san pham
        public string? ProductName { get; set; } // san pham
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public DateTime NSX { get; set; }
        public DateTime HSD { get; set; }
        public double Weight { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
