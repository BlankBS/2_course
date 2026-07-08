using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace lab_2
{
    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public int House { get; set; }
        public string Block { get; set; }
        public int FlatNumber { get; set; }
    }

    public class Developer
    {
        public string Name { get; set; }
        public string CompanyType { get; set; }
        public string LegalAddress { get; set; }
        public string INN { get; set; }
    }

    public class Apartament
    {
        public double Area { get; set; }
        public int RoomsCount { get; set; }
        public int Floor { get; set; }
        public int YearBuilt { get; set; }
        public string MaterialType { get; set; }
        
        public bool HasKitchen { get; set; }
        public bool HasBath { get; set; }
        public bool HasToilet { get; set; }
        public bool HasBasement { get; set; }
        public bool HasBalcony { get; set; }

        public string Condition { get; set; }

        public Address ApartamentAddress { get; set; } = new Address();
        public Developer ApartmentDeveloper { get; set; } = new Developer();

        public double CalculatedCost { get; set; }

        public double CalculateCost()
        {
            double basePrice = 1000;
            double total = Area * basePrice;

            if (MaterialType == "Кирпич")
            {
                total *= 1.2;
            }
            if(MaterialType == "Панель")
            {
                total *= 0.9;
            }

            total += RoomsCount * 500;

            if(HasBalcony)
            {
                total += 2000;
            }
            if(HasBasement)
            {
                total += 1500;
            }

            int age = DateTime.Now.Year - YearBuilt;
            total -= age * 100;

            return total;
        }
    }
}
