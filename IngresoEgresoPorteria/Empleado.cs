using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IngresoEgresoPorteria
{
    public class Empleado
    {
        private static String nombre;
        private static String apellido;
        private static String site;
        private static String modelo;
        private static String num_serie1;
        private static String num_serie2;

        public Empleado()
		{
			
		}
		
		public String Nombre 
        {
			get{
				return nombre;
			}
			set{
				Empleado.nombre = value;
			}
		}

        public String Apellido
        {
            get
            {
                return apellido;
            }
            set
            {
                Empleado.apellido = value;
            }
        }

        public String Site
        {
            get
            {
                return site;
            }
            set
            {
                Empleado.site = value;
            }
        }

        public String Modelo
        {
            get
            {
                return modelo;
            }
            set
            {
                Empleado.modelo = value;
            }
        }

        public String Num_Serie1
        {
            get
            {
                return num_serie1;
            }
            set
            {
                Empleado.num_serie1 = value;
            }
        }

        public String Num_Serie2
        {
            get
            {
                return num_serie2;
            }
            set
            {
                Empleado.num_serie2 = value;
            }
        }
    }
}
