using CarsDealer.Data;
using CarsDealer.DTOS;
using CarsDealer.Models;
using CarsDealer.Services.Interfaces;
using System;
using System.Linq;

namespace CarsDealer.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _db;

        public NotificationService(ApplicationDbContext db)
        {
            _db = db;
        }

        public void AddNotification(string userId, string message)
        {
            var notification = new Notification()
            {
                UserId = userId,
                Message = message,
                CreatedOn = DateTime.UtcNow,
            };

            _db.Add(notification);
            _db.SaveChanges();
        }

        public NotificationListDto[] GetMyNotifications(string userId)
        {
            return _db.Notifications
                .Where(x => x.UserId == userId)
                .Select(t => new NotificationListDto
                {
                    Id = t.Id,
                    Message = t.Message,
                    CreatedOn = t.CreatedOn,
                })
                .OrderByDescending(x => x.CreatedOn)
                .ToArray();
        }
    }
}
