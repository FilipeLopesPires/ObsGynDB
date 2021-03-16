namespace BD_Project
{
    internal class Bebe
    {

        private int bebe;
        private double peso;
        private string parto, apgar, indicacao, sexo;

        public int NumBebe
        {
            get
            {
                return bebe;
            }

            set
            {
                bebe = value;
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


        public string Parto
        {
            get
            {
                return parto;
            }

            set
            {
                parto = value;
            }
        }

        public string APGAR
        {
            get
            {
                return apgar;
            }

            set
            {
                apgar = value;
            }
        }

        public string Indicacao
        {
            get
            {
                return indicacao;
            }

            set
            {
                indicacao = value;
            }
        }

        public string Sexo
        {
            get
            {
                return sexo;
            }

            set
            {
                sexo = value;
            }
        }

        public override string ToString()
        {
            return bebe+"    "+parto+"    "+peso+" Kg"+"    "+sexo+"    "+apgar;
        }
    }
}