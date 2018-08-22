using System;
using System.IO;
using System.Threading.Tasks;
using Booking.Common.Storage.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Booking.Azure.Storage
{
	public class AzureBlobStorageClient : IStorage<byte[]>
	{
		private readonly AzureStorageSettings _settings;

		public AzureBlobStorageClient(IOptions<AzureStorageSettings> settings)
		{
			_settings = settings.Value;
		}

		private async Task<CloudBlockBlob> GetBlobAsync()
		{
			CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_settings.ConnectionString);
			CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
			CloudBlobContainer container = blobClient.GetContainerReference(_settings.Container);
			await container.CreateIfNotExistsAsync();

			return container.GetBlockBlobReference(_settings.Blob);
		}

		public async Task UploadFileAsync(byte[] input)
		{
			var blockBlob = await GetBlobAsync();

			await blockBlob.UploadFromByteArrayAsync(input, 0, input.Length);
		}

		public async Task<byte[]> DownloadFileAsync()
		{
			var blockBlob = await GetBlobAsync();

			using (MemoryStream stream = new MemoryStream())
			{
				await blockBlob.DownloadToStreamAsync(stream);
				return stream.ToArray();
			}
		}
	}
}
