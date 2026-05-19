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
    public partial class IHMCliente : Form
    {
        private Login_Cadastro usuarioLogado;

        public IHMCliente(Login_Cadastro lc)
        {
            InitializeComponent();
            usuarioLogado = lc;
        }

        private void IHMCliente_Load(object sender, EventArgs e)
        {
            // Exemplo: mostrar nome na tela
            // lblNome.Text = "Bem-vindo, " + usuarioLogado.getNome();
        }
    }
}
