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
    public partial class frmEditGastosOperativos : Form
    {
        public frmEditGastosOperativos()
        {
            InitializeComponent();
            mostrarTipoGasto();
            mostrarMetodoPago();
            mostrarEstado();


        }

        public GastosOperativos CrearObjeto()
        {
            int gastoID = int.Parse(textBox1.Text);
            int proveedorID = (int)comboBox1.SelectedValue;
            string tipoGasto = comboBox2.SelectedItem.ToString();
            string descripcion = textBox2.Text;
            decimal monto = decimal.Parse(textBox3.Text);
            DateTime fechaGasto = dateTimePicker1.Value;
            string metodoPago = comboBox3.SelectedItem.ToString();
            string comprobante = textBox4.Text;
            string estado = comboBox4.SelectedItem.ToString();
            DateTime fechaRegistro = dateTimePicker2.Value;

            GastosOperativos gasto = new GastosOperativos(
                gastoID,
                proveedorID,
                tipoGasto,
                descripcion,
                monto,
                fechaGasto,
                metodoPago,
                comprobante,
                estado,
                fechaRegistro
            );

            return gasto;
        }

        ProveedoresLN olProveedor = new ProveedoresLN();

        private void mostrarProveedores()
        {
            comboBox1.DataSource = olProveedor.ShowProveedoresFiltro("");
            comboBox1.SelectedIndex = 0;
            comboBox1.DisplayMember = "NombreProveedor";
            comboBox1.ValueMember = "ProveedorID";
        }

        private void mostrarTipoGasto()
        {
            comboBox2.Items.Add("Servicios");
            comboBox2.Items.Add("Mantenimiento");
            comboBox2.Items.Add("Insumos");
            comboBox2.Items.Add("Otros");
        }

        private void mostrarMetodoPago()
        {
            comboBox3.Items.Add("Efectivo");
            comboBox3.Items.Add("Transferencia");
            comboBox3.Items.Add("Tarjeta");
        }

        private void mostrarEstado()
        {
            comboBox4.Items.Add("Pagado");
            comboBox4.Items.Add("Pendiente");
            comboBox4.Items.Add("Anulado");
        }

        public bool ValidarDatos()
        {
            bool value = true;

            if (textBox1.Text.Trim().Length == 0 ||
                textBox2.Text.Trim().Length == 0 ||
                textBox3.Text.Trim().Length == 0 ||
                comboBox1.SelectedIndex < 0 ||
                comboBox2.SelectedIndex < 0 ||
                comboBox3.SelectedIndex < 0 ||
                comboBox4.SelectedIndex < 0)
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

        public void setDatos(GastosOperativos gasto)
        {
            textBox1.Text = gasto.GastoID.ToString();
            comboBox1.SelectedValue = gasto.ProveedorID;
            comboBox2.Text = gasto.TipoGasto;
            textBox2.Text = gasto.Descripcion;
            textBox3.Text = gasto.Monto.ToString();
            dateTimePicker1.Value = gasto.FechaGasto;
            comboBox3.Text = gasto.MetodoPago;
            textBox4.Text = gasto.Comprobante;
            comboBox4.Text = gasto.Estado;
            dateTimePicker2.Value = gasto.FechaRegistro;
        }

        private void frmEditGastosOperativos_Load(object sender, EventArgs e)
        {
            mostrarProveedores();
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
