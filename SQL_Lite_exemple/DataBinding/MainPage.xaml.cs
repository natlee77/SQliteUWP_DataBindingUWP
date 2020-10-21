using DataAccess_Library_UWP.Model;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DataBinding
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {  // ska vi skapa viewmodel

        public PersonViewModel ViewModel { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = new PersonViewModel();// när skapa den , då från ctor i class PersonViewModel-skapas lista automatisk därför den lista i ctor och den hämtas från DB
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {//mappa remove knapp del
            var context = ((FrameworkElement)sender).DataContext;
            (DataContext as PersonViewModel)?.RemoveItem((Person)context);// ++removeItem  f.i PersonVM

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var context = ((FrameworkElement)sender).DataContext;
            (DataContext as PersonViewModel)?.AddItem((Person)context);// ++addItem f. i PersonVM
        }
    }
}
