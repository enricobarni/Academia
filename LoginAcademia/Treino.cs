using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginAcademia
{
    public class Treino
    {
        private int cdTreino;
        public void setCdTreino(int _cdTreino) { cdTreino = _cdTreino; }
        public int getCdTreino() { return cdTreino; }

        private string nmTreino;
        public void setNmTreino(string _nmTreino) { nmTreino = _nmTreino; }
        public string getNmTreino() { return nmTreino; }

        private string dsTreino;
        public void setDsTreino(string _dsTreino) { dsTreino = _dsTreino; }
        public string getDsTreino() { return dsTreino; }

        private string tpDivisao;
        public void setTpDivisao(string _tpDivisao) { tpDivisao = _tpDivisao; }
        public string getTpDivisao() { return tpDivisao; }

        private DateTime dtInicio;
        public void setDtInicio(DateTime _dtInicio) { dtInicio = _dtInicio; }
        public DateTime getDtInicio() { return dtInicio; }

        private DateTime dtFim;
        public void setDtFim(DateTime _dtFim) { dtFim = _dtFim; }
        public DateTime getDtFim() { return dtFim; }

        private int cdUsuario;
        public void setCdUsuario(int _cdUsuario) { cdUsuario = _cdUsuario; }
        public int getCdUsuario() { return cdUsuario; }

        private int cdAdmin;
        public void setCdAdmin(int _cdAdmin) { cdAdmin = _cdAdmin; }
        public int getCdAdmin() { return cdAdmin; }

        private string nmAdmin;
        public void setNmAdmin(string _nmAdmin) { nmAdmin = _nmAdmin; }
        public string getNmAdmin() { return nmAdmin; }

        private bool icAtivo;
        public void setIcAtivo(bool _icAtivo) { icAtivo = _icAtivo; }
        public bool getIcAtivo() { return icAtivo; }

        private DateTime dtCadastro;
        public void setDtCadastro(DateTime _dtCadastro) { dtCadastro = _dtCadastro; }
        public DateTime getDtCadastro() { return dtCadastro; }
    }
}
