using CarsDealer.Data;
using CarsDealer.DTOS;
using CarsDealer.Enums;
using CarsDealer.Models;
using CarsDealer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarsDealer.Services.Implementation
{
    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext _db;
        private readonly IImageService _imageService;
        private readonly IOfferService _offerService;
        private readonly INotificationService _notificationService;

        public CarsService(ApplicationDbContext db, IImageService imageService, IOfferService offerService, INotificationService notificationService)
        {
            _db = db;
            _imageService = imageService;
            _offerService = offerService;
            _notificationService = notificationService;
        }

        public async Task<AdminCarListDto[]> AdminCars()
        {
            var adminCars = _db.Cars
                .Where(x => x.IsApproved == false && x.IsDeleted == false)
                .Select(x => new AdminCarListDto
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model,
                    ImageFileType = x.ImageFileType,
                    CreatedOn = x.CreatedOn,

                })
                .OrderBy(x => x.CreatedOn)
                .ToArrayAsync();

            foreach (var car in adminCars.Result)
            {
                var bytes = _imageService.GetImage(car.Id, car.ImageFileType);

                var imageBase64String = Convert.ToBase64String(bytes);

                car.ImageBase64 = imageBase64String;
            }

            return await adminCars;
        }

        public ApproveDisapproveDto ApproveCar(int carId)
        {
            var car = _db.Cars
                .Where(x => x.Id == carId)
                .FirstOrDefault();

            car.IsApproved = true;

            _db.SaveChanges();

            var message = $"Your car {car.Brand} with model {car.Model} was approved on {DateTime.UtcNow}.";

            _notificationService.AddNotification(car.UserId, message);

            return new ApproveDisapproveDto { State = true };
        }

        public ApproveDisapproveDto DisapproveCar(int carId)
        {
            var car = _db.Cars
                .Where(x => x.Id == carId)
                .FirstOrDefault();

            car.IsApproved = false;

            _db.SaveChanges();

            var message = $"Your car {car.Brand} with model {car.Model} was declined on {DateTime.UtcNow} because of unappropriated picture or description. Please add new one.";

            _notificationService.AddNotification(car.UserId, message);

            return new ApproveDisapproveDto { State = false };

        }

        public async Task<int> CreateCar(byte[] fileBytes, CarCreateRequestModel model)
        {
            var car = new Car()
            {
                Description = model.Description,
                Brand = model.Brand,
                Color = model.Color,
                Fuel = (Fuel)model.Fuel,
                GearLever = (GearLever)model.GearLever,
                ImageFileType = model.ImageFileType,
                IsApproved = false,
                IsDeleted = false,
                Model = model.Model,
                Price = model.Price,
                Year = model.Year,
                UserId = model.UserId,
                City = model.City,
                CreatedOn = DateTime.UtcNow,
                HorsePower = model.HorsePower,
                Kilometeres = model.Kilometeres,
                
            };

            this._db.Add(car);

            await this._db.SaveChangesAsync();
            _imageService.CreateImages(fileBytes, model.ImageFileType, car.Id);

            var message = $"Your car must be approved by Admin.";

            _notificationService.AddNotification(car.UserId, message);

            return car.Id;
        }

        public bool DeleteCar(int carId, string userId)
        {
            var car = _db.Cars
                .Where(x => x.Id == carId)
                .FirstOrDefault();

            if(car.UserId != userId)
            {
                return false;
            }

            car.IsDeleted = true;

            _db.SaveChanges();

            return true;
        }

  

        public CarsListDto[] GetAllCars(string searchTerm = null)
        {
            var cars = _db.Cars
                .Where(x => x.IsDeleted == false && x.IsApproved == true)
                .Select(x => new CarsListDto
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model,
                    Description = x.Description,
                    Fuel = x.Fuel.ToString(),
                    GearLever = x.GearLever.ToString(),
                    Price = x.Price,
                    Year = x.Year,
                    ImageFileType = x.ImageFileType,
                    CreatedOn = x.CreatedOn,
                    HorsePower = x.HorsePower,
                    Kilometeres = x.Kilometeres,
                    
                })
                .OrderByDescending(x => x.CreatedOn)
                .ToArray();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                cars = cars.Where(x => x.Brand.ToLower().Contains(searchTerm.ToLower())).ToArray();
            }

            foreach(var car in cars)
            {
                var bytes = _imageService.GetImage(car.Id, car.ImageFileType);

                var imageBase64String = Convert.ToBase64String(bytes);

                car.ImageBase64 = imageBase64String;
            }

            return cars;
        }

        public CarDetailsDto GetCarDetails(int carId)
        {
            var car = _db.Cars
                .Where(x => x.Id == carId)
                .Select(y => new CarDetailsDto
                {
                    Id = y.Id,
                    Brand = y.Brand,
                    Model = y.Model,
                    Description = y.Description,
                    Fuel = y.Fuel.ToString(),
                    GearLever = y.GearLever.ToString(),
                    Price = y.Price,
                    Year = y.Year,
                    ImageFileType = y.ImageFileType,
                    Color = y.Color,
                    City = y.City,
                    CreatedOn = y.CreatedOn,
                    HorsePower = y.HorsePower,
                    Kilometeres = y.Kilometeres,
                    
                })
                .FirstOrDefault();

            if(car == null)
            {
                return null;
            }

            var bytes = _imageService.GetImage(car.Id, car.ImageFileType);
            var imageBase64String = Convert.ToBase64String(bytes);

            car.ImageBase64 = imageBase64String;

            return  car;

        }

        public CarUpdateDetailsDto GetCarUpdateDetails(int carId)
        {
            var car = _db.Cars
               .Where(x => x.Id == carId)
               .Select(y => new CarUpdateDetailsDto
               {
                   Id = y.Id,
                   Brand = y.Brand,
                   Model = y.Model,
                   Description = y.Description,
                   Fuel = y.Fuel,
                   GearLever = y.GearLever,
                   Price = y.Price,
                   Year = y.Year,
                   ImageFileType = y.ImageFileType,
                   Color = y.Color,
                   City = y.City,
                   HorsePower = y.HorsePower,
                   Kilometeres = y.Kilometeres,


               })
               .FirstOrDefault();

            var bytes = _imageService.GetImage(car.Id, car.ImageFileType);
            var imageBase64String = Convert.ToBase64String(bytes);

            car.ImageBase64 = imageBase64String;

            if (car == null)
            {
                return null;
            }

            return car;
        }

        public async Task<CarsListDto[]> GetMyCars(string userId)
        {
            var cars = _db.Cars
                .Where(x => x.UserId == userId && x.IsDeleted == false && x.IsApproved == true)
                .Select(x => new CarsListDto
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model,
                    Description = x.Description,
                    Fuel = x.Fuel.ToString(),
                    GearLever = x.GearLever.ToString(),
                    Price = x.Price,
                    Year = x.Year,
                    ImageFileType = x.ImageFileType,
                    CreatedOn = x.CreatedOn,
                    HorsePower = x.HorsePower,
                    Kilometeres = x.Kilometeres,

                })
                .OrderByDescending(x => x.CreatedOn)
                .ToArrayAsync();

            foreach (var car in cars.Result)
            {
                var bytes = _imageService.GetImage(car.Id, car.ImageFileType);

                var imageBase64String = Convert.ToBase64String(bytes);

                car.ImageBase64 = imageBase64String;
            }

            return await cars;
        }

        public bool UpdateCar(CarUpdateRequestModel model, string userId)
        {
            var car = _db.Cars
                .Where(x => x.Id == model.Id)
                .FirstOrDefault();

            if(car == null)
            {
                return false;
            }

            if(car.UserId != userId)
            {
                return false;
            }

            if (car.Brand != model.Brand || car.Model != model.Model || car.Color != model.Color || car.Fuel != (Fuel)model.Fuel || car.GearLever != (GearLever)model.GearLever || car.Price != model.Price || car.Year != model.Year || car.Description != model.Description || car.City != model.City || car.Kilometeres != model.Kilometeres || car.HorsePower != model.HorsePower)
            {
                var message = $"Your car must be approved by Admin because you have changed car picture or details";

                _notificationService.AddNotification(car.UserId, message);
                car.IsApproved = false;
            }

            car.Brand = model.Brand;
            car.Model = model.Model;
            car.Color = model.Color;
            car.Fuel = (Fuel)model.Fuel;
            car.GearLever = (GearLever)model.GearLever;
            car.Price = model.Price;
            car.Year = model.Year;
            car.Description = model.Description;
            car.ImageFileType = car.ImageFileType;
            car.City = model.City;
            car.HorsePower = model.HorsePower;
            car.Kilometeres = model.Kilometeres;

            _db.SaveChanges();

            return true;

        }

        public void DeleteCarByAdmin(int carId)
        {
            var car = _db.Cars
                .Where(x => x.Id == carId)
                .FirstOrDefault();

            if(car != null)
            {
                car.IsDeleted = true;
            }

            _db.SaveChanges();
        }

        public bool SendOffer(OfferDto dto, string senderId)
        {
            var car = _db.Cars
                .Where(x => x.Id == dto.Id)
                .FirstOrDefault();

            if(senderId == car.UserId)
            {
                return false;
            }

            _offerService.SendOffer(senderId, car.UserId, car, dto.Price);

            return true;
        }

        public bool UpdateCarWithFile(byte[] fileBytes, CarUpdateRequestModel model, string userId)
        {
            _imageService.DeleteImages(model.Id);

            var car = _db.Cars
                .Where(x => x.Id == model.Id)
                .FirstOrDefault();

            if (car == null)
            {
                return false;
            }

            if (car.UserId != userId)
            {
                return false;
            }

            car.Brand = model.Brand;
            car.Model = model.Model;
            car.Color = model.Color;
            car.Fuel = (Fuel)model.Fuel;
            car.GearLever = (GearLever)model.GearLever;
            car.Price = model.Price;
            car.Year = model.Year;
            car.Description = model.Description;
            car.ImageFileType = model.ImageFileType;
            car.City = model.City;
            car.HorsePower = model.HorsePower;
            car.Kilometeres = model.Kilometeres;
            car.IsApproved = false;

            _db.SaveChanges();

            _imageService.CreateImages(fileBytes, model.ImageFileType, car.Id);

            var message = $"Your car must be approved by Admin because you have changed car picture or details";

            _notificationService.AddNotification(car.UserId, message);

            return true;

        }
    }
}
