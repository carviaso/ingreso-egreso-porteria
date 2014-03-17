using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace IngresoEgresoPorteria
{
    public partial class ListadoEgresoEmpleados : Form
    {
        private static DataGridViewPrinter MyDataGridViewPrinter;

        public ListadoEgresoEmpleados()
        {
            InitializeComponent();
        }

        private void tsbtExportar_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            tslbError.Text = "Comenzando la exportación...Espere";
            tsProgreso.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tsProgreso.Value = tsProgreso.Value + 2;

            if (tsProgreso.Value >= tsProgreso.Maximum)
            {
                timer1.Enabled = false;
                Herramientas.exportarAExcel(dgvEgresoEmpleados);
                tslbError.Text = "";
                tsProgreso.Value = 0;
                tsProgreso.Visible = false;
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            DialogResult resp = MessageBox.Show("Se eliminará el registro de la base de datos" + (char)13 + "¿Desea continuar?", "Notificación", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (resp == DialogResult.Yes)
            {
                tslbError.Text = "Eliminando registro...";
                String nombre = dgvEgresoEmpleados.CurrentRow.Cells["Nombre"].Value.ToString();
                String apellido = dgvEgresoEmpleados.CurrentRow.Cells["Apellido"].Value.ToString();
                String fecha = dgvEgresoEmpleados.CurrentRow.Cells["Fecha_Egreso"].Value.ToString();
                String hora = dgvEgresoEmpleados.CurrentRow.Cells["Hora_Egreso"].Value.ToString();
                Boolean eliminado = DAOEgreso.deleteEgresoEmpleado(nombre, apellido, fecha, hora);
                if (eliminado)
                {
                    tslbError.Text = "Registro Eliminado!";
                    cargarData();
                }
                else
                {
                    tslbError.Text = "Error!! No se pudo eliminar el registro";
                }
            }
        }

        private void cargarData()
        {
            DataSet ds = DAOEgreso.getEgresoEmpleadoAll();
            if (ds != null)
            {
                bindingSource1.DataSource = ds.Tables[0];
                bindingNavigator1.BindingSource = bindingSource1;
                dgvEgresoEmpleados.DataSource = bindingSource1;
                dgvEgresoEmpleados.Columns[0].HeaderText = "Nombre";
                dgvEgresoEmpleados.Columns[1].HeaderText = "Apellido";
                dgvEgresoEmpleados.Columns[2].HeaderText = "Planta";
                dgvEgresoEmpleados.Columns[3].HeaderText = "Fecha de Egreso";
                dgvEgresoEmpleados.Columns[4].HeaderText = "Hora de Egreso";
                dgvEgresoEmpleados.Columns[4].DefaultCellStyle.Format = "t";

                tscmbFiltro.Text = "Filtrar por...";
                for (int i = 0; i <= dgvEgresoEmpleados.Columns.Count-1; i++)
                {
                    tscmbFiltro.Items.Add(dgvEgresoEmpleados.Columns[i].HeaderText);
                }

            }
            else
            {
                tslbError.Text = "Error en la carga!!";
            }


            tsProgreso.Visible = false;
            tsProgreso.Minimum = 0;
            tsProgreso.Maximum = 100;
            timer1.Interval = 100;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "EgresoEmpleado").SingleOrDefault<Form>();

            if (existe != null)
            {

                existe.WindowState = FormWindowState.Normal;

            }
            else
            {
                DialogResult resp = MessageBox.Show("¿Desea agregar un nuevo registro?", "Notificación", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (resp == DialogResult.Yes)
                {
                    EgresoEmpleado egreso = new EgresoEmpleado();
                    egreso.FormClosed += new System.Windows.Forms.FormClosedEventHandler(egreso_FormClosed);
                    egreso.Show();

                }
            }
        }

        private void egreso_FormClosed(object sender, FormClosedEventArgs e)
        {

            cargarData();

        }

        private void tsbtnModificar_Click(object sender, EventArgs e)
        {
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "ModificaEgresoEmpleado").SingleOrDefault<Form>();

            if (existe != null)
            {

                existe.WindowState = FormWindowState.Normal;

            }
            else
            {
                DialogResult resp = MessageBox.Show("¿Desea modificar el registro?", "Notificación", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (resp == DialogResult.Yes)
                {
                    String nombre = dgvEgresoEmpleados.CurrentRow.Cells["Nombre"].Value.ToString();
                    String apellido = dgvEgresoEmpleados.CurrentRow.Cells["Apellido"].Value.ToString();
                    String planta = dgvEgresoEmpleados.CurrentRow.Cells["Descripcion"].Value.ToString();
                    String fecha = dgvEgresoEmpleados.CurrentRow.Cells["Fecha_Egreso"].Value.ToString();
                    String hora = dgvEgresoEmpleados.CurrentRow.Cells["Hora_Egreso"].Value.ToString();


                    ModificaEgresoEmpleado.nombre = nombre;
                    ModificaEgresoEmpleado.apellido = apellido;
                    ModificaEgresoEmpleado.planta = planta;
                    ModificaEgresoEmpleado.fecha = fecha;
                    ModificaEgresoEmpleado.hora = hora;

                    ModificaEgresoEmpleado modifica = new ModificaEgresoEmpleado();
                    modifica.FormClosed += new System.Windows.Forms.FormClosedEventHandler(modifica_FormClosed);
                    modifica.Show();

                }
            }
        }

        private void modifica_FormClosed(object sender, FormClosedEventArgs e)
        {

            cargarData();

        }

        private void ListadoEgresoEmpleados_Load(object sender, EventArgs e)
        {
            cargarData();
        }

        private void tstxtBusqueda_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tstxtBusqueda.Text))
            {

                this.bindingSource1.RemoveFilter();
                return;
            }
            if (tscmbFiltro.Text.Equals("Nombre"))
            {
                    this.bindingSource1.Filter = "Nombre LIKE '%" + this.tstxtBusqueda.Text + "%'";
                }
            
            else if (tscmbFiltro.Text.Equals("Apellido"))
            {
                
                this.bindingSource1.Filter = "Apellido LIKE '%" + this.tstxtBusqueda.Text + "%'";
            }
            else if (tscmbFiltro.Text.Equals("Planta"))
            {
               
                this.bindingSource1.Filter = "Descripcion LIKE '%" + this.tstxtBusqueda.Text + "%'";
            }
            else if (tscmbFiltro.Text.Equals("Fecha de Egreso"))
            {
                
                this.bindingSource1.Filter = "Fecha_Egreso LIKE '%" + this.tstxtBusqueda.Text + "%'";
            }
            else if (tscmbFiltro.Text.Equals("Hora de Egreso"))
            {
                
                this.bindingSource1.Filter = "Hora_Egreso LIKE '%" + this.tstxtBusqueda.Text + "%'";
            }
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (SetupThePrinting())
                printDocument1.Print();
        }

        private void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }

        private bool SetupThePrinting() // procedimientos para configurar la impresion
        {
            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false; // perimitir pagina actual
            MyPrintDialog.AllowPrintToFile = false; //permitir impresion a archivo
            MyPrintDialog.AllowSelection = false; // permitir seleccion
            MyPrintDialog.AllowSomePages = false; //permitir algunas paginas
            MyPrintDialog.PrintToFile = false;// imprimir a archivo
            MyPrintDialog.ShowHelp = false; //mostrar ayuda
            MyPrintDialog.ShowNetwork = false; // mostrar red

            if (MyPrintDialog.ShowDialog() != DialogResult.OK)
                return false;

            printDocument1.DocumentName = "Listado de Empleados"; //nombre del docuemnto
            printDocument1.PrinterSettings = MyPrintDialog.PrinterSettings; // ajustes de impresion
            printDocument1.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings; //ajustes por defecto
            printDocument1.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10); //mergenes de la impresion
            printDocument1.DefaultPageSettings.Landscape = false;

            MyDataGridViewPrinter = new DataGridViewPrinter(dgvEgresoEmpleados, printDocument1, true, true, "Listado de Empleados", new Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point), Color.Black, true);

            return true;
        }

       
    }
}
