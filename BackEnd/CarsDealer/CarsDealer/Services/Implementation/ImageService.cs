using CarsDealer.Services.Interfaces;
using System.IO;

namespace CarsDealer.Services.Implementation
{
    public class ImageService : IImageService
    {
        private const string _path = "C:\\Data\\CarsDealer\\Images";
        public void CreateImages(byte[] byteFile, string fileType, int mainEntityImageId)
        {
            var imagePath = _path;

            var fileName = $"{mainEntityImageId}.{fileType}";
            var filePath = Path.Combine(imagePath, fileName);

            using (var streamReader = new MemoryStream(byteFile))
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    var buffer = new byte[4096];
                    var bytesRead = 0;
                    while ((bytesRead = streamReader.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                    }

                    streamReader.Position = 0;
                }
            }

        }

        public void DeleteImages(int mainEntityImageId)
        {
            var imagePath = _path;

            var productImages = Directory.GetFiles(imagePath, $"{mainEntityImageId}.*");
            foreach (var productImage in productImages)
            {
                File.Delete(productImage);
            }
        }

        public byte[] GetImage(int mainEntityImageId, string fileType)
        {
            var imagePath = _path;

            byte[] buffer = null;

            var productImageName = mainEntityImageId + "." + fileType;

            using (FileStream fs = new FileStream($@"{imagePath}\{productImageName}", FileMode.Open, FileAccess.Read))
            {
                buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
            }
            return buffer;
        }
    }
}
