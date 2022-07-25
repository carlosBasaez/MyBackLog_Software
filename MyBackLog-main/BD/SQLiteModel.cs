using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BD
{
    public static class SQLiteModel
    {
        public static string path_import = "./exportedDB.sql";

        public static void CrearModelo(BD.DataBase db)
        {
            Import(db);
        }

        /// <summary>
        /// Carga un archivo de creacion .sql en la base de datos sqlite3
        /// </summary>
        /// <param name="db"></param>
        public static void Import(BD.DataBase db)
        {
            if (!File.Exists(path_import))
            {
                Console.WriteLine(">> ERROR: Exported SQL not found");
                return;
            }

            try
            {
                //Leer archivo creador (exportacion)
                string import = File.ReadAllText(path_import);

                //Load in db import file
                db.LoadStatement(import);
            }
            catch(Exception ex)
            {
                Console.WriteLine(">> EXCEPTION: Error en la lectura de datos\nInfo: " + ex.Message);
            }
        }
    }
}
