using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IngresoEgresoPorteria
{
    public class Planta
    {
        private static int id;
        private static String descripcion;

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                Planta.id = value;
            }
        }

        public String Descripcion
        {
            get
            {
                return descripcion;
            }
            set
            {
                Planta.descripcion = value;
            }
        }
    }
}
