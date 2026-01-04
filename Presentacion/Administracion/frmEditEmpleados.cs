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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Presentacion.Administracion
{
    public partial class frmEditEmpleados : Form
    {
        public frmEditEmpleados()
        {
            InitializeComponent();
            mostrarEstado();
        }

        public Empleados CrearObjeto()
        {
            int id = int.Parse(textBox1.Text);
            string nombre = textBox2.Text;
            string apellido = textBox3.Text;
            string email = textBox4.Text;
            string telefono = textBox5.Text;
            string cargo = textBox6.Text;
            DateTime fechaContratacion = dateTimePicker1.Value;
            decimal salario = decimal.Parse(textBox7.Text);
            string estado = comboBox1.SelectedItem.ToString();

            Empleados oe = new Empleados(
                id,
                nombre,
                apellido,
                email,
                telefono,
                cargo,
                fechaContratacion,
                salario,
                estado
            );

            return oe;
        }

        private void mostrarEstado()
        {
            comboBox1.Items.Add("Activo");
            comboBox1.Items.Add("Inactivo");
        }

        public bool ValidarDatos()
        {
            bool value = true;

            if (textBox1.Text.Trim().Length == 0 ||
                textBox2.Text.Trim().Length == 0 ||
                textBox3.Text.Trim().Length == 0 ||
                comboBox1.SelectedIndex < 0)
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

        public void setDatos(Empleados oe)
        {
            textBox1.Text = oe.EmpleadoID.ToString();
            textBox2.Text = oe.Nombre;
            textBox3.Text = oe.Apellido;
            textBox4.Text = oe.Email;
            textBox5.Text = oe.Telefono;
            textBox6.Text = oe.Cargo;
            dateTimePicker1.Value = oe.FechaContratacion;
            textBox7.Text = oe.Salario.ToString();
            comboBox1.Text = oe.Estado;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Guardar();
            this.Hide();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
