using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public static class Utilidades
    {
        public static void ResetBD()
        {
            BD.DataBase dataBase = new BD.DataBase();
            dataBase.DeleteDataBase();
            dataBase = new BD.DataBase();
        }
    }
}
