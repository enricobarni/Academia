using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace LoginAcademia
{
    public class AcademiaDAL
    {
        private static string strConexao =
    "Server=.\\SQLEXPRESS;" +
    "Database=AcademiaBD;" +
    "Integrated Security=True;" +
    "TrustServerCertificate=True;";

        private static SqlConnection conn = new SqlConnection(strConexao);
        private static SqlCommand strSQL;
        private static SqlDataReader result;

        private static void conecta()
        {
            Erro.setErro(false);

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
            }
            catch (Exception)
            {
                Erro.setMsg("Erro ao conectar ao banco de dados.");
            }
        }

        private static void desconecta()
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
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

            string aux = "SELECT " +
                         "u.nm_usuario, " +
                         "u.nm_cliente, " +
                         "u.ds_email, " +
                         "u.ds_telefone, " +
                         "u.ic_ativo, " +
                         "u.dt_cadastro, " +
                         "e.ds_rua, " +
                         "e.ds_numero, " +
                         "e.ds_complemento, " +
                         "e.ds_bairro, " +
                         "e.ds_cidade, " +
                         "e.ds_estado, " +
                         "e.ds_cep " +
                         "FROM Usuario u " +
                         "INNER JOIN Endereco e ON e.cd_usuario = u.cd_usuario " +
                         "WHERE u.cd_usuario = @cd_usuario";

            strSQL = new SqlCommand(aux, conn);
            strSQL.Parameters.AddWithValue("@cd_usuario", lc.getCdUsuario());

            Erro.setErro(false);

            try
            {
                result = strSQL.ExecuteReader();

                if (result.Read())
                {
                    lc.setUsuario(result["nm_usuario"].ToString());
                    lc.setNome(result["nm_cliente"].ToString());
                    lc.setEmail(result["ds_email"].ToString());
                    lc.setTelefone(result["ds_telefone"].ToString());
                    lc.setIcAtivo((bool)result["ic_ativo"]);
                    lc.setDtCadastro((DateTime)result["dt_cadastro"]);

                    end.setRua(result["ds_rua"].ToString());
                    end.setNumero(result["ds_numero"].ToString());
                    end.setComplemento(result["ds_complemento"] == DBNull.Value ? "" : result["ds_complemento"].ToString());
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
            }
            catch (Exception ex)
            {
                Erro.setMsg(ex.Message);
            }
            finally
            {
                desconecta();
            }
        }

        public static void editaPerfil(Login_Cadastro lc, Endereco end)
        {
            Erro.setErro(false);

            SqlTransaction transacao = null;

            try
            {
                conecta();

                if (Erro.getErro())
                {
                    return;
                }

                transacao = conn.BeginTransaction();

                string auxUsuario = "UPDATE Usuario SET " +
                                    "nm_usuario = @nm_usuario, " +
                                    "ds_email = @ds_email, " +
                                    "ds_telefone = @ds_telefone " +
                                    "WHERE cd_usuario = @cd_usuario";

                strSQL = new SqlCommand(auxUsuario, conn, transacao);
                strSQL.Parameters.AddWithValue("@nm_usuario", lc.getUsuario());
                strSQL.Parameters.AddWithValue("@ds_email", lc.getEmail());
                strSQL.Parameters.AddWithValue("@ds_telefone", lc.getTelefone());
                strSQL.Parameters.AddWithValue("@cd_usuario", lc.getCdUsuario());

                strSQL.ExecuteNonQuery();

                string auxEndereco = "UPDATE Endereco SET " +
                                     "ds_cep = @ds_cep, " +
                                     "ds_rua = @ds_rua, " +
                                     "ds_numero = @ds_numero, " +
                                     "ds_complemento = @ds_complemento, " +
                                     "ds_bairro = @ds_bairro, " +
                                     "ds_cidade = @ds_cidade, " +
                                     "ds_estado = @ds_estado " +
                                     "WHERE cd_usuario = @cd_usuario";

                strSQL = new SqlCommand(auxEndereco, conn, transacao);
                strSQL.Parameters.AddWithValue("@ds_cep", end.getCep());
                strSQL.Parameters.AddWithValue("@ds_rua", end.getRua());
                strSQL.Parameters.AddWithValue("@ds_numero", end.getNumero());
                strSQL.Parameters.AddWithValue("@ds_complemento", string.IsNullOrWhiteSpace(end.getComplemento()) ? (object)DBNull.Value : end.getComplemento());
                strSQL.Parameters.AddWithValue("@ds_bairro", end.getBairro());
                strSQL.Parameters.AddWithValue("@ds_cidade", end.getCidade());
                strSQL.Parameters.AddWithValue("@ds_estado", end.getEstado());
                strSQL.Parameters.AddWithValue("@cd_usuario", lc.getCdUsuario());

                strSQL.ExecuteNonQuery();

                transacao.Commit();
            }
            catch (Exception ex)
            {
                if (transacao != null)
                {
                    transacao.Rollback();
                }

                Erro.setMsg(ex.Message);
            }
            finally
            {
                desconecta();
            }
        }

        public static DataTable consultaClientesAdm(string filtro)
        {
            Erro.setErro(false);

            DataTable dt = new DataTable();

            try
            {
                conecta();
                if (Erro.getErro()) return dt;

                string aux = "SELECT " +
                             "u.cd_usuario AS ID, " +
                             "u.nm_cliente AS NOME, " +
                             "u.nm_usuario AS USUARIO, " +
                             "u.ds_email AS EMAIL, " +
                             "u.ds_telefone AS TELEFONE, " +
                             "e.ds_cidade AS CIDADE, " +
                             "e.ds_estado AS ESTADO " +
                             "FROM Usuario u " +
                             "INNER JOIN Endereco e ON e.cd_usuario = u.cd_usuario " +
                             "WHERE u.ic_admin = 0 " +
                             "AND u.ic_ativo = 1 " +
                             "AND (@filtro = '' " +
                             "OR u.nm_cliente LIKE @busca " +
                             "OR u.nm_usuario LIKE @busca " +
                             "OR u.ds_email LIKE @busca) " +
                             "ORDER BY u.nm_cliente";

                strSQL = new SqlCommand(aux, conn);
                strSQL.Parameters.AddWithValue("@filtro", filtro);
                strSQL.Parameters.AddWithValue("@busca", "%" + filtro + "%");

                SqlDataAdapter da = new SqlDataAdapter(strSQL);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Erro.setMsg(ex.Message);
            }
            finally
            {
                desconecta();
            }

            return dt;
        }
        public static DataTable consultaTreinosAdm(int cdUsuario)
        {
            Erro.setErro(false);

            DataTable dt = new DataTable();

            try
            {
                conecta();
                if (Erro.getErro()) return dt;

                string aux = "SELECT " +
                             "t.cd_treino AS CODIGO, " +
                             "t.nm_treino AS NOME_TREINO, " +
                             "t.tp_divisao AS DIVISAO, " +
                             "CONVERT(VARCHAR(10), t.dt_inicio, 103) AS DATA_INICIO, " +
                             "CONVERT(VARCHAR(10), t.dt_fim, 103) AS DATA_FIM, " +
                             "adm.nm_cliente AS CRIADO_POR " +
                             "FROM Treino t " +
                             "INNER JOIN Usuario adm ON adm.cd_usuario = t.cd_admin " +
                             "WHERE t.cd_usuario = @cd_usuario " +
                             "AND t.ic_ativo = 1 " +
                             "ORDER BY t.dt_inicio DESC, t.tp_divisao";

                strSQL = new SqlCommand(aux, conn);
                strSQL.Parameters.AddWithValue("@cd_usuario", cdUsuario);

                SqlDataAdapter da = new SqlDataAdapter(strSQL);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Erro.setMsg(ex.Message);
            }
            finally
            {
                desconecta();
            }

            return dt;
        }
        public static DataTable consultaTreinosCliente(int cdUsuario)
        {
            Erro.setErro(false);

            DataTable dt = new DataTable();

            try
            {
                conecta();

                if (Erro.getErro())
                {
                    return dt;
                }

                string aux = "SELECT " +
                             "t.cd_treino AS CODIGO_TREINO, " +
                             "te.cd_treinoExercicio AS CODIGO_TREINO_EXERCICIO, " +
                             "te.nr_ordem AS ORDEM, " +
                             "t.nm_treino AS TREINO, " +
                             "t.tp_divisao AS DIVISAO, " +
                             "CONVERT(VARCHAR(10), t.dt_inicio, 103) AS INICIO, " +
                             "CONVERT(VARCHAR(10), t.dt_fim, 103) AS FIM, " +
                             "gm.nm_grupoMuscular AS GRUPO_MUSCULAR, " +
                             "e.nm_exercicio AS EXERCICIO, " +
                             "te.qt_series AS SERIES, " +
                             "te.qt_repeticoes AS REPETICOES, " +
                             "CAST(te.qt_descansoSegundos AS VARCHAR) + 's' AS DESCANSO, " +
                             "ISNULL(te.ds_observacao, '') AS OBSERVACAO " +
                             "FROM Treino t " +
                             "INNER JOIN TreinoExercicio te ON te.cd_treino = t.cd_treino " +
                             "INNER JOIN Exercicio e ON e.cd_exercicio = te.cd_exercicio " +
                             "INNER JOIN GrupoMuscular gm ON gm.cd_grupoMuscular = e.cd_grupoMuscular " +
                             "WHERE t.cd_usuario = @cd_usuario " +
                             "ORDER BY t.dt_inicio DESC, t.tp_divisao, te.nr_ordem";

                strSQL = new SqlCommand(aux, conn);
                strSQL.Parameters.AddWithValue("@cd_usuario", cdUsuario);

                SqlDataAdapter da = new SqlDataAdapter(strSQL);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Erro.setMsg(ex.Message);
            }
            finally
            {
                desconecta();
            }

            return dt;
        }
        public static DataTable consultaGruposMusculares()
        {
            Erro.setErro(false);

            DataTable dt = new DataTable();

            try
            {
                conecta();
                if (Erro.getErro()) return dt;

                string aux = "SELECT " +
                             "cd_grupoMuscular, " +
                             "nm_grupoMuscular " +
                             "FROM GrupoMuscular " +
                             "ORDER BY nm_grupoMuscular";

                strSQL = new SqlCommand(aux, conn);

                SqlDataAdapter da = new SqlDataAdapter(strSQL);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Erro.setMsg(ex.Message);
            }
            finally
            {
                desconecta();
            }

            return dt;
        }
        public static DataTable consultaExerciciosPorGrupo(int cdGrupoMuscular)
        {
            Erro.setErro(false);

            DataTable dt = new DataTable();

            try
            {
                conecta();
                if (Erro.getErro()) return dt;

                string aux = "SELECT " +
                             "cd_exercicio, " +
                             "nm_exercicio " +
                             "FROM Exercicio " +
                             "WHERE cd_grupoMuscular = @cd_grupoMuscular " +
                             "ORDER BY nm_exercicio";

                strSQL = new SqlCommand(aux, conn);
                strSQL.Parameters.AddWithValue("@cd_grupoMuscular", cdGrupoMuscular);

                SqlDataAdapter da = new SqlDataAdapter(strSQL);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Erro.setMsg(ex.Message);
            }
            finally
            {
                desconecta();
            }

            return dt;
        }

        public static void insereTreino(Treino treino, List<TreinoExercicio> exercicios)
        {
            Erro.setErro(false);

            SqlTransaction transacao = null;

            try
            {
                conecta();

                if (Erro.getErro())
                {
                    return;
                }

                transacao = conn.BeginTransaction();

                string auxTreino = "INSERT INTO Treino " +
                                   "(nm_treino, ds_treino, tp_divisao, cd_usuario, cd_admin) " +
                                   "OUTPUT INSERTED.cd_treino " +
                                   "VALUES " +
                                   "(@nm_treino, @ds_treino, @tp_divisao, @cd_usuario, @cd_admin)";

                strSQL = new SqlCommand(auxTreino, conn, transacao);
                strSQL.Parameters.AddWithValue("@nm_treino", treino.getNmTreino());
                strSQL.Parameters.AddWithValue("@ds_treino", string.IsNullOrWhiteSpace(treino.getDsTreino()) ? (object)DBNull.Value : treino.getDsTreino());
                strSQL.Parameters.AddWithValue("@tp_divisao", treino.getTpDivisao());
                strSQL.Parameters.AddWithValue("@cd_usuario", treino.getCdUsuario());
                strSQL.Parameters.AddWithValue("@cd_admin", treino.getCdAdmin());

                int cdTreinoGerado = Convert.ToInt32(strSQL.ExecuteScalar());

                foreach (TreinoExercicio exercicio in exercicios)
                {
                    string auxExercicio = "INSERT INTO TreinoExercicio " +
                                          "(cd_treino, cd_exercicio, qt_series, qt_repeticoes, qt_descansoSegundos, nr_ordem, ds_observacao) " +
                                          "VALUES " +
                                          "(@cd_treino, @cd_exercicio, @qt_series, @qt_repeticoes, @qt_descansoSegundos, @nr_ordem, @ds_observacao)";

                    strSQL = new SqlCommand(auxExercicio, conn, transacao);
                    strSQL.Parameters.AddWithValue("@cd_treino", cdTreinoGerado);
                    strSQL.Parameters.AddWithValue("@cd_exercicio", exercicio.getCdExercicio());
                    strSQL.Parameters.AddWithValue("@qt_series", exercicio.getQtSeries());
                    strSQL.Parameters.AddWithValue("@qt_repeticoes", exercicio.getQtRepeticoes());
                    strSQL.Parameters.AddWithValue("@qt_descansoSegundos", exercicio.getQtDescansoSegundos());
                    strSQL.Parameters.AddWithValue("@nr_ordem", exercicio.getNrOrdem());
                    strSQL.Parameters.AddWithValue("@ds_observacao", string.IsNullOrWhiteSpace(exercicio.getDsObservacao()) ? (object)DBNull.Value : exercicio.getDsObservacao());

                    strSQL.ExecuteNonQuery();
                }

                transacao.Commit();
            }
            catch (Exception ex)
            {
                if (transacao != null)
                {
                    transacao.Rollback();
                }

                Erro.setMsg(ex.Message);
            }
            finally
            {
                desconecta();
            }
        }
        public static void deletaTreino(int cdTreino)
        {
            Erro.setErro(false);

            try
            {
                conecta();

                if (Erro.getErro())
                {
                    return;
                }

                string aux = "DELETE FROM Treino " +
                             "WHERE cd_treino = @cd_treino";

                strSQL = new SqlCommand(aux, conn);
                strSQL.Parameters.AddWithValue("@cd_treino", cdTreino);

                strSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Erro.setMsg(ex.Message);
            }
            finally
            {
                desconecta();
            }
        }

        public static Treino consultaTreinoPorId(int cdTreino)
        {
            Erro.setErro(false);

            Treino treino = null;

            try
            {
                conecta();

                if (Erro.getErro())
                {
                    return null;
                }

                string aux = "SELECT " +
                             "cd_treino, nm_treino, ds_treino, tp_divisao, dt_inicio, dt_fim, cd_usuario, cd_admin, ic_ativo, dt_cadastro " +
                             "FROM Treino " +
                             "WHERE cd_treino = @cd_treino";

                strSQL = new SqlCommand(aux, conn);
                strSQL.Parameters.AddWithValue("@cd_treino", cdTreino);

                result = strSQL.ExecuteReader();

                if (result.Read())
                {
                    treino = new Treino();

                    treino.setCdTreino(Convert.ToInt32(result["cd_treino"]));
                    treino.setNmTreino(result["nm_treino"].ToString());
                    treino.setDsTreino(result["ds_treino"] == DBNull.Value ? "" : result["ds_treino"].ToString());
                    treino.setTpDivisao(result["tp_divisao"].ToString());
                    treino.setDtInicio(Convert.ToDateTime(result["dt_inicio"]));
                    treino.setDtFim(Convert.ToDateTime(result["dt_fim"]));
                    treino.setCdUsuario(Convert.ToInt32(result["cd_usuario"]));
                    treino.setCdAdmin(Convert.ToInt32(result["cd_admin"]));
                    treino.setIcAtivo(Convert.ToBoolean(result["ic_ativo"]));
                    treino.setDtCadastro(Convert.ToDateTime(result["dt_cadastro"]));
                }
                else
                {
                    Erro.setMsg("Treino não encontrado.");
                }

                result.Close();
            }
            catch (Exception ex)
            {
                Erro.setMsg(ex.Message);
            }
            finally
            {
                desconecta();
            }

            return treino;
        }

        public static List<TreinoExercicio> consultaExerciciosDoTreino(int cdTreino)
        {
            Erro.setErro(false);

            List<TreinoExercicio> lista = new List<TreinoExercicio>();

            try
            {
                conecta();

                if (Erro.getErro())
                {
                    return lista;
                }

                string aux = "SELECT " +
                             "te.cd_treinoExercicio, " +
                             "te.cd_treino, " +
                             "te.cd_exercicio, " +
                             "gm.nm_grupoMuscular, " +
                             "e.nm_exercicio, " +
                             "te.qt_series, " +
                             "te.qt_repeticoes, " +
                             "te.qt_descansoSegundos, " +
                             "te.nr_ordem, " +
                             "te.ds_observacao " +
                             "FROM TreinoExercicio te " +
                             "INNER JOIN Exercicio e ON e.cd_exercicio = te.cd_exercicio " +
                             "INNER JOIN GrupoMuscular gm ON gm.cd_grupoMuscular = e.cd_grupoMuscular " +
                             "WHERE te.cd_treino = @cd_treino " +
                             "ORDER BY te.nr_ordem";

                strSQL = new SqlCommand(aux, conn);
                strSQL.Parameters.AddWithValue("@cd_treino", cdTreino);

                result = strSQL.ExecuteReader();

                while (result.Read())
                {
                    TreinoExercicio exercicio = new TreinoExercicio();

                    exercicio.setCdTreinoExercicio(Convert.ToInt32(result["cd_treinoExercicio"]));
                    exercicio.setCdTreino(Convert.ToInt32(result["cd_treino"]));
                    exercicio.setCdExercicio(Convert.ToInt32(result["cd_exercicio"]));
                    exercicio.setNmGrupoMuscular(result["nm_grupoMuscular"].ToString());
                    exercicio.setNmExercicio(result["nm_exercicio"].ToString());
                    exercicio.setQtSeries(Convert.ToInt32(result["qt_series"]));
                    exercicio.setQtRepeticoes(Convert.ToInt32(result["qt_repeticoes"]));
                    exercicio.setQtDescansoSegundos(Convert.ToInt32(result["qt_descansoSegundos"]));
                    exercicio.setNrOrdem(Convert.ToInt32(result["nr_ordem"]));
                    exercicio.setDsObservacao(result["ds_observacao"] == DBNull.Value ? "" : result["ds_observacao"].ToString());

                    lista.Add(exercicio);
                }

                result.Close();
            }
            catch (Exception ex)
            {
                Erro.setMsg(ex.Message);
            }
            finally
            {
                desconecta();
            }

            return lista;
        }

        public static void editaTreino(Treino treino, List<TreinoExercicio> exercicios)
        {
            Erro.setErro(false);

            SqlTransaction transacao = null;

            try
            {
                conecta();

                if (Erro.getErro())
                {
                    return;
                }

                transacao = conn.BeginTransaction();

                string auxTreino = "UPDATE Treino SET " +
                                   "nm_treino = @nm_treino, " +
                                   "ds_treino = @ds_treino, " +
                                   "tp_divisao = @tp_divisao " +
                                   "WHERE cd_treino = @cd_treino";

                strSQL = new SqlCommand(auxTreino, conn, transacao);
                strSQL.Parameters.AddWithValue("@nm_treino", treino.getNmTreino());
                strSQL.Parameters.AddWithValue("@ds_treino", string.IsNullOrWhiteSpace(treino.getDsTreino()) ? (object)DBNull.Value : treino.getDsTreino());
                strSQL.Parameters.AddWithValue("@tp_divisao", treino.getTpDivisao());
                strSQL.Parameters.AddWithValue("@cd_treino", treino.getCdTreino());

                strSQL.ExecuteNonQuery();

                string auxDeleteExercicios = "DELETE FROM TreinoExercicio " +
                                             "WHERE cd_treino = @cd_treino";

                strSQL = new SqlCommand(auxDeleteExercicios, conn, transacao);
                strSQL.Parameters.AddWithValue("@cd_treino", treino.getCdTreino());

                strSQL.ExecuteNonQuery();

                foreach (TreinoExercicio exercicio in exercicios)
                {
                    string auxInsertExercicio = "INSERT INTO TreinoExercicio " +
                                                "(cd_treino, cd_exercicio, qt_series, qt_repeticoes, qt_descansoSegundos, nr_ordem, ds_observacao) " +
                                                "VALUES " +
                                                "(@cd_treino, @cd_exercicio, @qt_series, @qt_repeticoes, @qt_descansoSegundos, @nr_ordem, @ds_observacao)";

                    strSQL = new SqlCommand(auxInsertExercicio, conn, transacao);
                    strSQL.Parameters.AddWithValue("@cd_treino", treino.getCdTreino());
                    strSQL.Parameters.AddWithValue("@cd_exercicio", exercicio.getCdExercicio());
                    strSQL.Parameters.AddWithValue("@qt_series", exercicio.getQtSeries());
                    strSQL.Parameters.AddWithValue("@qt_repeticoes", exercicio.getQtRepeticoes());
                    strSQL.Parameters.AddWithValue("@qt_descansoSegundos", exercicio.getQtDescansoSegundos());
                    strSQL.Parameters.AddWithValue("@nr_ordem", exercicio.getNrOrdem());
                    strSQL.Parameters.AddWithValue("@ds_observacao", string.IsNullOrWhiteSpace(exercicio.getDsObservacao()) ? (object)DBNull.Value : exercicio.getDsObservacao());

                    strSQL.ExecuteNonQuery();
                }

                transacao.Commit();
            }
            catch (Exception ex)
            {
                if (transacao != null)
                {
                    transacao.Rollback();
                }

                Erro.setMsg(ex.Message);
            }
            finally
            {
                desconecta();
            }

        }
        public static void deletaUsuario(int cdUsuario)
        {
            Erro.setErro(false);

            try
            {
                conecta();

                if (Erro.getErro())
                {
                    return;
                }

                string aux = "DELETE FROM Usuario " +
                             "WHERE cd_usuario = @cd_usuario " +
                             "AND ic_admin = 0";

                strSQL = new SqlCommand(aux, conn);
                strSQL.Parameters.AddWithValue("@cd_usuario", cdUsuario);

                int linhasAfetadas = strSQL.ExecuteNonQuery();

                if (linhasAfetadas == 0)
                {
                    Erro.setMsg("Usuário não encontrado ou não pode ser excluído.");
                }
            }
            catch (Exception ex)
            {
                Erro.setMsg(ex.Message);
            }
            finally
            {
                desconecta();
            }
        }
    }

}
