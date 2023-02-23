using Domain.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.LoginDto
{
    public class LoginResponDto : BaseDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public int Time { get; set; }
        public bool Status { get; set; }

    }
}
