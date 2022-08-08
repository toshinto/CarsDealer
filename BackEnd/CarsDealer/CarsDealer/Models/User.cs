using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CarsDealer.Models
{
    public class User : IdentityUser
    {
        public IEnumerable<Car> Cars { get; } = new HashSet<Car>();
    }
}
