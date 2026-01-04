using Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Administracion
{
    public partial class frmEditClientes : Form
    {
        public frmEditClientes()
        {
            InitializeComponent();
            mostrarEstado();
        }

        public Clientes CrearObjeto()
        {
            int id = int.Parse(textBox1.Text);
            string nom = textBox2.Text;
            string ape = textBox3.Text;
            string mail = textBox4.Text;
            string tel = textBox5.Text;
            string dir = textBox6.Text;
            DateTime fn = dateTimePicker1.Value;
            DateTime fr = dateTimePicker2.Value;
            string est = comboBox1.SelectedItem.ToString();

            Clientes oc = new Clientes(
                id,
                nom,
                ape,
                mail,
                tel,
                dir,
                fn,
                fr,
                est
            );

            return oc;
        }


        private void mostrarEstado()
        {
            comboBox1.Items.Add("Activo");
            comboBox1.Items.Add("Inactivo");
        }
        public bool ValidarDatos()
        {
            bool value = true;

            if (textBox1.Text.Trim().Length == 0 &&
                textBox2.Text.Trim().Length == 0 &&
                textBox3.Text.Trim().Length == 0 &&
                comboBox1.SelectedIndex >= 0)
            {
                value = false;
            }

            return value;
        }

        public void Guardar()
        {
            try
            {
                if (ValidarDatos())
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Los campos con (*) son obligatorios");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void setDatos(Clientes oc)
        {
            
            textBox1.Text = oc.ClienteID.ToString();
            textBox2.Text = oc.Nombre;
            textBox3.Text = oc.Apellido;
            textBox4.Text = oc.Email;
            textBox5.Text = oc.Telefono;
            textBox6.Text = oc.Direccion;
            dateTimePicker1.Value = oc.FechaNacimiento;
            dateTimePicker2.Value = oc.FechaRegistro;
            comboBox1.Text = oc.Estado;

        }


        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            Guardar();
            this.Hide();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }
    }


}
