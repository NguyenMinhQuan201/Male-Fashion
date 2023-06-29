using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.UserDto
{
    public class OperationDto
    {
        public long Id {get;set;}
        public long ModuleId { get; set; }
        public string Name { get; set; }
        public long Url { get; set; }
        public long Code { get; set; }
        public long IsShow { get; set; }
        public long Icon { get; set; }
    }
    public class UserOperationDto
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public long OperationId { get; set; }
        public bool IsAccess { get; set; }
    }
    public class RoleOperationDto
    {
        public long Id { get; set; }
        public int RoleId { get; set; }
        public long OperationId { get; set; }
        public bool IsAccess { get; set; }
    }public class ModuleDto
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int Order { get; set; }
        public bool IsShow { get; set; }
    }
}
