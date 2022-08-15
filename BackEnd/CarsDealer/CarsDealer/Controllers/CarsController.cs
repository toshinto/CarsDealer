using CarsDealer.Data;
using CarsDealer.DTOS;
using CarsDealer.Infrastructure.Extensions;
using CarsDealer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
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
        private readonly IImageService imageService;

        public CarsController(ApplicationDbContext db, ICarsService carService, IUserService userService, IImageService imageService)
        {
            _db = db;
            this.carService = carService;
            this.userService = userService;
            this.imageService = imageService;
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
        public async Task<ActionResult<int>> Create([FromForm] IFormFile file, [FromForm] string details)
        {
            var userId = this.User.GetId();
            var model = JsonConvert.DeserializeObject<CarCreateRequestModel>(details);
            model.UserId = userId;

            var fileType = file.GetFileType();
            model.ImageFileType = fileType;

            var fileBytes = file.GetByteFromFileStream();

            return await carService.CreateCar(fileBytes, model);

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
        [HttpGet("Test/{id}")]
        public string Test(int id)
        {
            var car = _db.Cars
                .Where(x => x.Id == id)
                .FirstOrDefault();

            var bytes = imageService.GetImage(car.Id, car.ImageFileType);

            var imageBase64String = Convert.ToBase64String(bytes);

            return imageBase64String;
        }
    }
}
