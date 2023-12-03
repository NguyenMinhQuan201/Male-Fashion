using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Notifi
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Link { get; set; }
        public bool IsRead { get; set; }

    }
}
