using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class QA
    {
        public int IdQA { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public string? CreateBy { get; set; } // ai dang cau hoi - tra loi
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


    }
}
