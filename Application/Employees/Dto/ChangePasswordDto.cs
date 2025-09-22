using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employees.Dto
{
    public class ChangePasswordDto
    { 

        [Required(ErrorMessage = "Old Password is required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New Password is required")]
         public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
