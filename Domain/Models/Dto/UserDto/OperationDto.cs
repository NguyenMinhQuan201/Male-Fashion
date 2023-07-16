using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.UserDto
{
    public class OperationDto: BaseDto
    {
        public int Id {get;set;}
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public long Url { get; set; }
        public string Code { get; set; }
        public bool IsShow { get; set; }
        public long Icon { get; set; }
    }
    public class UserOperationDto : BaseDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int OperationId { get; set; }
        public bool IsAccess { get; set; }
    }
    public class RoleOperationDto: BaseDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int OperationId { get; set; }
        public bool IsAccess { get; set; }
    }public class ModuleDto : BaseDto
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int Order { get; set; }
        public bool IsShow { get; set; }
    }
}
