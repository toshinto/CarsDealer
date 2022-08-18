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

        public CarsService(ApplicationDbContext db, IImageService imageService)
        {
            _db = db;
            _imageService = imageService;
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

                })
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

            return new ApproveDisapproveDto { State = true };
        }

        public ApproveDisapproveDto DisapproveCar(int carId)
        {
            var car = _db.Cars
                .Where(x => x.Id == carId)
                .FirstOrDefault();

            car.IsApproved = false;

            _db.SaveChanges();

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
                UserId = model.UserId
            };

            this._db.Add(car);

            await this._db.SaveChangesAsync();
            _imageService.CreateImages(fileBytes, model.ImageFileType, car.Id);

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

  

        public async Task<CarsListDto[]> GetAllCars()
        {
            var cars = _db.Cars
                .Where(x => x.IsDeleted == false)
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
                    
                })
                .ToArrayAsync();

            foreach(var car in cars.Result)
            {
                var bytes = _imageService.GetImage(car.Id, car.ImageFileType);

                var imageBase64String = Convert.ToBase64String(bytes);

                car.ImageBase64 = imageBase64String;
            }

            return await cars;
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
               })
               .FirstOrDefault();

            if (car == null)
            {
                return null;
            }

            return car;
        }

        public async Task<CarsListDto[]> GetMyCars(string userId)
        {
            var cars = _db.Cars
                .Where(x => x.UserId == userId && x.IsDeleted == false)
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

                })
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

            car.Brand = model.Brand;
            car.Model = model.Model;
            car.Color = model.Color;
            car.Fuel = (Fuel)model.Fuel;
            car.GearLever = (GearLever)model.GearLever;
            car.Price = model.Price;
            car.Year = model.Year;
            car.Description = model.Description;

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
    }
}
