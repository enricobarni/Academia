using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;               
using System.Text.Json.Serialization;

namespace LoginAcademia
{
    class AcademiaBLL
    {
        //Formatação Telefone
        public static string formatacaotelefone(string txtNumero)
        {
            //Remove tudo que não for número, @"\D" significa qualquer caractere que não seja dígito, "" substitui por nada
            string numeros = Regex.Replace(txtNumero, @"\D", "");

            //Permite apagar tudo
            if (numeros.Length == 0)
                return "";

            //Limita a 11 dígitos (DDD + 9 dígitos)
            if (numeros.Length > 11)
                numeros = numeros.Substring(0, 11);

            //Apenas 1 dígito: "(1"
            if (numeros.Length <= 2)
                return "(" + numeros;

            //DDD completo
            string ddd = numeros.Substring(0, 2);

            //Parte do telefone
            string telefone = numeros.Substring(2);

            //Até 5 dígitos, sem "-"
            if (telefone.Length <= 5)
                return $"({ddd}) {telefone}";

            //5 dígitos + "-" + restante
            return $"({ddd}) {telefone.Substring(0, 5)}-{telefone.Substring(5)}";
        }

        // Validação Do Numero
        public static void validacaotelefone(string txtNumero)
        {
            Erro.setErro(false);

            if (string.IsNullOrWhiteSpace(txtNumero))
            {
                Erro.setMsg("O Telefone é inválido!");
                return;
            }

            string numeros = Regex.Replace(txtNumero, @"\D", "");

            if (numeros.Length != 11)
            {
                Erro.setMsg("O Telefone deve conter DDD + 9 dígitos.");
                return;
            }

            if (!Regex.IsMatch(numeros, @"^\d{11}$"))
            {
                Erro.setMsg("O Telefone deve conter apenas números.");
                return;
            }
        }

        // Validação Do Email

        public static void validacaoemail(string txtEmail)
        {
            Erro.setErro(false);
            if (string.IsNullOrWhiteSpace(txtEmail))
            {
                Erro.setMsg("O Email é inválido!");
                return;
            }
            if (!Regex.IsMatch(txtEmail, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9-]+\.(com|com\.br|br)$"))
            {
                Erro.setMsg("O Email é invalido");
                return;
            }
        }

        //Formatação CEP

        public static string formatacaocep(string txtCep)
        {
            // Remove tudo que não for número
            string numeros = Regex.Replace(txtCep, @"\D", "");

            // Permite apagar tudo
            if (numeros.Length == 0)
                return "";

            // Limita a 8 dígitos
            if (numeros.Length > 8)
                numeros = numeros.Substring(0, 8);

            // Até 5 dígitos sem "-"
            if (numeros.Length <= 5)
                return numeros;

            // Formato 12345-678
            return $"{numeros.Substring(0, 5)}-{numeros.Substring(5)}";
        }

        //Validação CEP

        public static async Task<Endereco> buscarcepinternet(string cep)
        {
            Erro.setErro(false);

            cep = Regex.Replace(cep, @"\D", "");

            validacaocep(cep);

            if (Erro.getErro())
            {
                return null;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = "https://viacep.com.br/ws/" + cep + "/json/";
                    string json = await client.GetStringAsync(url);

                    Endereco endereco = JsonSerializer.Deserialize<Endereco>(json);

                    if (endereco == null || endereco.ErroViaCep)
                    {
                        Erro.setMsg("CEP não encontrado!");
                        return null;
                    }

                    endereco.setCep(cep);

                    return endereco;
                }
            }
            catch
            {
                Erro.setMsg("Erro ao buscar CEP. Verifique sua conexão.");
                return null;
            }
        }

        public static void validacaocep(string cep)
        {
            Erro.setErro(false);

            if (string.IsNullOrWhiteSpace(cep))
            {
                Erro.setMsg("O CEP é inválido!");
                return;
            }

            cep = Regex.Replace(cep, @"\D", "");

            if (cep.Length != 8)
            {
                Erro.setMsg("O CEP deve conter 8 números!");
                return;
            }

            if (!Regex.IsMatch(cep, @"^\d{8}$"))
            {
                Erro.setMsg("O CEP deve conter apenas números!");
                return;
            }
        }

        //Validação Nome

        public static void validacaonome(string txtNome)
        {
            Erro.setErro(false);

            if (string.IsNullOrWhiteSpace(txtNome))
            {
                Erro.setMsg("O nome é inválido!");
                return;
            }

            bool ultimoFoiEspaco = false;

            // Não pode começar ou terminar com espaço
            if (txtNome[0] == ' ' || txtNome[txtNome.Length - 1] == ' ')
            {
                Erro.setMsg("O nome é inválido!");
                return;
            }

            foreach (char c in txtNome)
            {
                // Verifica se é letra ou espaço
                if (!char.IsLetter(c) && c != ' ')
                {
                    Erro.setMsg("O nome é inválido!");
                    return;
                }

                // Verifica espaço duplo
                if (c == ' ')
                {
                    if (ultimoFoiEspaco)
                    {
                        Erro.setMsg("O nome é inválido!");
                        return;
                    }
                    ultimoFoiEspaco = true;
                }
                else
                {
                    ultimoFoiEspaco = false;
                }
            }
        }

