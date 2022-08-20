
using CarsDealer.DTOS;
using CarsDealer.Models;

namespace CarsDealer.Services.Interfaces
{
    public interface IOfferService
    {
        void SendOffer(string senderId, string receiverId, Car car, decimal price);
        void AcceptOffer(int id);
        void DeclineOffer(int id);
        OfferListDto[] GetOffers(string userId);
    }
}
