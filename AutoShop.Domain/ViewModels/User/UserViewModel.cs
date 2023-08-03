using System.ComponentModel.DataAnnotations;

namespace AutoShop.Domain.ViewModels.User
{
    public class UserViewModel
    {
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Display(Name = "Login")]
        public string Name { get; set; }

        [Display(Name = "Age")]
        public short Age { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }
    }
}