using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.Discount
{
    public class GetAllDiscount
    {
        public int IdPromotion { get; set; }
        public string? Name { get; set; }
        public decimal Percent { get; set; } // phan tram giam gia
        public string FromDate { get; set; } // ngay bat dau
        public string ToDate { get; set; } // ngay ket thuc
        public string CreatedAt { get; set; } 
        public string UpdatedAt { get; set; }
        public bool IsEnable { get; set; }
    }
    public class GetDiscount
    {
        public int IdPromotion { get; set; }
        public string? Name { get; set; }
        public decimal Percent { get; set; } // phan tram giam gia
        public string FromDate { get; set; } // ngay bat dau
        public string ToDate { get; set; } // ngay ket thuc
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
