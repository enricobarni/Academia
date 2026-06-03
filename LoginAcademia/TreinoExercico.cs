using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginAcademia
{
    public class TreinoExercicio
    {
        private int cdTreinoExercicio;
        public void setCdTreinoExercicio(int _cdTreinoExercicio) { cdTreinoExercicio = _cdTreinoExercicio; }
        public int getCdTreinoExercicio() { return cdTreinoExercicio; }

        private int cdTreino;
        public void setCdTreino(int _cdTreino) { cdTreino = _cdTreino; }
        public int getCdTreino() { return cdTreino; }

        private int cdExercicio;
        public void setCdExercicio(int _cdExercicio) { cdExercicio = _cdExercicio; }
        public int getCdExercicio() { return cdExercicio; }

        private string nmExercicio;
        public void setNmExercicio(string _nmExercicio) { nmExercicio = _nmExercicio; }
        public string getNmExercicio() { return nmExercicio; }

        private string nmGrupoMuscular;
        public void setNmGrupoMuscular(string _nmGrupoMuscular) { nmGrupoMuscular = _nmGrupoMuscular; }
        public string getNmGrupoMuscular() { return nmGrupoMuscular; }

        private int qtSeries;
        public void setQtSeries(int _qtSeries) { qtSeries = _qtSeries; }
        public int getQtSeries() { return qtSeries; }

        private int qtRepeticoes;
        public void setQtRepeticoes(int _qtRepeticoes) { qtRepeticoes = _qtRepeticoes; }
        public int getQtRepeticoes() { return qtRepeticoes; }

        private int qtDescansoSegundos;
        public void setQtDescansoSegundos(int _qtDescansoSegundos) { qtDescansoSegundos = _qtDescansoSegundos; }
        public int getQtDescansoSegundos() { return qtDescansoSegundos; }

        private int nrOrdem;
        public void setNrOrdem(int _nrOrdem) { nrOrdem = _nrOrdem; }
        public int getNrOrdem() { return nrOrdem; }

        private string dsObservacao;
        public void setDsObservacao(string _dsObservacao) { dsObservacao = _dsObservacao; }
        public string getDsObservacao() { return dsObservacao; }

        public string getDescansoFormatado()
        {
            int minutos = qtDescansoSegundos / 60;
            int segundos = qtDescansoSegundos % 60;

            if (minutos > 0 && segundos > 0)
            {
                return minutos + "min " + segundos + "s";
            }

            if (minutos > 0)
            {
                return minutos + "min";
            }

            return segundos + "s";
        }
    }
}
