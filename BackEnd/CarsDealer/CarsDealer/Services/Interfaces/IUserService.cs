using CarsDealer.Models;
using System.Threading.Tasks;

namespace CarsDealer.Services.Interfaces
{
    public interface IUserService
    {
        string GetUserName(string userId);
        bool isUserInAdminRole(string userId);
    }
}
