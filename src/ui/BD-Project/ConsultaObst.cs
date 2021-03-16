namespace BD_Project
{
    internal class ConsultaObst
    {
        private int id, numgravidez, gravidezPac, afu;
        private bool maf, foco;
        private double peso, hora;
        private string ta, exgin, queixas, medicacao, obs, pueramam, puermens, puerobs, tipo, data;

        public int ID {
            get {
                return id;
            }
            set {
                id = value;
            }
        }

        public int AFU
        {
            get
            {
                return afu;
            }
            set
            {
                afu = value;
            }
        }

        public double Peso
        {
            get
            {
                return peso;
            }
            set
            {
                peso = value;
            }
        }

        public int NumeroGravidez
        {
            get
            {
                return numgravidez;
            }
            set
            {
                numgravidez = value;
            }
        }

        public int GravidezPac
        {
            get
            {
                return gravidezPac;
            }
            set
            {
                gravidezPac = value;
            }
        }

        public bool MAF
        {
            get
            {
                return maf;
            }
            set
            {
                maf = value;
            }
        }

        public bool Foco
        {
            get
            {
                return foco;
            }
            set
            {
                foco = value;
            }
        }

        public string TA
        {
            get
            {
                return ta;
            }
            set
            {
                ta = value;
            }
        }

        public string ExGin
        {
            get
            {
                return exgin;
            }
            set
            {
                exgin = value;
            }
        }

        public string Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        public string Tipo
        {
            get
            {
                return tipo;
            }
            set
            {
                tipo = value;
            }
        }

        public string Queixas
        {
            get
            {
                return queixas;
            }
            set
            {
                queixas = value;
            }
        }

        public string Medicacao
        {
            get
            {
                return medicacao;
            }
            set
            {
                medicacao = value;
            }
        }

        public string Obs
        {
            get
            {
                return obs;
            }
            set
            {
                obs = value;
            }
        }

        public double Hora
        {
            get
            {
                return hora;
            }
            set
            {
                hora = value;
            }
        }

        public string PuerAmamentacao
        {
            get
            {
                return pueramam;
            }
            set
            {
                pueramam = value;
            }
        }

        public string PuerMenstruacao
        {
            get
            {
                return puermens;
            }
            set
            {
                puermens = value;
            }
        }

        public string PuerObs
        {
            get
            {
                return puerobs;
            }
            set
            {
                puerobs = value;
            }
        }
    }
}