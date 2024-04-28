using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.Order
{
    public class OrderDetailDto
    {
        public int IdProduct { get; set; }
        public int IdOrder { get; set; }
        public decimal Price { get; set; }
        public decimal Discounnt { get; set; }
        public int Quantity { get; set; }
        public string?NameProduct { get; set; }
        public string? Img { get; set; }
        public int IsRating { get; set; }
    }
    public class OrderDetailRequest
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public decimal Price { get; set; }
        public decimal Discounnt { get; set; }
        public int Quantity { get; set; }
    }
}
