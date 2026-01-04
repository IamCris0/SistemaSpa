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
    public partial class frmEditCompras : Form
    {
        public frmEditCompras()
        {
            InitializeComponent();
            mostrarEstado();
            
        }
        
        public Compras CrearObjeto()
        {
            int compraID = int.Parse(textBox1.Text);
            int proveedorID = (int)comboBox1.SelectedValue;
            DateTime fechaCompra = dateTimePicker1.Value;
            decimal total = decimal.Parse(textBox2.Text);
            string estadoCompra = comboBox2.SelectedItem.ToString();
            string observaciones = textBox3.Text;

            Compras oc = new Compras(
                compraID,
                proveedorID,
                fechaCompra,
                total,
                estadoCompra,
                observaciones
            );

            return oc;
        }

        ProveedoresLN olProveedor = new ProveedoresLN();

        private void mostrarProveedor()
        {
            comboBox1.DataSource = olProveedor.ShowProveedoresFiltro("");
            comboBox1.SelectedIndex = 0;
            comboBox1.DisplayMember = "NombreProveedor";
            comboBox1.ValueMember = "ProveedorID";
        }

        private void mostrarEstado()
        {
            comboBox2.Items.Add("Registrada");
            comboBox2.Items.Add("Anulada");
            comboBox2.Items.Add("Pagada");
        }
        public bool ValidarDatos()
        {
            bool value = true;

            if (textBox1.Text.Trim().Length == 0 ||
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

        public void setDatos(Compras oc)
        {
            textBox1.Text = oc.CompraID.ToString();
            comboBox1.SelectedValue = oc.ProveedorID;
            dateTimePicker1.Value = oc.FechaCompra;
            textBox2.Text = oc.Total.ToString();
            comboBox2.Text = oc.EstadoCompra;
            textBox3.Text = oc.Observaciones;
        }

        private void frmEditCompras_Load(object sender, EventArgs e)
        {
            mostrarProveedor();
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
