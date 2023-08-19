using System.ComponentModel.DataAnnotations;

namespace Male_Fashion.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}
