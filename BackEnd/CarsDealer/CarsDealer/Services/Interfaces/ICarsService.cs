using CarsDealer.DTOS;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarsDealer.Services.Interfaces
{
    public interface ICarsService
    {
        Task<int> CreateCar(byte[] fileBytes, CarCreateRequestModel model);
        Task<CarsListDto[]> GetAllCars();
        Task<CarsListDto[]> GetMyCars(string userId);
        CarDetailsDto GetCarDetails(int carId);
        bool DeleteCar(int carId, string userId);
        bool UpdateCar(CarUpdateRequestModel model, string userId);
        ApproveDisapproveDto ApproveCar(int carId);
        ApproveDisapproveDto DisapproveCar(int carId);
        Task<AdminCarListDto[]> AdminCars();
        CarUpdateDetailsDto GetCarUpdateDetails(int carId);
        void DeleteCarByAdmin(int carId);

    }
}
