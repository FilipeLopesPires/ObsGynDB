namespace BD_Project
{
    internal class Analise
    {

        private int id, requisicao;
        private string data, descricao;

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

        public int Requisicao
        {
            get
            {
                return requisicao;
            }

            set
            {
                requisicao = value;
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

        public override string ToString()
        {
            return id + "    " + requisicao + "    " + data;
        }
    }
}