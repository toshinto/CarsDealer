using CarsDealer.Models;
using System.Threading.Tasks;

namespace CarsDealer.Services.Interfaces
{
    public interface IUserService
    {
        string CurrentUserName(string userName);
        bool isUserInAdminRole(string userId);
    }
}
