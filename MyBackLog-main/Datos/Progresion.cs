using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Progresion
    {
        int id_progresion;
        string nombre_estado;
        string descripcion;

        public int Id_progresion { get => id_progresion; set => id_progresion = value; }
        public string Nombre_estado { get => nombre_estado; set => nombre_estado = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }

        public override string ToString()
        {
            return $"id progresion: {Id_progresion}, nombre estado: {Nombre_estado}, descripcion: {Descripcion}";
        }
    }
}
