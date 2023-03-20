using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.Color
{
    public class ColorRequestDto
    {
        public int Id { get; set; }
        public string ColorName { get; set; } = string.Empty;
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return true;
        }
    }
}
