using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Nota
    {
        int id_nota;
        int id_contenido;
        string descripcion;
        bool completado;

        public int Id_nota { get => id_nota; set => id_nota = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public bool Completado { get => completado; set => completado = value; }
        public int Id_contenido { get => id_contenido; set => id_contenido = value; }

        public override string ToString()
        {
            return $"id nota: {Id_nota}, id juego/contenido: {Id_contenido}, descripcion: {Descripcion}, completado: {Completado}";
        }
    }
}
