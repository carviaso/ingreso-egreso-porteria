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
    public partial class ModificaEmpleado : Form
    {
        public static String error;
        public static String num_serie_busqueda;
        public static String nombre;
        public static String apellido;
        public static String site;
        public static String modelo;
        public static String num_serie1;
        public static String num_serie2;

        public ModificaEmpleado()
        {
            InitializeComponent();
        }

        private void tsbtnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarForm();
        }

        private void limpiarForm()
        {
            txtApellido.Text = "";
            txtModelo.Text = "";
            txtNombre.Text = "";
            txtNumSerie1.Text = "";
            txtNumSerie2.Text = "";
            tslbError.Text = "";
            txtNombre.Focus();
        }

        private Boolean verificarData()
        {
            Boolean verifacado = false;

            if (txtNombre.Text.Trim().Equals("") || txtApellido.Text.Trim().Equals("")
                || txtNumSerie1.Text.Trim().Equals(""))
            {
                tslbError.Text = "Datos Incompletos!!";
                verifacado = true;
            }
            else
            {
                verifacado = false;
            }

            return verifacado;
        }

        private void tlbtnGuardar_Click(object sender, EventArgs e)
        {
            Boolean modificado = false;

            try
            {
                if (!verificarData())
                {
                    
                    modificado = DAOEmpleado.updateEmpleado(txtNombre.Text.Trim(), txtApellido.Text.Trim(), cmbPlanta.Text.Trim(), txtModelo.Text.Trim()
                        , txtNumSerie1.Text.Trim(), txtNumSerie2.Text.Trim(),num_serie_busqueda);
                    if (modificado)
                    {
                        limpiarForm();
                        tslbError.Text = "Empleado modificado exitosamente!!";

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

        private void ModificaEmpleado_Load(object sender, EventArgs e)
        {
            List<String> plantas = DAOEmpleado.getPlantas();

            foreach (String planta in plantas)
            {
                cmbPlanta.Items.Add(planta);
            }

            cmbPlanta.Text = site;
            txtApellido.Text = apellido;
            txtModelo.Text = modelo;
            txtNombre.Text = nombre;
            txtNumSerie1.Text = num_serie1;
            txtNumSerie2.Text = num_serie2;
        }

    }
}
