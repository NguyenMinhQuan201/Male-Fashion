using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class RoleOperation :BaseEntity
    {
        public string RoleId { get; set; }
        public int OperationId { get; set; }
        public bool IsAccess { get; set; }
    }
}
