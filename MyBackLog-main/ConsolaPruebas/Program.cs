using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ConsolaPruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            
            BD.DataBase.DeleteDataBase("myBackLogDataBase.sqlite3");
            Console.ReadLine();
            BD.DataBase db = new BD.DataBase();


            db.NonQuery("Insert into plataforma values (0, 'Steam', 'PC Master Race')");

            List<object[]> reader = db.Select("Select * from progresion");


            try
            {
                
                    Console.WriteLine("Progesion List");
                foreach (var r in reader)
                {
                    Console.WriteLine("ID: {0} - Nombre: {1} - Descripcion: {2}", r[0], r[1], r[2]);
                }
                 
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(">> EXCEPTION: " + ex.Message);
            }


            Console.Read();
        }
    }
}
