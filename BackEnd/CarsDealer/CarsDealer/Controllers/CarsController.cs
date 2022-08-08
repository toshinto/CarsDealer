using CarsDealer.Data;
using CarsDealer.DTOS;
using CarsDealer.Infrastructure.Extensions;
using CarsDealer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly ICarsService carService;

        public CarsController(ApplicationDbContext db, ICarsService carService)
        {
            _db = db;
            this.carService = carService;
        }

        [Authorize]
        [HttpGet("GetAllCars")]
        public async Task<CarsListDto[]> GetAllCars()
        {
            return await carService.GetAllCars();
        }

        [Authorize]
        [HttpGet("GetMyCars")]
        public async Task<CarsListDto[]> GetMyCars()
        {
            var userId = this.User.GetId();

            return await carService.GetMyCars(userId);
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create(CarCreateRequestModel model)
        {
            var userId = this.User.GetId();
            model.UserId = userId;

            return await carService.CreateCar(model);

        }

    }
}
