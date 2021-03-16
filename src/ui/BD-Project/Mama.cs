namespace BD_Project
{
    internal class Mama
    {

        private int id;
        private string mamografia, ecografia;

        public string EcografiaMamaria
        {
            get
            {
                return ecografia;
            }

            set
            {
                ecografia = value;
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

        public string Mamografia
        {
            get
            {
                return mamografia;
            }

            set
            {
                mamografia = value;
            }
        }
    }
}