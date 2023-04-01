﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.Blog
{
    public class BlogRequestEditDto
    {
        public int IdBlog { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string? CreateAtBy { get; set; } // duoc tao boi ai do
    }
}
