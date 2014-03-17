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
    public partial class IngresoEmpleado : Form
    {
        public static String error = "";

        public IngresoEmpleado()
        {
            InitializeComponent();
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
            txtNumSerieBusqueda.Focus();
            txtPlanta.Text = "";
        }

        private void IngresoEmpleado_Load(object sender, EventArgs e)
        {
            txtNumSerieBusqueda.Focus();
        }

        private void txtNumSerieBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == (char)13)
            {
                Empleado empleado = null;
                empleado = DAOIngreso.getEmpleadobyNumSerie(txtNumSerieBusqueda.Text.Trim());
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

        private void tlbtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int id_empleado = DAOIngreso.getIdEmpleado(txtNumSerieBusqueda.Text.Trim());
                if (id_empleado == 0)
                {
                    tslbError.Text = error;
                }
                else
                {
                    Boolean ingresado = DAOIngreso.saveIngreso(id_empleado);
                    if (ingresado)
                    {
                         limpiarFormulario();
                         tslbError.Text = "Ingreso cargado exitosamente!!";
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

    }
}
