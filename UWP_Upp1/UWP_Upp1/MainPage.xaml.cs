using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;//++
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP_Upp1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        StorageFolder storageFolder;
        StorageFile storageFile;



        public MainPage()
        {
            this.InitializeComponent();
            CreateFileAsync().GetAwaiter();
            //WriteToFileAsync().GetAwaiter();
            WriteTextToFileAsync().GetAwaiter();
            ReadTextFromFileAsync().GetAwaiter();
        }

        private async Task CreateFileAsync()
        {              // ++ Windows.Storage.- på using del

            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                //LocalFolder-catalog som app kör in/anvand, skapa,hämta file hear
                //andra plats att hämta:
                //StorageFolder storageFolder = KnownFolders.DocumentsLibrary;


            

            await storageFolder.CreateFileAsync("my_story.txt", CreationCollisionOption.ReplaceExisting);
                      // StorageFile storageFile = await storageFolder.CreateFileAsync("my_store.txt");//skapa fil-.txt
                      //vi skapad filen i D:\46735\Documents
                      // för att skriva in - behöver hämta fil first from StorageFile:

        }



        //private async Task WriteToFileAsync()//dont work for mig
        //{
        //    StorageFile file = await storageFolder.GetFileAsync("my_story.txt");
        //    await FileIO.WriteTextAsync(file, "hello");
        //}
        private async Task WriteTextToFileAsync()  //spara i windows.storage 
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.CreateFileAsync("my_story.txt", CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(file, "Example of writing a string\r\n");

            // Append a list of strings, one per line, to the file
            var listOfStrings = new List<string> { "line1", "line2", "line3" };
            await FileIO.AppendLinesAsync(file, listOfStrings); // each entry in the list is written to the file on its own line.

        }


        private  async Task ReadTextFromFileAsync()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.GetFileAsync("my_story.txt");

            string text = await FileIO.ReadTextAsync(file); 
        }
    }
}
