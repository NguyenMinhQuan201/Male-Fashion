using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.RoleDto
{
    public class RoleRequestDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid Id { get; set; }

    }
}
