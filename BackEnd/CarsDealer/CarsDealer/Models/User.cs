using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CarsDealer.Models
{
    public class User : IdentityUser
    {
        public IEnumerable<Car> Cars { get; } = new HashSet<Car>();
        public IEnumerable<Offer> SenderOffers { get; } = new HashSet<Offer>();
        public IEnumerable<Offer> ReceiverOffers { get; } = new HashSet<Offer>();
        public IEnumerable<Notification> Notifications { get; } = new HashSet<Notification>();

    }
}
