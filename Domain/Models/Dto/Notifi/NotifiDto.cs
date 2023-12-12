using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.Notifi
{
    public class NotifiDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Link { get; set; }
        public bool IsRead { get; set; }
        public decimal Price { get; set; }
        public DateTime Time { get; set; }
    }
}
