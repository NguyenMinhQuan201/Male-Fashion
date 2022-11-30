using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class ProductImg
    {

        public int IdProductImg { get; set; }
        public int IdProduct { get; set; }
        public virtual Product? Product { get; set; }
        public string? Img { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
