using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.Blog
{
    public class BlogRequestDto
    {
        public string? Name { get; set; }
        public IFormFile? Image { get; set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        public DateTime?CreatedAt { get; set; }
        public DateTime?UpdatedAt { get; set; }
        public string? CreateAtBy { get; set; } // duoc tao boi ai do
    }
}
