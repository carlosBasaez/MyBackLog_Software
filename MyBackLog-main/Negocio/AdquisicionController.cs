using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD;
using Datos;


namespace Negocio
{
    public class AdquisicionController
    {
        public static List<Adquisicion> listaAdquisiciones()
        {
            List<Adquisicion> lista = new List<Adquisicion>();

            DataBase db = new DataBase();

            string query = "select id_adquisicion, nombre_adquisicion from adquisicion";

            List<object[]> select = db.Select(query);

            if (select != null && select.Count > 0)
            {
                foreach (var row in select)
                {
                    Adquisicion adquisicion = new Adquisicion();
                    adquisicion.Id_adquisicion = int.Parse(row[0].ToString());
                    adquisicion.Nombre_adquision = (string)row[1];
                    lista.Add(adquisicion);
                }
            }

            return lista;
        }

        public static Adquisicion getAdquisicion(int index)
        {
            DataBase db = new DataBase();

            string query = "select id_adquisicion, nombre_adquisicion from adquisicion where id_adquisicion = " + index;

            List<object[]> select = db.Select(query);

            if (select == null || select.Count == 0) return null;

            object[] row = select[0];

            Adquisicion adquisicion = new Adquisicion();
            adquisicion.Id_adquisicion = int.Parse(row[0].ToString());
            adquisicion.Nombre_adquision = (string)row[1];
            
            return adquisicion;
        }

        public static bool existAdquisicion(int index)
        {
            return getAdquisicion(index) != null;
        }

    }
}
