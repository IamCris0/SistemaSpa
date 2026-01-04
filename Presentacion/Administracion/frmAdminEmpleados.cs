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
    public partial class frmAdminEmpleados : Form
    {
        public frmAdminEmpleados()
        {
            InitializeComponent();
        }
        EmpleadosLN oln = new EmpleadosLN();
        public Empleados obj = new Empleados();

        public void ListarEmpleados(string val)
        {
            dataGridView1.DataSource = oln.ShowEmpleadosFiltro(val);

        }

        public void Nuevo()
        {
            try
            {
                frmEditEmpleados frm = new frmEditEmpleados();
                frm.Text = "Insertar Empleados";
                frm.label1.Text = "Insertar Empleados";
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {
                    Empleados oc = frm.CrearObjeto();
                    oln.InsertEmpleados(oc);
                    frm.Close();
                    toolStripStatusLabel1.Text = "Empleados ingresado correctamente";
                    ListarEmpleados(textBox1.Text);
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error al insertar Empleados. " + ex.Message;
            }

        }

        public void Actualizar()
        {

            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    frmEditEmpleados frm = new frmEditEmpleados();
                    frm.Text = "Modificar Categoria";

                    Empleados obj = dataGridView1.CurrentRow.DataBoundItem as Empleados;
                    frm.setDatos(obj);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        Empleados oe = frm.CrearObjeto();
                        oln.UpdateEmpleados(oe);
                        ListarEmpleados(textBox1.Text);
                        toolStripStatusLabel1.Text = "Empleados actualizado correctamente";
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
                MessageBox.Show("Error al modificar Empleados: " + ex.Message);
            }
        }

        public void Eliminar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var resp = MessageBox.Show(
                        "¿Desea eliminar el Empleados?",
                        "Eliminar Empleados",
                        MessageBoxButtons.YesNo);

                    if (resp == DialogResult.Yes)
                    {
                        Empleados obj = dataGridView1.CurrentRow.DataBoundItem as Empleados;
                        oln.DeleteEmpleados(obj);
                        ListarEmpleados(textBox1.Text);
                        toolStripStatusLabel1.Text = "Empleados eliminado correctamente";
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
                toolStripStatusLabel1.Text = "Error al eliminar Empleados. " + ex.Message;
            }
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "...";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListarEmpleados(textBox1.Text);
        }

        private void frmAdminEmpleados_Load(object sender, EventArgs e)
        {
            ListarEmpleados("");
        }
    }
}
