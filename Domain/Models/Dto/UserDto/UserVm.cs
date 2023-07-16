using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models.Dto.UserDto
{
    public class UserVm
    {
        public Guid Id { get; set; }
        [Display(Name = "Ten")]
        public string? FirstName { get; set; }
        [Display(Name = "Ho")]
        public string? LastName { get; set; }
        [Display(Name = "SDT")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "TK")]
        public string? UserName { get; set; }
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Display(Name = "Ngay sinh")]
        public DateTime Dob { get; set; }
        public IList<string>? Roles { get; set; }
    }
    public class UserVmById
    {
        public Guid Id { get; set; }
        [Display(Name = "Ten")]
        public string? FirstName { get; set; }
        [Display(Name = "Ho")]
        public string? LastName { get; set; }
        [Display(Name = "SDT")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "TK")]
        public string? UserName { get; set; }
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Display(Name = "Ngay sinh")]
        public DateTime Dob { get; set; }
        public string PassWord { get; set; }
        public IList<string>? Roles { get; set; }
    }
    public class RoleDto
    {
        public string?Name { get; set; }
        public string?Description { get; set; }
    }
}
