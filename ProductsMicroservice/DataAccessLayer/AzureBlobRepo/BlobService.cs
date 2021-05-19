using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.AzureBlobRepo
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blob;

        public BlobService(BlobServiceClient blob)
        {
            this._blob = blob;
        }
        public Task DeleteBlobAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public  string GetBlobAsync(string name)
        {
            var containerClient = _blob.GetBlobContainerClient("products");
            var blobClient = containerClient.GetBlobClient(name);
            if (blobClient.CanGenerateSasUri)
            {
                BlobSasBuilder sas = new BlobSasBuilder()
                {
                    BlobContainerName = "products",
                    BlobName = name,
                    Resource = "proep"
                };
                sas.ExpiresOn = DateTimeOffset.UtcNow.AddHours(1);
                sas.SetPermissions(BlobSasPermissions.Read);
                return blobClient.GenerateSasUri(sas).AbsoluteUri;
            }
            return "";
            
        }

        public async Task UploadBlobAsync(Stream content, string contentType, string fileName)
        {
            var containerClient = _blob.GetBlobContainerClient("products");
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(content, new BlobHttpHeaders { ContentType = contentType });
        }
    }
}
