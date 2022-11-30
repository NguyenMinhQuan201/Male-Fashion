using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.Discount
{
    public class DiscountUpdateRequestDto
    {
        public string? Name { get; set; }
        public decimal Percent { get; set; } // phan tram giam gia
        public DateTime FromDate { get; set; } // ngay bat dau
        public DateTime ToDate { get; set; } // ngay ket thuc
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
