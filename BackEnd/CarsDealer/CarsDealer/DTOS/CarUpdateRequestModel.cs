using System.ComponentModel.DataAnnotations;
using static CarsDealer.Data.Validation.Car;


namespace CarsDealer.DTOS
{
    public class CarUpdateRequestModel
    {
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public byte Fuel { get; set; }

        [Required]
        public byte GearLever { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Color { get; set; }

        public string ImageFileType { get; set; }
    }
}
