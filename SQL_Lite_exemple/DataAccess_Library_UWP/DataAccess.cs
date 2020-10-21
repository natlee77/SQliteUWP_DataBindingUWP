using DataAccess_Library_UWP.Model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DataAccess_Library_UWP
{
    public class DataAccess
    {

        private static readonly string _dbname = "SQLitedb.db";
        private static readonly string _dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, _dbname);//effectivizera  sök vägen

        public static async Task IntializeDatabaseAsync()//skapa DB local fil i system catalog--när app startar
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync(_dbname, CreationCollisionOption.OpenIfExists);//1.skapa fil
           // var dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "SQLitedb.db");// sök vägen/effectiviserad - flyttad upp


            Console.WriteLine($"dbPath={_dbpath}");


            //koppling /! EntetyFramework/med traditionel SQLcode
            using (var db =new SqliteConnection($"Filename={_dbpath}"))
            {
                db.Open();//oppna db for komunisera med db
                var query = "CREATE TABLE IF NOT EXISTS Persons(Id INTEGER PRIMARY KEY, Name NVARCHAR(2048) NULL)";//sql guery// ta borted-- AUTOINCREMENT
                //form SQLcode / 2.skapa tabel/samma som SQL Query i ServerExplore// FLERA GÅNG FOR MÅNGA TABEL//AUTOINCREMENT-Id automatisk öka upp

                var cmd = new SqliteCommand(query, db);//

                //3.kör
                //cmd.ExecuteNonQuery();//utan svar tillbacka
                cmd.ExecuteReader();  //svar flera

                db.Close();
            }
        }


        public static async Task   AddAsync(string input)//++name(text) //ta borted-Person person(object)//
        {
            using (var db = new SqliteConnection($"Filename={_dbpath}")) //oppna/close db 
            {
                db.Open();


                var query = "INSERT INTO Persons(Id, Name) VALUES(NULL,@Name)"; //add , öka automatisk, @-parametr raise query
                var cmd = new SqliteCommand(query , db);//                               
                
                cmd.Parameters.AddWithValue("@Name", input);//mappa parametr -input text
                // cmd.Parameters.AddWithValue("@LastName", person.LastName);

               await  cmd.ExecuteReaderAsync();
                db.Close();
            }
        }

        
        public static IEnumerable<string> GetAllAsync() //hämta/return alla person i db
        {
            var list = new List<string>();//skapa list

            using (var db = new SqliteConnection($"Filename={_dbpath}")) //oppna/close db 
            {
                db.Open();

                var query = "SELECT Name FROM  Persons "; //välja ,*=all
                var cmd = new SqliteCommand(query, db);//                               

                var result =  cmd.ExecuteReader();//spara in var result // här problem i ASYNC 

                while ( result.Read())
                {                    //++ till list
                    list.Add(result.GetString(0));             //o=column   
                }
            
                db.Close();
               return list;//utan den/ fel-not all code path return value
            }
        }


        //public static async Task<Person> GetAsync(int id) //hämta/return 1 person  i db
        //{ 
        //    using (var db = new SqliteConnection($"Filename={_dbpath}")) //oppna/close db 
        //    {
        //        db.Open();

        //        var query = "SELECT * FROM  Persons WHERE Id= @Id "; //välja ,parametr
        //        var cmd = new SqliteCommand(query, db);//                               
        //        cmd.Parameters.AddWithValue("@Id", id);
        //        Person result = (Person)await cmd.ExecuteScalarAsync();//spara result till att be Persoin
               
        //        db.Close();
        //        return result; //1 perosn
        //    }
        //}// kan bygga vidare gör uppdate,delete på den access delen

    }
}
