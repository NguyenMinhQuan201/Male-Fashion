using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Contact
    {
        public int IdContact { get; set; }
        public string? Hotline { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? Twitter { get; set; }
        public string? WorkTime { get; set; } // thoi gian lam viec

    }
}
