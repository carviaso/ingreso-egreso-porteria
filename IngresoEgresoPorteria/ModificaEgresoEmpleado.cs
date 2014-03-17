﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IngresoEgresoPorteria
{
    public partial class ModificaEgresoEmpleado : Form
    {
        public ModificaEgresoEmpleado()
        {
            InitializeComponent();
        }

         public static String error;
        public static String nombre;
        public static String apellido;
        public static String planta;
        public static String fecha;
        public static String hora;

        private void tsbtnLimpiar_Click(object sender, EventArgs e)
        {

        }

        private void limpiarFormulario()
        {
            txtApellido.Text = "";
            txtNombre.Text = "";
            txtPlanta.Text = "";
            txtHora.Text = "";
            txtFecha.Text = "";

        }

        private Boolean verificarData()
        {
            Boolean verifacado = false;

            if (txtNombre.Text.Trim().Equals("") || txtApellido.Text.Trim().Equals("")
                || txtFecha.Text.Trim().Equals("") || txtHora.Text.Trim().Equals(""))
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

        private void ModificaEgresoEmpleado_Load(object sender, EventArgs e)
        {
            txtFecha.Text = fecha;
            txtHora.Text = hora;
            txtApellido.Text = apellido;
            txtNombre.Text = nombre;
            txtPlanta.Text = planta;
        }

        private void tlbtnGuardar_Click(object sender, EventArgs e)
        {
            Boolean modificado = false;

            try
            {
                if (!verificarData())
                {
                   
                    modificado = DAOEgreso.updateEgresoEmpleado(txtNombre.Text.Trim(), txtApellido.Text.Trim(),txtFecha.Text.Trim(), txtHora.Text.Trim(), fecha, hora);
                    if (modificado)
                    {
                        limpiarFormulario();
                        tslbError.Text = "Modificado exitosamente!!";

                    }
                    else
                    {
                        tslbError.Text = "Error al realizar la modificación!!";
                    }

                }
            }
            catch (Exception)
            {

                tslbError.Text = "Error al realizar la modificación!!";
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            txtFecha.Text = monthCalendar1.SelectionStart.ToShortDateString();
        }
    }
}
