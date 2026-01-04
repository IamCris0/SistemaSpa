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
    public partial class frmEditMembresias : Form
    {
        public frmEditMembresias()
        {
            InitializeComponent();
            mostrarEstado();
        }
        public Membresias CrearObjeto()
        {
            int id = int.Parse(textBox1.Text);
            string nom = textBox2.Text;
            string des = textBox3.Text;
            int dur = int.Parse(textBox4.Text);
            double pre = double.Parse(textBox5.Text);
            double desc = double.Parse(textBox6.Text);
            string est = comboBox1.SelectedItem.ToString();

            Membresias oc = new Membresias(
                id,
                nom,
                des,
                dur,
                pre,
                desc,
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
        public void setDatos(Membresias oc)
        {

            textBox1.Text = oc.MembresiaID.ToString();
            textBox2.Text = oc.NombreMenbresia;
            textBox3.Text = oc.Descripcion;
            textBox4.Text = oc.DuracionMeses.ToString();
            textBox5.Text = oc.Precio.ToString();
            textBox6.Text = oc.Descuento.ToString();
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
