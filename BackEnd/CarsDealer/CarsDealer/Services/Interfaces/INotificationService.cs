using CarsDealer.DTOS;

namespace CarsDealer.Services.Interfaces
{
    public interface INotificationService
    {
        void AddNotification(string userId, string message);
        void DeleteNotification(int notificationId);
        NotificationListDto[] GetMyNotifications(string userId);
    }
}
