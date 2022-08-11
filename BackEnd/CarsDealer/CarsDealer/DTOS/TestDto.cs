using Microsoft.AspNetCore.Http;

namespace CarsDealer.DTOS
{
    public class TestDto
    {
        public IFormFile File { get; set; }
        public string Details { get; set; }
    }
}
