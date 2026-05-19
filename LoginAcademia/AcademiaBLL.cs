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
            if (!Regex.IsMatch(txtNumero, @"^\(\d{2}\)\s\d{5}-\d{4}$"))
            {
                Erro.setMsg("O Telefone é inválido!");
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

        public static async Task<Endereco> buscarcepinternet(string txtCep)
        {
            // Valida o formato antes de gastar internet à toa
            validacaocep(txtCep);

            if (Erro.getErro())
            {
                return null;
            }

            // Limpa formatação para a URL
            string cepLimpo = Regex.Replace(txtCep, @"\D", "");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"https://viacep.com.br/ws/{cepLimpo}/json/";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();

                        // O ViaCEP retorna o campo "erro": true dentro do JSON caso o CEP não exista
                        if (jsonString.Contains("\"erro\":") || jsonString.Contains("true"))
                        {
                            Erro.setErro(true);
                            Erro.setMsg("CEP Não Foi Encontrado!");
                            return null;
                        }

                        // Configuração opcional para ignorar maiúsculas/minúsculas se necessário
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        // DESSERIALIZAÇÃO: Transforma o texto JSON no objeto Endereco do C#
                        Endereco end = JsonSerializer.Deserialize<Endereco>(jsonString, options);
                        return end;
                    }
                    else
                    {
                        Erro.setErro(true);
                        Erro.setMsg("Falha ao conectar com o serviço de CEP!");
                        return null;
                    }
                }
                catch (Exception)
                {
                    Erro.setErro(true);
                    Erro.setMsg("Erro de conexão. Verifique sua internet!");
                    return null;
                }
            }
        }
        public static void validacaocep(string txtCep)
        {
            Erro.setErro(false);
            if (string.IsNullOrWhiteSpace(txtCep))
            {
                Erro.setMsg("O CEP é inválido!");
                return;
            }

            if (!Regex.IsMatch(txtCep, @"^\d{5}-\d{3}$"))
            {
                Erro.setMsg("O CEP é inválido!");
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

            bool ultimoFoiEspaco = false;

            // Não pode começar ou terminar com espaço
            if (txtUsuario[0] == ' ' || txtUsuario[txtUsuario.Length - 1] == ' ')
            {
                Erro.setMsg("O Usuário é inválido!");
                return;
            }

            foreach (char c in txtUsuario)
            {
                // Verifica se é letra ou espaço
                if (!char.IsLetterOrDigit(c) && c != ' ')
                {
                    Erro.setMsg("O Usuário é inválido!");
                    return;
                }

                // Verifica espaço duplo
                if (c == ' ')
                {
                    if (ultimoFoiEspaco)
                    {
                        Erro.setMsg("O Usuário é inválido!");
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
    }
}
