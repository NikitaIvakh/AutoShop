using System.ComponentModel.DataAnnotations;

namespace AutoShop.Domain.ViewModels.Profile
{
    public class ProfileViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter the address")]
        [MinLength(5, ErrorMessage = "The minimum length must be more than 5 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Specify the age")]
        [Range(0, 150, ErrorMessage = "The age range should be from 0 to 150")]
        [MaxLength(250, ErrorMessage = "The maximum length must be less than 250 characters")]
        public short Age { get; set; }
    }
}