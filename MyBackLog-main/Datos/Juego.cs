using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Juego
    {
        int id_contenido;

        public int Id_contenido { get => id_contenido; set => id_contenido = value; }

        public override string ToString()
        {
            return $"id contenido: {Id_contenido}";
        }
    }
}
