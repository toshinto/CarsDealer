using CarsDealer.Data;
using CarsDealer.DTOS;
using CarsDealer.Enums;
using CarsDealer.Models;
using CarsDealer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CarsDealer.Services.Implementation
{
    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext _db;

        public CarsService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<int> CreateCar(CarCreateRequestModel model)
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

            return car.Id;
        }

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

        public async Task<CarsListDto[]> GetMyCars(string userId)
        {
            return await _db.Cars
               .Where(x => x.UserId == userId)
               .Select(x => new CarsListDto
               {
                   Id = x.Id,
                   Brand = x.Brand
               })
               .ToArrayAsync();
        }
    }
}
