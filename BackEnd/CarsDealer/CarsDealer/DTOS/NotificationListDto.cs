using System;

namespace CarsDealer.DTOS
{
    public class NotificationListDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
