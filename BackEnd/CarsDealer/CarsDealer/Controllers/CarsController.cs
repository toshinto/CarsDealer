using CarsDealer.Data;
using CarsDealer.DTOS;
using CarsDealer.Infrastructure.Extensions;
using CarsDealer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IUserService userService;

        public CarsController(ApplicationDbContext db, ICarsService carService, IUserService userService)
        {
            _db = db;
            this.carService = carService;
            this.userService = userService;
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

        [Authorize]
        [HttpGet("GetUserName")]
        public UserRequestNameDto GetUserName()
        {
            string userName = this.User.GetUserName();

            return new UserRequestNameDto { UserName = userName };
        }

        [Authorize]
        [HttpGet("CheckForAdminRole")]
        public UserAdminDto CheckIfUserIsAdmin()
        {
            var userId = this.User.GetId();
            var isAdmin = this.userService.isUserInAdminRole(userId);

            return new UserAdminDto { IsAdmin = isAdmin };
        }

        [Authorize]
        [HttpPost("Test")]
        public void Test([FromForm]  IFormFile file, string details)
        {
            ;
        }
    }
}
