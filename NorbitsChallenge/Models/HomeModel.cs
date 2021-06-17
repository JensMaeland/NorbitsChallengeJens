using NorbitsChallenge.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorbitsChallenge.Models
{
    public class HomeModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int? TireCount { get; set; }
        public List<Car> AllCars{ get; set; }
        public Car searchedCar { get; set; }
    }
}