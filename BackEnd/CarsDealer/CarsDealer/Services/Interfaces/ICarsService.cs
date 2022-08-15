using CarsDealer.DTOS;
using System.Threading.Tasks;

namespace CarsDealer.Services.Interfaces
{
    public interface ICarsService
    {
        Task<int> CreateCar(byte[] fileBytes, CarCreateRequestModel model);
        Task<CarsListDto[]> GetAllCars();
        Task<CarsListDto[]> GetMyCars(string userId);
    }
}
