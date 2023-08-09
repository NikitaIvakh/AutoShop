using System.ComponentModel.DataAnnotations;

namespace AutoShop.Domain.ViewModels.Order
{
    public class CreateOrderViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Quantity")]
        [Range(1, 10, ErrorMessage = "The number should be from 1 to 10")]
        public int Quantity { get; set; }

        [Display(Name = "Date of creation")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Enter the address")]
        [MinLength(5, ErrorMessage = "The address must be more than 5 characters")]
        [MaxLength(200, ErrorMessage = "The address must be less than 250 characters")]
        public string Address { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter a name")]
        [MaxLength(20, ErrorMessage = "The name must be less than 20 characters long")]
        [MinLength(3, ErrorMessage = "The name must be longer than 3 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        [MaxLength(50, ErrorMessage = "The last name must be less than 50 characters long")]
        [MinLength(2, ErrorMessage = "The last name must be longer than 2 characters")]
        public string LastName { get; set; }

        [Display(Name = "Middle name")]
        [MaxLength(50, ErrorMessage = "The patronymic must be less than 50 characters long")]
        [MinLength(2, ErrorMessage = "The patronymic must be longer than 2 characters")]
        public string MiddleName { get; set; }

        public long CarId { get; set; }

        public string Login { get; set; }
    }
}