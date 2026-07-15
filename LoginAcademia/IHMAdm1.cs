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
    public partial class IHMAdm1 : Form
    {
        private Login_Cadastro adminLogado;

        public IHMAdm1()
        {
            InitializeComponent();
        }

        public IHMAdm1(Login_Cadastro admin)
        {
            InitializeComponent();
            adminLogado = admin;
        }
        private void carregarClientes(string filtro)
        {
            DataTable dt = AcademiaDAL.consultaClientesAdm(filtro);

            if (Erro.getErro())
            {
                MessageBox.Show(Erro.getMsg(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvClientes.Rows.Clear();

            foreach (DataRow linha in dt.Rows)
            {
                dgvClientes.Rows.Add(
                    linha["ID"],
                    linha["NOME"],
                    linha["USUARIO"],
                    linha["EMAIL"],
                    linha["TELEFONE"],
                    linha["CIDADE"],
                    linha["ESTADO"]
                );
            }
        }

        private void IHMAdm1_Load(object sender, EventArgs e)
        {
            dgvClientes.Columns["colEditar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvClientes.Columns["colExcluir"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            carregarClientes("");
        }

        private void txtBucarUsuario_TextChanged(object sender, EventArgs e)
        {
            carregarClientes(txtBuscarUsuario.Text);
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string coluna = dgvClientes.Columns[e.ColumnIndex].Name;

            int cdUsuario = Convert.ToInt32(dgvClientes.Rows[e.RowIndex].Cells["colId"].Value);
            string nomeCliente = dgvClientes.Rows[e.RowIndex].Cells["colNome"].Value.ToString();

            if (coluna == "colEditar")
            {
                IHMAdm3 tela = new IHMAdm3(cdUsuario, nomeCliente, adminLogado);
                tela.Show();
                this.Hide();
            }
            else if (coluna == "colExcluir")
            {
                DialogResult resposta = MessageBox.Show(
                    "Deseja realmente excluir o usuário \"" + nomeCliente + "\"?\n\n" +
                    "Todos os treinos e dados vinculados a esse usuário também serão apagados.\n\n" +
                    "Essa ação não poderá ser desfeita.",
                    "Confirmar exclusão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (resposta == DialogResult.Yes)
                {
                    AcademiaBLL.deletarUsuario(cdUsuario);

                    if (Erro.getErro())
                    {
                        MessageBox.Show(
                            Erro.getMsg(),
                            "Erro",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return;
                    }

                    MessageBox.Show(
                        "Usuário excluído com sucesso!",
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    carregarClientes(txtBuscarUsuario.Text);
                }
            }
        }

        private void btnSairAdm_Click(object sender, EventArgs e)
        {
            IHMLogin formLogin = new IHMLogin();
            formLogin.Show();
            this.Hide();
        }
    }
}
