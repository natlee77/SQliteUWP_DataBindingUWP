using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DataAccess_Library_UWP;
using DataAccess_Library_UWP.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            lvOutput.ItemsSource = DataAccess.GetAllAsync();//POPULERA LIST VIEW NÄR APP STARTAS
        }

        private async  void btbAdd_Click(object sender, RoutedEventArgs e)//vad jag vill göra när jag trycker på knappen
        {
            //skapa 1 newperson , ++til model del

            await DataAccess.AddAsync(tbxInput.Text); //förväntas string
           
           // await DataAccess.AddAsync(new Person { Name = tbxInput.Text }); //förväntas person object - inte string
                                                                            // i add f. skapa person object att kan  uttveckla mer / i den person finns name som jag stoppar in

          
            lvOutput.ItemsSource = DataAccess.GetAllAsync();    //listview -när trycker på knapp --populeras
        }
    }
}
