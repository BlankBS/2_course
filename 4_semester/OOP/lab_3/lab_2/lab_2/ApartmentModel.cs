using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace lab_2
{
    public class FutureYearAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is int year)
            {
                return year <= DateTime.Now.Year + 10 && year > 1800;
            }
            return false;
        }
    }

    public class Address
    {
        public string Country { get; set; }
        [Required, RegularExpression(@"^[А-ЯA-Z][а-яa-z]+", ErrorMessage = "Город должен начинаться с большой буквы")]
        public string City { get; set; }
        public string District { get; set; }
        [Required, StringLength(50, MinimumLength = 3)]
        public string Street { get; set; }
        public int House { get; set; }
        public string Block { get; set; }
        public int FlatNumber { get; set; }
    }

    public class Developer
    {
        [Required]
        public string Name { get; set; }
        public string CompanyType { get; set; }
        public string LegalAddress { get; set; }
        [RegularExpression(@"^\d{10}$|^\d{12}$", ErrorMessage = "ИНН должен содержать 10 или 12 цифр")]
        public string INN { get; set; }

    }

    public class Apartament
    {
        [Range(10, 500, ErrorMessage = "Площадь должна быть от 10 до 500 м²")]
        public double Area { get; set; }
        [Range(1, 20, ErrorMessage = "Количество комнат: 1-20")]
        public int RoomsCount { get; set; }
        public int Floor { get; set; }
        [FutureYear(ErrorMessage = "Неверный год постройки")]
        public int YearBuilt { get; set; }
        [Required(ErrorMessage = "Тип материала обязателен")]
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
