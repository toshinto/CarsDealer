using System.Linq;
using System.Security.Claims;

namespace CarsDealer.Infrastructure.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
            ?.Value;

        public static string GetUserName(this ClaimsPrincipal user)
           => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)
           ?.Value;
    }
}
