using CarsDealer.Enums;
using System.ComponentModel.DataAnnotations;
using static CarsDealer.Data.Validation.Car;

namespace CarsDealer.DTOS
{
    public class CarCreateRequestModel
    {
        [Required]
        public string Model { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public Fuel Fuel { get; set; }

        [Required]
        public GearLever GearLever { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Color { get; set; }


        [Required]
        public string ImageFileType { get; set; }
        public string UserId { get; set; }
    }
}
