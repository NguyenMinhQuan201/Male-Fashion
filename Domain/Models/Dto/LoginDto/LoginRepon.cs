using Domain.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.LoginDto
{
    public class LoginRepon : BaseDto
    {
        public string Token { get; set; }
        public string Time { get; set; }
        public string UserName { get; set; }
        public bool Status { get; set; }

    }
}
