using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class RoleOperation :BaseEntity
    {
        public int RoleId { get; set; }
        public long OperationId { get; set; }
        public bool IsAccess { get; set; }
    }
}
