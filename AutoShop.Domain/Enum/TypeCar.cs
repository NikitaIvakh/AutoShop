using System.ComponentModel.DataAnnotations;

namespace AutoShop.Domain.Enum
{
    public enum TypeCar
    {
        [Display(Name = "Passenger car")]
        PassengerCar = 0,

        [Display(Name = "Sedan")]
        Sedan = 1,

        [Display(Name = "HatchBack")]
        HatchBack = 2,

        [Display(Name = "Minivan")]
        Minivan = 3,

        [Display(Name = "SportsCar")]
        SportsCar = 4,

        [Display(Name = "Suv")]
        Suv = 5,
    }
}