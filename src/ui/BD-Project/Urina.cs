namespace BD_Project
{
    internal class Urina
    {
        private int id;
        private string sumario, valorUroc;
        private bool urocultura, tig;

        public int ID
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

        public string Sumario
        {
            get
            {
                return sumario;
            }

            set
            {
                sumario = value;
            }
        }

        public bool TIG
        {
            get
            {
                return tig;
            }

            set
            {
                tig = value;
            }
        }

        public bool Urocultura
        {
            get
            {
                return urocultura;
            }

            set
            {
                urocultura = value;
            }
        }

        public string ValorUroc
        {
            get
            {
                return valorUroc;
            }

            set
            {
                valorUroc = value;
            }
        }
    }
}