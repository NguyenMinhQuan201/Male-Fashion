using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Img // luu tru cac hinh anh tren web
    {
        public int IdImg { get; set; }
        public string? Name { get; set; }
        public string? Link { get; set; }
        public string? Image { get; set; }
        public string? IsType { get; set; } // banner, slider, ...
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
