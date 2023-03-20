using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.Order
{
    public class OrderDto
    {
        /*public int IdOrder { get; set; }*/
        public decimal SumPrice { get; set; }
        public string? NameCustomer { get; set; }
        public string? Address { get; set; }
        public int Phone { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; } // chu thich
        public bool? Status { get; set; } // trang thai cua don hang
        public string? Payments { get; set; } // hinh thuc thanh toan
        public DateTime DeliveryAt { get; set; } // ngay giao hang
        public ICollection<OrderDetailDto> ? OrderDetails { get; set; } //List OrderDetail
    }
}
