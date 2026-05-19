using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginAcademia
{
    public class Login_Cadastro
    {
        private int cdUsuario;
        public void setCdUsuario(int _cdUsuario) { cdUsuario = _cdUsuario; }
        public int getCdUsuario() { return cdUsuario; }

        private string nome;
        public void setNome(string _nome) { nome = _nome; }
        public string getNome() { return nome; }

        private string usuario;
        public void setUsuario(string _usuario) { usuario = _usuario; }
        public string getUsuario() { return usuario; }

        private string email;
        public void setEmail(string _email) { email = _email; }
        public string getEmail() { return email; }

        private string senha;
        public void setSenha(string _senha) { senha = _senha; }
        public string getSenha() { return senha; }

        private string telefone;
        public void setTelefone(string _telefone) { telefone = _telefone; }
        public string getTelefone() { return telefone; }

        private bool icAdmin;
        public void setIcAdmin(bool _icAdmin) { icAdmin = _icAdmin; }
        public bool getIcAdmin() { return icAdmin; }
    }
}
