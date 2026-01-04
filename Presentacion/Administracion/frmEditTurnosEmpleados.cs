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
    public partial class frmEditTurnosEmpleados : Form
    {
        public frmEditTurnosEmpleados()
        {
            InitializeComponent();
            mostrarDias();
            mostrarEstado();
            mostrarTipoTurno();
        }
        EmpleadosLN olEmpleado = new EmpleadosLN();

        public TurnosEmpleados CrearObjeto()
        {
            int turnoID = int.Parse(textBox1.Text);
            int empleadoID = (int)comboBox1.SelectedValue;
            string diaSemana = comboBox2.Text;
            TimeSpan horaInicio = dateTimePicker1.Value.TimeOfDay;
            TimeSpan horaFin = dateTimePicker2.Value.TimeOfDay;
            string tipoTurno = comboBox3.Text;
            string estado = comboBox4.Text;
            DateTime fechaRegistro = dateTimePicker3.Value;

            TurnosEmpleados turno = new TurnosEmpleados(
                turnoID,
                empleadoID,
                diaSemana,
                horaInicio,
                horaFin,
                tipoTurno,
                estado,
                fechaRegistro
            );

            return turno;
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
        public bool ValidarDatos()
        {
            bool value = true;

            if (textBox1.Text.Trim().Length == 0 ||
                comboBox1.SelectedIndex < 0 ||
                comboBox2.SelectedIndex < 0 ||
                comboBox3.SelectedIndex < 0 ||
                comboBox4.SelectedIndex < 0)
            {
                value = false;
            }

            return value;
        }
        public void setDatos(TurnosEmpleados turno)
        {
            textBox1.Text = turno.TurnoID.ToString();
            comboBox1.SelectedValue = turno.EmpleadoID;
            comboBox2.Text = turno.DiaSemana;
            dateTimePicker1.Value = DateTime.Today.Add(turno.HoraInicio);
            dateTimePicker2.Value = DateTime.Today.Add(turno.HoraFin);
            comboBox3.Text = turno.TipoTurno;
            comboBox4.Text = turno.Estado;
            dateTimePicker3.Value = turno.FechaRegistro;
        }
        private void mostrarEmpleados()
        {
            comboBox1.DataSource = olEmpleado.ShowEmpleadosFiltro("");
            comboBox1.SelectedIndex = 0;
            comboBox1.DisplayMember = "Nombre";
            comboBox1.ValueMember = "EmpleadoID";
        }


        private void mostrarDias()
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add("Lunes");
            comboBox2.Items.Add("Martes");
            comboBox2.Items.Add("Miércoles");
            comboBox2.Items.Add("Jueves");
            comboBox2.Items.Add("Viernes");
            comboBox2.SelectedIndex = 0;
        }
        private void mostrarTipoTurno()
        {
            comboBox3.Items.Clear();
            comboBox3.Items.Add("Mañana");
            comboBox3.Items.Add("Tarde");
            comboBox3.Items.Add("Noche");
            comboBox3.SelectedIndex = 0;
        }
        private void mostrarEstado()
        {
            comboBox4.Items.Clear();
            comboBox4.Items.Add("Activo");
            comboBox4.Items.Add("Inactivo");
            comboBox4.SelectedIndex = 0;
        }



        private void frmEditTurnosEmpleados_Load(object sender, EventArgs e)
        {
            mostrarEmpleados();
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
