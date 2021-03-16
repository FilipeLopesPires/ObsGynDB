namespace BD_Project
{
    internal class CMColo
    {

        private int id;
        private string meioLiq, convencional;

        public string Convencional
        {
            get
            {
                return convencional;
            }

            set
            {
                convencional = value;
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

        public string MeioLiquido
        {
            get
            {
                return meioLiq;
            }

            set
            {
                meioLiq = value;
            }
        }
    }
}