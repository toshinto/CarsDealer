using System;

namespace CarsDealer.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Message { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public int CarId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
        public virtual Car Car { get; set; }
    }
}
