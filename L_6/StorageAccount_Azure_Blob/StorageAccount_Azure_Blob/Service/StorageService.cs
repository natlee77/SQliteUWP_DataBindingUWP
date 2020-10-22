using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace StorageAccount_Azure_Blob.Services
{
    public static class StorageService  //ska ha olika tjänster 
    {
        private static BlobContainerClient _containerClient { get; set; }
        private static BlobClient _blobClient { get; set; }



        public static async Task InitializeStorageAsync(string connectionString, string containerName, bool uniqueName = false)
        {
            if (uniqueName)
                containerName = $"{containerName}-{Guid.NewGuid()}"; // GUID skapar unik namn till container- varja gång vi generera

            try
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

                try  // försök skapa container , onm det redan finns en
                {
                    _containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
                }
                catch //då
                {
                    try //hämta container som existeras
                    { 
                        _containerClient = blobServiceClient.GetBlobContainerClient(containerName); 
                    }
                    catch { } // kan ha mdl-- nån gick fel
                }
            }
            catch { }
        }  //++ i programm

        public static async Task WriteToFileAsync(string @filePath, string content)// skapa fil med namn , och write in file
        {   // @= D:\Blob\download\
            //  =D:\\Blob\\download\\

            try // måste ha file directory -wheare file ska ligga
            {
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))// OM DET FINNS INTE D:\Blob\download
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath)); //DÅ SKAPA DIRECTORY TILL

                await File.WriteAllTextAsync(filePath, content);
            }
            catch { }
        }// ++I PROGRAMM

        public static async Task UploadFileAsync(string @filePath)
        {
            try
            {
                _blobClient = _containerClient.GetBlobClient(Path.GetFileName(filePath));

                using FileStream fileStream = File.OpenRead(filePath);
                await _blobClient.UploadAsync(fileStream, true);
                fileStream.Close();

                File.Delete(filePath);
            }
            catch { }
        }//ladda upp fil

        public static async Task DownloadFileAsync(string @downloadPath)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(downloadPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(downloadPath));

                BlobDownloadInfo download = await _blobClient.DownloadAsync();

                using FileStream fileStream = File.OpenWrite(downloadPath);
                await download.Content.CopyToAsync(fileStream);

                fileStream.Close();
            }
            catch { }
        }

        public static async Task<string> ReadDownloadedFileAsync(string @downloadPath)
        {
            try
            {
                return await File.ReadAllTextAsync(downloadPath);
            }
            catch { }

            return "";
        }
    }
}
