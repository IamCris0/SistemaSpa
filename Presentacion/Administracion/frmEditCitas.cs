using Entidades.Administracion;
using Logica.Administracion;
using System;
using System.Collections;
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
    public partial class frmEditCitas : Form
    {
        public frmEditCitas()
        {
            InitializeComponent();
            mostrarEstado();
        }

        public Citas CrearObjeto()
        {
            int citaID = int.Parse(textBox1.Text);
            int clienteID = (int)comboBox1.SelectedValue;
            int empleadoID = (int)comboBox2.SelectedValue;
            int salaID = (int)comboBox3.SelectedValue;
            DateTime fechaCita = dateTimePicker1.Value;
            TimeSpan horaInicio = dateTimePicker2.Value.TimeOfDay;
            TimeSpan horaFin = dateTimePicker3.Value.TimeOfDay;
            string estadoCita = comboBox4.SelectedItem.ToString();
            string observaciones = textBox2.Text;
            DateTime fechaCreacion = dateTimePicker4.Value;

            Citas cita = new Citas(
                citaID,
                clienteID,
                empleadoID,
                salaID,
                fechaCita,
                horaInicio,
                horaFin,
                estadoCita,
                observaciones,
                fechaCreacion
            );

            return cita;
        }
        ClientesLN ols = new ClientesLN();
        EmpleadosLN olest = new EmpleadosLN();
        SalasLN ol3 = new SalasLN();
        private void mostrarCliente()
        {
            comboBox1.DataSource = ols.ShowClientesFiltro("");
            comboBox1.SelectedIndex = 0;
            comboBox1.DisplayMember = "Nombre";
            comboBox1.ValueMember = "ClienteID";
        }
        private void mostrarEmpleado()
        {
            comboBox2.DataSource = olest.ShowEmpleadosFiltro("");
            comboBox2.SelectedIndex = 0;
            comboBox2.DisplayMember = "Nombre";
            comboBox2.ValueMember = "EmpleadoID";
        }
        private void mostrarSalas()
        {
           comboBox3.DataSource = ol3.ShowSalasFiltro("");
           comboBox3.SelectedIndex = 0;
           comboBox3.DisplayMember = "NombreSala";
           comboBox3.ValueMember = "SalaID";
        }
        
        private void mostrarEstado()
        {
            comboBox4.Items.Add("Programada");
            comboBox4.Items.Add("Cancelada");
            comboBox4.Items.Add("Finalizada");
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
        public void setDatos(Citas cita)
        {
            textBox1.Text = cita.CitaID.ToString();
            comboBox1.SelectedValue = cita.ClienteID;
            comboBox2.SelectedValue = cita.EmpleadoID;
            comboBox3.SelectedValue = cita.SalaID;
            dateTimePicker1.Value = cita.FechaCita;
            dateTimePicker2.Value = DateTime.Today.Add(cita.HoraInicio);
            dateTimePicker3.Value = DateTime.Today.Add(cita.HoraFin);
            comboBox4.Text = cita.EstadoCita;
            textBox2.Text = cita.Observaciones;
            dateTimePicker4.Value = cita.FechaCreacion;
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

        private void frmEditCitas_Load(object sender, EventArgs e)
        {
            mostrarCliente();
            mostrarEmpleado();
            mostrarSalas();
        }
    }
}
