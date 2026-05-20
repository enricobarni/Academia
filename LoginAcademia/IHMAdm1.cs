using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginAcademia
{
    public partial class IHMAdm1: Form
    {
        public IHMAdm1()
        {
            InitializeComponent();
        }

        private void IHMAdm1_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.ColumnCount = 3;
            guna2DataGridView1.Rows.Add("1", "Matheus Ferreira", "matheus@email.com");
            guna2DataGridView1.Rows.Add("2", "Lucas Almeida", "lucas@email.com");
            guna2DataGridView1.Rows.Add("3", "Bruno Rodrigues", "bruno@email.com");
        }
    }
}
