using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization; 

namespace LoginAcademia
{
    public class Endereco
    {
        private string cep;
        private string rua;
        private string bairro;
        private string cidade;
        private string estado;

        public void setCep(string _cep) { cep = _cep; }
        public string getCep() { return cep; }

        public void setRua(string _rua) { rua = _rua; }
        public string getRua() { return rua; }

        public void setBairro(string _bairro) { bairro = _bairro; }
        public string getBairro() { return bairro; }

        public void setCidade(string _cidade) { cidade = _cidade; }
        public string getCidade() { return cidade; }

        public void setEstado(string _estado) { estado = _estado; }
        public string getEstado() { return estado; }


        [JsonPropertyName("logradouro")]
        public string JsonRua
        {
            get { return rua; }
            set { rua = value; }
        }

        [JsonPropertyName("bairro")]
        public string JsonBairro
        {
            get { return bairro; }
            set { bairro = value; }
        }

        [JsonPropertyName("localidade")]
        public string JsonCidade
        {
            get { return cidade; }
            set { cidade = value; }
        }

        [JsonPropertyName("uf")]
        public string JsonEstado
        {
            get { return estado; }
            set { estado = value; }
        }
    }
}