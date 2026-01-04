using Entidades.Administracion;
using Logica.Administracion;
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
    public partial class frmEditServicios : Form
    {
        public frmEditServicios()
        {
            InitializeComponent();
            mostrarEstado();
        }
        public Servicios CrearObjeto()
        {
            int servicioID = int.Parse(textBox1.Text);
            int categoriaID = int.Parse(comboBox1.SelectedValue.ToString());
            string nombreServicio = textBox2.Text;
            string descripcion = textBox3.Text;
            int duracion = int.Parse(textBox4.Text);
            double precio = double.Parse(textBox5.Text);
            string estado = comboBox2.SelectedItem.ToString();

            Servicios os = new Servicios(
                servicioID,
                categoriaID,
                nombreServicio,
                descripcion,
                duracion,
                precio,
                estado
            );

            return os;
        }
        private void mostrarEstado()
        {
            comboBox2.Items.Add("Activo");
            comboBox2.Items.Add("Inactivo");
        }

        CategoriasServiciosLN olCat = new CategoriasServiciosLN();
        private void mostrarCategorias()
        {
            comboBox1.DataSource = olCat.ShowCategoriasServiciosFiltro("");
            comboBox1.SelectedIndex = 0;
            comboBox1.DisplayMember = "NombreCategoria";
            comboBox1.ValueMember = "CategoriaID";
        }

        public bool ValidarDatos()
        {
            bool value = true;

            if (textBox1.Text.Trim().Length == 0 ||
                textBox2.Text.Trim().Length == 0 ||
                textBox3.Text.Trim().Length == 0 ||
                textBox4.Text.Trim().Length == 0 ||
                textBox5.Text.Trim().Length == 0 ||
                comboBox1.SelectedIndex < 0 ||
                comboBox2.SelectedIndex < 0)
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
        public void setDatos(Servicios os)
        {
            textBox1.Text = os.ServicioID.ToString();
            comboBox1.Text = os.CategoriaID.ToString();
            textBox2.Text = os.NombreServicio;
            textBox3.Text = os.Descripcion;
            textBox4.Text = os.Duracion.ToString();
            textBox5.Text = os.Precio.ToString();
            comboBox2.Text = os.Estado;
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

        private void frmEditServicios_Load(object sender, EventArgs e)
        {
            mostrarCategorias();
        }
    }
}
