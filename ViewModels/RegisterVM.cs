using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket_Demo.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name is required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "User Name must be between 3 and 50 chars")]
        public string UserName { get; set; }
        [Display(Name = "Profile Image Url")]
        [Required(ErrorMessage = "Url is required!")]
        public string ProfileImgUrl { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Password is required!")]
        public string ConfirmPassword { get; set; }
    }
}
