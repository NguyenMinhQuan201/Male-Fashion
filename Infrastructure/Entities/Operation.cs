using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Operation : BaseEntity
    {
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public long Url { get; set; }
        public string Code { get; set; }
        public bool IsShow { get; set; }
        public long Icon { get; set; }
    }
}
