using BD;
using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PlataformaController
    {
        public static bool insertarPlataforma (Plataforma plataforma)
        {
            DataBase db = new DataBase();
            bool insert;
            string nonQuery = "Insert into plataforma (titulo, descripcion) values ('"+plataforma.Titulo+"', '"+plataforma.Descripcion+"')";
            insert = db.NonQuery(nonQuery);
            return insert;
        }       
        public static bool eliminarPlataforma(int id_plataforma)
        {
            bool delete;
            DataBase db = new DataBase();
            string nonQuery = "Delete from plataforma where id_plataforma = " + id_plataforma;
            delete = db.NonQuery(nonQuery);
            return delete;
        }
        public static bool updatePlataforma(Plataforma plataforma)
        {
            DataBase db = new DataBase();
            bool update;
            string nonQuery = $"Update plataforma set titulo = '{plataforma.Titulo}', descripcion = '{plataforma.Descripcion}' where id_plataforma = {plataforma.Id_plataforma}";
            update = db.NonQuery(nonQuery);
            return update;
        }
        public static List<object[]> verPlataforma()
        {
            DataBase db = new DataBase();
            string nonQuery = "Select id_plataforma, titulo, descripcion from plataforma";
            List<object[]> datos = db.Select(nonQuery);
            return datos;
        }

        public static List<Datos.Plataforma> listaPlataformas()
        {
            List<Datos.Plataforma> lista = new List<Plataforma>();
            List<object[]> lista_codificada = null;
            lista_codificada = verPlataforma();
            if (lista_codificada != null) 
            {
                try
                {
                    foreach (var row in lista_codificada)
                    {
                        Datos.Plataforma plataforma = new Plataforma
                        {
                            Id_plataforma = int.Parse(row[0].ToString()),
                            Titulo = (string)row[1],
                            Descripcion = (string)row[2]
                        };
                        lista.Add(plataforma);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }


            return lista;
        }

        public static Datos.Plataforma getPlataforma(int index)
        {
            DataBase db = new DataBase();

            string query = "Select id_plataforma, titulo, descripcion from plataforma where id_plataforma = " + index;
            List<object[]> lista = db.Select(query);

            if (lista == null || lista.Count == 0) return null;

            Plataforma plataforma = new Plataforma();
            plataforma.Id_plataforma = int.Parse(lista[0][0].ToString());
            plataforma.Titulo = (string)lista[0][1];
            plataforma.Descripcion = (string)lista[0][2];

            return plataforma;
        }

        public static bool existePlataforma(int index)
        {
            return getPlataforma(index) != null;
        }
    }
}
