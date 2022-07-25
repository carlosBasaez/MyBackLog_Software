using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using BD;

namespace Negocio
{
    public class NotaController
    {
        public static bool insert(Nota nota)
        {
            DataBase bd = new DataBase();
            string nonquery = $"INSERT INTO nota (id_juego, descripcion, completado) VALUES ({nota.Id_contenido}, '{nota.Descripcion}', {(nota.Completado?1:0)})";
            bool respusta = bd.NonQuery(nonquery);
            return respusta;
        }

        public static bool update(Nota nota)
        {
            DataBase bd = new DataBase();
            string nonquery = $"UPDATE nota SET descripcion = '{nota.Descripcion}', completado = {(nota.Completado?1:0)} WHERE id_nota = {nota.Id_nota} and id_juego = {nota.Id_contenido}";
            bool respusta = bd.NonQuery(nonquery);
            return respusta;
        }

        public static bool delete(int id_nota, int id_juego)
        {
            DataBase bd = new DataBase();
            string nonquery = $"DELETE FROM nota WHERE id_nota = {id_nota} and id_juego = {id_juego}";
            bool respusta = bd.NonQuery(nonquery);
            return respusta;
        }

        public static Nota get(int id_nota, int id_juego)
        {
            DataBase bd = new DataBase();

            string query = $"SELECT id_nota, id_juego, descripcion, completado FROM nota WHERE id_nota = {id_nota} and id_juego = {id_juego}";

            List<object[]> select = bd.Select(query);

            if (select == null || select.Count == 0) return null;

            Nota nota = new Nota();
            nota.Id_nota = int.Parse(select[0][0].ToString());
            nota.Id_contenido = int.Parse(select[0][1].ToString());
            nota.Descripcion = select[0][2].ToString();
            nota.Completado = select[0][3].ToString() == "1";

            return nota;
        }

        public static List<Nota> listar(int id_juego)
        {
            List<Nota> lista = new List<Nota>();

            DataBase bd = new DataBase();

            string query = $"SELECT id_nota, id_juego, descripcion, completado FROM nota WHERE id_juego = {id_juego}";

            List<object[]> select = bd.Select(query);

            if (select == null || select.Count == 0) return lista;

            foreach (var row in select)
            {
                Nota nota = new Nota();
                nota.Id_nota = int.Parse(row[0].ToString());
                nota.Id_contenido = int.Parse(row[1].ToString());
                nota.Descripcion = row[2].ToString();
                nota.Completado = row[3].ToString() == "1";
                lista.Add(nota);
            }

            return lista;
        }

        public static bool exist(int id_nota, int id_juego)
        {
            return get(id_nota, id_juego) != null;
        }
    }
}
