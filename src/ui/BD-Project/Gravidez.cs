namespace BD_Project
{
    internal class Gravidez
    {

        private int nif, numerogravidez, numeroConsultas;

        public int NIF {
            get {
                return nif;
            }
            set {
                nif = value;
            }
        }

        public int NumGravidez
        {
            get
            {
                return numerogravidez;
            }
            set
            {
                numerogravidez = value;
            }
        }

        public int NumConsultas
        {
            get
            {
                return numeroConsultas;
            }
            set
            {
                numeroConsultas = value;
            }
        }

        public override string ToString()
        {
            return numerogravidez+" gravidezes" + "    " + numeroConsultas+" consultas";
        }

    }
}