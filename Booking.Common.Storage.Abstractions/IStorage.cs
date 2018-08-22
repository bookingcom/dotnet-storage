using System;
using System.Threading.Tasks;

namespace Booking.Common.Storage.Abstractions
{

        public interface IStorage<T>
        {
            Task<T> DownloadFileAsync();
            Task UploadFileAsync(T input);
        }
    
}
