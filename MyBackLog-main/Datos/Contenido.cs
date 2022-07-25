using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Contenido
    {
        int id_contenido;
        string titulo;
        string descripcion;
        int calificacion;
        int horas_inversion;
        int id_plataforma;
        int id_progresion;
        int id_adquisicion;

        public int Id_contenido { get => id_contenido; set => id_contenido = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int Calificacion { get => calificacion; set => calificacion = value; }
        public int Horas_inversion { get => horas_inversion; set => horas_inversion = value; }
        public int Id_plataforma { get => id_plataforma; set => id_plataforma = value; }
        public int Id_progresion { get => id_progresion; set => id_progresion = value; }
        public int Id_adquisicion { get => id_adquisicion; set => id_adquisicion = value; }

        public override string ToString()
        {
            return $"id contenido: {Id_contenido}, titulo: {Titulo}, descripcion: {Descripcion}, calificacion: {Calificacion}, horas invertidas: {Horas_inversion}, plataforma: {Id_plataforma}, progresion: {Id_progresion}, adquisicion: {Id_adquisicion}";
        }
    }
}
