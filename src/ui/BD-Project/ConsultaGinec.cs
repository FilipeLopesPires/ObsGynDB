

namespace BD_Project
{
    internal class ConsultaGinec
    {
        private int id;
        private double hora;
        private string descricao, data;

        public int ID {
            get {
                return id;
            }
            set {
                id = value;
            }
        }

        public string Descricao
        {
            get
            {
                return descricao;
            }
            set
            {
                descricao = value;
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

    }
}