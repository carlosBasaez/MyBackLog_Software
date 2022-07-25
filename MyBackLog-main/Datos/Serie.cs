using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Serie
    {
        int id_contenido;
        int tiempo_capitulo;
        int capitulos_temporada;
        int temporada;
        int capitulo;
        int minuto;

        public int Tiempo_capitulo { get => tiempo_capitulo; set => tiempo_capitulo = value; }
        public int Capitulos_temporada { get => capitulos_temporada; set => capitulos_temporada = value; }
        public int Temporada { get => temporada; set => temporada = value; }
        public int Capitulo { get => capitulo; set => capitulo = value; }
        public int Minuto { get => minuto; set => minuto = value; }
        public int Id_contenido { get => id_contenido; set => id_contenido = value; }

        public override string ToString()
        {
            return $"id contenido: {id_contenido}, duracion capitulo: {Tiempo_capitulo}, capitulos temporada: {Capitulos_temporada}, temporada: {Temporada}, capitulo: {Capitulo}, minuto: {Minuto}";
        }
    }
}
