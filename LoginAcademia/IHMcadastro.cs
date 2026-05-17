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
    public partial class IHMCadastro: Form
    {
        Login_Cadastro academia = new Login_Cadastro();
        Endereco endereco = new Endereco();
        public IHMCadastro()
        {
            InitializeComponent();
            lblErroNome.Visible = false;
            lblErroUsuario.Visible = false;
            lblErroEmail.Visible = false;
            lblErroSenha.Visible = false;
            lblErroConfirmarsenha.Visible = false;
            lblErroTelefone.Visible = false;
            lblErroCep.Visible = false;
        }

        private void IHMcadastro_Load(object sender, EventArgs e)
        {
            //Centralizar painel
            pnCadastro.Left = (this.ClientSize.Width - pnCadastro.Width) / 2;
            pnCadastro.Top = (this.ClientSize.Height - pnCadastro.Height) / 2;
        }

        private void btnCriarContaCadastro_Click(object sender, EventArgs e)
        {
            lblErroNome.Text = "";
            lblErroEmail.Text = "";
            lblErroSenha.Text = "";
            lblErroTelefone.Text = "";
            lblErroCep.Text = "";

            bool possuiErro = false;

            academia.setNome(txtNome.Text);
            academia.setUsuario(txtUsuario.Text);
            academia.setEmail(txtEmail.Text);
            academia.setSenha(txtSenha.Text);
            academia.setTelefone(txtTelefone.Text);
            AcademiaBLL.validacaonome(txtNome.Text);
            if (Erro.getErro())
            {
                lblErroNome.Text = Erro.getMsg();
                lblErroNome.Visible = true;
                possuiErro = true;
            }
            AcademiaBLL.validacaousuario(txtUsuario.Text);
            if (Erro.getErro())
            {
                lblErroUsuario.Text = Erro.getMsg();
                lblErroUsuario.Visible = true;
                possuiErro = true;
            }
            AcademiaBLL.validacaoemail(txtEmail.Text);
            if (Erro.getErro())
            {
                lblErroEmail.Text = Erro.getMsg();
                lblErroEmail.Visible = true;
                possuiErro = true;
            }
            AcademiaBLL.validacaotelefone(txtTelefone.Text);
            if (Erro.getErro())
            {
                lblErroTelefone.Text = Erro.getMsg();
                lblErroTelefone.Visible = true;
                possuiErro = true;
            }
            AcademiaBLL.validacaocep(txtCep.Text);
            if (Erro.getErro())
            {
                lblErroCep.Text = Erro.getMsg();
                lblErroCep.Visible = true;
                possuiErro = true;
            }

            // Se houver qualquer erro, não continua
            if (possuiErro)
                return;

            MessageBox.Show("Cadastro realizado com sucesso!");

        }
        private void txtTelefone_TextChanged(object sender, EventArgs e)
        {
            //Formata o telefone no Textbox do telefone
            txtTelefone.Text = AcademiaBLL.formatacaotelefone(txtTelefone.Text);

            //Posiciona o cursor pois ao alterar o Text, o cursor pode voltar para o início do campo.
            txtTelefone.SelectionStart = txtTelefone.Text.Length;
        }

        private void txtCep_TextChanged(object sender, EventArgs e)
        {
            txtCep.Text = AcademiaBLL.formatacaocep(txtCep.Text);
            txtCep.SelectionStart = txtCep.Text.Length;
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            // Cria uma instância do outro formulário
            IHMLogin formLogin = new IHMLogin();

            // Exibe o novo formulário
            formLogin.Show();

            // Opcional: esconder o formulário atual
            this.Hide();
        }

        private async void btnBucarCep_Click(object sender, EventArgs e)
        {
            endereco.setCep(txtCep.Text);

            Endereco enderecoPreenchido = await AcademiaBLL.buscarcepinternet(txtCep.Text);

            if (enderecoPreenchido != null)
            {
                txtRua.Text = enderecoPreenchido.getRua();
                txtBairro.Text = enderecoPreenchido.getBairro();
                txtCidade.Text = enderecoPreenchido.getCidade();
                txtEstado.Text = enderecoPreenchido.getEstado();
            }
            else
            {
                MessageBox.Show(Erro.getMsg(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
