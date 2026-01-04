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
    public partial class frmAdminCompleto : Form
    {
        public frmAdminCompleto()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAdminCompras frm = new frmAdminCompras();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmAdminDetalleCompras frm = new frmAdminDetalleCompras();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmAdminDetalleVentas frm = new frmAdminDetalleVentas();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmAdminVentas frm = new frmAdminVentas();
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmAdminPagosCitas frm = new frmAdminPagosCitas();
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmAdminDetalleCitas frm = new frmAdminDetalleCitas();
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmAdminCitas frm = new frmAdminCitas();
            frm.ShowDialog();
        }
    }
}
