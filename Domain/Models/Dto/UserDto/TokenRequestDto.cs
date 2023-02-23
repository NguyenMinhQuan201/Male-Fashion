using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.UserDto
{
    public class TokenRequestDto
    {
        public string? Access_Token { get; set; }
        public string? Refresh_Token { get; set; }
    }
}
