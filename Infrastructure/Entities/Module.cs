using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Module : BaseEntity
    {
        public string?Code { get; set; }
        public string?Name { get; set; }
        public int Order { get; set; }
        public bool IsShow { get; set; }
    }
}
