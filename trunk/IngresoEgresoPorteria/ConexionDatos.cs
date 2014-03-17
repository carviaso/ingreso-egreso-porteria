using System;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IngresoEgresoPorteria
{
    class ConexionDatos
    {
        static string CadenaConexion;
        static OleDbConnection Conex;

        public ConexionDatos()
        {

        }
        public static OleDbConnection conectarAccess()
        {
            try
            {

                String ubicacion = Application.StartupPath + @"\Data\Original\RegistroPersonal.accdb";
                CadenaConexion = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ubicacion + ";Persist Security Info=False";
                Conex = new OleDbConnection(CadenaConexion);

            }
            catch (Exception)
            {
                Conex = null;
                MessageBox.Show("Error de Conexión!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Conex;
        }
        public static void desconectarAccess()
        {
            Conex.Close();
        }
    }
}
