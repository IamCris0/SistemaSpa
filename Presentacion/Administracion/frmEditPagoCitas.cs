using Entidades.Administracion;
using Logica.Administracion;
using Logica.Vistas;
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
    public partial class frmEditPagoCitas : Form
    {
        public frmEditPagoCitas()
        {
            InitializeComponent();
            mostrarMetodoPago();
            mostrarEstadoPago();


        }

        // ================== CREAR OBJETO ==================
        public PagosCitas CrearObjeto()
        {
            int pagoID = int.Parse(textBox1.Text);
            int citaID = (int)comboBox1.SelectedValue;
            DateTime fechaPago = dateTimePicker1.Value;
            double monto = double.Parse(textBox2.Text);
            string metodoPago = comboBox2.Text;
            string referencia = textBox3.Text;
            string estadoPago = comboBox3.Text;

            PagosCitas pago = new PagosCitas(
                pagoID,
                citaID,
                fechaPago,
                monto,
                metodoPago,
                referencia,
                estadoPago
            );

            return pago;
        }

        // ================== LOGICA ==================
        PagosCitasLN olCitas = new PagosCitasLN();

        private void mostrarCitas()
        {
            comboBox1.DataSource = olCitas.ShowPagosCitasFiltro("");
            comboBox1.SelectedIndex = 0;
            comboBox1.DisplayMember = "Cliente";
            comboBox1.ValueMember = "CitaID";
        }

        private void mostrarMetodoPago()
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add("Efectivo");
            comboBox2.Items.Add("Transferencia");
            comboBox2.Items.Add("Tarjeta");
            comboBox2.Items.Add("Otro");
            comboBox2.SelectedIndex = 0;
        }

        private void mostrarEstadoPago()
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add("Pagado");
            comboBox3.Items.Add("Pendiente");
            comboBox3.Items.Add("Anulado");
            comboBox3.SelectedIndex = 0;
        }

        // ================== VALIDAR ==================
        public bool ValidarDatos()
        {
            bool value = true;

            if (textBox1.Text.Trim().Length == 0 ||
                textBox2.Text.Trim().Length == 0 ||
                comboBox1.SelectedIndex < 0 ||
                comboBox2.SelectedIndex < 0 ||
                comboBox3.SelectedIndex < 0)
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
        public void setDatos(PagosCitas pago)
        {
            textBox1.Text = pago.PagoID.ToString();
            comboBox1.SelectedValue = pago.CitaID;
            dateTimePicker1.Value = pago.FechaPago;
            textBox2.Text = pago.Monto.ToString();
            comboBox2.Text = pago.MetodoPago;
            textBox3.Text = pago.Referencia;
            comboBox3.Text = pago.EstadoPago;
        }
        private void frmEditPagoCitas_Load(object sender, EventArgs e)
        {
            mostrarCitas();
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
