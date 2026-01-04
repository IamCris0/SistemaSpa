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
    public partial class frmEditSalas : Form
    {
        public frmEditSalas()
        {
            InitializeComponent();
            mostrarEstado();
            mostrarTipoSala();
        }
        public Salas CrearObjeto()
        {
            int salaID = int.Parse(textBox1.Text);
            string nombreSala = textBox2.Text;
            string tipoSala = comboBox1.SelectedItem.ToString();
            int capacidad = int.Parse(textBox3.Text);
            string estado = comboBox2.SelectedItem.ToString();

            Salas os = new Salas(
                salaID,
                nombreSala,
                tipoSala,
                capacidad,
                estado
            );

            return os;
        }

        private void mostrarEstado()
        {
            comboBox2.Items.Add("Disponible");
            comboBox2.Items.Add("Ocupada");
            comboBox2.Items.Add("Mantenimiento");
        }

        private void mostrarTipoSala()
        {
            comboBox1.Items.Add("Masajes");
            comboBox1.Items.Add("Meditación");
            comboBox1.Items.Add("Sauna");
            comboBox1.Items.Add("Tratamientos corporales");
            comboBox1.Items.Add("Atención VIP");
        }
        public bool ValidarDatos()
        {
            bool value = true;

            if (textBox1.Text.Trim().Length == 0 ||
                textBox2.Text.Trim().Length == 0 ||
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
        public void setDatos(Salas os)
        {
            textBox1.Text = os.SalaID.ToString();
            textBox2.Text = os.NombreSala;
            comboBox1.Text = os.TipoSala;
            textBox3.Text = os.Capacidad.ToString();
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
    }
}
