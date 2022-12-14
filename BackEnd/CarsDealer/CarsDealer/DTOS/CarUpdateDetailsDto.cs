using CarsDealer.Enums;

namespace CarsDealer.DTOS
{
    public class CarUpdateDetailsDto
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; }
        public string Description { get; set; }
        public Fuel Fuel { get; set; }
        public GearLever GearLever { get; set; }
        public string ImageFileType { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }
        public string ImageBase64 { get; set; }
        public string City { get; set; }
        public string Color { get; set; }
        public int HorsePower { get; set; }
        public int Kilometeres { get; set; }
    }
}
