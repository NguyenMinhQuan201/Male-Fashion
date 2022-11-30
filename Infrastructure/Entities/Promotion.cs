using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Promotion // chuong trinh giam gia
    {
        public int IdPromotion { get; set; }
        public string? Name { get; set; }
        public decimal Percent { get; set; } // phan tram giam gia
        public DateTime FromDate { get; set; } // ngay bat dau
        public DateTime ToDate { get; set; } // ngay ket thuc
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsEnable { get; set; }
        public virtual ICollection<Product>? Product { get; set; }

    }
}
