using DataDemo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.RoleDto
{
    public class RoleAssignRequestDto
    {
        /*public Guid Id { get; set; }*/
        public List<SelectItem> Roles { get; set; } = new List<SelectItem>();
    }
}
