namespace BD_Project
{
    internal class Exsudado
    {

        private int id;
        private string vaginalbac, vaginalmico, vaginalparasit;
        private bool vaginoretal;

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

        public string VaginalBacteriologico
        {
            get
            {
                return vaginalbac;
            }

            set
            {
                vaginalbac = value;
            }
        }

        public string VaginalMicologico
        {
            get
            {
                return vaginalmico;
            }

            set
            {
                vaginalmico = value;
            }
        }

        public string VaginalParasitologico
        {
            get
            {
                return vaginalparasit;
            }

            set
            {
                vaginalparasit = value;
            }
        }

        public bool VaginoRetalSGB
        {
            get
            {
                return vaginoretal;
            }

            set
            {
                vaginoretal = value;
            }
        }
    }
}