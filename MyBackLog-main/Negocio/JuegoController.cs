using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using BD;

namespace Negocio
{
    public class JuegoController
    {
        public static bool insert(Juego juego)
        {
            DataBase bd = new DataBase();

            string query = $"INSERT INTO juego (id_contenido) VALUES ({juego.Id_contenido})";

            bool resultado = bd.NonQuery(query);

            return resultado;
        }

        public static bool delete(int id_juego)
        {
            DataBase bd = new DataBase();

            string query = $"DELETE FROM juego WHERE id_contenido = {id_juego}";

            bool resultado = bd.NonQuery(query);

            return resultado;
        }

        public static Juego get(int id_juego)
        {
            DataBase bd = new DataBase();

            string query = $"SELECT id_contenido FROM juego WHERE id_contenido = {id_juego}";

            List<object[]> select = bd.Select(query);

            if (select == null || select.Count == 0) return null;

            Juego juego = new Juego();
            juego.Id_contenido = int.Parse(select[0][0].ToString());

            return juego;
        }

        public static List<Juego> listaJuegos()
        {
            List<Juego> lista = new List<Juego>();

            DataBase bd = new DataBase();

            string query = $"SELECT id_contenido FROM juego";

            List<object[]> select = bd.Select(query);

            if (select == null || select.Count == 0) return lista;

            foreach (var row in select)
            {
                Juego juego = new Juego();
                juego.Id_contenido = int.Parse(row[0].ToString());
                lista.Add(juego);
            }

            return lista;
        }

        public static bool exist(int id)
        {
            return get(id) != null;
        }

        public static bool update(int id_contenido)
        {
            return true;
        }
    }
}
