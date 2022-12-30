using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Size
    {
        public int Id { get; set; }
        public string NameSize { get; set; }
        public ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
