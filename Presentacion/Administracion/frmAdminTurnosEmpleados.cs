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
    public partial class frmAdminTurnosEmpleados : Form
    {
        public frmAdminTurnosEmpleados()
        {
            InitializeComponent();
        }
        TurnosEmpleadosLN oln = new TurnosEmpleadosLN();
        public TurnosEmpleados obj = new TurnosEmpleados();

        public void ListarTurnosEmpleados(string val)
        {
            dataGridView1.DataSource = oln.ShowTurnosEmpleadosFiltro(val);

        }

        public void Nuevo()
        {
            try
            {
                frmEditTurnosEmpleados frm = new frmEditTurnosEmpleados();
                frm.Text = "Insertar TurnosEmpleados";
                frm.label1.Text = "Insertar TurnosEmpleados";
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {
                    TurnosEmpleados oc = frm.CrearObjeto();
                    oln.InsertTurnosEmpleados(oc);
                    frm.Close();
                    toolStripStatusLabel1.Text = "TurnosEmpleados ingresado correctamente";
                    ListarTurnosEmpleados(textBox1.Text);
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error al insertar TurnosEmpleados. " + ex.Message;
            }

        }

        public void Actualizar()
        {

            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    frmEditTurnosEmpleados frm = new frmEditTurnosEmpleados();
                    frm.Text = "Modificar Categoria";

                    TurnosEmpleados obj = dataGridView1.CurrentRow.DataBoundItem as TurnosEmpleados;
                    frm.setDatos(obj);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        TurnosEmpleados oe = frm.CrearObjeto();
                        oln.UpdateTurnosEmpleados(oe);
                        ListarTurnosEmpleados(textBox1.Text);
                        toolStripStatusLabel1.Text = "TurnosEmpleados actualizado correctamente";
                        timer1.Start();
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione la fila a modificar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar TurnosEmpleados: " + ex.Message);
            }
        }

        public void Eliminar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var resp = MessageBox.Show(
                        "¿Desea eliminar el TurnosEmpleados?",
                        "Eliminar TurnosEmpleados",
                        MessageBoxButtons.YesNo);

                    if (resp == DialogResult.Yes)
                    {
                        TurnosEmpleados obj = dataGridView1.CurrentRow.DataBoundItem as TurnosEmpleados;
                        oln.DeleteTurnosEmpleados(obj);
                        ListarTurnosEmpleados(textBox1.Text);
                        toolStripStatusLabel1.Text = "TurnosEmpleados eliminado correctamente";
                        timer1.Start();
                    }
                    else
                        MessageBox.Show("Eliminación cancelada.");
                }
                else
                {
                    MessageBox.Show("Seleccione la fila a eliminar.");
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error al eliminar TurnosEmpleados. " + ex.Message;
            }
        }

        private void frmAdminTurnosEmpleados_Load(object sender, EventArgs e)
        {
            ListarTurnosEmpleados("");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListarTurnosEmpleados(textBox1.Text);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void frmAdminTurnosEmpleados_Load_1(object sender, EventArgs e)
        {
            ListarTurnosEmpleados("");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "...";
        }
    }
}
