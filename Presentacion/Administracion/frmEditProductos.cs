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
    public partial class frmEditProductos : Form
    {
        public frmEditProductos()
        {
            InitializeComponent();
            mostrarMarca();
            mostrarEstado();
        }

        // ================== CREAR OBJETO ==================
        public Productos CrearObjeto()
        {
            int productoID = int.Parse(textBox1.Text);
            string nombreProducto = textBox2.Text;
            string descripcion = textBox3.Text;
            string marca = comboBox1.Text;
            double precioUnitario = double.Parse(textBox4.Text);
            int stock = int.Parse(textBox5.Text);
            int stockMinimo = int.Parse(textBox6.Text);
            string estado = comboBox2.Text;

            Productos producto = new Productos(
                productoID,
                nombreProducto,
                descripcion,
                marca,
                precioUnitario,
                stock,
                stockMinimo,
                estado
            );

            return producto;
        }

        // ================== COMBOS ==================
        private void mostrarMarca()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("AromaSpa");
            comboBox1.Items.Add("SkinCare Pro");
            comboBox1.Items.Add("RelaxPlus");
            comboBox1.Items.Add("NaturalSpa");
            comboBox1.Items.Add("BeautyLine");
            comboBox1.SelectedIndex = 0;
        }

        private void mostrarEstado()
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add("Activo");
            comboBox2.Items.Add("Inactivo");
            comboBox2.SelectedIndex = 0;
        }

        // ================== VALIDAR ==================
        public bool ValidarDatos()
        {
            bool value = true;

            if (textBox1.Text.Trim().Length == 0 ||
                textBox2.Text.Trim().Length == 0 ||
                textBox4.Text.Trim().Length == 0 ||
                textBox5.Text.Trim().Length == 0 ||
                textBox6.Text.Trim().Length == 0 ||
                comboBox1.SelectedIndex < 0 ||
                comboBox2.SelectedIndex < 0)
            {
                value = false;
            }

            return value;
        }

        // ================== GUARDAR ==================
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

        // ================== SET DATOS ==================
        public void setDatos(Productos producto)
        {
            textBox1.Text = producto.ProductoID.ToString();
            textBox2.Text = producto.NombreProducto;
            textBox3.Text = producto.Descripcion;
            comboBox1.Text = producto.Marca;
            textBox4.Text = producto.PrecioUnitario.ToString();
            textBox5.Text = producto.Stock.ToString();
            textBox6.Text = producto.StockMinimo.ToString();
            comboBox2.Text = producto.Estado;
        }


        private void frmEditProductos_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Guardar();this.Hide();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
