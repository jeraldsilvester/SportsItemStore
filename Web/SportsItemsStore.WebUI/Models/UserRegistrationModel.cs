using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace SportsItemsStore.WebUI.Models
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage ="Please enter a User Name")]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        [StringLength(50)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password cannot be left blank ")]
        [Compare("Password", ErrorMessage = "Password entered does not match with confirm password")]
        [DataType(DataType.Password)]
        [StringLength(50)]
        public string ComfirmPassword { get; set; }

        [Required(ErrorMessage="Email is required")]
        [DataType(DataType.EmailAddress)]
        [StringLength(128)]
        public string Email { get; set; }
    }
}
