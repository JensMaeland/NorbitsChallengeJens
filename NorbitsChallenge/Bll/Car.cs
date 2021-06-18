using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorbitsChallenge.Bll
{
    //Database model
    public class Car
    {
        public int CompanyId { get; set; }
        public string LicensePlate { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int? TireCount { get; set; }
        override public string ToString()
        {
            return String.Format("License Plate : {0}, Description : {1} Model : {2}, Brand : {3}, Tire Count : {4}",
                                  LicensePlate,        Description,      Model,       Brand,       TireCount);
        }
    }
}
