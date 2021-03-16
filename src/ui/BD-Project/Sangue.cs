namespace BD_Project
{
    internal class Sangue
    {
        private int id, plaquetas;
        private string rast1trim, rast2trim, gruposang, tp, ttp, ptgo, serocmvigg, serocmvigm, serorubigg, serorubigm, serotoxoigg, serotoxoigm, outros;
        private bool coombsind, vdrl, aghbs, hcv, hiv, anticoagolantelup, antifosfo, antitrombina, antidna, anticardio, antogliadina, antinucleares, antiendomisio, antireti, antitransglu, proteinac, proteinas, imunoglob, glicoproteina;
        private double hemograma, vs, glicose, ureia, creatinina, acidourico, fosfatase, bilirubinasdir, bilirubinasind, tgo, tgp, colesttot, triglicireados, hdl, ldl, tsh, t3, t4, fsh, lh, estradiol, progesterona, prolactina, dhea, testosterona, ca125, ca153, ca199, cea;

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

        public int Plaquetas
        {
            get
            {
                return plaquetas;
            }

            set
            {
                plaquetas = value;
            }
        }

        public string Rastreio1Trim
        {
            get
            {
                return rast1trim;
            }

            set
            {
                rast1trim = value;
            }
        }

        public string Rastreio2Trim
        {
            get
            {
                return rast2trim;
            }

            set
            {
                rast2trim = value;
            }
        }

        public string GrupoSanguineo
        {
            get
            {
                return gruposang;
            }

            set
            {
                gruposang = value;
            }
        }

        public string TP
        {
            get
            {
                return tp;
            }

            set
            {
                tp = value;
            }
        }

        public string TTP
        {
            get
            {
                return ttp;
            }

            set
            {
                ttp = value;
            }
        }

        public string PTGO
        {
            get
            {
                return ptgo;
            }

            set
            {
                ptgo = value;
            }
        }

        public string SerologiaCMV_IgG
        {
            get
            {
                return serocmvigg;
            }

            set
            {
                serocmvigg = value;
            }
        }

        public string SerologiaCMV_IgM
        {
            get
            {
                return serocmvigm;
            }

            set
            {
                serocmvigm = value;
            }
        }

        public string SerologiaRubeola_IgG
        {
            get
            {
                return serorubigg;
            }

            set
            {
                serorubigg = value;
            }
        }

        public string SerologiaRubeola_IgM
        {
            get
            {
                return serorubigm;
            }

            set
            {
                serorubigm = value;
            }
        }

        public string SerologiaToxoplasmose_IgG
        {
            get
            {
                return serotoxoigg;
            }

            set
            {
                serotoxoigg = value;
            }
        }

        public string SerologiaToxoplasmose_IgM
        {
            get
            {
                return serotoxoigm;
            }

            set
            {
                serotoxoigm = value;
            }
        }

        public string Outros
        {
            get
            {
                return outros;
            }

            set
            {
                outros = value;
            }
        }

        public bool CoombsIndireto
        {
            get
            {
                return coombsind;
            }

            set
            {
                coombsind = value;
            }
        }

        public bool VDRL
        {
            get
            {
                return vdrl;
            }

            set
            {
                vdrl = value;
            }
        }

        public bool AgHBs
        {
            get
            {
                return aghbs;
            }

            set
            {
                aghbs = value;
            }
        }

        public bool HCV
        {
            get
            {
                return hcv;
            }

            set
            {
                hcv = value;
            }
        }

        public bool HIV
        {
            get
            {
                return hiv;
            }

            set
            {
                hiv = value;
            }
        }

        public bool AntiCoagolanteLupico
        {
            get
            {
                return anticoagolantelup;
            }

            set
            {
                anticoagolantelup = value;
            }
        }

        public bool Antifosfolipidico
        {
            get
            {
                return antifosfo;
            }

            set
            {
                antifosfo = value;
            }
        }

        public bool Antitrombina
        {
            get
            {
                return antitrombina;
            }

            set
            {
                antitrombina = value;
            }
        }

        public bool AntidDNA
        {
            get
            {
                return antidna;
            }

            set
            {
                antidna = value;
            }
        }

        public bool AntiCardiolipina
        {
            get
            {
                return anticardio;
            }

            set
            {
                anticardio = value;
            }
        }

        public bool AntoGliadina
        {
            get
            {
                return antogliadina;
            }

            set
            {
                antogliadina = value;
            }
        }

        public bool AntiNucleares
        {
            get
            {
                return antinucleares;
            }

            set
            {
                antinucleares = value;
            }
        }

        public bool AntiEndomisio
        {
            get
            {
                return antiendomisio;
            }

            set
            {
                antiendomisio = value;
            }
        }

        public bool AntiReticulina
        {
            get
            {
                return antireti;
            }

            set
            {
                antireti = value;
            }
        }

        public bool AntiTransglutaminase
        {
            get
            {
                return antitransglu;
            }

            set
            {
                antitransglu = value;
            }
        }

        public bool ProteinaC
        {
            get
            {
                return proteinac;
            }

            set
            {
                proteinac = value;
            }
        }

        public bool ProteinaS
        {
            get
            {
                return proteinas;
            }

            set
            {
                proteinas = value;
            }
        }

        public bool Imunoglobulnas
        {
            get
            {
                return imunoglob;
            }

            set
            {
                imunoglob = value;
            }
        }

        public bool Glicoproteina
        {
            get
            {
                return glicoproteina;
            }

            set
            {
                glicoproteina = value;
            }
        }

        public double HemogramaHemoglobina
        {
            get
            {
                return hemograma;
            }

            set
            {
                hemograma = value;
            }
        }

        public double VS
        {
            get
            {
                return vs;
            }

            set
            {
                vs = value;
            }
        }

        public double Glicose
        {
            get
            {
                return glicose;
            }

            set
            {
                glicose = value;
            }
        }

        public double Ureia
        {
            get
            {
                return ureia;
            }

            set
            {
                ureia = value;
            }
        }

        public double Creatinina
        {
            get
            {
                return creatinina;
            }

            set
            {
                creatinina = value;
            }
        }

        public double AcidoUrico
        {
            get
            {
                return acidourico;
            }

            set
            {
                acidourico = value;
            }
        }

        public double FosfataseAlcalina
        {
            get
            {
                return fosfatase;
            }

            set
            {
                fosfatase = value;
            }
        }

        public double BilirubinasDireta
        {
            get
            {
                return bilirubinasdir;
            }

            set
            {
                bilirubinasdir = value;
            }
        }

        public double BilirubinasIndireta
        {
            get
            {
                return bilirubinasind;
            }

            set
            {
                bilirubinasind = value;
            }
        }

        public double TGO
        {
            get
            {
                return tgo;
            }

            set
            {
                tgo = value;
            }
        }

        public double TGP
        {
            get
            {
                return tgp;
            }

            set
            {
                tgp = value;
            }
        }

        public double ColestTotal
        {
            get
            {
                return colesttot;
            }

            set
            {
                colesttot = value;
            }
        }

        public double Triglicerideos
        {
            get
            {
                return triglicireados;
            }

            set
            {
                triglicireados = value;
            }
        }

        public double HDL
        {
            get
            {
                return hdl;
            }

            set
            {
                hdl = value;
            }
        }

        public double LDL
        {
            get
            {
                return ldl;
            }

            set
            {
                ldl = value;
            }
        }

        public double TSH
        {
            get
            {
                return tsh;
            }

            set
            {
                tsh = value;
            }
        }

        public double T3
        {
            get
            {
                return t3;
            }

            set
            {
                t3 = value;
            }
        }

        public double T4
        {
            get
            {
                return t4;
            }

            set
            {
                t4 = value;
            }
        }

        public double FSH
        {
            get
            {
                return fsh;
            }

            set
            {
                fsh = value;
            }
        }

        public double LH
        {
            get
            {
                return lh;
            }

            set
            {
                lh = value;
            }
        }

        public double Estradiol
        {
            get
            {
                return estradiol;
            }

            set
            {
                estradiol = value;
            }
        }

        public double Progesterona
        {
            get
            {
                return progesterona;
            }

            set
            {
                progesterona = value;
            }
        }

        public double Prolactina
        {
            get
            {
                return prolactina;
            }

            set
            {
                prolactina = value;
            }
        }

        public double DHEA
        {
            get
            {
                return dhea;
            }

            set
            {
                dhea = value;
            }
        }

        public double Testosterona
        {
            get
            {
                return testosterona;
            }

            set
            {
                testosterona = value;
            }
        }

        public double CA125
        {
            get
            {
                return ca125;
            }

            set
            {
                ca125 = value;
            }
        }

        public double CA153
        {
            get
            {
                return ca153;
            }

            set
            {
                ca153 = value;
            }
        }

        public double CA199
        {
            get
            {
                return ca199;
            }

            set
            {
                ca199 = value;
            }
        }

        public double CEA
        {
            get
            {
                return cea;
            }

            set
            {
                cea = value;
            }
        }
    }
}