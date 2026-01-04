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
    public partial class frmEditDetalleCitas : Form
    {
        public frmEditDetalleCitas()
        {
            InitializeComponent();
        }
        public DetalleCitas CrearObjeto()
        {
            int detalleCitaID = int.Parse(textBox1.Text);
            int citaID = (int)comboBox1.SelectedValue;
            int servicioID = (int)comboBox2.SelectedValue;
            decimal precioServicio = decimal.Parse(textBox2.Text);
            decimal descuento = decimal.Parse(textBox3.Text);

            DetalleCitas detalle = new DetalleCitas(
                detalleCitaID,
                citaID,
                servicioID,
                precioServicio,
                descuento
            );

            return detalle;
        }

        CitasLN olCitas = new CitasLN();
        ServiciosLN olServicios = new ServiciosLN();

        private void mostrarCitas()
        {
            comboBox1.DataSource = olCitas.ShowCitasFiltro("");
            comboBox1.SelectedIndex = 0;
            comboBox1.DisplayMember = "CitaID";
            comboBox1.ValueMember = "CitaID";
        }

        private void mostrarServicios()
        {
            comboBox2.DataSource = olServicios.ShowServiciosFiltro("");
            comboBox2.SelectedIndex = 0;
            comboBox2.DisplayMember = "NombreServicio";
            comboBox2.ValueMember = "ServicioID";
        }

        public bool ValidarDatos()
        {
            bool value = true;

            if (textBox1.Text.Trim().Length == 0 ||
                textBox2.Text.Trim().Length == 0 ||
                textBox3.Text.Trim().Length == 0 ||
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

        public void setDatos(DetalleCitas detalle)
        {
            textBox1.Text = detalle.DetalleCitaID.ToString();
            comboBox1.SelectedValue = detalle.CitaID;
            comboBox2.SelectedValue = detalle.ServicioID;
            textBox2.Text = detalle.PrecioServicio.ToString();
            textBox3.Text = detalle.Descuento.ToString();
        }

        private void frmEditDetalleCitas_Load(object sender, EventArgs e)
        {
            mostrarCitas();
            mostrarServicios();
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
