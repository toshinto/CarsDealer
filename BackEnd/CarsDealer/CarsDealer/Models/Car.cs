using CarsDealer.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static CarsDealer.Data.Validation.Car;

namespace CarsDealer.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Brand { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Fuel Fuel { get; set; }

        [Required]
        public GearLever GearLever { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Color { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsApproved { get; set; }
        public string ImageFileType { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        public IEnumerable<Notification> Notifications { get; } = new HashSet<Notification>();

    }
}
