using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.Order
{
    public class GetOrderDto
    {
        public int IdOrder { get; set; }
        public decimal SumPrice { get; set; }
        public string? NameCustomer { get; set; }
        public string? Address { get; set; }
        public int Phone { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; } // chu thich
        public int? Status { get; set; } // trang thai cua don hang
        public string? Payments { get; set; } // hinh thuc thanh toan
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime DeliveryAt { get; set; } // ngay giao hang
        public DateTime FinishAt { get; set; }// ngay hoan thanh don hang
    }
    public class OrderRequestByPhoneModel
    {
        public OrderDetailDto? OrderDetailDto { get; set; }
        public GetOrderDto? GetOrderDto { get; set; }
    } 

}
