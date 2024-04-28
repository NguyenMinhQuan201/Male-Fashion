﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.Order
{
    public class OrderDto
    {
        public long? Id { get; set; }
        public decimal SumPrice { get; set; }
        public string? NameCustomer { get; set; }
        public string? Address { get; set; }
        public int Phone { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; } // chu thich
        public int? Status { get; set; } // trang thai cua don hang
        public string? Payments { get; set; } // hinh thuc thanh toan
        public DateTime DeliveryAt { get; set; } // ngay giao hang
        public ICollection<OrderDetailDto> ? OrderDetails { get; set; } //List OrderDetail
    }
    public class ChartCol
    {
        public decimal ChartPrice { get; set; }
        public decimal Month { get; set; }
    }
    public class ChartColDay
    {
        public decimal ChartPrice { get; set; }
        public decimal Day { get; set; }
    }
    public class ChartRadius
    {
        public decimal ChartPrice { get; set; }
        public decimal Year { get; set; }
    }
}
