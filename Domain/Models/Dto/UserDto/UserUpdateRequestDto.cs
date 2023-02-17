using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.UserDto
{
    public class UserUpdateRequestDto
    {
        /*public Guid Id { get; set; }*/
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PassWord { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime Dob { get; set; }
    }
}
