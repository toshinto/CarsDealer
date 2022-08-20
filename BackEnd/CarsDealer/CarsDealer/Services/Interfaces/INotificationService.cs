
using CarsDealer.DTOS;
using CarsDealer.Models;

namespace CarsDealer.Services.Interfaces
{
    public interface INotificationService
    {
        void SendNotification(string senderId, string receiverId, Car car, decimal price);
        void AcceptOffer(int id);
        void DeclineOffer(int id);
        GetNotificationDto[] GetNotification(string userId);
    }
}
