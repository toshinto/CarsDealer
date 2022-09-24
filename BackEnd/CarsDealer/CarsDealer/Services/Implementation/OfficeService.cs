using CarsDealer.Data;
using CarsDealer.DTOS;
using CarsDealer.Models;
using CarsDealer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CarsDealer.Services.Implementation
{
    public class OfficeService : IOfferService
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;

        public OfficeService(ApplicationDbContext db, IUserService userService, INotificationService notificationService)
        {
            _db = db;
            _userService = userService;
            _notificationService = notificationService;
        }

        public void AcceptOffer(int id)
        {
            var offer = _db.Offers
                .Where(x => x.Id == id)
                .Include(x => x.Car)
                .FirstOrDefault();

            offer.IsDeleted = true;

            var senderUserName = _userService.GetUserName(offer.SenderId);
            var receiverUserName = _userService.GetUserName(offer.ReceiverId);

            var message = $"{receiverUserName} accept your offer for {offer.Car.Brand} with model {offer.Car.Model} for {offer.Price}lv. on {DateTime.UtcNow}";
            var messageTo = $"You accepted {senderUserName} offer so, your car {offer.Car.Brand} with model {offer.Car.Model} for {offer.Car.Price}lv. will be removed";

            offer.Car.IsDeleted = true;

            _notificationService.AddNotification(offer.SenderId, message);
            _notificationService.AddNotification(offer.ReceiverId, messageTo);

            _db.SaveChanges();
        }

        public void DeclineOffer(int id)
        {
            var offer = _db.Offers
                .Where(x => x.Id == id)
                .Include(x => x.Car)
                .FirstOrDefault();

            offer.IsDeleted = true;

            var senderUserName = _userService.GetUserName(offer.SenderId);
            var receiverUserName = _userService.GetUserName(offer.ReceiverId);

            var message = $"{receiverUserName} reject your offer for {offer.Car.Brand} with model {offer.Car.Model} for {offer.Price}lv. on {DateTime.UtcNow}";

            _notificationService.AddNotification(offer.SenderId, message);

            _db.SaveChanges();
        }

        public OfferListDto[] GetOffers(string userId)
        {
            var offers = _db.Offers
                .Where(x => x.ReceiverId == userId && x.IsDeleted == false)
                .Select(x => new OfferListDto()
                {
                    Id = x.Id,
                    Message = x.Message,
                })
                .ToArray();

            return offers;
        }

        public void SendOffer(string senderId, string receiverId, Car car, decimal price)
        {
            var senderUserName = _userService.GetUserName(senderId);
            var message = $"{senderUserName} send you an offer for {car.Brand} with model {car.Model} for {price}lv. Please accept or decline.";

            var offer = new Offer()
            {
                CarId = car.Id,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                Price = price,
                ReceiverId = receiverId,
                Message = message,
                SenderId = senderId,
            };

            this._db.Add(offer);

            _db.SaveChanges();
        }
    }
}
