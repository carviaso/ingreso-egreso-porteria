using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IngresoEgresoPorteria
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = SystemInformation.UserName;
        }

        private void empleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "AltaEmpleado").SingleOrDefault<Form>();

            if (existe != null)
            {

                existe.WindowState = FormWindowState.Normal;

            }
            else
            {
                AltaEmpleado altaEmpleado = new AltaEmpleado();
                altaEmpleado.Show();
            }
            
        }

        private void empleadoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "IngresoEmpleado").SingleOrDefault<Form>();

            if (existe != null)
            {

                existe.WindowState = FormWindowState.Normal;

            }
            else
            {
                IngresoEmpleado ingresoEmpleado = new IngresoEmpleado();
                ingresoEmpleado.Show();
            }
           
        }

        private void empleadoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "EgresoEmpleado").SingleOrDefault<Form>();

            if (existe != null)
            {

                existe.WindowState = FormWindowState.Normal;

            }
            else
            {
                EgresoEmpleado egresoEmpleado = new EgresoEmpleado();
                egresoEmpleado.Show();
            }
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "ListadoEmpleados").SingleOrDefault<Form>();

            if (existe != null)
            {

                existe.WindowState = FormWindowState.Normal;

            }
            else
            {
                ListadoEmpleados listado = new ListadoEmpleados();
                listado.Show();
            }
        }

        private void empleadoToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "ListadoIngresoEmpleados").SingleOrDefault<Form>();

            if (existe != null)
            {

                existe.WindowState = FormWindowState.Normal;

            }
            else
            {
                ListadoIngresoEmpleados listadoIngresoEmpleados = new ListadoIngresoEmpleados();
                listadoIngresoEmpleados.Show();
            }
        }

        private void empleadoToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "ListadoEgresoEmpleados").SingleOrDefault<Form>();

            if (existe != null)
            {

                existe.WindowState = FormWindowState.Normal;

            }
            else
            {
                ListadoEgresoEmpleados listadoEgresoEmpleados = new ListadoEgresoEmpleados();
                listadoEgresoEmpleados.Show();
            }
        }

        private void crearBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                Herramientas.crearBackup();
           
        }

        private void recuperarBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Herramientas.recuperarBackup();

        }

       
    }
}
