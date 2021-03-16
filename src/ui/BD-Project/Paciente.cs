using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_Project
{
    class Paciente
    {

        private int nif, ss, sns;
        private String nome, contactos, dataNascimento, residencia, codigoPostal, mail, profissao, estadoCivil, subsistema;


        public int NIF {
            get {
                return nif;
            } set {
                nif = value;
            }
        }

        public String Nome
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

        public String Contactos {
            get {
                return contactos;
            } set {
                contactos = value;
            }
        }

        public String DataNascimento
        {
            get
            {
                return dataNascimento;
            }
            set
            {
                dataNascimento = value;
            }
        }

        public String Residencia
        {
            get
            {
                return residencia;
            }
            set
            {
                residencia = value;
            }
        }

        public String CodigoPostal
        {
            get
            {
                return codigoPostal;
            }
            set
            {
                codigoPostal = value;
            }
        }

        public String Mail
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

        public String Profissao
        {
            get
            {
                return profissao;
            }
            set
            {
                profissao = value;
            }
        }

        public String EstadoCivil
        {
            get
            {
                return estadoCivil;
            }
            set
            {
                estadoCivil = value;
            }
        }

        public String Subsistema
        {
            get
            {
                return subsistema;
            }
            set
            {
                subsistema = value;
            }
        }

        public int SS
        {
            get
            {
                return ss;
            }
            set
            {
                ss = value;
            }
        }

        public int SNS
        {
            get
            {
                return sns;
            }
            set
            {
                sns = value;
            }
        }

        public override string ToString()
        {
            return nome + "    " + nif +"    "+contactos+ "    " + subsistema;
        }


    }
}
