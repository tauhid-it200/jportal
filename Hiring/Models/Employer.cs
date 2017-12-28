using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hiring.Models
{
    public class Employer
    {
        public int EmployerId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(length: 50, ErrorMessage = "Maximum name length is 50 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [MaxLength(length: 20, ErrorMessage = "Maximum password length is 20 characters")]
        [MinLength(length: 6, ErrorMessage = "Minimum password length is 6 characters")]
        public string Password { get; set; }
        [NotMapped] // Does not effect with your database
        [Compare("Password", ErrorMessage = "Password is not confirmed")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Contact Number is Required")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(length: 10, ErrorMessage = "Maximum Username length is 10 characters")]
        [MinLength(length: 5, ErrorMessage = "Minimum Username length is 5 characters")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Organization is required")]
        [MaxLength(length: 50, ErrorMessage = "Maximum Organization name length is 50 characters")]
        public string OrganizationName { get; set; }
        public string OrganizationInfo { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Job> Jobs {get; set; }
    }
}