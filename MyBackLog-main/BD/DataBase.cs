using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace BD
{
    public class DataBase
    {
        public string nameDB = "myBackLogDataBase.sqlite3";
        public string connectionString = "Data Source=myBackLogDataBase.sqlite3";

        public SQLiteConnection myConnection;

        public DataBase()
        {
            myConnection = new SQLiteConnection(connectionString);
            Console.WriteLine("Start Database" + "\nPath: " + Directory.GetCurrentDirectory());
           

            if (!File.Exists("./" + nameDB))
            {
                try
                {
                    SQLiteConnection.CreateFile(nameDB);
                    BD.SQLiteModel.CrearModelo(this);

                    System.Console.WriteLine(">> Creada la base de datos: " + nameDB + "\nPath: " + Directory.GetCurrentDirectory() + "\\" + nameDB);
                }
                catch(Exception ex)
                {
                    System.Console.WriteLine(">> ERROR: No se ha podido crear la base de datos\n" + ex.Message);
                }
            }
        }

        public void OpenConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }

        public void DeleteDataBase()
        {
            File.Delete("./" + nameDB);
            Console.WriteLine("Delete DataBase");
        }

        public static void DeleteDataBase(string name)
        {

            Console.WriteLine("Eliminando: " + "./" + name);
            if (File.Exists("./" + name))
            {
                File.Delete("./" + name);
                if (!File.Exists("./" + name))
                    Console.WriteLine("Delete DataBase" + name);
                else
                    Console.WriteLine("No se pudo eliminar");
            }
            else
            {
                Console.WriteLine("No existe para eliminar");
            }
        }

        /// <summary>
        /// Carga todas las sentencias que se le ingresen.
        /// Sirve para realizar configuraciones.
        /// </summary>
        /// <param name="sentences"></param>
        public void LoadStatement(string sentences)
        {
            SQLiteCommand command = new SQLiteCommand(sentences, this.myConnection);
            OpenConnection();
            Console.WriteLine(">> DB: LoadStatement > " + (sentences.Length > 100 ? sentences.Substring(0, 100) : sentences) + ".......");
            
            try
            {
                int cambios = command.ExecuteNonQuery(System.Data.CommandBehavior.Default);
                Console.WriteLine("LoadStatement Correcto");
            }
            catch(Exception ex)
            {
                Console.WriteLine(">> EXCEPTION: LoadStatement no ha funcionado\nMessage: " + ex.Message);

            }

            CloseConnection();
        }

        /// <summary>
        /// Retorna los datos de un Select
        /// </summary>
        /// <remarks>No funciona actualmente</remarks>
        /// <param name="sentence"></param>
        /// <returns></returns>
        public List<object[]> Select(string sentence)
        {
            SQLiteCommand command = new SQLiteCommand(sentence, this.myConnection);
            SQLiteDataReader result;
            OpenConnection();
            Console.WriteLine(">> DB: Select > " + (sentence.Length > 100?sentence.Substring(0, 100):sentence) + ".......");

            List<object[]> entrega = null;
            object[] row = null;

            try
            {
                result = command.ExecuteReader();
                int columns = result.FieldCount;
                Console.WriteLine("Columns: " + columns);

                entrega = new List<object[]>();

                while (result.Read())
                {
                    row = new object[columns];
                    for (int pos_y = 0; pos_y < columns; pos_y++)
                    {
                        row[pos_y] = result[pos_y];
                    }
                    entrega.Add(row);
                }

                Console.WriteLine("Select Correcto");
            }
            catch (Exception ex)
            {
                Console.WriteLine(">> EXCEPTION: Select no ha funcionado\nMessage: " + ex.Message);
                entrega = null;
            }

            CloseConnection();
            return entrega;
        }

        /// <summary>
        /// Se utiliza con Insert y Update
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        public bool NonQuery(string sentence)
        {
            int cambios = 0;
            SQLiteCommand command = new SQLiteCommand(sentence, this.myConnection);
            OpenConnection();
            Console.WriteLine(">> DB: NonQuery: " + sentence);
            try
            {
                cambios = command.ExecuteNonQuery(System.Data.CommandBehavior.Default);
                Console.WriteLine("NonQuery Correcto");
            }
            catch (Exception ex)
            {
                Console.WriteLine(">> EXCEPTION: NonQuery no ha funcionado\nMessage: " + ex.Message);
                cambios = 0;
            }

            CloseConnection();

            return cambios > 0; //Devuelve true si ha funcionado correctamente
        }
    }
}

//Guide: https://www.youtube.com/watch?v=anTP-mgktiI&ab_channel=OverSeasMedia
