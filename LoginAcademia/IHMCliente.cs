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
            // Primeiro nome apenas
            string nomeCompleto = usuarioLogado.getNome();
            string primeiroNome = nomeCompleto.Split(' ')[0];
            lbNomeCliente.Text = primeiroNome + "!";

            // Data de hoje em português
            lbData.Text = DateTime.Now.ToString("dd 'de' MMMM', 'yyyy",
                          new System.Globalization.CultureInfo("pt-BR"));
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            IHMPerfil formPerfil = new IHMPerfil(usuarioLogado);
            formPerfil.Show();
            this.Close();
        }

        private void btnSair_Click_1(object sender, EventArgs e)
        {
            IHMLogin formLogin = new IHMLogin();
            formLogin.Show();
            this.Close();
        }
    }
}
