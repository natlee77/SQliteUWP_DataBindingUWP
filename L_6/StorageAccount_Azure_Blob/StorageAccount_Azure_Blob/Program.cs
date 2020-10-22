using StorageAccount_Azure_Blob.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace StorageAccount
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=blobazurewin20;AccountKey=YY53jnU4/8Lcr92qy0P3rcHW9X4XZllm7KGRy8eqHx44W6bqJs17Oovab3rr2Os1xaAndi7eGEqC+DcgIEiafQ==;EndpointSuffix=core.windows.net";
            var containerName = "BLOB";

            Console.WriteLine("Initializing Storage Account with containerName: " + containerName);
            await StorageService.InitializeStorageAsync(connectionString, containerName);//kan .Awaiter/Get.Awaiter/Task.Run



            var fileName = $"myfile-{Guid.NewGuid()}.txt";// ska ha unik namn
            var content = "This is the content of the file";
            var filePath = Path.Combine(@"D:\Blob\download\", fileName);    

            Console.WriteLine("Creating and writing content in file: " + filePath);
            await StorageService.WriteToFileAsync(filePath, content);



            //var downloadPath = Path.Combine(@"d:\WIN20\Downloads\", fileName);

            //Console.WriteLine("Uploading file to Azure Storage Blob in container: " + containerName);
            //await StorageService.UploadFileAsync(filePath);

            //Console.WriteLine("Downloading file from Azure Storage Blob to: " + Path.GetDirectoryName(downloadPath));
            //await StorageService.DownloadFileAsync(downloadPath);

            //Console.WriteLine("Reading content from file: " + downloadPath);
            //Console.WriteLine(await StorageService.ReadDownloadedFileAsync(downloadPath));


            //Console.WriteLine("Initializing Storage Account with containerName: " + containerName);
            //await StorageService.InitializeStorageAsync(connectionString, containerName);

            //Console.WriteLine("Uploading file to Azure Storage Blob in container: " + containerName);
            //await StorageService.UploadFileAsync(@"d:\WIN20\slide1.jpg");
        }
    }
}
