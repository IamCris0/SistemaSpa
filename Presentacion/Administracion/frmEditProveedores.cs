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
    public partial class frmEditProveedores : Form
    {
        public frmEditProveedores()
        {
            InitializeComponent();
            mostrarEstado();
        }

        public Proveedores CrearObjeto()
        {
            int proveedorID = int.Parse(textBox1.Text);
            string nombreProveedor = textBox2.Text;
            string contacto = textBox3.Text;
            string telefono = textBox4.Text;
            string email = textBox5.Text;
            string direccion = textBox6.Text;
            string estado = comboBox1.SelectedItem.ToString();

            Proveedores proveedor = new Proveedores(
                proveedorID,
                nombreProveedor,
                contacto,
                telefono,
                email,
                direccion,
                estado
            );

            return proveedor;
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

        public void setDatos(Proveedores proveedor)
        {
            textBox1.Text = proveedor.ProveedorID.ToString();
            textBox2.Text = proveedor.NombreProveedor;
            textBox3.Text = proveedor.Contacto;
            textBox4.Text = proveedor.Telefono;
            textBox5.Text = proveedor.Email;
            textBox6.Text = proveedor.Direccion;
            comboBox1.Text = proveedor.Estado;
        }

        private void frmEditProveedores_Load(object sender, EventArgs e)
        {

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
