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
    public partial class IHMPerfil : Form
    {
        private Login_Cadastro usuarioLogado;
        private Endereco endereco = new Endereco();

        public IHMPerfil(Login_Cadastro lc)
        {
            InitializeComponent();
            usuarioLogado = lc;
        }

        private void IHMPerfil_Load(object sender, EventArgs e)
        {
            AcademiaDAL.consultaPerfil(usuarioLogado, endereco);

            if (Erro.getErro())
            {
                MessageBox.Show(Erro.getMsg());
                return;
            }

            lbMeusTreinos.Text = usuarioLogado.getNome();
            lbNome.Text = usuarioLogado.getNome();
            lbEmail.Text = usuarioLogado.getEmail();
            lbTelefone.Text = AcademiaBLL.formatacaotelefone(usuarioLogado.getTelefone());
            lbEndereco.Text = $"{endereco.getRua()}, {endereco.getNumero()} - {endereco.getBairro()}, {endereco.getCidade()} - {endereco.getEstado()}";
            lbData.Text = usuarioLogado.getDtCadastro().ToString("dd/MM/yyyy");
            lbAtivo.Text = usuarioLogado.getIcAtivo() ? "Ativo" : "Inativo";
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            IHMLogin formLogin = new IHMLogin();
            formLogin.Show();
            this.Close();
        }

        private void btnMeusTreinos_Click(object sender, EventArgs e)
        {
            IHMCliente formCliente = new IHMCliente(usuarioLogado);
            formCliente.Show();
            this.Close();
        }

        private void btnEditarPerfil_Click(object sender, EventArgs e)
        {
            IHMEditarPerfil formEditar = new IHMEditarPerfil(usuarioLogado);
            formEditar.Show();
            this.Close();
        }
    }
}