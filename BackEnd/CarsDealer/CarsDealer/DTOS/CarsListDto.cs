namespace CarsDealer.DTOS
{
    public class CarsListDto
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; }
        public string Description { get; set; }
        public string Fuel { get; set; }
        public string GearLever { get; set; }
        public string ImageFileType { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }
        public string ImageBase64 { get; set; }
    }
}
