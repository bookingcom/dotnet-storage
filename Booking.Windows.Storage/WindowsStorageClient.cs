using System;
using System.IO;
using System.Threading.Tasks;
using Booking.Common.Storage.Abstractions;
using Microsoft.Extensions.Options;

namespace Booking.Windows.Storage
{
    public class WindowsStorageClient: IStorage<byte[]>
    {
        private readonly WindowsStorageSettings _settings;

        public WindowsStorageClient(IOptions<WindowsStorageSettings> settings)
        {
            _settings = settings.Value;
        }
        public Task<byte[]> DownloadFileAsync()
        {
           return Task.FromResult(   File.ReadAllBytes(_settings.Path));
        }

        public Task UploadFileAsync(byte[] input)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_settings.Path));
            File.WriteAllBytes(_settings.Path,input);
            return Task.CompletedTask;
        }
    }
}
