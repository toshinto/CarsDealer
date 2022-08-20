using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CarsDealer.Models
{
    public class User : IdentityUser
    {
        public IEnumerable<Car> Cars { get; } = new HashSet<Car>();
        public IEnumerable<Notification> SenderNotifications { get; } = new HashSet<Notification>();
        public IEnumerable<Notification> ReceiverNotifications { get; } = new HashSet<Notification>();
    }
}
