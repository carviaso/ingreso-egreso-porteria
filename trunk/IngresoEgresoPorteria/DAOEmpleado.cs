using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace IngresoEgresoPorteria
{


    public class DAOEmpleado
    {
        static OleDbConnection conexion;

        public static Boolean saveEmpleado(String nombre, String apellido, String site, String modelo, String num_serie1, String num_serie2)
        {
            Boolean ingresado = false;
            
            try
            {
                List<String> plantas = DAOEmpleado.getPlantas();
                int id_site = plantas.IndexOf(site) + 1 ;
                
                conexion = ConexionDatos.conectarAccess();
                string sql = "INSERT INTO empleado(Nombre,Apellido,ID_Site,Modelo_Equipo,Num_Serie1,Num_Serie2)" +
                    "VALUES(@Nombre,@Apellido,@ID_Area,@Modelo_Equipo,@Num_Serie1,@Num_Serie2)";
                OleDbCommand cmd = new OleDbCommand(sql, conexion);
                conexion.Open();
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Apellido", apellido);
                cmd.Parameters.AddWithValue("@ID_Site", id_site);
                cmd.Parameters.AddWithValue("@Modelo_Equipo", modelo);
                cmd.Parameters.AddWithValue("@Num_Serie1", num_serie1);
                cmd.Parameters.AddWithValue("@Num_Serie2", num_serie2);

                cmd.ExecuteNonQuery();
                ingresado = true;
            }
            catch
            {
                AltaEmpleado.error = "Error!! No se pudo cargar el Alta!!";
            }
            finally
            {
                ConexionDatos.desconectarAccess();
            }

            return ingresado;
        }

        public static List<String> getPlantas()
        {
            List<String> plantas = new List<String>();

            try
            {
                conexion = ConexionDatos.conectarAccess();

                string sql = "SELECT ID, Descripcion FROM site";
                OleDbDataAdapter adaptador = new OleDbDataAdapter(sql, conexion);
                DataSet ds = new DataSet();

                conexion.Open();
                adaptador.Fill(ds, "nueva");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow campo = ds.Tables[0].Rows[i];

                    String descripcion = campo["Descripcion"].ToString();
                    plantas.Add(descripcion);

                }

            }
            catch (Exception)
            {

                plantas = null;
            }
            finally
            {
                ConexionDatos.desconectarAccess();
            }

            return plantas;
        }

        public static DataSet getEmpleadosAll()
        {
            DataSet ds = new DataSet();
            try
            {
                conexion = ConexionDatos.conectarAccess();

                string sql = "SELECT Nombre, Apellido, Descripcion, Modelo_Equipo, Num_Serie1, Num_Serie2 FROM empleado,site" +
                " WHERE empleado.ID_Site = site.ID";
                OleDbDataAdapter adaptador = new OleDbDataAdapter(sql, conexion);


                conexion.Open();
                adaptador.Fill(ds);

            }
            catch (Exception)
            {

                ds = null;
            }
            finally
            {
                ConexionDatos.desconectarAccess();
            }

            return ds;


        }

        public static Boolean deleteEmpleado(string num_serie)
        {
            Boolean eliminado = false;

            try
            {
                conexion = ConexionDatos.conectarAccess();
                string sql = "DELETE FROM empleado WHERE Num_Serie1 = @Num_Serie1";
                OleDbCommand cmd = new OleDbCommand(sql, conexion);
                conexion.Open();
                cmd.Parameters.AddWithValue("@Num_Serie1", num_serie);

                cmd.ExecuteNonQuery();
                eliminado = true;
            }
            catch
            {
                eliminado = false;
            }
            finally
            {
                ConexionDatos.desconectarAccess();
            }

            return eliminado;
        }

        public static Boolean updateEmpleado(String nombre, String apellido, String planta, String modelo, String num_serie1,
            String num_serie2, String num_serie_busqueda)
        {
            Boolean modificado = false;

            try
            {
                List<String> plantas = DAOEmpleado.getPlantas();
                int id_site = plantas.IndexOf(planta) + 1;

                conexion = ConexionDatos.conectarAccess();
                string sql = "UPDATE empleado SET Nombre = @Nombre, Apellido = @Apellido, ID_Site = @ID_Site,"+
                " Modelo_Equipo = @Modelo_Equipo, Num_Serie1 = @Num_Serie1, Num_Serie2 = @Num_Serie2" +
                    " WHERE Num_Serie1 = @Num_Serie_Busqueda";
                OleDbCommand cmd = new OleDbCommand(sql, conexion);
                conexion.Open();
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Apellido", apellido);
                cmd.Parameters.AddWithValue("@ID_Site", id_site);
                cmd.Parameters.AddWithValue("@Modelo_Equipo", modelo);
                cmd.Parameters.AddWithValue("@Num_Serie1", num_serie1);
                cmd.Parameters.AddWithValue("@Num_Serie2", num_serie2);
                cmd.Parameters.AddWithValue("@Num_Serie_Busqueda", num_serie_busqueda);
                cmd.ExecuteNonQuery();
                modificado = true;
            }
            catch (Exception)
            {

                modificado = false;
            }
            finally 
            {
            }

            return modificado;
        }

        public static Empleado getEmpleadobyNumSerie(String num_serie)
        {
            Empleado empleado = new Empleado();

            try
            {
                conexion = ConexionDatos.conectarAccess();

                string sql = "SELECT Nombre, Apellido, Descripcion, Modelo_Equipo, Num_Serie1, Num_Serie2 FROM empleado,site" +
                " WHERE empleado.ID_Site = site.ID AND (empleado.Num_Serie1 = '" + num_serie + "' OR empleado.Num_Serie2 = '" + num_serie + "')";
                OleDbDataAdapter adaptador = new OleDbDataAdapter(sql, conexion);
                DataSet ds = new DataSet();

                conexion.Open();
                adaptador.Fill(ds, "nueva");
                DataRow campo = ds.Tables[0].Rows[0];
                empleado.Nombre = campo["Nombre"].ToString();
                empleado.Apellido = campo["Apellido"].ToString();
                empleado.Site = campo["Descripcion"].ToString();
                empleado.Modelo = campo["Modelo_Equipo"].ToString();
                empleado.Num_Serie1 = campo["Num_Serie1"].ToString();
                empleado.Num_Serie2 = campo["Num_Serie2"].ToString();
            }
            catch (Exception)
            {

                IngresoEmpleado.error = "Error en la busqueda!!";
            }
            finally
            {
                ConexionDatos.desconectarAccess();
            }

            return empleado;

        }

        public static int getIdEmpleadobyNombre(String nombre, String apellido)
        {
            int id_empleado = 0;
            try
            {
                conexion = ConexionDatos.conectarAccess();

                string sql = "SELECT ID FROM empleado" +
                " WHERE empleado.Nombre = '" + nombre + "' AND empleado.Apellido = '" + apellido + "'";
                OleDbDataAdapter adaptador = new OleDbDataAdapter(sql, conexion);
                DataSet ds = new DataSet();

                conexion.Open();
                adaptador.Fill(ds, "nueva");
                DataRow campo = ds.Tables[0].Rows[0];
                id_empleado = Convert.ToInt32(campo["ID"].ToString());
            }
            catch (Exception)
            {

                id_empleado = 0;
            }
            finally
            {
                ConexionDatos.desconectarAccess();
            }

            return id_empleado;
        }
    }
}
