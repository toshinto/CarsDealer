namespace CarsDealer.Services.Interfaces
{
    public interface IImageService
    {
        void CreateImages(byte[] byteFile, string fileType, int mainEntityImageId);
        byte[] GetImage(int mainEntityImageId, string fileType);
        void DeleteImages(int value);
    }
}
