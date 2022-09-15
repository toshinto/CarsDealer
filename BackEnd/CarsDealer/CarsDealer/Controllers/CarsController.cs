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
        private readonly IOfferService offerService;
        private readonly INotificationService notificationService;

        public CarsController(ApplicationDbContext db, ICarsService carService, IUserService userService, IImageService imageService, IOfferService offerService, INotificationService notificationService)
        {
            _db = db;
            this.carService = carService;
            this.userService = userService;
            this.imageService = imageService;
            this.offerService = offerService;
            this.notificationService = notificationService;
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
            var isValidImageType = file.IsValidImageFile();
            var isValdImageSize = file.ValidateImageSize();

            if (!isValidImageType)
            {
                throw new Exception("File must be jpg, jpeg or png.");
            }

            if (!isValdImageSize)
            {
                throw new Exception("File must be below 1MB");
            }

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
        [HttpGet("CarDetails/{carId}")]
        public CarDetailsDto CarDetails(int carId)
        {
            return carService.GetCarDetails(carId);
        }

        [Authorize]
        [HttpDelete("DeleteCar/{carId}")]
        public bool DeleteCar(int carId)
        {
            var userId = this.User.GetId();
            return carService.DeleteCar(carId, userId);
        }

        [Authorize]
        [HttpPost("UpdateCar")]
        public bool UpdateCar([FromForm] IFormFile file, [FromForm] string details)
        {
            var userId = this.User.GetId();
            var model = JsonConvert.DeserializeObject<CarUpdateRequestModel>(details);

            if (file != null)
            {
                var isValidImageType = file.IsValidImageFile();
                var isValdImageSize = file.ValidateImageSize();

                if (!isValidImageType)
                {
                    throw new Exception("File must be jpg, jpeg or png.");
                }

                if (!isValdImageSize)
                {
                    throw new Exception("File must be below 1MB");
                }

                var fileType = file.GetFileType();
                model.ImageFileType = fileType;

                var fileBytes = file.GetByteFromFileStream();

                return carService.UpdateCarWithFile(fileBytes, model, userId);
            }
            {
                return carService.UpdateCar(model, userId);
            }
        }

        [Authorize]
        [HttpPost("ApproveCar/{carId}")]
        public ApproveDisapproveDto ApproveCar(int carId)
        {
           return carService.ApproveCar(carId);
        }


        [Authorize]
        [HttpPost("DisApproveCar/{carId}")]
        public ApproveDisapproveDto DisApproveCar(int carId)
        {
            return carService.DisapproveCar(carId);
        }

        [Authorize]
        [HttpGet("AdminCars")]
        public Task<AdminCarListDto[]> AdminCars()
        {
            return carService.AdminCars();
        }


        [Authorize]
        [HttpGet("CarUpdateDetails/{carId}")]
        public CarUpdateDetailsDto CarUpdateDetails(int carId)
        {
            return carService.GetCarUpdateDetails(carId);
        }

        [Authorize]
        [HttpDelete("DeleteCarByAdmin/{carId}")]
        public void DeleteCarByAdmin(int carId)
        {
            carService.DeleteCarByAdmin(carId);
        }

        [HttpPost("MakeOffer")]
        public bool MakeOffer(OfferDto dto)
        {
            var userId = this.User.GetId();

            return carService.SendOffer(dto, userId);
        }

        [HttpGet("GetMyOffers")]
        public OfferListDto[] GetMyOffers()
        {
            var userId = this.User.GetId();

            return offerService.GetOffers(userId);
        }

        [HttpPost("AcceptOffer/{offerId}")]
        public void AcceptOffer(int offerId)
        {
            offerService.AcceptOffer(offerId);
        }

        [HttpPost("DeclineOffer/{offerId}")]
        public void DeclineOffer(int offerId)
        {
            offerService.DeclineOffer(offerId);
        }

        [HttpGet("GetMyNotifications")]
        public NotificationListDto[] GetMyNotifications()
        {
            var userId = this.User.GetId();
            return notificationService.GetMyNotifications(userId);
        }
    }
}
