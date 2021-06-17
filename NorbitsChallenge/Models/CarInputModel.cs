using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NorbitsChallenge.Models
{
    //Frontend car model
    public class CarInputModel
    {    
        [Display(Name = "License plate")]
        [Required(ErrorMessage ="Car license plate is required.")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "License plate must be 7 characters long.")]
        public string CarLicensePlate { get; set; }

        [Display(Name = "Description")]
        public string CarDescription { get; set; }
        [Display(Name = "Model")]
        [Required(ErrorMessage = "Car model is required.")]
        public string CarModel { get; set; }

        [Display(Name = "Brand")]
        [Required(ErrorMessage = "Brand name is required.")]
        public string CarBrand { get; set; }

        [Display(Name = "Tire count")]
        [Required(ErrorMessage = "Car license plate is required.")]
        public int TireCount { get; set; }
    }
}
