using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class ProductImg
    {

        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ImagePath { get; set; } = string.Empty;

        public string Caption { get; set; } = string.Empty;

        public bool IsDefault { get; set; }

        public DateTime DateCreated { get; set; }

        //public int SortOrder { get; set; }

        public long FileSize { get; set; }

        /*public Product Product { get; set; } = new Product();*/
    }
}
