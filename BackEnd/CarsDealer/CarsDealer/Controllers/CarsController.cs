using CarsDealer.Data;
using CarsDealer.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CarsDealer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CarsController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet("Cars")]
        public async Task<CarsListDto[]> GetAllCars()
        {
            return await _db.Cars
                .Select(x => new CarsListDto
                {
                    Id = x.Id,
                    Brand = x.Brand
                })
                .ToArrayAsync();
        }
    }
}
