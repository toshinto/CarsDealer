using System;

namespace CarsDealer.DTOS
{
    public class AdminCarListDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ImageFileType { get; set; }
        public string ImageBase64 { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string City { get; set; }
    }
}
