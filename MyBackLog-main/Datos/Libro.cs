using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Libro
    {
        int id_contenido;
        int cantidad_paginas;
        int pagina;

        public int Cantidad_paginas { get => cantidad_paginas; set => cantidad_paginas = value; }
        public int Pagina { get => pagina; set => pagina = value; }
        public int Id_contenido { get => id_contenido; set => id_contenido = value; }

        public override string ToString()
        {
            return $"id contenido: {id_contenido}, Paginas totales: {Cantidad_paginas}, Pagina: {Pagina}";
        }
    }
}
