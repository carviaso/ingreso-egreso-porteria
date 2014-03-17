using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace IngresoEgresoPorteria
{
    class DAOIngreso
    {
        static OleDbConnection conexion;

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
                empleado = null;
            }finally
            {
                ConexionDatos.desconectarAccess();
            }

            return empleado;

        }

        public static Boolean saveIngreso(int id_empleado) 
        { 
            Boolean ingresado = false;

            try
            {
                conexion = ConexionDatos.conectarAccess();
                string sql = "INSERT INTO ingreso_empleado(ID_Empleado, Fecha_Ingreso, Hora_Ingreso)" +
                    "VALUES(@ID_Empleado, @Fecha_Ingreso, @Hora_Ingreso)";
                OleDbCommand cmd = new OleDbCommand(sql, conexion);
                conexion.Open();
                cmd.Parameters.AddWithValue("@ID_Empleado", id_empleado);
                cmd.Parameters.AddWithValue("@Fecha_Ingreso", DateTime.Today.ToShortDateString());
                cmd.Parameters.AddWithValue("@Hora_Ingreso",DateTime.Now.ToShortTimeString());
                cmd.ExecuteNonQuery();
                ingresado = true;
            }
            catch
            {
                IngresoEmpleado.error = "Error!! No se pudo cargar el Alta!!";
            }
            finally {
                ConexionDatos.desconectarAccess();
            }

            return ingresado;
        }

        public static int getIdEmpleado(String num_serie)
        {
            int id_empleado = 0;
        try
        {
            conexion = ConexionDatos.conectarAccess();

                string sql = "SELECT ID FROM empleado" +
                " WHERE empleado.Num_Serie1 = '" + num_serie + "' OR empleado.Num_Serie2 = '" + num_serie + "'";
                OleDbDataAdapter adaptador = new OleDbDataAdapter(sql, conexion);
                DataSet ds = new DataSet();

                conexion.Open();
                adaptador.Fill(ds, "nueva");
                DataRow campo = ds.Tables[0].Rows[0];
                id_empleado = Convert.ToInt32(campo["ID"].ToString());
           }
            catch (Exception)
            {

                IngresoEmpleado.error = "Error en la busqueda!!";
            }finally
            {
                ConexionDatos.desconectarAccess();
            }

            return id_empleado;
        }

        public static DataSet getIngresoEmpleadoAll()
        {

            DataSet ds = new DataSet();
            try
            {
                conexion = ConexionDatos.conectarAccess();

                string sql = "SELECT Nombre, Apellido, Descripcion, Fecha_Ingreso, Hora_Ingreso FROM empleado,site,ingreso_empleado WHERE empleado.ID_Site = site.ID AND ingreso_empleado.ID_Empleado = empleado.ID";
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

        public static Boolean deleteIngresoEmpleado(String nombre, String apellido, String fecha, String hora)
        {
            Boolean eliminado = false;

            try
            {
                int id = DAOIngreso.getIdEmpleadobyNombre(nombre, apellido);
                conexion = ConexionDatos.conectarAccess();
                string sql = "DELETE FROM ingreso_empleado WHERE ID_Empleado = @ID_Empleado AND Fecha_Ingreso = @Fecha_Ingreso AND Hora_Ingreso = @Hora_Ingreso";
                OleDbCommand cmd = new OleDbCommand(sql, conexion);
                conexion.Open();
                cmd.Parameters.AddWithValue("@ID_Empleado", id);
                cmd.Parameters.AddWithValue("@Fecha_Ingreso", fecha);
                cmd.Parameters.AddWithValue("@Hora_Ingreso", hora);

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

        public static Boolean updateIngresoEmpleado(String nombre, String apellido, String fecha, String hora, String fecha_vieja, String hora_vieja)
        {
            Boolean modificado = false;

            try
            {

                int id = DAOIngreso.getIdEmpleadobyNombre(nombre, apellido);
                conexion = ConexionDatos.conectarAccess();
                string sql = "UPDATE ingreso_empleado SET Fecha_Ingreso = @Fecha_Ingreso, Hora_Ingreso = @Hora_Ingreso" +
                              " WHERE ID_Empleado = @ID_Empleado_viejo AND (Fecha_Ingreso = @Fecha_Vieja AND Hora_Ingreso = @Hora_Vieja) ";
                OleDbCommand cmd = new OleDbCommand(sql, conexion);
                conexion.Open();
                cmd.Parameters.AddWithValue("@Fecha_Ingreso", fecha);
                cmd.Parameters.AddWithValue("@Hora_Ingreso", hora);
                cmd.Parameters.AddWithValue("@ID_Empleado_viejo", id);
                cmd.Parameters.AddWithValue("@Fecha_Vieja", fecha_vieja);
                cmd.Parameters.AddWithValue("@Hora_Vieja", hora_vieja);
                
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
        
    }
}
