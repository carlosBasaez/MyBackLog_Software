using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD;
using Datos;

namespace Negocio
{
    public static class SerieController
    {
        public static bool insertSerie(Serie serie)
        {
            bool correcto = false;
            DataBase db = new DataBase();
            string nonQuery = $"insert into Serie (id_contenido, tiempo_capitulo,capitulos_temporada,temporada,capitulo ,minuto) values ({serie.Id_contenido},{serie.Tiempo_capitulo},{serie.Capitulos_temporada}," +
                $"{serie.Temporada},{serie.Capitulo},{serie.Minuto})";
            correcto = db.NonQuery(nonQuery);
            return correcto;
        }

        public static bool updateSerie(Serie serie)
        {
            bool correcto = false;
            DataBase db = new DataBase();
            string nonQuery = $"update serie set tiempo_capitulo = {serie.Tiempo_capitulo}, capitulos_temporada = {serie.Capitulos_temporada},temporada = {serie.Temporada},capitulo = {serie.Capitulo}" +
                $" , minuto = {serie.Minuto} where id_contenido = {serie.Id_contenido}";
            correcto = db.NonQuery(nonQuery);
            return correcto;
        }

        public static bool deleteSerie(int index)
        {
            bool correcto = false;
            DataBase db = new DataBase();
            string nonQuery = $"delete from serie where id_contenido = {index}";
            correcto = db.NonQuery(nonQuery);
            return correcto;
        }

        public static List<Serie> listarSerie()
        {
            List<Serie> lista = new List<Serie>();
            DataBase db = new DataBase();
            string query = $"select id_contenido, tiempo_capitulo,capitulos_temporada,temporada,capitulo ,minuto from serie";
            List<object[]> select = db.Select(query);

            if (select != null)
            {
                foreach (var row in select)
                {
                    Serie seri = new Serie();
                    seri.Id_contenido = int.Parse(row[0].ToString());
                    seri.Tiempo_capitulo = int.Parse(row[1].ToString());
                    seri.Capitulos_temporada = int.Parse(row[2].ToString());
                    seri.Temporada = int.Parse(row[3].ToString());
                    seri.Capitulo = int.Parse(row[4].ToString());
                    seri.Minuto = int.Parse(row[5].ToString());
                    lista.Add(seri);
                }
            }

            return lista;
        }

        public static Serie getSerie(int index)
        {
            DataBase db = new DataBase();
            string query = $"select id_contenido, tiempo_capitulo,capitulos_temporada,temporada,capitulo ,minuto from serie where id_contenido = {index} ";
            List<object[]> select = db.Select(query);

            if (select == null || select.Count == 0) return null;

            object[] row = select[0];
            Serie seri = new Serie();
            seri.Id_contenido = int.Parse(row[0].ToString());
            seri.Tiempo_capitulo = int.Parse(row[1].ToString());
            seri.Capitulos_temporada = int.Parse(row[2].ToString());
            seri.Temporada = int.Parse(row[3].ToString());
            seri.Capitulo = int.Parse(row[4].ToString());
            seri.Minuto = int.Parse(row[5].ToString());
            return seri;
        }

        public static bool existSerie(int index)
        {
            return getSerie(index) != null;
        }
    }
}