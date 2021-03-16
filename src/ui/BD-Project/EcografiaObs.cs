namespace BD_Project
{
    internal class EcografiaObs
    {

        private int id, semanas, dias;
        private string data;

        public int Dias
        {
            get
            {
                return dias;
            }

            set
            {
                dias = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public int Semanas
        {
            get
            {
                return semanas;
            }

            set
            {
                semanas = value;
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

        public override string ToString()
        {
            return id + "    " + data;
        }
    }
}