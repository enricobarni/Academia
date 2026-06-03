using System;
using System.Data;
using System.Drawing;
using System.Globalization;
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

            dgvTreinosCliente.Columns["colCodigoTreino"].Visible = false;
            dgvTreinosCliente.Columns["colCodigoTreinoExercicio"].Visible = false;

            string nomeCompleto = usuarioLogado.getNome();
            string primeiroNome = nomeCompleto.Split(' ')[0];
            lbNomeCliente.Text = primeiroNome + "!";

            lbData.Text = DateTime.Now.ToString(
                "dd 'de' MMMM', 'yyyy",
                new CultureInfo("pt-BR")
            );

            configurarGridTreinosCliente();
            carregarTreinosCliente();
        }

        private void configurarGridTreinosCliente()
        {
            dgvTreinosCliente.DefaultCellStyle.BackColor = Color.FromArgb(15, 15, 25);
            dgvTreinosCliente.DefaultCellStyle.ForeColor = Color.White;

            dgvTreinosCliente.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(18, 18, 30);
            dgvTreinosCliente.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;

            dgvTreinosCliente.DefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 20, 85);
            dgvTreinosCliente.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvTreinosCliente.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 20, 85);
            dgvTreinosCliente.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;

            dgvTreinosCliente.EnableHeadersVisualStyles = false;
            dgvTreinosCliente.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 15, 25);
            dgvTreinosCliente.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(128, 0, 255);
            dgvTreinosCliente.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(15, 15, 25);
            dgvTreinosCliente.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(128, 0, 255);

            dgvTreinosCliente.RowHeadersVisible = false;
            dgvTreinosCliente.BorderStyle = BorderStyle.None;
            dgvTreinosCliente.BackgroundColor = Color.FromArgb(15, 15, 25);
            dgvTreinosCliente.GridColor = Color.FromArgb(80, 80, 100);

            dgvTreinosCliente.ClearSelection();
        }

        private void carregarTreinosCliente()
        {
            DataTable dt = AcademiaDAL.consultaTreinosCliente(usuarioLogado.getCdUsuario());

            if (Erro.getErro())
            {
                MessageBox.Show(Erro.getMsg(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvTreinosCliente.Rows.Clear();

            foreach (DataRow linha in dt.Rows)
            {
                dgvTreinosCliente.Rows.Add(
                    linha["CODIGO_TREINO"],
                    linha["CODIGO_TREINO_EXERCICIO"],
                    linha["ORDEM"],
                    linha["TREINO"],
                    linha["DIVISAO"],
                    linha["INICIO"],
                    linha["FIM"],
                    linha["GRUPO_MUSCULAR"],
                    linha["EXERCICIO"],
                    linha["SERIES"],
                    linha["REPETICOES"],
                    linha["DESCANSO"],
                    linha["OBSERVACAO"]
                );
            }

            dgvTreinosCliente.ClearSelection();
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