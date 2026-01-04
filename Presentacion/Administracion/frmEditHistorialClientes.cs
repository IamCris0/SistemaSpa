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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Presentacion.Administracion
{
    public partial class frmEditHistorialClientes : Form
    {
        public frmEditHistorialClientes()
        {
            InitializeComponent();
        
        mostrarCalificacion();
        }

        // ================== CREAR OBJETO ==================
        public HistorialClientes CrearObjeto()
        {
            int historialID = int.Parse(textBox1.Text);
            int clienteID = (int)comboBox1.SelectedValue;
            int citaID = (int)comboBox2.SelectedValue;
            DateTime fechaVisita = dateTimePicker1.Value;
            string observaciones = textBox2.Text;
            int calificacion = int.Parse(comboBox3.Text);
            string alergias = textBox3.Text;
            string resultados = textBox3.Text;
            DateTime fechaRegistro = dateTimePicker2.Value;

            HistorialClientes historial = new HistorialClientes(
                historialID,
                clienteID,
                citaID,
                fechaVisita,
                observaciones,
                calificacion,
                alergias,
                resultados,
                fechaRegistro
            );

            return historial;
        }

        // ================== LOGICA ==================
        ClientesLN olClientes = new ClientesLN();
        CitasLN olCitas = new CitasLN();

        private void mostrarClientes()
        {
            comboBox1.DataSource = olClientes.ShowClientesFiltro("");
            comboBox1.SelectedIndex = 0;
            comboBox1.DisplayMember = "Nombre";
            comboBox1.ValueMember = "ClienteID";
        }

        private void mostrarCitas()
        {
            comboBox2.DataSource = olCitas.ShowCitasFiltro("");
            comboBox2.SelectedIndex = 0;
            comboBox2.DisplayMember = "CitaID";
            comboBox2.ValueMember = "CitaID";
        }

        private void mostrarCalificacion()
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add("1");
            comboBox3.Items.Add("2");
            comboBox3.Items.Add("3");
            comboBox3.Items.Add("4");
            comboBox3.Items.Add("5");
            comboBox3.SelectedIndex = 0;
        }

        // ================== VALIDAR ==================
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
        public void setDatos(HistorialClientes historial)
        {
            textBox1.Text = historial.HistorialID.ToString();
            comboBox1.SelectedValue = historial.ClienteID;
            comboBox2.SelectedValue = historial.CitaID;
            dateTimePicker1.Value = historial.FechaVisita;
            textBox2.Text = historial.Observaciones;
            comboBox3.Text = historial.Calificacion.ToString();
            textBox3.Text = historial.AlergiasProcedimiento;
            textBox3.Text = historial.ResultadosTratamiento;
            dateTimePicker2.Value = historial.FechaRegistro;
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Guardar();this.Hide();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmEditHistorialClientes_Load(object sender, EventArgs e)
        {
            mostrarClientes();
            mostrarCitas();

        }
    }
}
