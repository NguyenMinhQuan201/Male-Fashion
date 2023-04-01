using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.Category
{
    public class CategoryRequestDto
    {
        public int IdCategory { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Icon { get; set; } = string.Empty;
        public DateTime ? CreatedAt { get; set; }
        public DateTime ? UpdatedAt { get; set; }
        public bool Status { get; set; }
    }
}
