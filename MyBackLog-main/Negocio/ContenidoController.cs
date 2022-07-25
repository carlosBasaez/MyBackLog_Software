using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD;
using Datos;

namespace Negocio
{
    public static class ContenidoController
    {
        public static bool insertContenido(Contenido contenido)
        {
            bool correcto = false;

            DataBase db = new DataBase();
            string nonQuery = 
                $"insert into contenido (titulo, descripcion, calificacion, horas_inversion, id_plataforma, id_progresion, id_adquisicion) " +
                $"values ('{contenido.Titulo}', '{contenido.Descripcion}', {contenido.Calificacion}, {contenido.Horas_inversion}, {contenido.Id_plataforma}, {contenido.Id_progresion}, {contenido.Id_adquisicion})";

            correcto = db.NonQuery(nonQuery);

            return correcto;
        }

        public static bool deleteContenido(int index)
        {
            bool correcto = false;

            DataBase db = new DataBase();
            string nonQuery = "delete from contenido where id_contenido = " +  index;
            try
            {
                correcto = db.NonQuery(nonQuery);
            }
            catch (Exception ex)
            {
                Console.WriteLine(">>>>>>>>>>>>>"+ex);
            }

            return correcto;
        }

        public static bool updateContenido(Contenido contenido)
        {
            bool correcto = false;

            DataBase db = new DataBase();
            string nonQuery =
                $"Update contenido " +
                $"set titulo = '{contenido.Titulo}', descripcion = '{contenido.Descripcion}', calificacion = {contenido.Calificacion}, horas_inversion = {contenido.Horas_inversion}, id_plataforma = {contenido.Id_plataforma}, id_progresion = {contenido.Id_progresion}, id_adquisicion = {contenido.Id_adquisicion} " +
                $"where id_contenido = {contenido.Id_contenido}";

            correcto = db.NonQuery(nonQuery);

            return correcto;
        }

        public static List<Contenido> listarContenido()
        {
            List<Contenido> lista = new List<Contenido>();
            DataBase db = new DataBase();

            string query = "select id_contenido, titulo, descripcion, calificacion, horas_inversion, id_plataforma, id_progresion, id_adquisicion from contenido";
            List<object[]> lista_codificada = db.Select(query);

            if (lista_codificada != null)
                foreach (var row in lista_codificada)
                {
                    Contenido contenido = new Contenido();
                    contenido.Id_contenido = int.Parse(row[0].ToString());
                    contenido.Titulo = (string)row[1];
                    contenido.Descripcion = (string)row[2];
                    contenido.Calificacion = int.Parse(row[3].ToString());
                    contenido.Horas_inversion = int.Parse(row[4].ToString());
                    contenido.Id_plataforma = int.Parse(row[5].ToString());
                    contenido.Id_progresion = int.Parse(row[6].ToString());
                    contenido.Id_adquisicion = int.Parse(row[7].ToString());
                    lista.Add(contenido);
                }

            return lista;
        }

        public static Contenido getContenido(int index)
        {
            DataBase db = new DataBase();

            string query = "select id_contenido, titulo, descripcion, calificacion, horas_inversion, id_plataforma, id_progresion, id_adquisicion from contenido where id_contenido = " + index;
            List<object[]> lista_codificada = db.Select(query);

            if (lista_codificada == null || lista_codificada.Count == 0) return null;

            object[] row = lista_codificada[0];
            Contenido contenido = new Contenido();
            contenido.Id_contenido = int.Parse(row[0].ToString());
            contenido.Titulo = (string)row[1];
            contenido.Descripcion = (string)row[2];
            contenido.Calificacion = int.Parse(row[3].ToString());
            contenido.Horas_inversion = int.Parse(row[4].ToString());
            contenido.Id_plataforma = int.Parse(row[5].ToString());
            contenido.Id_progresion = int.Parse(row[6].ToString());
            contenido.Id_adquisicion = int.Parse(row[7].ToString());
        
            return contenido;
        }

        public static bool exist(int index)
        {
            return getContenido(index) != null;
        }

        public static int LastID()
        {
            DataBase db = new DataBase();

            string query = "select MAX(id_contenido) from contenido";
            List<object[]> lista_codificada = db.Select(query);

            if (lista_codificada == null || lista_codificada.Count == 0) return 0;

            object[] row = lista_codificada[0];
            
            return int.Parse(row[0].ToString());
        }
        public static List<Contenido> Filtro(string where, string tabla = null)
        {
            List<Contenido> listaFiltrada = new List<Contenido>();
            DataBase db = new DataBase();
            string query;
            if (tabla != null)
            {
                query = $"select id_contenido, titulo, descripcion, calificacion, horas_inversion, id_plataforma, id_progresion, id_adquisicion from contenido join {tabla} using(id_contenido) " + where;
            }
            else
            {
                query = $"select id_contenido, titulo, descripcion, calificacion, horas_inversion, id_plataforma, id_progresion, id_adquisicion from contenido {where}";
            }
            List<object[]> lista_codificada = db.Select(query);
            if (lista_codificada != null)
                foreach (var row in lista_codificada)
                {
                    Contenido contenido = new Contenido
                    {
                        Id_contenido = int.Parse(row[0].ToString()),
                        Titulo = (string)row[1],
                        Descripcion = (string)row[2],
                        Calificacion = int.Parse(row[3].ToString()),
                        Horas_inversion = int.Parse(row[4].ToString()),
                        Id_plataforma = int.Parse(row[5].ToString()),
                        Id_progresion = int.Parse(row[6].ToString()),
                        Id_adquisicion = int.Parse(row[7].ToString())
                    };
                    listaFiltrada.Add(contenido);
                }
            return listaFiltrada;
        }
    }
}
