namespace BD_Project
{
    internal class MedicoInfo
    {
        private int nif, horas;
        private double salario;
        private string nome, mail, especialidade, dataInicio;

        public int NIF
        {
            get {
                return nif;
            }
            set
            {
                nif = value;
            }
        }

        public int HorasSemanais
        {
            get
            {
                return horas;
            }
            set
            {
                horas = value;
            }
        }

        public double Salario
        {
            get
            {
                return salario;
            }
            set
            {
                salario = value;
            }
        }

        public string Mail
        {
            get
            {
                return mail;
            }
            set
            {
                mail = value;
            }
        }

        public string Nome
        {
            get
            {
                return nome;
            }
            set
            {
                nome = value;
            }
        }

        public string DataInicio
        {
            get
            {
                return dataInicio;
            }
            set
            {
                dataInicio = value;
            }
        }

        public string Espacialidade
        {
            get
            {
                return especialidade;
            }
            set
            {
                especialidade = value;
            }
        }


        public override string ToString()
        {
            return nome+"    "+nif+"    "+especialidade;
        }


    }
}