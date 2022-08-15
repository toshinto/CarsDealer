using CarsDealer.Data;
using CarsDealer.DTOS;
using CarsDealer.Enums;
using CarsDealer.Models;
using CarsDealer.Services.Interfaces;
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

        public async Task<CarsListDto[]> GetAllCars()
        {
            var cars = _db.Cars
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

        public async Task<CarsListDto[]> GetMyCars(string userId)
        {
            var cars = _db.Cars
                .Where(x => x.UserId == userId)
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
    }
}
