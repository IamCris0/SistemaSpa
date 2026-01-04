using Entidades.Administracion;
using Logica;
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
    public partial class frmEditClientesMembresias : Form
    {
        public frmEditClientesMembresias()
        {
            InitializeComponent();
            mostrarEstado();
            mostrarCliente();
            mostrarMembresia();
        }
        public ClientesMembresias CrearObjeto()
        {
            int clienteMembresiaID = int.Parse(textBox1.Text);
            int clienteID = (int)comboBox1.SelectedValue;
            int membresiaID = (int)comboBox2.SelectedValue;
            DateTime fechaInicio = dateTimePicker1.Value;
            DateTime fechaFin = dateTimePicker2.Value;
            string estadoMembresia = comboBox3.SelectedItem.ToString();
            DateTime fechaRegistro = dateTimePicker3.Value;

            ClientesMembresias cm = new ClientesMembresias(
                clienteMembresiaID,
                clienteID,
                membresiaID,
                fechaInicio,
                fechaFin,
                estadoMembresia,
                fechaRegistro
            );

            return cm;
        }

        ClientesLN olCliente = new ClientesLN();
        MembresiasLN olMembresia = new MembresiasLN();

        private void mostrarCliente()
        {
            comboBox1.DataSource = olCliente.ShowClientesFiltro("");
            comboBox1.SelectedIndex = 0;
            comboBox1.DisplayMember = "Nombre";
            comboBox1.ValueMember = "ClienteID";
        }

        private void mostrarMembresia()
        {
            comboBox2.DataSource = olMembresia.ShowMembresiasFiltro("");
            comboBox2.SelectedIndex = 0;
            comboBox2.DisplayMember ="NombreMenbresia";
            comboBox2.ValueMember = "MembresiaID";
        }

        private void mostrarEstado()
        {
            comboBox3.Items.Add("Activa");
            comboBox3.Items.Add("Suspendida");
            comboBox3.Items.Add("Finalizada");
        }

        public bool ValidarDatos()
        {
            bool value = true;

            if (textBox1.Text.Trim().Length == 0 ||
                comboBox1.SelectedIndex < 0 ||
                comboBox2.SelectedIndex < 0 ||
                comboBox3.SelectedIndex < 0)
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

        public void setDatos(ClientesMembresias cm)
        {
            textBox1.Text = cm.ClienteMembresiaID.ToString();
            comboBox1.SelectedValue = cm.ClienteID;
            comboBox2.SelectedValue = cm.MembresiaID;
            dateTimePicker1.Value = cm.FechaInicio;
            dateTimePicker2.Value = cm.FechaFin;
            comboBox3.Text = cm.EstadoMembresia;
            dateTimePicker3.Value = cm.FechaRegistro;
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

        private void frmEditClientesMembresias_Load(object sender, EventArgs e)
        {
          
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
