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
    public partial class ListadoIngresoEmpleados : Form
    {
        private static DataGridViewPrinter MyDataGridViewPrinter;

        public ListadoIngresoEmpleados()
        {
            InitializeComponent();
        }

        private void ListadoIngresoEmpleados_Load(object sender, EventArgs e)
        {
            cargarData();
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
                Herramientas.exportarAExcel(dgvIngresoEmpleados);
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
                String nombre = dgvIngresoEmpleados.CurrentRow.Cells["Nombre"].Value.ToString();
                String apellido = dgvIngresoEmpleados.CurrentRow.Cells["Apellido"].Value.ToString();
                String fecha = dgvIngresoEmpleados.CurrentRow.Cells["Fecha_Ingreso"].Value.ToString();
                String hora = dgvIngresoEmpleados.CurrentRow.Cells["Hora_Ingreso"].Value.ToString();
                Boolean eliminado = DAOIngreso.deleteIngresoEmpleado(nombre, apellido, fecha, hora);
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
            DataSet ds = DAOIngreso.getIngresoEmpleadoAll();
            if (ds != null)
            {
                bindingSource1.DataSource = ds.Tables[0];
                bindingNavigator1.BindingSource = bindingSource1;
                dgvIngresoEmpleados.DataSource = bindingSource1;
                dgvIngresoEmpleados.Columns[0].HeaderText = "Nombre";
                dgvIngresoEmpleados.Columns[1].HeaderText = "Apellido";
                dgvIngresoEmpleados.Columns[2].HeaderText = "Planta";
                dgvIngresoEmpleados.Columns[3].HeaderText = "Fecha de Ingreso";
                dgvIngresoEmpleados.Columns[4].HeaderText = "Hora de Ingreso";
                dgvIngresoEmpleados.Columns[4].DefaultCellStyle.Format = "t";

                tscmbFiltro.Text = "Filtrar por...";
                for (int i = 0; i <= dgvIngresoEmpleados.Columns.Count - 1; i++)
                {
                    tscmbFiltro.Items.Add(dgvIngresoEmpleados.Columns[i].HeaderText);
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
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "IngresoEmpleado").SingleOrDefault<Form>();

            if (existe != null)
            {

                existe.WindowState = FormWindowState.Normal;

            }
            else
            {
            DialogResult resp = MessageBox.Show("¿Desea agregar un nuevo registro?", "Notificación", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (resp == DialogResult.Yes)
            {
                
                    IngresoEmpleado ingreso = new IngresoEmpleado();
                    ingreso.FormClosed += new System.Windows.Forms.FormClosedEventHandler(ingreso_FormClosed);
                    ingreso.Show();
                }
            }
        }

        private void ingreso_FormClosed(object sender, FormClosedEventArgs e)
        {

            cargarData();

        }

        private void tsbtnModificar_Click(object sender, EventArgs e)
        {
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "ModificaIngresoEmpleado").SingleOrDefault<Form>();

            if (existe != null)
            {

                existe.WindowState = FormWindowState.Normal;

            }
            else
            {
                DialogResult resp = MessageBox.Show("¿Desea modificar el registro?", "Notificación", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (resp == DialogResult.Yes)
                {
                    String nombre = dgvIngresoEmpleados.CurrentRow.Cells["Nombre"].Value.ToString();
                    String apellido = dgvIngresoEmpleados.CurrentRow.Cells["Apellido"].Value.ToString();
                    String planta = dgvIngresoEmpleados.CurrentRow.Cells["Descripcion"].Value.ToString();
                    String fecha = dgvIngresoEmpleados.CurrentRow.Cells["Fecha_Ingreso"].Value.ToString();
                    String hora = dgvIngresoEmpleados.CurrentRow.Cells["Hora_Ingreso"].Value.ToString();


                    ModificaIngresoEmpleado.nombre = nombre;
                    ModificaIngresoEmpleado.apellido = apellido;
                    ModificaIngresoEmpleado.planta = planta;
                    ModificaIngresoEmpleado.fecha = fecha;
                    ModificaIngresoEmpleado.hora = hora;


                    ModificaIngresoEmpleado modifica = new ModificaIngresoEmpleado();
                    modifica.FormClosed += new System.Windows.Forms.FormClosedEventHandler(modifica_FormClosed);
                    modifica.Show();

                }
            }
        }

        private void modifica_FormClosed(object sender, FormClosedEventArgs e)
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
            else if (tscmbFiltro.Text.Equals("Fecha de Ingreso"))
            {

                this.bindingSource1.Filter = "Fecha_Ingreso LIKE '%" + this.tstxtBusqueda.Text + "%'";
            }
            else if (tscmbFiltro.Text.Equals("Hora de Ingreso"))
            {

                this.bindingSource1.Filter = "Hora_Ingreso LIKE '%" + this.tstxtBusqueda.Text + "%'";
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

            printDocument1.DocumentName = "Listado Ingreso de Empleados"; //nombre del docuemnto
            printDocument1.PrinterSettings = MyPrintDialog.PrinterSettings; // ajustes de impresion
            printDocument1.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings; //ajustes por defecto
            printDocument1.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10); //mergenes de la impresion
            printDocument1.DefaultPageSettings.Landscape = false;

            MyDataGridViewPrinter = new DataGridViewPrinter(dgvIngresoEmpleados, printDocument1,true, true, "Listado Ingreso de Empleados", new Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point), Color.Black, true);

            return true;
        }

        
    }
}
