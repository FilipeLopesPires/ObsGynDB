namespace BD_Project
{
    internal class ConsultaPac
    {
        private int id;
        private string data, tipo;

        public int ID {
            get {
                return id;
            }
            set {
                id = value;
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

        public override string ToString()
        {
            return id + "    " + data + "    " + tipo;
        }

    }
}