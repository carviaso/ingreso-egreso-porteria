using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace IngresoEgresoPorteria
{
   public class Herramientas
    {
        public static void crearBackup()
        {
            string CurrentDatabasePath = Application.StartupPath + @"\Data\Original\RegistroPersonal.accdb";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            String rutaBackup = Application.StartupPath + @"\Data\Backup";
            try
            {
                comprobarDirectorio(rutaBackup);
                fbd.SelectedPath = rutaBackup;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string PathtobackUp = fbd.SelectedPath.ToString();
                    DateTime fecha = DateTime.Now;
                    String sfecha = "";
                    sfecha = Convert.ToString(fecha.ToString("yyyy.MM.dd"));
                    File.Copy(CurrentDatabasePath, PathtobackUp + "\\" + sfecha + ".accdb", true);
                    MessageBox.Show("Copia de Seguridad Exitosa!!", "Exito", MessageBoxButtons.OK);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error al realizar Copia de Seguridad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public static void exportarAExcel(DataGridView dgv)
        {
            String nombreTabla = dgv.Name.Substring(3);
            DateTime fecha = DateTime.Now;
            String sfecha = "";
            sfecha = Convert.ToString(fecha.ToString("yyyy.MM.dd"));
            SaveFileDialog fichero = new SaveFileDialog();
            String rutaExportacion = "";
            switch (nombreTabla)
            {
                case "Empleados":
                    rutaExportacion = Application.StartupPath + @"\Tablas Exportadas\Empleados";
                    break;
                case "Contratistas":
                    rutaExportacion = Application.StartupPath + @"\Tablas Exportadas\Contratistas";
                    break;
                case "IngresoEmpleados":
                    rutaExportacion = Application.StartupPath + @"\Tablas Exportadas\Ingreso";
                    break;
                case "EgresoEmpleados":
                    rutaExportacion = Application.StartupPath + @"\Tablas Exportadas\Egreso";
                    break;
            }

            fichero.Filter = "Excel (*.xls)|*.xls";
            fichero.FileName = nombreTabla + "." + sfecha + ".xls";

            try
            {
                comprobarDirectorio(rutaExportacion);
                fichero.InitialDirectory = rutaExportacion;
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application aplicacion;
                    Microsoft.Office.Interop.Excel.Workbook libro_exportacion = null;
                    Microsoft.Office.Interop.Excel.Worksheet hoja_exportacion;
                    aplicacion = new Microsoft.Office.Interop.Excel.Application();
                    switch (nombreTabla)
                    {
                        case "Empleados":
                            libro_exportacion = aplicacion.Workbooks.Add(Application.StartupPath + @"\Templates\Empleados.xlt");
                            break;
                        case "Contratistas":
                            libro_exportacion = aplicacion.Workbooks.Add(Application.StartupPath + @"\Templates\Contratistas.xltx");
                            break;
                        case "IngresoEmpleados":
                            libro_exportacion = aplicacion.Workbooks.Add(Application.StartupPath + @"\Templates\IngresoEmpleado.xlt");
                            break;
                        case "EgresoEmpleados":
                            libro_exportacion = aplicacion.Workbooks.Add(Application.StartupPath + @"\Templates\EgresoEmpleado.xlt");
                            break;
                    }

                    hoja_exportacion =
                        (Microsoft.Office.Interop.Excel.Worksheet)libro_exportacion.Worksheets.get_Item(1);


                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        hoja_exportacion.Cells[2, i + 1] = dgv.Columns[i].HeaderText;
                    }
                    //Recorremos el DataGridView rellenando la hoja de trabajo
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgv.Columns.Count; j++)
                        {
                            hoja_exportacion.Cells[i + 3, j + 1] = dgv.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                    libro_exportacion.SaveAs(fichero.FileName,
                                             Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                    libro_exportacion.Close(true);
                    aplicacion.Quit();
                    MessageBox.Show("La exportación se realizó correctamente", "Exito", MessageBoxButtons.OK);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("No se pudo realizar la exportación!!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public static void recuperarBackup()
        {
            try
            {
                string PathToRestoreDB = Application.StartupPath + @"\Data\Original\RegistroPersonal.accdb";
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = Application.StartupPath + @"\Data\Backup\";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string Filetorestore = ofd.FileName;

                    // Rename Current Database to .Bak

                    //File.Move(PathToRestoreDB, Filetorestore);

                    //Restore the Databse From Backup Folder
                    File.Copy(Filetorestore, PathToRestoreDB, true);
                    MessageBox.Show("Recuperación exitosa", "Exito", MessageBoxButtons.OK);
                }

            }
            catch (Exception)
            {

                MessageBox.Show("No se pudo realizar la recuperación!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public static void comprobarDirectorio(String ruta)
        {
            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }
        }


    }
}
