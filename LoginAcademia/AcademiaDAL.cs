using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LoginAcademia
{
    public class AcademiaDAL
    {
        private static string strConexao =
            "Server=.\\SQLEXPRESS;" +
            "AttachDbFilename=|DataDirectory|AcademiaBD.mdf;" +
            "Database=AcademiaBD;" +
            "Integrated Security=True;" +
            "TrustServerCertificate=True;";

        private static SqlConnection conn = new SqlConnection(strConexao);
        private static SqlCommand strSQL;
        private static SqlDataReader result;

        private static void conecta()
        {
            try { conn.Open(); }
            catch (Exception) { Erro.setMsg("Erro ao conectar ao Banco de Dados!"); }
        }

        private static void desconecta()
        {
            conn.Close();
        }

        public static void consultaLogin(Login_Cadastro lc)
        {
            conecta();
            if (Erro.getErro()) return;

            string aux = "SELECT cd_usuario, nm_cliente, ds_senha, ic_admin, ic_ativo, dt_cadastro " +
             "FROM Usuario " +
             "WHERE (ds_email = @login OR nm_usuario = @login) AND ic_ativo = 1";

            strSQL = new SqlCommand(aux, conn);
            strSQL.Parameters.AddWithValue("@login", lc.getUsuario());

            Erro.setErro(false);
            result = strSQL.ExecuteReader();

            if (result.Read())
            {
                lc.setCdUsuario((int)result["cd_usuario"]);
                lc.setNome(result["nm_cliente"].ToString());
                lc.setSenha(result["ds_senha"].ToString());
                lc.setIcAdmin((bool)result["ic_admin"]);
                lc.setIcAtivo((bool)result["ic_ativo"]);
                lc.setDtCadastro((DateTime)result["dt_cadastro"]);
            }
            else
            {
                Erro.setMsg("Usuário não encontrado!");
            }

            result.Close();
            desconecta();
        }

        public static void insereCadastro(Login_Cadastro lc, Endereco end)
        {
            conecta();
            if (Erro.getErro()) return;

            string auxUsuario = "INSERT INTO Usuario (nm_usuario, nm_cliente, ds_email, ds_senha, ds_telefone, ic_admin, ic_ativo) " +
                                "OUTPUT INSERTED.cd_usuario " +
                                "VALUES (@nm_usuario, @nm_cliente, @ds_email, @ds_senha, @ds_telefone, 0, 1)";

            strSQL = new SqlCommand(auxUsuario, conn);
            strSQL.Parameters.AddWithValue("@nm_usuario", lc.getUsuario());
            strSQL.Parameters.AddWithValue("@nm_cliente", lc.getNome());
            strSQL.Parameters.AddWithValue("@ds_email", lc.getEmail());
            strSQL.Parameters.AddWithValue("@ds_senha", BCrypt.Net.BCrypt.HashPassword(lc.getSenha()));
            strSQL.Parameters.AddWithValue("@ds_telefone", lc.getTelefone());

            Erro.setErro(false);
            try
            {
                int idGerado = (int)strSQL.ExecuteScalar();
                lc.setCdUsuario(idGerado);

                string auxEndereco = "INSERT INTO Endereco (cd_usuario, ds_cep, ds_rua, ds_bairro, ds_cidade, ds_estado, ds_numero, ds_complemento) " +
                                     "VALUES (@cd_usuario, @ds_cep, @ds_rua, @ds_bairro, @ds_cidade, @ds_estado, @ds_numero, @ds_complemento)";

                strSQL = new SqlCommand(auxEndereco, conn);
                strSQL.Parameters.AddWithValue("@cd_usuario", idGerado);
                strSQL.Parameters.AddWithValue("@ds_cep", end.getCep());
                strSQL.Parameters.AddWithValue("@ds_rua", end.getRua());
                strSQL.Parameters.AddWithValue("@ds_bairro", end.getBairro());
                strSQL.Parameters.AddWithValue("@ds_cidade", end.getCidade());
                strSQL.Parameters.AddWithValue("@ds_estado", end.getEstado());
                strSQL.Parameters.AddWithValue("@ds_numero", end.getNumero());
                strSQL.Parameters.AddWithValue("@ds_complemento", string.IsNullOrWhiteSpace(end.getComplemento()) ? (object)DBNull.Value : end.getComplemento());

                strSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Erro.setMsg(ex.Message);
            }

            desconecta();
        }
        public static void consultaPerfil(Login_Cadastro lc, Endereco end)
        {
            conecta();
            if (Erro.getErro()) return;

            string aux = "SELECT u.nm_cliente, u.ds_email, u.ds_telefone, u.ic_ativo, u.dt_cadastro, " +
                         "e.ds_rua, e.ds_numero, e.ds_bairro, e.ds_cidade, e.ds_estado, e.ds_cep " +
                         "FROM Usuario u " +
                         "INNER JOIN Endereco e ON e.cd_usuario = u.cd_usuario " +
                         "WHERE u.cd_usuario = @cd_usuario";

            strSQL = new SqlCommand(aux, conn);
            strSQL.Parameters.AddWithValue("@cd_usuario", lc.getCdUsuario());

            Erro.setErro(false);
            result = strSQL.ExecuteReader();

            if (result.Read())
            {
                lc.setNome(result["nm_cliente"].ToString());
                lc.setEmail(result["ds_email"].ToString());
                lc.setTelefone(result["ds_telefone"].ToString());
                lc.setIcAtivo((bool)result["ic_ativo"]);
                lc.setDtCadastro((DateTime)result["dt_cadastro"]);

                end.setRua(result["ds_rua"].ToString());
                end.setNumero(result["ds_numero"].ToString());
                end.setBairro(result["ds_bairro"].ToString());
                end.setCidade(result["ds_cidade"].ToString());
                end.setEstado(result["ds_estado"].ToString());
                end.setCep(result["ds_cep"].ToString());
            }
            else
            {
                Erro.setMsg("Perfil não encontrado!");
            }

            result.Close();
            desconecta();
        }
    }
}
