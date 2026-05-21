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
            lblErroUsuario.Visible = false;
            lblErroSenha.Visible = false;

            bool possuiErro = false;

            AcademiaBLL.validacaologin(txtUsuario.Text);
            if (Erro.getErro())
            {
                lblErroUsuario.Text = Erro.getMsg();
                lblErroUsuario.Visible = true;
                possuiErro = true;
            }

            if (string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                lblErroSenha.Text = "Digite sua senha!";
                lblErroSenha.Visible = true;
                possuiErro = true;
            }

            if (possuiErro) return;

            academia.setUsuario(txtUsuario.Text);

            AcademiaDAL.consultaLogin(academia);

            if (Erro.getErro())
            {
                lblErroUsuario.Text = "Usuário ou senha incorretos!";
                lblErroUsuario.Visible = true;
                return;
            }

            if (!BCrypt.Net.BCrypt.Verify(txtSenha.Text, academia.getSenha()))
            {
                lblErroUsuario.Text = "Usuário ou senha incorretos!";
                lblErroUsuario.Visible = true;
                lblErroSenha.Text = "Usuário ou senha incorretos!";
                lblErroSenha.Visible = true;
                return;
            }

            if (academia.getIcAdmin())
            {
                IHMAdm1 formAdmin = new IHMAdm1(academia);
                formAdmin.Show();
                this.Hide();
            }
            else
            {
                IHMCliente formCliente = new IHMCliente(academia);
                formCliente.Show();
                this.Hide();
            }
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

        private void txtSenha_IconRightClick(object sender, EventArgs e)
        {
            //Funcionalidade pro olho esconder e revelar a senha
            txtSenha.UseSystemPasswordChar =
            !txtSenha.UseSystemPasswordChar;
        }
    }
}
