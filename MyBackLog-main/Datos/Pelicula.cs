using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Pelicula
    {
        int id_contenido;
        int duracion_minutos;
        int minuto;

        public int Duracion_minutos { get => duracion_minutos; set => duracion_minutos = value; }
        public int Minuto { get => minuto; set => minuto = value; }
        public int Id_contenido { get => id_contenido; set => id_contenido = value; }

        public override string ToString()
        {
            return $"id contenido: {id_contenido}, Minutos totales: { Duracion_minutos}, Minuto: {Minuto}";
        }
    }
}
