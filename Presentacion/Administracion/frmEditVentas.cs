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
    public partial class frmEditVentas : Form
    {
        public frmEditVentas()
        {
            InitializeComponent();
            mostrarMetodoPago();
            mostrarEstado();
            mostrarClientes();
            mostrarEmpleados();
            
        }

        public Ventas CrearObjeto()
        {
            int ventaID = int.Parse(textBox1.Text);
            int clienteID = (int)comboBox1.SelectedValue;
            int empleadoID = (int)comboBox2.SelectedValue;
            DateTime fechaVenta = dateTimePicker1.Value;
            double total = double.Parse(textBox2.Text);
            string metodoPago = comboBox3.SelectedItem.ToString();
            string estado = comboBox4.SelectedItem.ToString();

            Ventas venta = new Ventas(
                ventaID,
                clienteID,
                empleadoID,
                fechaVenta,
                total,
                metodoPago,
                estado
            );

            return venta;
        }

        ClientesLN olClientes = new ClientesLN();
        EmpleadosLN olEmpleados = new EmpleadosLN();

        private void mostrarClientes()
        {
            comboBox1.DataSource = olClientes.ShowClientesFiltro("");
            comboBox1.SelectedIndex = 0;
            comboBox1.DisplayMember = "Nombre";
            comboBox1.ValueMember = "ClienteID";
        }

        private void mostrarEmpleados()
        {
            comboBox2.DataSource = olEmpleados.ShowEmpleadosFiltro("");
            comboBox2.SelectedIndex = 0;
            comboBox2.DisplayMember = "Nombre";
            comboBox2.ValueMember = "EmpleadoID";
        }

        private void mostrarMetodoPago()
        {
            comboBox3.Items.Add("Efectivo");
            comboBox3.Items.Add("Transferencia");
            comboBox3.Items.Add("Tarjeta");
        }

        private void mostrarEstado()
        {
            comboBox4.Items.Add("Pagada");
            comboBox4.Items.Add("Pendiente");
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

        public void setDatos(Ventas venta)
        {
            textBox1.Text = venta.VentaID.ToString();
            comboBox1.SelectedValue = venta.ClienteID;   
            comboBox2.SelectedValue = venta.EmpleadoID;
            dateTimePicker1.Value = venta.FechaVenta;
            textBox2.Text = venta.Total.ToString();
            comboBox3.Text = venta.MetodoPago;
            comboBox4.Text = venta.Estado;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Guardar();
            this.Hide();
        }

        private void frmEditVentas_Load(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
