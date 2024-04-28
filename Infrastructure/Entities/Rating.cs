using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int Stars {  get; set; }
        public string? Name { get; set; }
        public int SDT { get; set; }
        public string? Des { get; set; }
        public DateTime DateCreate { get; set; }
        public int IdOrder { get; set; }
    }
}
