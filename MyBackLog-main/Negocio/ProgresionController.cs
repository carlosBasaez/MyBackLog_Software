using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD;
using Datos;

namespace Negocio
{
    public class ProgresionController
    {
        public static List<Progresion> listarProgresiones()
        {
            List<Progresion> lista = new List<Progresion>();

            DataBase db = new DataBase();

            string query = "select id_progresion, nombre_estado, descripcion from progresion";

            List<object[]> select = db.Select(query);

            if (select != null && select.Count > 0)
            {
                foreach (var row in select)
                {
                    Progresion progresion = new Progresion();
                    progresion.Id_progresion = int.Parse(row[0].ToString());
                    progresion.Nombre_estado = (string)row[1];
                    progresion.Descripcion = (string)row[2];
                    lista.Add(progresion);
                }
            }

            return lista;
        }

        public static Progresion getProgresion(int index)
        {
            DataBase db = new DataBase();

            string query = "select id_progresion, nombre_estado, descripcion from progresion where id_progresion = " + index;

            List<object[]> select = db.Select(query);

            if (select == null || select.Count == 0) return null;

            object[] row = select[0];

            Progresion progresion = new Progresion();
            progresion.Id_progresion = int.Parse(row[0].ToString());
            progresion.Nombre_estado = (string)row[1];
            progresion.Descripcion = (string)row[2];

            return progresion;
        }

        public static bool existProgresion(int index)
        {
            return getProgresion(index) != null;
        }
    }
}
