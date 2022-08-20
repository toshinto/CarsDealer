using CarsDealer.Data;
using CarsDealer.Models;
using CarsDealer.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace CarsDealer.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;

        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string GetUserName(string userId)
        {
            return db.Users
                .Where(x => x.Id == userId)
                .Select(t => t.UserName)
                .FirstOrDefault();
        }

        public bool isUserInAdminRole(string userId)
        {
            var user = db.Users
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            var isTrue = db.UserRoles.Any(x => x.UserId == user.Id && x.RoleId == "1");

            if (isTrue)
            {
                return true;
            }

            return false;
        }

    }
}
