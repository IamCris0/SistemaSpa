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
    public partial class frmEditCategoriasServicios : Form
    {
        public frmEditCategoriasServicios()
        {
            InitializeComponent();
            mostrarEstado();
        }
        public CategoriasServicios CrearObjeto()
        {
            int id = int.Parse(textBox1.Text);
            string nombre = textBox2.Text;
            string descripcion = textBox3.Text;
            string estado = comboBox1.SelectedItem.ToString();

            CategoriasServicios oc = new CategoriasServicios(
                id,
                nombre,
                descripcion,
                estado
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
        public void setDatos(CategoriasServicios oc)
        {
            textBox1.Text = oc.CategoriaID.ToString();
            textBox2.Text = oc.NombreCategoria;
            textBox3.Text = oc.Descripcion;
            comboBox1.Text = oc.Estado;
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
