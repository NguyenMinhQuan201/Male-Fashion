using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid?UserCreateId { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public Guid?UserUpdateId { get; set; }
        
        public DateTime DeleteAt { get; set; } = DateTime.Now;
        public Guid?UserDeleteId { get; set; }
    }
}
