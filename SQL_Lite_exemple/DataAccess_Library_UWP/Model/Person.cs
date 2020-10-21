using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Library_UWP.Model
{
    public class Person
    {
        public Person()
        {

        }

        public Person(string firstname, string lastname, DateTime dateofbirth)
        {
            FirstName = firstname;
            LastName = lastname;
            DateOfBirth  = dateofbirth;
        }

        public int Id { get; private set; } //private set -kan hämta , men inte sätta (måste använda ctor att sätta värde)
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string DisplayName => $"{ FirstName }  {LastName}";
        public string Summary => $"{ DisplayName }  {DateOfBirth:G}";
    }



    public class PersonViewModel
    {//view model använda att hämta ut och visa saker// vi vill ha  lista 
     //lista - kan använda
        public ObservableCollection<Person> People { get; } = new ObservableCollection<Person>();// gör new instance- var fel-not sätt instance object
       
        
        public PersonViewModel()//skapa lista i ctor -kan hämta från DB här-utan DATAACCESS DELEN 
        {//populera
            People.Add(new Person("Nataliya", "Lisjö", new DateTime(1971, 06, 06)));   //dataformat year/month/day
            People.Add(new Person("Max", "Lisjö", new DateTime(2017, 11, 04)));
        }

        public void RemoveItem(Person person) //delete knapp f.
        {
            People.Remove(person);
        }

        public void AddItem(Person person)
        {
            People.Add(person);
        }
    } //=> till grafiska mainpage

}  /*vi ska bygga MVVM  view model utav det 
    * --finns:
    * MVC -- ASP.net--model view Controler,
    *MVVM --- Design ---model View Viewmodel    
    
    vi har Person class   ----skapa i den fil class PersonViewModel

    */

