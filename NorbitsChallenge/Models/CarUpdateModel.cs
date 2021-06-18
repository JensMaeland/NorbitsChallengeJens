using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NorbitsChallenge.Models
{
    public class CarUpdateModel
    {
        [Display(Name = "Old License plate")]
        public string OldLicensePlate { get; set; }

        [Display(Name = "New License plate")]
        [Required(ErrorMessage = "Car license plate is required.")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "License plate must be 7 characters long.")]
        public string CarLicensePlate { get; set; }

        [Required(ErrorMessage = "Car description is required.")]
        [Display(Name = "Description")]
        public string CarDescription { get; set; }
        

        [Display(Name = "Model")]
        [Required(ErrorMessage = "Car model is required.")]
        public string CarModel { get; set; }

        [Display(Name = "Brand")]
        [Required(ErrorMessage = "Brand name is required.")]
        public string CarBrand { get; set; }

        [Display(Name = "Tire count")]
        [Range(0, 16)]
        [Required(ErrorMessage = "Tire count must be between 0 and 16.")]
        public int TireCount { get; set; }
    }
}
