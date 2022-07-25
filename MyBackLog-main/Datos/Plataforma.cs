using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Plataforma
    {
        int id_plataforma;
        string titulo;
        string descripcion;

        public int Id_plataforma { get => id_plataforma; set => id_plataforma = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }

        public override string ToString()
        {
            return $"id plataforma: {Id_plataforma}, titulo: {Titulo}, descripcion: {Descripcion}";
        }
    }
}
