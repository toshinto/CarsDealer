using System;
using System.ComponentModel.DataAnnotations;

namespace CarsDealer.Models
{
    public class Notification
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public User User { get; set; }
    }
}
