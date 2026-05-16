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
    public partial class IHMcadastro: Form
    {
        public IHMcadastro()
        {
            InitializeComponent();
        }
        private void IHMcadastro_Load(object sender, EventArgs e)
        {
            //Centralizar painel
            pnCadastro.Left = (this.ClientSize.Width - pnCadastro.Width) / 2;
            pnCadastro.Top = (this.ClientSize.Height - pnCadastro.Height) / 2;
        }
    }
}
