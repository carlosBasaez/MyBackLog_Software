using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD;
using Datos;

namespace Negocio
{
    public static class LibroController
    {
        public static bool insertLibro(Libro libro)
        {
            bool correcto = false;
            DataBase db = new DataBase();
            string nonQuery = $"insert into libro (id_contenido, cantidad_paginas, pagina) values ({libro.Id_contenido}, {libro.Cantidad_paginas}, {libro.Pagina})";
            correcto = db.NonQuery(nonQuery);
            return correcto;
        }

        public static bool updateLibro(Libro libro)
        {
            bool correcto = false;
            DataBase db = new DataBase();
            string nonQuery = $"update libro set cantidad_paginas = {libro.Cantidad_paginas}, pagina = {libro.Pagina} where id_contenido = {libro.Id_contenido}";
            correcto = db.NonQuery(nonQuery);
            return correcto;
        }

        public static bool deleteLibro(int index)
        {
            bool correcto = false;
            DataBase db = new DataBase();
            string nonQuery = $"delete from libro where id_contenido = {index}";
            correcto = db.NonQuery(nonQuery);
            return correcto;
        }

        public static List<Libro> listarLibro()
        {
            List<Libro> lista = new List<Libro>();
            DataBase db = new DataBase();
            string query = $"select id_contenido, cantidad_paginas, pagina from libro";
            List<object[]> select = db.Select(query);
            
            if (select != null)
            {
                foreach (var row in select)
                {
                    Libro libro = new Libro();
                    libro.Id_contenido = int.Parse(row[0].ToString());
                    libro.Cantidad_paginas = int.Parse(row[1].ToString());
                    libro.Pagina = int.Parse(row[2].ToString());
                    lista.Add(libro);
                }
            }

            return lista;
        }

        public static Libro getLibro(int index)
        {
            DataBase db = new DataBase();
            string query = $"select id_contenido, cantidad_paginas, pagina from libro where id_contenido = {index} ";
            List<object[]> select = db.Select(query);

            if (select == null || select.Count == 0) return null;

            object[] row = select[0];
            Libro libro = new Libro();
            libro.Id_contenido = int.Parse(row[0].ToString());
            libro.Cantidad_paginas = int.Parse(row[1].ToString());
            libro.Pagina = int.Parse(row[2].ToString());
            return libro;
        }

        public static bool existLibro(int index)
        {
            return getLibro(index) != null;
        }
    }
}
