using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace LoginAcademia
{
    public partial class IHMLogin: Form
    {
        public IHMLogin()
        {
            InitializeComponent();
        }

        private void IHMLogin_Load(object sender, EventArgs e)
        {
            //Centralizar painel
            pnLogin.Left = (this.ClientSize.Width - pnLogin.Width) / 2;
            pnLogin.Top = (this.ClientSize.Height - pnLogin.Height) / 2;
        }
    }
}
