using System.Threading.Tasks;
using Booking.Common.Encryption;
using Booking.Common.Storage.Abstractions;

namespace Booking.Common.Storage.Encryption
{
    public class EncryptedStorage : IStorage<string>
    {
        private readonly IStorage<byte[]> _storage;
        private readonly IEncryptor _crypto;

        public EncryptedStorage(IStorage<byte[]> storage, IEncryptor crypto)
        {
            this._storage = storage;
            this._crypto = crypto;
        }

        public async Task<string> DownloadFileAsync()
        {
            var file = await _storage.DownloadFileAsync();
            return _crypto.Decrypt(file);
        }

        public Task UploadFileAsync(string input)
        {
            return _storage.UploadFileAsync(_crypto.Encrypt(input));
        }
    }
}

