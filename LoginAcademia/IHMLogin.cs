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
        Login_Cadastro academia = new Login_Cadastro();
        public IHMLogin()
        {
            InitializeComponent();
            lblErroUsuario.Visible = false;
            lblErroSenha.Visible = false;
        }

        private void IHMLogin_Load(object sender, EventArgs e)
        {
            //Centralizar painel
            pnLogin.Left = (this.ClientSize.Width - pnLogin.Width) / 2;
            pnLogin.Top = (this.ClientSize.Height - pnLogin.Height) / 2;
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            lblErroUsuario.Text = "";
            lblErroSenha.Text = "";

            bool possuiErro = false;

            academia.setUsuario(txtUsuario.Text);
            academia.setSenha(txtSenha.Text);
            AcademiaBLL.validacaousuario(txtUsuario.Text);
            if (Erro.getErro())
            {
                lblErroUsuario.Text = Erro.getMsg();
                lblErroUsuario.Visible = true;
                possuiErro = true;
            }

            // Se houver qualquer erro, não continua
            if (possuiErro)
                return;
        }

        private void btnCriarConta_Click(object sender, EventArgs e)
        {
            // Cria uma instância do outro formulário
            IHMCadastro formLogin = new IHMCadastro();

            // Exibe o novo formulário
            formLogin.Show();

            // Opcional: esconder o formulário atual
            this.Hide();
        }
    }
}
