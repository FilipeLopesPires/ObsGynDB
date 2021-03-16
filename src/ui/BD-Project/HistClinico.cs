using System;

namespace BD_Project
{
    internal class HistClinico
    {
        private int nif, menarca, coitarca, gravidezes, partos, menopausa;
        private bool transsangue;
        private double peso, altura;
        private string gruposanguineo, antfamiliares, antpessoais, cirurgias, alergias, tabaco, alcool, medicamentos, ciclos, contracecao, histObs, dum, dpp;

        public int NIF {
            get
            {
                return nif; 
            }
            set {
                nif = value;
            }
        }

        public bool TranferenciaSanguinea
        {
            get
            {
                return transsangue;
            }
            set
            {

                transsangue = value;
            }
        }

        public int Menarca
        {
            get
            {
                return menarca;
            }
            set
            {
                menarca = value;
            }
        }

        public int Coitarca
        {
            get
            {
                return coitarca;
            }
            set
            {
                coitarca = value;
            }
        }

        public int Menopausa
        {
            get
            {
                return menopausa;
            }
            set
            {
                menopausa = value;
            }
        }

        public int Gravidezes
        {
            get
            {
                return gravidezes;
            }
            set
            {
                gravidezes = value;
            }
        }

        public int Partos
        {
            get
            {
                return partos;
            }
            set
            {
                partos = value;
            }
        }

        public double Peso
        {
            get
            {
                return peso;
            }
            set
            {
                peso = value;
            }
        }


        public double Altura
        {
            get
            {
                return altura;
            }
            set
            {
                altura = value;
            }
        }

        public string GrupoSanguineo
        {
            get
            {
                return gruposanguineo;
            }
            set
            {
                gruposanguineo = value;
            }
        }

        public string Cirurgias
        {
            get
            {
                return cirurgias;
            }
            set
            {
                cirurgias = value;
            }
        }

        public string AntecedentesFamiliares
        {
            get
            {
                return antfamiliares;
            }
            set
            {
                antfamiliares = value;
            }
        }

        public string AntecedentesPessoais
        {
            get
            {
                return antpessoais;
            }
            set
            {
                antpessoais = value;
            }
        }

        public string Alergias
        {
            get
            {
                return alergias;
            }
            set
            {
                alergias = value;
            }
        }

        public string Tabaco
        {
            get
            {
                return tabaco;
            }
            set
            {
                tabaco = value;
            }
        }

        public string Alcool
        {
            get
            {
                return alcool;
            }
            set
            {
                alcool = value;
            }
        }

        public string Medicamentos
        {
            get
            {
                return medicamentos;
            }
            set
            {
                medicamentos = value;
            }
        }

        public string Ciclos
        {
            get
            {
                return ciclos;
            }
            set
            {
                ciclos = value;
            }
        }

        public string Contracecao
        { 
            get
            {
                return contracecao;
            }
            set
            {
                contracecao = value;
            }
        }

        public string HistoriaObs
        {
            get
            {
                return histObs;
            }
            set
            {
                histObs = value;
            }
        }

        public string DUM
        {
            get
            {
                return dum;
            }
            set
            {
                dum = value;
            }
        }

        public string DPP
        {
            get
            {
                return dpp;
            }
            set
            {
                dpp = value;
            }
        }


        public override string ToString()
        {
            return nif+"    "+gruposanguineo;
        }



    }

    
}