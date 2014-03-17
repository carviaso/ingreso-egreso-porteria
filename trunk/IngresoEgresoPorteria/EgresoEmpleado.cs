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
    public partial class EgresoEmpleado : Form
    {
        public static String error = "";

        public EgresoEmpleado()
        {
            InitializeComponent();
        }

        private void EgresoEmpleado_Load(object sender, EventArgs e)
        {
            txtNumSerieBusqueda.Focus();
        }

        private void tsbtnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarFormulario();
        }

        private void limpiarFormulario()
        {
            txtNumSerieBusqueda.Text = "";
            txtApellido.Text = "";
            txtModelo.Text = "";
            txtNombre.Text = "";
            txtNumSerie1.Text = "";
            txtNumSerie2.Text = "";
            tslbError.Text = "";
            txtPlanta.Text = "";
            txtNumSerieBusqueda.Focus();
        }

        private void tlbtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int id_empleado = DAOEgreso.getIdEmpleado(txtNumSerieBusqueda.Text.Trim());
                if (id_empleado == 0)
                {
                    tslbError.Text = error;
                }
                else
                {
                    Boolean ingresado = DAOEgreso.saveEgreso(id_empleado);
                    if (ingresado)
                    {
                        limpiarFormulario();
                        tslbError.Text = "Egreso cargado exitosamente!!";
                    }
                    else
                    {
                        tslbError.Text = error;
                    }

                }
            }
            catch (Exception)
            {

                tslbError.Text = error;
            }
        }

        private void txtNumSerieBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            Empleado empleado = new Empleado();

            if (e.KeyChar == (char)13)
            {
                empleado = DAOEgreso.getEmpleadobyNumSerie(txtNumSerieBusqueda.Text.Trim());
                if (empleado == null)
                {
                    limpiarFormulario();
                    tslbError.Text = "No se encuentra ningún Empleado con ese N° de Serie!!";
                }
                else
                {
                    txtApellido.Text = empleado.Apellido;
                    txtModelo.Text = empleado.Modelo;
                    txtNombre.Text = empleado.Nombre;
                    txtNumSerie1.Text = empleado.Num_Serie1;
                    txtNumSerie2.Text = empleado.Num_Serie2;
                    txtPlanta.Text = empleado.Site;

                    tslbError.Text = "Empleado Encontrado!";
                }
            }
        }

        
    }
}
