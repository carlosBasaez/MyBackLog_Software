using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD;
using Datos;

namespace Negocio
{
    public static class PeliculaController
    {
        public static bool insertPelicula(Pelicula pelicula)
        {
            bool correcto = false;
            DataBase db = new DataBase();
            string nonQuery = $"insert into pelicula (id_contenido, duracion_minutos, minuto) values ({pelicula.Id_contenido}, {pelicula.Duracion_minutos}, {pelicula.Minuto})";
            correcto = db.NonQuery(nonQuery);
            return correcto;
        }

        public static bool updatePelicula(Pelicula pelicula)
        {
            bool correcto = false;
            DataBase db = new DataBase();
            string nonQuery = $"update pelicula set duracion_minutos = {pelicula.Duracion_minutos}, minuto = {pelicula.Minuto} where id_contenido = {pelicula.Id_contenido}";
            correcto = db.NonQuery(nonQuery);
            return correcto;
        }

        public static bool deletePelicula(int index)
        {
            bool correcto = false;
            DataBase db = new DataBase();
            string nonQuery = $"delete from pelicula where id_contenido = {index}";
            correcto = db.NonQuery(nonQuery);
            return correcto;
        }

        public static List<Pelicula> listarPelicula()
        {
            List<Pelicula> lista = new List<Pelicula>();
            DataBase db = new DataBase();
            string query = $"select id_contenido, duracion_minutos, minuto from pelicula";
            List<object[]> select = db.Select(query);

            if (select != null)
            {
                foreach (var row in select)
                {
                    Pelicula peli = new Pelicula();
                    peli.Id_contenido = int.Parse(row[0].ToString());
                    peli.Duracion_minutos = int.Parse(row[1].ToString());
                    peli.Minuto = int.Parse(row[2].ToString());
                    lista.Add(peli);
                }
            }

            return lista;
        }

        public static Pelicula getPelicula(int index)
        {
            DataBase db = new DataBase();
            string query = $"select id_contenido, duracion_minutos, minuto from pelicula where id_contenido = {index} ";
            List<object[]> select = db.Select(query);

            if (select == null || select.Count == 0) return null;

            object[] row = select[0];
            Pelicula peli = new Pelicula();
            peli.Id_contenido = int.Parse(row[0].ToString());
            peli.Duracion_minutos = int.Parse(row[1].ToString());
            peli.Minuto = int.Parse(row[2].ToString());
            return peli;
        }

        public static bool existPelicula(int index)
        {
            return getPelicula(index) != null;
        }
    }
}
