using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginAcademia
{
    public partial class IHMEditarPerfil : Form
    {
        private Login_Cadastro usuarioLogado;
        private Endereco enderecoUsuario = new Endereco();
        private bool ajustandoTexto = false;

        public IHMEditarPerfil()
        {
            InitializeComponent();

            configurarCampos();
            conectarEventosValidacao();
        }

        public IHMEditarPerfil(Login_Cadastro lc)
        {
            InitializeComponent();

            usuarioLogado = lc;

            configurarCampos();
            conectarEventosValidacao();
            carregarDadosPerfil();
        }

        private void IHMEditarPerfil_Load(object sender, EventArgs e)
        {
            
        }

        private void configurarCampos()
        {
            txtUsuario.MaxLength = 30;
            txtTelefone.MaxLength = 11;
            txtCEP.MaxLength = 8;
            txtEmail.MaxLength = 100;
            txtNumero.MaxLength = 10;
            txtComplemento.MaxLength = 50;

            txtRua.ReadOnly = true;
            txtBairro.ReadOnly = true;
            txtCidade.ReadOnly = true;
            txtEstado.ReadOnly = true;
        }

        private void conectarEventosValidacao()
        {
            txtUsuario.TextChanged -= txtUsuario_TextChanged;
            txtUsuario.TextChanged += txtUsuario_TextChanged;

            txtTelefone.TextChanged -= txtTelefone_TextChanged;
            txtTelefone.TextChanged += txtTelefone_TextChanged;

            txtCEP.TextChanged -= txtCEP_TextChanged;
            txtCEP.TextChanged += txtCEP_TextChanged;

            txtNumero.TextChanged -= txtNumero_TextChanged;
            txtNumero.TextChanged += txtNumero_TextChanged;
        }

        private void txtEstado_TextChanged(object sender, EventArgs e)
        {
            if (ajustandoTexto)
            {
                return;
            }

            ajustandoTexto = true;

            string textoLimpo = "";

            foreach (char c in txtEstado.Text)
            {
                if (char.IsLetter(c))
                {
                    textoLimpo += char.ToUpper(c);
                }
            }

            if (textoLimpo.Length > 2)
            {
                textoLimpo = textoLimpo.Substring(0, 2);
            }

            txtEstado.Text = textoLimpo;
            txtEstado.SelectionStart = txtEstado.Text.Length;

            ajustandoTexto = false;
        }

        private void txtNumero_TextChanged(object sender, EventArgs e)
        {
            if (ajustandoTexto)
            {
                return;
            }

            ajustandoTexto = true;

            string numeros = Regex.Replace(txtNumero.Text, @"\D", "");

            if (numeros.Length > 10)
            {
                numeros = numeros.Substring(0, 10);
            }

            txtNumero.Text = numeros;
            txtNumero.SelectionStart = txtNumero.Text.Length;

            ajustandoTexto = false;
        }

        private void txtCEP_TextChanged(object sender, EventArgs e)
        {
            if (ajustandoTexto)
            {
                return;
            }

            ajustandoTexto = true;

            string numeros = Regex.Replace(txtCEP.Text, @"\D", "");

            if (numeros.Length > 8)
            {
                numeros = numeros.Substring(0, 8);
            }

            txtCEP.Text = numeros;
            txtCEP.SelectionStart = txtCEP.Text.Length;

            ajustandoTexto = false;
        }

        private void txtTelefone_TextChanged(object sender, EventArgs e)
        {
            if (ajustandoTexto)
            {
                return;
            }

            ajustandoTexto = true;

            string numeros = Regex.Replace(txtTelefone.Text, @"\D", "");

            if (numeros.Length > 11)
            {
                numeros = numeros.Substring(0, 11);
            }

            txtTelefone.Text = numeros;
            txtTelefone.SelectionStart = txtTelefone.Text.Length;

            ajustandoTexto = false;
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            if (ajustandoTexto)
            {
                return;
            }

            ajustandoTexto = true;

            string textoLimpo = "";

            foreach (char c in txtUsuario.Text)
            {
                if (char.IsLetterOrDigit(c))
                {
                    textoLimpo += c;
                }
            }

            if (textoLimpo.Length > 30)
            {
                textoLimpo = textoLimpo.Substring(0, 30);
            }

            txtUsuario.Text = textoLimpo;
            txtUsuario.SelectionStart = txtUsuario.Text.Length;

            ajustandoTexto = false;
        }

        private void carregarDadosPerfil()
        {
            if (usuarioLogado == null)
            {
                MessageBox.Show(
                    "Erro: usuário não identificado. Faça login novamente.",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                IHMLogin formLogin = new IHMLogin();
                formLogin.Show();
                this.Close();
                return;
            }

            AcademiaDAL.consultaPerfil(usuarioLogado, enderecoUsuario);

            if (Erro.getErro())
            {
                MessageBox.Show(Erro.getMsg(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtUsuario.Text = usuarioLogado.getUsuario();
            txtTelefone.Text = usuarioLogado.getTelefone();
            txtEmail.Text = usuarioLogado.getEmail();

            txtCEP.Text = enderecoUsuario.getCep();
            txtRua.Text = enderecoUsuario.getRua();
            txtNumero.Text = enderecoUsuario.getNumero();
            txtComplemento.Text = enderecoUsuario.getComplemento();
            txtBairro.Text = enderecoUsuario.getBairro();
            txtCidade.Text = enderecoUsuario.getCidade();
            txtEstado.Text = enderecoUsuario.getEstado();
        }


        private async void btnBucarCep_Click(object sender, EventArgs e)
        {
            Endereco enderecoBuscado = await AcademiaBLL.buscarcepinternet(txtCEP.Text);

            if (Erro.getErro())
            {
                MessageBox.Show(Erro.getMsg(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (enderecoBuscado == null)
            {
                return;
            }

            txtCEP.Text = Regex.Replace(enderecoBuscado.getCep(), @"\D", "");
            txtRua.Text = enderecoBuscado.getRua();
            txtBairro.Text = enderecoBuscado.getBairro();
            txtCidade.Text = enderecoBuscado.getCidade();
            txtEstado.Text = enderecoBuscado.getEstado();

            txtNumero.Focus();
        }

        private void btnEditarPerfil_Click(object sender, EventArgs e)
        {
            if (usuarioLogado == null)
            {
                MessageBox.Show(
                    "Erro: usuário não identificado. Faça login novamente.",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            usuarioLogado.setUsuario(txtUsuario.Text);
            usuarioLogado.setEmail(txtEmail.Text);
            usuarioLogado.setTelefone(Regex.Replace(txtTelefone.Text, @"\D", ""));

            enderecoUsuario.setCep(Regex.Replace(txtCEP.Text, @"\D", ""));
            enderecoUsuario.setRua(txtRua.Text);
            enderecoUsuario.setNumero(txtNumero.Text);
            enderecoUsuario.setComplemento(txtComplemento.Text);
            enderecoUsuario.setBairro(txtBairro.Text);
            enderecoUsuario.setCidade(txtCidade.Text);
            enderecoUsuario.setEstado(txtEstado.Text.ToUpper());

            AcademiaBLL.editarPerfil(usuarioLogado, enderecoUsuario);

            if (Erro.getErro())
            {
                MessageBox.Show(
                    Erro.getMsg(),
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            MessageBox.Show(
                "Perfil atualizado com sucesso!",
                "Sucesso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            IHMPerfil formPerfil = new IHMPerfil(usuarioLogado);
            formPerfil.Show();
            this.Close();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            if (usuarioLogado == null)
            {
                MessageBox.Show(
                    "Erro: usuário não identificado. Faça login novamente.",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                IHMLogin formLogin = new IHMLogin();
                formLogin.Show();
                this.Close();
                return;
            }

            IHMPerfil formPerfil = new IHMPerfil(usuarioLogado);
            formPerfil.Show();
            this.Close();
        }
        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            if (!char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTelefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (Regex.Replace(txtTelefone.Text, @"\D", "").Length >= 11)
            {
                e.Handled = true;
            }
        }

        private void txtCEP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (Regex.Replace(txtCEP.Text, @"\D", "").Length >= 8)
            {
                e.Handled = true;
            }
        }

        private void txtEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            if (!char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (txtEstado.Text.Length >= 2)
            {
                e.Handled = true;
            }

            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
