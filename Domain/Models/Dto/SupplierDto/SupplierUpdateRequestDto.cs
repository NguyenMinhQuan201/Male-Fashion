using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Supplier.Dto
{
    public class SupplierUpdateRequestDto
    {
        public int  Id { get; set; }
        public string? Name { get; set; }
        public int Phone { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; }
        /*public DateTime UpdatedAt { get; set; }*/
        public bool IsEnable { get; set; }
    }
}
