﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AutoShop.Domain.ViewModels.Car
{
    public class CarViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Enter a title")]
        [MinLength(2, ErrorMessage = "The minimum length must be more than two characters")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [MinLength(10, ErrorMessage = "The minimum length must be more than 10 characters")]
        public string Description { get; set; }

        [Display(Name = "Model")]
        [Required(ErrorMessage = "Specify the model")]
        [MinLength(2, ErrorMessage = "The minimum length must be more than two characters")]
        public string Model { get; set; }

        [Display(Name = "Speed")]
        [Required(ErrorMessage = "Specify the speed")]
        [Range(0, 600, ErrorMessage = "The length should be in the range from 0 to 600")]
        public double Speed { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Specify the cost")]
        public decimal Price { get; set; } 

        public string DateCreate { get; set; }

        [Display(Name = "Type of car")]
        [Required(ErrorMessage = "Select the type")]
        public string TypeCar { get; set; }

        public IFormFile Avatar { get; set; }

        public byte[]? Image { get; set; }
    }
}