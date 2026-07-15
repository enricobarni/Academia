using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LoginAcademia
{
    public partial class IHMAdm2 : Form
    {
        private int cdUsuarioSelecionado;
        private string nomeClienteSelecionado;
        private Login_Cadastro adminLogado;
        private int cdTreinoSelecionado;
        private bool modoEdicao;
        private bool limpandoCampos = false;

        private List<TreinoExercicio> listaExercicios = new List<TreinoExercicio>();

        public IHMAdm2()
        {
            InitializeComponent();
        }

        public IHMAdm2(int cdUsuario, string nomeCliente, Login_Cadastro admin, int cdTreino)
        {
            InitializeComponent();

            cdUsuarioSelecionado = cdUsuario;
            nomeClienteSelecionado = nomeCliente;
            adminLogado = admin;

            cdTreinoSelecionado = cdTreino;
            modoEdicao = true;
        }

        public IHMAdm2(int cdUsuario, string nomeCliente, Login_Cadastro admin)
        {
            InitializeComponent();

            cdUsuarioSelecionado = cdUsuario;
            nomeClienteSelecionado = nomeCliente;
            adminLogado = admin;

            cdTreinoSelecionado = 0;
            modoEdicao = false;
        }

        private void IHMAdm2_Load(object sender, EventArgs e)
        {
            prepararTela();
            carregarDivisoes();
            carregarGruposMusculares();
            prepararCamposNumericos();

            if (modoEdicao)
            {
                carregarTreinoParaEdicao();
            }
        }

        private void prepararTela()
        {
            if (modoEdicao)
            {
                this.Text = "Editar Treino";
            }
            else
            {
                this.Text = "Criar Treino";
            }

            lblNomeCliente.Text = "Cliente: " + nomeClienteSelecionado;
        }

        private void carregarDivisoes()
        {
            cmbDivisao.Items.Clear();

            cmbDivisao.Items.Add("A");
            cmbDivisao.Items.Add("B");
            cmbDivisao.Items.Add("C");
            cmbDivisao.Items.Add("D");
            cmbDivisao.Items.Add("E");

            cmbDivisao.SelectedIndex = -1;
        }

        private void prepararCamposNumericos()
        {
            numOrdem.Minimum = 1;
            numOrdem.Maximum = 50;
            numOrdem.Value = 1;

            numSeries.Minimum = 1;
            numSeries.Maximum = 20;
            numSeries.Value = 3;

            numRepeticoes.Minimum = 1;
            numRepeticoes.Maximum = 100;
            numRepeticoes.Value = 10;

            numDescanso.Minimum = 0;
            numDescanso.Maximum = 600;
            numDescanso.Value = 60;
        }

        private void carregarGruposMusculares()
        {
            DataTable dt = AcademiaDAL.consultaGruposMusculares();

            if (Erro.getErro())
            {
                MessageBox.Show(Erro.getMsg(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmbGrupoMuscular.DataSource = dt;
            cmbGrupoMuscular.DisplayMember = "nm_grupoMuscular";
            cmbGrupoMuscular.ValueMember = "cd_grupoMuscular";
            cmbGrupoMuscular.SelectedIndex = -1;
            cmbGrupoMuscular.Text = "";
        }

        private void cmbGrupoMuscular_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (limpandoCampos)
            {
                return;
            }

            if (cmbGrupoMuscular.SelectedIndex == -1)
            {
                cmbExercicio.DataSource = null;
                cmbExercicio.Text = "";
                return;
            }

            if (cmbGrupoMuscular.SelectedValue == null)
            {
                return;
            }

            if (cmbGrupoMuscular.SelectedValue is DataRowView)
            {
                return;
            }

            int cdGrupoMuscular = Convert.ToInt32(cmbGrupoMuscular.SelectedValue);
            carregarExercicios(cdGrupoMuscular);
        }

        private void carregarExercicios(int cdGrupoMuscular)
        {
            DataTable dt = AcademiaDAL.consultaExerciciosPorGrupo(cdGrupoMuscular);

            if (Erro.getErro())
            {
                MessageBox.Show(Erro.getMsg(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmbExercicio.DataSource = dt;
            cmbExercicio.DisplayMember = "nm_exercicio";
            cmbExercicio.ValueMember = "cd_exercicio";
            cmbExercicio.SelectedIndex = -1;
        }

        private bool validarExercicio()
        {
            if (cmbDivisao.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione a divisão do treino.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDivisao.Focus();
                return false;
            }

            if (cmbGrupoMuscular.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione o grupo muscular.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbGrupoMuscular.Focus();
                return false;
            }

            if (cmbExercicio.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione o exercício.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbExercicio.Focus();
                return false;
            }

            return true;
        }

        private void btnAdicionarExercicio_Click(object sender, EventArgs e)
        {
            if (!validarExercicio())
            {
                return;
            }

            TreinoExercicio exercicio = new TreinoExercicio();

            exercicio.setCdExercicio(Convert.ToInt32(cmbExercicio.SelectedValue));
            exercicio.setNmGrupoMuscular(cmbGrupoMuscular.Text);
            exercicio.setNmExercicio(cmbExercicio.Text);
            exercicio.setNrOrdem(Convert.ToInt32(numOrdem.Value));
            exercicio.setQtSeries(Convert.ToInt32(numSeries.Value));
            exercicio.setQtRepeticoes(Convert.ToInt32(numRepeticoes.Value));
            exercicio.setQtDescansoSegundos(Convert.ToInt32(numDescanso.Value));
            exercicio.setDsObservacao(txtObservacao.Text);

            listaExercicios.Add(exercicio);

            atualizarGridExercicios();
            limparCamposExercicio();
        }

        private void atualizarGridExercicios()
        {
            dgvExercicios.Rows.Clear();

            foreach (TreinoExercicio exercicio in listaExercicios)
            {
                dgvExercicios.Rows.Add(
                    exercicio.getCdExercicio(),
                    exercicio.getNrOrdem(),
                    exercicio.getNmGrupoMuscular(),
                    exercicio.getNmExercicio(),
                    exercicio.getQtSeries(),
                    exercicio.getQtRepeticoes(),
                    exercicio.getDescansoFormatado(),
                    exercicio.getDsObservacao()
                );
            }
        }

        private void limparCamposExercicio()
        {
            limpandoCampos = true;

            cmbExercicio.DataSource = null;
            cmbExercicio.SelectedIndex = -1;
            cmbExercicio.Text = "";

            cmbGrupoMuscular.SelectedIndex = -1;
            cmbGrupoMuscular.SelectedItem = null;
            cmbGrupoMuscular.Text = "";

            limpandoCampos = false;

            if (numOrdem.Value < numOrdem.Maximum)
            {
                numOrdem.Value = numOrdem.Value + 1;
            }

            numSeries.Value = 3;
            numRepeticoes.Value = 10;
            numDescanso.Value = 60;

            txtObservacao.Clear();
        }

        private void dgvExercicios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string coluna = dgvExercicios.Columns[e.ColumnIndex].Name;

            if (coluna == "colExcluir")
            {
                listaExercicios.RemoveAt(e.RowIndex);
                atualizarGridExercicios();
            }
        }

        private void btnSalvarTreino_Click(object sender, EventArgs e)
        {
            if (adminLogado == null)
            {
                MessageBox.Show("Erro: administrador não identificado. Faça login novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Treino treino = new Treino();

            treino.setNmTreino(txtNomeTreino.Text);
            treino.setTpDivisao(cmbDivisao.Text);
            treino.setCdUsuario(cdUsuarioSelecionado);
            treino.setCdAdmin(adminLogado.getCdUsuario());

            if (modoEdicao)
            {
                treino.setCdTreino(cdTreinoSelecionado);
                AcademiaBLL.editarTreino(treino, listaExercicios);
            }
            else
            {
                AcademiaBLL.inserirTreino(treino, listaExercicios);
            }

            if (Erro.getErro())
            {
                MessageBox.Show(Erro.getMsg(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (modoEdicao)
            {
                MessageBox.Show("Treino editado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Treino cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            IHMAdm3 tela = new IHMAdm3(cdUsuarioSelecionado, nomeClienteSelecionado, adminLogado);
            tela.Show();
            this.Hide();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            IHMLogin formLogin = new IHMLogin();
            formLogin.Show();
            this.Hide();
        }

        private void btnMeusTreinos_Click(object sender, EventArgs e)
        {
            IHMAdm3 formTreino = new IHMAdm3(cdUsuarioSelecionado, nomeClienteSelecionado, adminLogado);
            formTreino.Show();
            this.Hide();
        }

        private void carregarTreinoParaEdicao()
        {
            Treino treino = AcademiaDAL.consultaTreinoPorId(cdTreinoSelecionado);

            if (Erro.getErro())
            {
                MessageBox.Show(Erro.getMsg(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (treino == null)
            {
                MessageBox.Show("Treino não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtNomeTreino.Text = treino.getNmTreino();
            cmbDivisao.Text = treino.getTpDivisao();

            listaExercicios = AcademiaDAL.consultaExerciciosDoTreino(cdTreinoSelecionado);

            if (Erro.getErro())
            {
                MessageBox.Show(Erro.getMsg(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            atualizarGridExercicios();
        }
    }
}