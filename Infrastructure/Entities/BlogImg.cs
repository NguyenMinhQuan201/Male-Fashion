using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class BlogImg
    {
        public int IdBlogImg { get; set; }
        public string? Name { get; set; }
        public string? Img { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int IdBlog { get; set; }
        public virtual Blog? Blog { get; set; }
    }
}
