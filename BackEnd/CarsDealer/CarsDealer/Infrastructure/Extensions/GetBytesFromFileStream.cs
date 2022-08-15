using Microsoft.AspNetCore.Http;
using System.IO;

namespace CarsDealer.Infrastructure.Extensions
{
    public static class GetBytesFromFileStream
    {
        public static byte[] GetByteFromFileStream(this IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                return fileBytes;
            }
        }
    }
}
