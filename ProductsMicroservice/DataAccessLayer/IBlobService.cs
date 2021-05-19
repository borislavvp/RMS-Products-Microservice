using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer
{
    public interface IBlobService
    {
        public string GetBlobAsync(string name);

        public Task UploadBlobAsync(Stream content, string contentType, string fileName);

        public Task DeleteBlobAsync(string fileName);
    }
}