        //Validação Usuario

        public static void validacaousuario(string txtUsuario)
        {
            Erro.setErro(false);

            if (string.IsNullOrWhiteSpace(txtUsuario))
            {
                Erro.setMsg("O Usuário é inválido!");
                return;
            }

            if (txtUsuario.Length < 3 || txtUsuario.Length > 30)
            {
                Erro.setMsg("O Usuário deve ter entre 3 e 30 caracteres!");
                return;
            }

            foreach (char c in txtUsuario)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    Erro.setMsg("O Usuário deve conter apenas letras e números!");
                    return;
                }
            }
        }

        //Validação Senha
        public static void validacaosenha(string txtSenha)
        {
            Erro.setErro(false);

            if (string.IsNullOrWhiteSpace(txtSenha))
            {
                Erro.setMsg("A Senha é inválida!");
                return;
            }

            if (txtSenha.Length < 8)
            {
                Erro.setMsg("A senha deve ter no mínimo 8 caracteres!");
                return;
            }

            if (!Regex.IsMatch(txtSenha, @"[A-Z]"))
            {
                Erro.setMsg("A senha deve ter ao menos uma letra maiúscula!");
                return;
            }

            if (!Regex.IsMatch(txtSenha, @"[a-z]"))
            {
                Erro.setMsg("A senha deve ter ao menos uma letra minúscula!");
                return;
            }

            if (!Regex.IsMatch(txtSenha, @"[0-9]"))
            {
                Erro.setMsg("A senha deve ter ao menos um número!");
                return;
            }

            if (!Regex.IsMatch(txtSenha, @"[!@#$%^&*()_+\-=\[\]{}|;':"",./<>?]"))
            {
                Erro.setMsg("A senha deve ter ao menos um caractere especial!");
                return;
            }
        }

        //Validação Confirmar Senha
        public static void validacaoconfirmarsenha(string senha, string confirmarSenha)
        {
            Erro.setErro(false);

            if (string.IsNullOrWhiteSpace(confirmarSenha))
            {
                Erro.setMsg("Confirme a senha!");
                return;
            }

            if (senha != confirmarSenha)
            {
                Erro.setMsg("As Senhas Não São Iguais!");
                return;
            }
        }
        //Formatação Numero Da Casa
        public static string formatacaonumero(string txtNumero)
        {
            txtNumero = Regex.Replace(txtNumero, @"\D", "");
            return txtNumero;
        }
        //Validação Numero Da Residencia
        public static void validacaonumero(string txtNumero)
        {
            Erro.setErro(false);

            if (string.IsNullOrWhiteSpace(txtNumero))
            {
                Erro.setMsg("O Nº é inválido!");
                return;
            }
        }

        public static void validacaologin(string txtLogin)
        {
            Erro.setErro(false);
            if (string.IsNullOrWhiteSpace(txtLogin))
            {
                Erro.setMsg("Informe seu usuário ou email!");
                return;
            }
        }

        public static void validacaoExercicioTreino(TreinoExercicio exercicio)
        {
            Erro.setErro(false);

            if (exercicio == null)
            {
                Erro.setMsg("Exercício inválido.");
                return;
            }

            if (exercicio.getCdExercicio() <= 0)
            {
                Erro.setMsg("Selecione um exercício.");
                return;
            }

            if (exercicio.getNrOrdem() <= 0)
            {
                Erro.setMsg("A ordem do exercício deve ser maior que zero.");
                return;
            }

            if (exercicio.getQtSeries() <= 0)
            {
                Erro.setMsg("A quantidade de séries deve ser maior que zero.");
                return;
            }

            if (exercicio.getQtRepeticoes() <= 0)
            {
                Erro.setMsg("A quantidade de repetições deve ser maior que zero.");
                return;
            }

            if (exercicio.getQtDescansoSegundos() < 0)
            {
                Erro.setMsg("O descanso não pode ser negativo.");
                return;
            }

            if (exercicio.getDsObservacao() != null && exercicio.getDsObservacao().Length > 500)
            {
                Erro.setMsg("A observação deve ter no máximo 500 caracteres.");
                return;
            }
        }
        public static void validacaoTreino(Treino treino, List<TreinoExercicio> exercicios)
        {
            Erro.setErro(false);

            if (treino == null)
            {
                Erro.setMsg("Treino inválido.");
                return;
            }

            string nomeTreino = treino.getNmTreino();

            if (string.IsNullOrWhiteSpace(nomeTreino))
            {
                Erro.setMsg("Informe o nome do treino.");
                return;
            }

            if (nomeTreino != nomeTreino.Trim())
            {
                Erro.setMsg("O nome do treino não pode começar ou terminar com espaço.");
                return;
            }

            if (nomeTreino.Contains("  "))
            {
                Erro.setMsg("O nome do treino não pode conter mais de um espaço seguido.");
                return;
            }

            if (nomeTreino.Length < 3)
            {
                Erro.setMsg("O nome do treino deve ter pelo menos 3 caracteres.");
                return;
            }

            if (nomeTreino.Length > 100)
            {
                Erro.setMsg("O nome do treino deve ter no máximo 100 caracteres.");
                return;
            }

            if (string.IsNullOrWhiteSpace(treino.getTpDivisao()))
            {
                Erro.setMsg("Selecione a divisão do treino.");
                return;
            }

            if (treino.getTpDivisao() != "A" &&
                treino.getTpDivisao() != "B" &&
                treino.getTpDivisao() != "C" &&
                treino.getTpDivisao() != "D" &&
                treino.getTpDivisao() != "E")
            {
                Erro.setMsg("Divisão inválida.");
                return;
            }

            if (treino.getCdUsuario() <= 0)
            {
                Erro.setMsg("Cliente inválido.");
                return;
            }

            if (treino.getCdAdmin() <= 0)
            {
                Erro.setMsg("Administrador inválido.");
                return;
            }

            if (exercicios == null || exercicios.Count == 0)
            {
                Erro.setMsg("Adicione pelo menos um exercício ao treino.");
                return;
            }
        }
        public static void inserirTreino(Treino treino, List<TreinoExercicio> exercicios)
        {
            Erro.setErro(false);

            validacaoTreino(treino, exercicios);

            if (Erro.getErro())
            {
                return;
            }

            foreach (TreinoExercicio exercicio in exercicios)
            {
                validacaoExercicioTreino(exercicio);

                if (Erro.getErro())
                {
                    return;
                }
            }

            AcademiaDAL.insereTreino(treino, exercicios);
        }

        public static void editarTreino(Treino treino, List<TreinoExercicio> exercicios)
        {
            Erro.setErro(false);

            if (treino == null)
            {
                Erro.setMsg("Treino inválido.");
                return;
            }

            if (treino.getCdTreino() <= 0)
            {
                Erro.setMsg("Código do treino inválido.");
                return;
            }

            validacaoTreino(treino, exercicios);

            if (Erro.getErro())
            {
                return;
            }

            foreach (TreinoExercicio exercicio in exercicios)
            {
                validacaoExercicioTreino(exercicio);

                if (Erro.getErro())
                {
                    return;
                }
            }

            AcademiaDAL.editaTreino(treino, exercicios);
        }

        public static void deletarTreino(int cdTreino)
        {
            Erro.setErro(false);

            if (cdTreino <= 0)
            {
                Erro.setMsg("Treino inválido.");
                return;
            }

            AcademiaDAL.deletaTreino(cdTreino);
        }

        public static void editarPerfil(Login_Cadastro lc, Endereco end)
        {
            Erro.setErro(false);

            if (lc == null)
            {
                Erro.setMsg("Usuário inválido.");
                return;
            }

            if (end == null)
            {
                Erro.setMsg("Endereço inválido.");
                return;
            }

            if (lc.getCdUsuario() <= 0)
            {
                Erro.setMsg("Código do usuário inválido.");
                return;
            }

            validacaousuario(lc.getUsuario());

            if (Erro.getErro())
            {
                return;
            }

            validacaoemail(lc.getEmail());

            if (Erro.getErro())
            {
                return;
            }

            validacaotelefone(lc.getTelefone());

            if (Erro.getErro())
            {
                return;
            }

            validacaocep(end.getCep());

            if (Erro.getErro())
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(end.getRua()))
            {
                Erro.setMsg("Busque um CEP válido para preencher a rua.");
                return;
            }

            if (string.IsNullOrWhiteSpace(end.getNumero()))
            {
                Erro.setMsg("Informe o número.");
                return;
            }

            if (string.IsNullOrWhiteSpace(end.getBairro()))
            {
                Erro.setMsg("Busque um CEP válido para preencher o bairro.");
                return;
            }

            if (string.IsNullOrWhiteSpace(end.getCidade()))
            {
                Erro.setMsg("Busque um CEP válido para preencher a cidade.");
                return;
            }

            if (string.IsNullOrWhiteSpace(end.getEstado()))
            {
                Erro.setMsg("Busque um CEP válido para preencher o estado.");
                return;
            }

            if (!Regex.IsMatch(end.getEstado(), @"^[A-Z]{2}$"))
            {
                Erro.setMsg("O estado deve conter a sigla com 2 letras maiúsculas.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(end.getComplemento()) && end.getComplemento().Length > 50)
            {
                Erro.setMsg("O complemento deve ter no máximo 50 caracteres.");
                return;
            }

            AcademiaDAL.editaPerfil(lc, end);
        }

        public static void deletarUsuario(int cdUsuario)
        {
            Erro.setErro(false);

            if (cdUsuario <= 0)
            {
                Erro.setMsg("Usuário inválido.");
                return;
            }

            AcademiaDAL.deletaUsuario(cdUsuario);
        }
    }
}
