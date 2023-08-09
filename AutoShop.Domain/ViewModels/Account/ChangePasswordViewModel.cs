using System.ComponentModel.DataAnnotations;

namespace AutoShop.Domain.ViewModels.Account
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Enter a name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter the password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MinLength(5, ErrorMessage = "The password must be greater than or equal to 5 characters")]
        public string NewPassword { get; set; }
    }
}