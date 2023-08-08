using System.ComponentModel.DataAnnotations;

namespace AutoShop.Domain.ViewModels.User
{
    public class UserViewModel
    {
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Enter your Login")]
        [Display(Name = "Login")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Specify the role")]
        [Display(Name = "Role")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Enter the password")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}