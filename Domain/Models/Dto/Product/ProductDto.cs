using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.Product
{
    public class ProductDto
    {
        /*public int IdProduct { get; set; }*/
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } // tong so luong (hieu la kho)
        public List<IFormFile>?Img { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
        public int IdCategory { get; set; }
    }
    public class AddImageRequest
    {
        public int Id { get; set; }
        public List<IFormFile> ProductImageVMs { get; set; }
    }
}
