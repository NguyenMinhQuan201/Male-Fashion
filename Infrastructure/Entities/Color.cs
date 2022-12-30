using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Color
    {
        public int Id { get; set; }
        public string NameColor { get; set; }
        public ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
