using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Adquisicion
    {
        int id_adquisicion;
        string nombre_adquision;

        public int Id_adquisicion { get => id_adquisicion; set => id_adquisicion = value; }
        public string Nombre_adquision { get => nombre_adquision; set => nombre_adquision = value; }

        public override string ToString()
        {
            return $"id progresion: {Id_adquisicion}, nombre adquision: {Nombre_adquision}";
        }
    }
}
