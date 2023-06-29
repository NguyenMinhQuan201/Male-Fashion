using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Operation : BaseEntity
    {
        public long ModuleId { get; set; }
        public string Name { get; set; }
        public long Url { get; set; }
        public long Code { get; set; }
        public long IsShow { get; set; }
        public long Icon { get; set; }
    }
}
