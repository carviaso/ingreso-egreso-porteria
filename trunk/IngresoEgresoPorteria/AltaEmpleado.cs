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
    public partial class AltaEmpleado : Form
    {
        public static String error = "";

        public AltaEmpleado()
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
            Boolean ingresado = false;

            try
            {
                if (!verificarData())
                {
                    int id_verificado = DAOEmpleado.getIdEmpleadobyNombre(txtNombre.Text.Trim(), txtApellido.Text.Trim());
                    if (id_verificado == 0)
                    {

                        ingresado = DAOEmpleado.saveEmpleado(txtNombre.Text.Trim(), txtApellido.Text.Trim(), cmbPlanta.Text.Trim(), txtModelo.Text.Trim()
                            , txtNumSerie1.Text.Trim(), txtNumSerie2.Text.Trim());
                        if (ingresado)
                        {
                            limpiarForm();
                            tslbError.Text = "Empleado dado de Alta exitosamente!!";

                        }
                        else
                        {
                            tslbError.Text = error;
                        }

                    }
                    else
                    {
                        tslbError.Text = "El empleado ya se encuentra registrado!!";
                    }
                }
            }
            catch (Exception)
            {

                tslbError.Text = error;
            }
            
                    
                }

        private void AltaEmpleado_Load(object sender, EventArgs e)
        {
            List<String> plantas = DAOEmpleado.getPlantas();

            foreach (String planta in plantas)
            {
                cmbPlanta.Items.Add(planta);
            }

            cmbPlanta.Text = plantas.ElementAt(0);
        }
            }
        }

        

