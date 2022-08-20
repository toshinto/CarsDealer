using CarsDealer.Data;
using CarsDealer.DTOS;
using CarsDealer.Models;
using CarsDealer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CarsDealer.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserService _userService;

        public NotificationService(ApplicationDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }

        public void AcceptOffer(int id)
        {
            var notification = _db.Notifications
                .Where(x => x.Id == id)
                .Include(x => x.Car)
                .FirstOrDefault();

            notification.IsDeleted = true;

            var senderUserName = _userService.GetUserName(notification.SenderId);
            var receiverUserName = _userService.GetUserName(notification.ReceiverId);

            var message = $"{receiverUserName} accept your offer for {notification.Car.Brand} with model {notification.Car.Model} for {notification.Price} on {DateTime.UtcNow}";

            var acceptNotification = new Notification()
            {
                CarId = notification.CarId,
                Message = message,
                Price = notification.Price,
                ReceiverId = notification.SenderId,
                SenderId = notification.ReceiverId,
                CreatedOn = DateTime.UtcNow,
            };

            _db.Add(acceptNotification);
            _db.SaveChanges();
        }

        public void DeclineOffer(int id)
        {
            var notification = _db.Notifications
                .Where(x => x.Id == id)
                .Include(x => x.Car)
                .FirstOrDefault();

            notification.IsDeleted = true;

            var senderUserName = _userService.GetUserName(notification.SenderId);
            var receiverUserName = _userService.GetUserName(notification.ReceiverId);

            var message = $"{receiverUserName} reject your offer for {notification.Car.Brand} with model {notification.Car.Model} for {notification.Price} on {DateTime.UtcNow}";

            var declineNotification = new Notification()
            {
                CarId = notification.CarId,
                Message = message,
                Price = notification.Price,
                ReceiverId = notification.SenderId,
                SenderId = notification.ReceiverId,
                CreatedOn = DateTime.UtcNow,
            };

            _db.Add(declineNotification);
            _db.SaveChanges();
        }

        public GetNotificationDto[] GetNotification(string userId)
        {
            var notifications = _db.Notifications
                .Where(x => x.ReceiverId == userId)
                .Select(x => new GetNotificationDto()
                {
                    Id = x.Id,
                    Message = x.Message,
                })
                .ToArray();

            return notifications;
        }

        public void SendNotification(string senderId, string receiverId, Car car, decimal price)
        {
            var senderUserName = _userService.GetUserName(senderId);
            var message = $"{senderUserName} send you an offer for {car.Brand} with model {car.Model} for {price}. Please accept or decline.";

            var notification = new Notification()
            {
                CarId = car.Id,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                Message = message,
                Price = price,
                ReceiverId = receiverId,
                SenderId = senderId,
            };

            this._db.Add(notification);

            _db.SaveChanges();
        }
    }
}
