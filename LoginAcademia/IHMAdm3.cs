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
    public partial class IHMAdm3 : Form
    {
        private int cdUsuarioSelecionado;
        private string nomeClienteSelecionado;
        private Login_Cadastro adminLogado;

        public IHMAdm3()
        {
            InitializeComponent();
        }

        public IHMAdm3(int cdUsuario, string nomeCliente, Login_Cadastro admin)
        {
            InitializeComponent();

            cdUsuarioSelecionado = cdUsuario;
            nomeClienteSelecionado = nomeCliente;
            adminLogado = admin;
        }

        private void IHMAdmMeio_Load(object sender, EventArgs e)
        {
            if (adminLogado == null || cdUsuarioSelecionado <= 0)
            {
                MessageBox.Show(
                    "Erro ao carregar os dados do cliente. Volte ao login e tente novamente.",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                IHMLogin formLogin = new IHMLogin();
                formLogin.Show();
                this.Hide();
                return;
            }

            lblNomeCliente.Text = nomeClienteSelecionado;

            carregarTreinos();
        }

        private void carregarTreinos()
        {
            DataTable dt = AcademiaDAL.consultaTreinosAdm(cdUsuarioSelecionado);

            if (Erro.getErro())
            {
                MessageBox.Show(Erro.getMsg(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvTreinos.Rows.Clear();

            foreach (DataRow linha in dt.Rows)
            {
                dgvTreinos.Rows.Add(
                    linha["CODIGO"],
                    linha["NOME_TREINO"],
                    linha["DIVISAO"],
                    linha["DATA_INICIO"],
                    linha["DATA_FIM"],
                    linha["CRIADO_POR"]
                );
            }
        }

        private void btnCriarTreino_Click(object sender, EventArgs e)
        {
            if (adminLogado == null)
            {
                MessageBox.Show(
                    "Erro: administrador não identificado. Faça login novamente.",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            IHMAdm2 tela = new IHMAdm2(cdUsuarioSelecionado, nomeClienteSelecionado, adminLogado);
            tela.Show();
            this.Hide();
        }

        private void dgvTreinos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string coluna = dgvTreinos.Columns[e.ColumnIndex].Name;

            int cdTreino = Convert.ToInt32(dgvTreinos.Rows[e.RowIndex].Cells["colCodigo"].Value);

            if (coluna == "colEditar")
            {
                IHMAdm2 tela = new IHMAdm2(cdUsuarioSelecionado, nomeClienteSelecionado, adminLogado, cdTreino);
                tela.Show();
                this.Hide();
            }
            else if (coluna == "colExcluir")
            {
                DialogResult resposta = MessageBox.Show(
                    "Deseja realmente excluir este treino? Essa ação não poderá ser desfeita.",
                    "Confirmar exclusão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (resposta == DialogResult.Yes)
                {
                    AcademiaBLL.deletarTreino(cdTreino);

                    if (Erro.getErro())
                    {
                        MessageBox.Show(Erro.getMsg(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("Treino excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    carregarTreinos();
                }
            }
        }

        private void btnSairAdm_Click(object sender, EventArgs e)
        {
            IHMLogin formLogin = new IHMLogin();
            formLogin.Show();
            this.Hide();
        }

        private void btnMeusTreinosAdm_Click(object sender, EventArgs e)
        {
            IHMAdm1 formAdm1 = new IHMAdm1(adminLogado);
            formAdm1.Show();
            this.Hide();
        }
    }
}
