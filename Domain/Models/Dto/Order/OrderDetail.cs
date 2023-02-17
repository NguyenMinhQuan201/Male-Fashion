using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.Order
{
    public class OrderDetailDto
    {
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public decimal Price { get; set; }
        public decimal Discounnt { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime DeliveryAt { get; set; } = DateTime.Now; // ngay giao hang
        public DateTime FinishAt { get; set; } // ngay hoan thanh don hang
    }
}
