using CarsDealer.DTOS;

namespace CarsDealer.Services.Interfaces
{
    public interface INotificationService
    {
        void AddNotification(string userId, string message);
        NotificationListDto[] GetMyNotifications(string userId);
    }
}
