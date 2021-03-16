using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BD_Project
{
    public partial class DescricaoAnalise : Form
    {
        Dictionary<string, int> meses = new Dictionary<string, int>() { { "janeiro", 1 }, { "fevereiro", 2 }, { "março", 3 }, { "abril", 4 }, { "maio", 5 }, { "junho", 6 }, { "julho", 7 }, { "agosto", 8 }, { "setembro", 9 }, { "outubro", 10 }, { "novembro", 11 }, { "dezembro", 12 } };
        private int ID=-1;
        private int cons=-1;
        private SqlConnection cn;

        public DescricaoAnalise()
        {
            InitializeComponent();
        }

        public DescricaoAnalise(int id)
        {
            InitializeComponent();
            ID = id;
        }

        public DescricaoAnalise(int id, int c)
        {
            InitializeComponent();
            ID = id;
            cons = c;
        }

        private void descClick(object sender, EventArgs e)
        {
            allOff();
            RadioButton rb = (RadioButton)sender;

            switch (rb.Name)
            {
                case "sangue":
                    sangue.Checked = true;
                    this.Controls.Find("descSangue", true)[0].BringToFront();
                    this.Controls.Find("descSangue", true)[0].Show();
                    break;
                case "ecografiagin":
                    ecografiagin.Checked = true;
                    this.Controls.Find("descecografiagin", true)[0].BringToFront();
                    this.Controls.Find("descecografiagin", true)[0].Show();
                    break;
                case "ecografiaobs":
                    ecografiaobs.Checked = true;
                    this.Controls.Find("descecografiaobs", true)[0].BringToFront();
                    this.Controls.Find("descecografiaobs", true)[0].Show();
                    break;
                case "urina":
                    urina.Checked = true;
                    this.Controls.Find("descUrina", true)[0].BringToFront();
                    this.Controls.Find("descUrina", true)[0].Show();
                    break;
                case "ecg":
                    ecg.Checked = true;
                    this.Controls.Find("descECG", true)[0].BringToFront();
                    this.Controls.Find("descECG", true)[0].Show();
                    break;
                case "rx":
                    rx.Checked = true;
                    this.Controls.Find("descRX", true)[0].BringToFront();
                    this.Controls.Find("descRX", true)[0].Show();
                    break;
                case "cmcolo":
                    cmcolo.Checked = true;
                    this.Controls.Find("descCMColo", true)[0].BringToFront();
                    this.Controls.Find("descCMColo", true)[0].Show();
                    break;
                case "mamografia":
                    mamografia.Checked = true;
                    this.Controls.Find("descMamografia", true)[0].BringToFront();
                    this.Controls.Find("descMamografia", true)[0].Show();
                    break;
                case "espermograma":
                    espermograma.Checked = true;
                    this.Controls.Find("descEspermograma", true)[0].BringToFront();
                    this.Controls.Find("descEspermograma", true)[0].Show();
                    break;
                case "hsg":
                    hsg.Checked = true;
                    this.Controls.Find("descHSG", true)[0].BringToFront();
                    this.Controls.Find("descHSG", true)[0].Show();
                    break;
                case "exsudado":
                    exsudado.Checked = true;
                    this.Controls.Find("descExsudado", true)[0].BringToFront();
                    this.Controls.Find("descExsudado", true)[0].Show();
                    break;
            }
        }

        private void allOff()
        {
            this.Controls.Find("descSangue", true)[0].Hide();
            sangue.Checked = false;
            this.Controls.Find("descUrina", true)[0].Hide();
            urina.Checked = false;
            this.Controls.Find("descecografiagin", true)[0].Hide();
            ecografiagin.Checked = false;
            this.Controls.Find("descecografiaobs", true)[0].Hide();
            ecografiaobs.Checked = false;
            this.Controls.Find("descECG", true)[0].Hide();
            ecg.Checked = false;
            this.Controls.Find("descRX", true)[0].Hide();
            rx.Checked = false;
            this.Controls.Find("descCMColo", true)[0].Hide();
            cmcolo.Checked = false;
            this.Controls.Find("descMamografia", true)[0].Hide();
            mamografia.Checked = false;
            this.Controls.Find("descEspermograma", true)[0].Hide();
            espermograma.Checked = false;
            this.Controls.Find("descHSG", true)[0].Hide();
            hsg.Checked = false;
            this.Controls.Find("descExsudado", true)[0].Hide();
            exsudado.Checked = false;
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form sistema = new Pacientes();
            sistema.ShowDialog();
            this.Close();
        }

        private void DescricaoAnalise_Load(object sender, EventArgs e)
        {

            if (ID == -1)
            {
                allOff();
                return;
            }

            cn = getSGBDConnection();

            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select dbo.getReqAnalise(" + ID + ")", cn);
            SqlDataReader reader;
            requisicao.Text=((int)cmd.ExecuteScalar()).ToString();

            cmd = new SqlCommand("select dbo.tipoAnalise("+ID+")", cn);
            switch (((string)cmd.ExecuteScalar()))
            {
                case "Sangue":
                    sangue.Checked = true;
                    descSangue.BringToFront();
                    descSangue.Show();


                    Sangue s = new Sangue();
                    reader = new SqlCommand("exec searchSangueByID " + ID, cn).ExecuteReader();

                    while (reader.Read())
                    {
                        s.Id = int.Parse(reader["IDDA"].ToString());
                        s.AcidoUrico = double.Parse(reader["AcidoUrico"].ToString());
                        s.AgHBs = bool.Parse(reader["AgHBs"].ToString()); 
                        s.AntiCardiolipina = bool.Parse(reader["AntiCardiolipina"].ToString());
                        s.AntiCoagolanteLupico = bool.Parse(reader["AntiCoagulanteLupico"].ToString()); 
                        s.AntidDNA = bool.Parse(reader["AntiDNA"].ToString()); 
                        s.AntiEndomisio = bool.Parse(reader["AntiEndomisio"].ToString()); 
                        s.Antifosfolipidico = bool.Parse(reader["Antifosfolipidico"].ToString()); 
                        s.AntiNucleares = bool.Parse(reader["AntiNucleares"].ToString()); 
                        s.AntiReticulina = bool.Parse(reader["AntiReticulina"].ToString()); 
                        s.AntiTransglutaminase = bool.Parse(reader["AntiTransglutaminase"].ToString()); 
                        s.Antitrombina = bool.Parse(reader["Antitrombina"].ToString()); 
                        s.AntoGliadina = bool.Parse(reader["AntiGliadina"].ToString()); 
                        s.BilirubinasDireta = double.Parse(reader["BilirubinasDireta"].ToString()); 
                        s.BilirubinasIndireta = double.Parse(reader["BilirubinasIndireta"].ToString()); 
                        s.CA125 = double.Parse(reader["CA125"].ToString()); 
                        s.CA153 = double.Parse(reader["CA15_3"].ToString()); 
                        s.CA199 = double.Parse(reader["CA19_9"].ToString()); 
                        s.CEA = double.Parse(reader["CEA"].ToString()); 
                        s.ColestTotal = double.Parse(reader["ColestTotal"].ToString()); 
                        s.CoombsIndireto = bool.Parse(reader["CoombsIndireto"].ToString()); 
                        s.Creatinina = double.Parse(reader["Creatinina"].ToString()); 
                        s.DHEA = double.Parse(reader["DHEA"].ToString()); 
                        s.Estradiol = double.Parse(reader["Estradiol"].ToString()); 
                        s.FosfataseAlcalina = double.Parse(reader["FosfataseAlcalina"].ToString()); 
                        s.FSH = double.Parse(reader["FSH"].ToString()); 
                        s.Glicoproteina = bool.Parse(reader["Glicoproteina"].ToString()); 
                        s.Glicose = double.Parse(reader["Glicose"].ToString()); 
                        s.GrupoSanguineo = reader["GrupoSanguineo"].ToString(); 
                        s.HCV = bool.Parse(reader["HCV"].ToString()); 
                        s.HDL = double.Parse(reader["HDL"].ToString()); 
                        s.HemogramaHemoglobina = double.Parse(reader["HemogramaHemoglobina"].ToString()); 
                        s.HIV = bool.Parse(reader["HIV"].ToString()); 
                        s.Imunoglobulnas = bool.Parse(reader["Imunoglobulinas"].ToString()); 
                        s.LDL = double.Parse(reader["LDL"].ToString()); 
                        s.LH = double.Parse(reader["LH"].ToString()); 
                        s.Outros = reader["Outros"].ToString(); 
                        s.Plaquetas = int.Parse(reader["Plaquetas"].ToString()); 
                        s.Progesterona = double.Parse(reader["Progesterona"].ToString()); 
                        s.Prolactina = double.Parse(reader["Prolactina"].ToString()); 
                        s.ProteinaC = bool.Parse(reader["ProteinaC"].ToString()); 
                        s.ProteinaS = bool.Parse(reader["ProteinaS"].ToString()); 
                        s.PTGO = reader["PTGO"].ToString(); 
                        s.Rastreio1Trim = reader["Rastreio1Trim"].ToString(); 
                        s.Rastreio2Trim = reader["Rastreio2Trim"].ToString(); 
                        s.SerologiaCMV_IgG = reader["SerologiaCMV_IgG"].ToString(); 
                        s.SerologiaCMV_IgM = reader["SerologiaCMV_IgM"].ToString(); 
                        s.SerologiaRubeola_IgG = reader["SerologiaRubeola_IgG"].ToString(); 
                        s.SerologiaRubeola_IgM = reader["SerologiaRubeola_IgM"].ToString(); 
                        s.SerologiaToxoplasmose_IgG = reader["SerologiaToxoplasmose_IgG"].ToString(); 
                        s.SerologiaToxoplasmose_IgM = reader["SerologiaToxoplasmose_IgM"].ToString(); 
                        s.T3 = double.Parse(reader["T3"].ToString()); 
                        s.T4 = double.Parse(reader["T4"].ToString()); 
                        s.Testosterona = double.Parse(reader["Testosterona"].ToString()); 
                        s.TGO = double.Parse(reader["TGO"].ToString()); 
                        s.TGP = double.Parse(reader["TGP"].ToString()); 
                        s.TP = reader["TP"].ToString(); 
                        s.Triglicerideos = double.Parse(reader["Triglicerideos"].ToString()); 
                        s.TSH = double.Parse(reader["TSH"].ToString()); 
                        s.TTP = reader["TTP"].ToString(); 
                        s.Ureia = double.Parse(reader["Ureia"].ToString()); 
                        s.VDRL = bool.Parse(reader["VDRL"].ToString()); 
                        s.VS = double.Parse(reader["VS"].ToString()); 
                    }



                    cn.Close();




                    acidourico.Text=s.AcidoUrico.ToString();
                    aghbs.Text = s.AgHBs.ToString();
                    anticardio.Text = s.AntiCardiolipina.ToString();
                    anticoaglup.Text = s.AntiCoagolanteLupico.ToString();
                    antidna.Text = s.AntidDNA.ToString();
                    antiendomisio.Text = s.AntiEndomisio.ToString();
                    antifosfo.Text = s.Antifosfolipidico.ToString();
                    antinucleares.Text = s.AntiNucleares.ToString();
                    antireti.Text = s.AntiReticulina.ToString();
                    antitransglut.Text = s.AntiTransglutaminase.ToString();
                    antitromb.Text =s.Antitrombina.ToString();
                    antiglidina.Text = s.AntoGliadina.ToString();
                    bilirubdir.Text = s.BilirubinasDireta.ToString();
                    bilirubind.Text = s.BilirubinasIndireta.ToString();
                    ca125.Text = s.CA125.ToString();
                    ca153.Text = s.CA153.ToString();
                    ca199.Text = s.CA199.ToString();
                    //cea.Text = s.CEA.ToString();
                    colesttot.Text = s.ColestTotal.ToString();
                    coombsind.Text = s.CoombsIndireto.ToString();
                    creatinina.Text = s.Creatinina.ToString();
                    dhea.Text = s.DHEA.ToString();
                    estradiol.Text = s.Estradiol.ToString();
                    fosfatalc.Text = s.FosfataseAlcalina.ToString();
                    fsh.Text = s.FSH.ToString();
                    glicoproteina.Text = s.Glicoproteina.ToString();
                    glicose.Text = s.Glicose.ToString();
                    grupoSang.Text = s.GrupoSanguineo.ToString();
                    hcv.Text = s.HCV.ToString();
                    hdl.Text = s.HDL.ToString();
                    hemhemo.Text = s.HemogramaHemoglobina.ToString();
                    hiv.Text = s.HIV.ToString();
                    imunoglob.Text = s.Imunoglobulnas.ToString();
                    ldl.Text = s.LDL.ToString();
                    lh.Text = s.LH.ToString();
                    outros.Text = s.Outros.ToString();
                    plaquetas.Text = s.Plaquetas.ToString();
                    progesterona.Text = s.Progesterona.ToString();
                    prolactina.Text = s.Prolactina.ToString();
                    proteinac.Text = s.ProteinaC.ToString();
                    proteinas.Text = s.ProteinaS.ToString();
                    ptgo.Text = s.PTGO.ToString();
                    rast1trim.Text = s.Rastreio1Trim.ToString();
                    rast2trim.Text = s.Rastreio2Trim.ToString();
                    serocmvigg.Text = s.SerologiaCMV_IgG.ToString();
                    serocmvigm.Text = s.SerologiaCMV_IgM.ToString();
                    serorubigg.Text = s.SerologiaRubeola_IgG.ToString();
                    serorubigm.Text = s.SerologiaRubeola_IgM.ToString();
                    serotoxoigg.Text = s.SerologiaToxoplasmose_IgG.ToString();
                    serotoxoigm.Text = s.SerologiaToxoplasmose_IgM.ToString();
                    t3.Text = s.T3.ToString();
                    t4.Text = s.T4.ToString();
                    testosterona.Text = s.Testosterona.ToString();
                    tgo.Text = s.TGO.ToString();
                    tgp.Text = s.TGP.ToString();
                    tp.Text = s.TP.ToString();
                    trigliciredeos.Text = s.Triglicerideos.ToString();
                    tsh.Text = s.TSH.ToString();
                    acidourico.Text = s.TTP.ToString();
                    ttp.Text = s.Ureia.ToString();
                    vdrl.Text = s.VDRL.ToString();
                    vs.Text = s.VS.ToString();



                    break;
                case "Urina":
                    urina.Checked = true;
                    descUrina.BringToFront();
                    descUrina.Show();

                    Urina u = new Urina();
                    reader = new SqlCommand("exec searchUrinaByID " + ID, cn).ExecuteReader();

                    while (reader.Read())
                    {
                        u.ID=int.Parse(reader["IDDA"].ToString());
                        u.Sumario = reader["SumariaUrinas"].ToString();
                        u.Urocultura = bool.Parse(reader["Urocultura"].ToString());
                        u.TIG = bool.Parse(reader["TIG"].ToString());
                        u.ValorUroc = reader["ValorUroc"].ToString();
                    }

                    cn.Close();


                    sumario.Text = u.Sumario; 
                    urocultura.Text = u.Urocultura.ToString();
                    tig.Text = u.TIG.ToString();
                    if (u.Urocultura)
                    {
                        labelvalorUroc.BringToFront();
                        labelvalorUroc.Show();
                        valorUroc.BringToFront();
                        valorUroc.Show();
                        valorUroc.Text = u.ValorUroc;
                    }

                    break;
                case "RX":
                    rx.Checked = true;
                    descRX.BringToFront();
                    descRX.Show();


                    RX r = new RX();
                    reader = new SqlCommand("exec searchRXByID " + ID, cn).ExecuteReader();

                    while (reader.Read())
                    {
                        r.Id = int.Parse(reader["IDDA"].ToString());
                        r.Tipo = reader["Tipo"].ToString();
                    }

                    cn.Close();


                    rxTipo.Text = r.Tipo;

                    break;
                case "Mama":
                    mamografia.Checked = true;
                    descMamografia.BringToFront();
                    descMamografia.Show();


                    Mama m = new Mama();
                    reader = new SqlCommand("exec searchMamaByID " + ID, cn).ExecuteReader();

                    while (reader.Read())
                    {
                        m.Id = int.Parse(reader["IDDA"].ToString());
                        m.Mamografia = reader["Mamografia"].ToString();
                        m.EcografiaMamaria = reader["EcografiaMamaria"].ToString();

                    }

                    cn.Close();
                    
                    mamaMamografia.Text = m.Mamografia;
                    ecografiamam.Text = m.EcografiaMamaria;

                    break;
                case "ECG":
                    ecg.Checked = true;
                    descECG.BringToFront();
                    descECG.Show();
                    break;
                case "HSG":
                    hsg.Checked = true;
                    descHSG.BringToFront();
                    descHSG.Show();
                    break;
                case "Espermograma":
                    espermograma.Checked = true;
                    descEspermograma.BringToFront();
                    descEspermograma.Show();
                    break;
                case "EcografiaGinecologia":
                    ecografiagin.Checked = true;
                    descecografiagin.BringToFront();
                    descecografiagin.Show();
                    break;
                case "EcografiaObstetrica":
                    ecografiaobs.Checked = true;
                    descecografiaobs.BringToFront();
                    descecografiaobs.Show();


                    EcografiaObs eo = new EcografiaObs();
                    reader = new SqlCommand("exec searchMamaByID " + ID, cn).ExecuteReader();

                    while (reader.Read())
                    {
                        eo.Id = int.Parse(reader["IDDA"].ToString());
                        eo.Semanas = int.Parse(reader["Semanas"].ToString());
                        eo.Dias = int.Parse(reader["Dias"].ToString());
                    }

                    cn.Close();


                    semanas.Text = eo.Semanas.ToString();
                    dias.Text = eo.Dias.ToString();

                    break;
                case "CMColo":
                    cmcolo.Checked = true;
                    descCMColo.BringToFront();
                    descCMColo.Show();


                    CMColo c = new CMColo();
                    reader = new SqlCommand("exec searchCMColoByID " + ID, cn).ExecuteReader();

                    while (reader.Read())
                    {
                        c.Id = int.Parse(reader["IDDA"].ToString());
                        c.Convencional = reader["Convencional"].ToString();
                        c.MeioLiquido = reader["MeioLiquido"].ToString();
                    }

                    cn.Close();


                    meioliquido.Text = c.MeioLiquido;
                    convencional.Text = c.Convencional;

                    break;
                case "Exsudado":
                    exsudado.Checked = true;
                    descExsudado.BringToFront();
                    descExsudado.Show();


                    Exsudado ex = new Exsudado();
                    reader = new SqlCommand("exec searchExsudadoByID " + ID, cn).ExecuteReader();

                    while (reader.Read())
                    {
                        ex.Id = int.Parse(reader["IDDA"].ToString());
                        ex.VaginalBacteriologico = reader["VaginalBacteriologico"].ToString();
                        ex.VaginalMicologico = reader["VaginalMicologico"].ToString();
                        ex.VaginalParasitologico = reader["VaginalParasitologico"].ToString();
                        ex.VaginoRetalSGB = bool.Parse(reader["VaginoRetalSGB"].ToString());
                        
                    }

                    cn.Close();

                    vagbac.Text = ex.VaginalBacteriologico;
                    vagmic.Text = ex.VaginalMicologico;
                    vagparas.Text = ex.VaginalParasitologico;
                    vagretal.Text = ex.VaginoRetalSGB.ToString();
                    break;

            }

            cn.Close();

        }


        private SqlConnection getSGBDConnection()
        {
            return new SqlConnection("Data Source = localhost;Initial Catalog = BD_Project;Integrated Security=true");
        }

        private bool verifySGBDConnection()
        {
            if (cn == null)
                cn = getSGBDConnection();

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }



        private void submitButton_Click(object sender, EventArgs e)
        {

            if (!verifySGBDConnection())
                return;

            if (ID == -1)//insert
            {
                int rows = 0;
                int esteID = ((int)new SqlCommand("select dbo.maxAnalise()", cn).ExecuteScalar()) + 1;
                ID = esteID;

                if (!verifySGBDConnection())
                    return;
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "insert into ProjetoConsultorio.DescricaoAnalise values( @IDDA,@Consulta, @Requisicao, @DataDA, @Descricao)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IDDA", esteID);
                cmd.Parameters.AddWithValue("@Consulta", cons);
                cmd.Parameters.AddWithValue("@Requisicao", (string.IsNullOrEmpty(requisicao.Text))?((int)new SqlCommand("select dbo.maxReqAnalise()", cn).ExecuteScalar()): int.Parse(requisicao.Text));
                string[] datadb = Regex.Split(data.Text, "de");
                cmd.Parameters.AddWithValue("@DataDA", datadb[2] + "-" + meses[datadb[1].ToLower().Trim()] + "-" + datadb[0]);
                cmd.Parameters.AddWithValue("@Descricao", descricao.Text);
                cmd.Connection = cn;
                try
                {
                    rows = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to insert descricao in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                }


                if (sangue.Checked == true) {
                    cmd = new SqlCommand();
                    cmd.CommandText = "insert into ProjetoConsultorio.Sangue values(@IDDA, @Rastreio1Trim,@Rastreio2Trim,@GrupoSanguineo,@CoombsIndireto,@HemogramaHemoglobina,@Plaquetas,@TP,@TTP,@VS,@Glicose,@PTGO,@Ureia,@Creatinina, @AcidoUrico,@FosfataseAlcalina,@BilirubinasDireta,@BilirubinasIndireta,@TGO,@TGP,@ColestTotal,@Triglicerideos,@HDL,@LDL,@VDRL,@SerologiaCMV_IgG,@SerologiaCMV_IgM,@SerologiaRubeola_IgG,@SerologiaRubeola_IgM,@SerologiaToxoplasmose_IgG,@SerologiaToxoplasmose_IgM,@AgHBs,@HCV,@HIV,@TSH,@T3,@T4,@FSH,@LH,@Estradiol,@Progesterona,@Prolactina,@DHEA,@Testosterona,@CA125,@CA153,@CA199,@CEA,@AntiCoagolanteLupico,@Antifosfolipidico, @Antitrombina,@AntidDNA,@AntiCardiolipina,@AntoGliadina, @AntiNucleares,@AntiEndomisio,@AntiReticulina,@AntiTransglutaminase,@ProteinaC, @ProteinaS,@Imunoglobulnas, @Glicoproteina,@Outros)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IDDA", esteID);
                    cmd.Parameters.AddWithValue("@Rastreio1Trim", rast1trim.Text);
                    cmd.Parameters.AddWithValue("@Rastreio2Trim", rast2trim.Text);
                    cmd.Parameters.AddWithValue("@GrupoSanguineo",grupoSang.Text);
                    cmd.Parameters.AddWithValue("@CoombsIndireto", (coombsind.Text.Equals("true", StringComparison.OrdinalIgnoreCase))?1:0);
                    cmd.Parameters.AddWithValue("@HemogramaHemoglobina", (string.IsNullOrEmpty(hemhemo.Text)) ? -1 : double.Parse(hemhemo.Text));
                    cmd.Parameters.AddWithValue("@Plaquetas", (string.IsNullOrEmpty(plaquetas.Text)) ? -1 : int.Parse(plaquetas.Text));
                    cmd.Parameters.AddWithValue("@TP",tp.Text);
                    cmd.Parameters.AddWithValue("@TTP",ttp.Text);
                    cmd.Parameters.AddWithValue("@VS", (string.IsNullOrEmpty(vs.Text)) ? -1 : double.Parse(vs.Text));
                    cmd.Parameters.AddWithValue("@Glicose", (string.IsNullOrEmpty(glicose.Text)) ? -1 : double.Parse(glicose.Text));
                    cmd.Parameters.AddWithValue("@PTGO",ptgo.Text);
                    cmd.Parameters.AddWithValue("@Ureia", (string.IsNullOrEmpty(ureia.Text)) ? -1 : double.Parse(ureia.Text));
                    cmd.Parameters.AddWithValue("@Creatinina", (string.IsNullOrEmpty(creatinina.Text)) ? -1 : double.Parse(creatinina.Text));
                    cmd.Parameters.AddWithValue("@AcidoUrico", (string.IsNullOrEmpty(acidourico.Text)) ? -1 : double.Parse(acidourico.Text));
                    cmd.Parameters.AddWithValue("@FosfataseAlcalina", (string.IsNullOrEmpty(fosfatalc.Text)) ? -1 : double.Parse(fosfatalc.Text));
                    cmd.Parameters.AddWithValue("@BilirubinasDireta", (string.IsNullOrEmpty(bilirubdir.Text)) ? -1 : double.Parse(bilirubdir.Text));
                    cmd.Parameters.AddWithValue("@BilirubinasIndireta", (string.IsNullOrEmpty(bilirubind.Text)) ? -1 : double.Parse(bilirubind.Text));
                    cmd.Parameters.AddWithValue("@TGO", (string.IsNullOrEmpty(tgo.Text)) ? -1 : double.Parse(tgo.Text));
                    cmd.Parameters.AddWithValue("@TGP", (string.IsNullOrEmpty(tgp.Text)) ? -1 : double.Parse(tgp.Text));
                    cmd.Parameters.AddWithValue("@ColestTotal", (string.IsNullOrEmpty(colesttot.Text)) ? -1 : double.Parse(colesttot.Text));
                    cmd.Parameters.AddWithValue("@Triglicerideos", (string.IsNullOrEmpty(trigliciredeos.Text)) ? -1 : double.Parse(trigliciredeos.Text));
                    cmd.Parameters.AddWithValue("@HDL", (string.IsNullOrEmpty(hdl.Text)) ? -1 : double.Parse(hdl.Text));
                    cmd.Parameters.AddWithValue("@LDL", (string.IsNullOrEmpty(ldl.Text)) ? -1 : double.Parse(ldl.Text));
                    cmd.Parameters.AddWithValue("@VDRL", (vdrl.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@SerologiaCMV_IgG", serocmvigg.Text);
                    cmd.Parameters.AddWithValue("@SerologiaCMV_IgM",serocmvigm.Text);
                    cmd.Parameters.AddWithValue("@SerologiaRubeola_IgG",serorubigg.Text);
                    cmd.Parameters.AddWithValue("@SerologiaRubeola_IgM",serorubigm.Text);
                    cmd.Parameters.AddWithValue("@SerologiaToxoplasmose_IgG",serotoxoigg.Text);
                    cmd.Parameters.AddWithValue("@SerologiaToxoplasmose_IgM",serotoxoigm.Text);
                    cmd.Parameters.AddWithValue("@AgHBs", (aghbs.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@HCV", (hcv.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@HIV", (hiv.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@TSH", (string.IsNullOrEmpty(tsh.Text)) ? -1 : double.Parse(tsh.Text));
                    cmd.Parameters.AddWithValue("@T3", (string.IsNullOrEmpty(t3.Text)) ? -1 : double.Parse(t3.Text));
                    cmd.Parameters.AddWithValue("@T4", (string.IsNullOrEmpty(t4.Text)) ? -1 : double.Parse(t4.Text));
                    cmd.Parameters.AddWithValue("@FSH", (string.IsNullOrEmpty(fsh.Text)) ? -1 : double.Parse(fsh.Text));
                    cmd.Parameters.AddWithValue("@LH", (string.IsNullOrEmpty(lh.Text)) ? -1 : double.Parse(lh.Text));
                    cmd.Parameters.AddWithValue("@Estradiol", (string.IsNullOrEmpty(estradiol.Text)) ? -1 : double.Parse(estradiol.Text));
                    cmd.Parameters.AddWithValue("@Progesterona", (string.IsNullOrEmpty(progesterona.Text)) ? -1 : double.Parse(progesterona.Text));
                    cmd.Parameters.AddWithValue("@Prolactina", (string.IsNullOrEmpty(prolactina.Text)) ? -1 : double.Parse(prolactina.Text));
                    cmd.Parameters.AddWithValue("@DHEA", (string.IsNullOrEmpty(dhea.Text)) ? -1 : double.Parse(dhea.Text));
                    cmd.Parameters.AddWithValue("@Testosterona", (string.IsNullOrEmpty(testosterona.Text)) ? -1 : double.Parse(testosterona.Text));
                    cmd.Parameters.AddWithValue("@CA125", (string.IsNullOrEmpty(ca125.Text)) ? -1 : double.Parse(ca125.Text));
                    cmd.Parameters.AddWithValue("@CA153", (string.IsNullOrEmpty(ca153.Text)) ? -1 : double.Parse(ca153.Text));
                    cmd.Parameters.AddWithValue("@CA199", (string.IsNullOrEmpty(ca199.Text)) ? -1 : double.Parse(ca199.Text));
                    cmd.Parameters.AddWithValue("@CEA",0);
                    cmd.Parameters.AddWithValue("@AntiCoagolanteLupico", (anticoaglup.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Antifosfolipidico", (antifosfo.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Antitrombina", (antitromb.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@AntidDNA", (antidna.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@AntiCardiolipina", (anticardio.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@AntoGliadina", (antiglidina.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@AntiNucleares", (antinucleares.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@AntiEndomisio", (antiendomisio.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@AntiReticulina", (antireti.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@AntiTransglutaminase", (antitransglut.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@ProteinaC", (proteinac.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@ProteinaS", (proteinas.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Imunoglobulnas", (imunoglob.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Glicoproteina", (glicoproteina.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Outros", outros.Text);
                    cmd.Connection = cn;
                    try { rows = cmd.ExecuteNonQuery(); }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to insert sangue in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }
                }
                else if (urina.Checked == true) {
                    cmd = new SqlCommand();
                    cmd.CommandText = "insert into ProjetoConsultorio.Urina values(@IDDA, @SumariaUrinas, @Urocultura, @ValorUroc,@TIG)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IDDA", esteID);
                    cmd.Parameters.AddWithValue("@SumariaUrinas", sumario.Text);
                    cmd.Parameters.AddWithValue("@Urocultura", ((urocultura.Text).Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@ValorUroc", valorUroc.Text);
                    cmd.Parameters.AddWithValue("@TIG", ((tig.Text).Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Connection = cn;
                    try { rows = cmd.ExecuteNonQuery(); }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to insert urina in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }
                }
                else if (ecografiagin.Checked == true) {
                    cmd = new SqlCommand();
                    cmd.CommandText = "insert into ProjetoConsultorio.EcografiaGinecologica values( @IDDA)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IDDA", esteID);
                    cmd.Connection = cn;
                    try { rows = cmd.ExecuteNonQuery(); }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to insert ecografiaGinec in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }
                }
                else if (ecografiaobs.Checked == true) {
                    cmd = new SqlCommand();
                    cmd.CommandText = "insert into ProjetoConsultorio.EcografiaObstetrica values( @IDDA,@Semana,@Dias)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IDDA", esteID);
                    cmd.Parameters.AddWithValue("@Semana", (string.IsNullOrEmpty(semanas.Text)) ? -1 : int.Parse(semanas.Text));
                    cmd.Parameters.AddWithValue("@Dias", (string.IsNullOrEmpty(dias.Text)) ? -1 : int.Parse(dias.Text));
                    cmd.Connection = cn;
                    try { rows = cmd.ExecuteNonQuery(); }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to insert ecografiaObs in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }
                }
                else if (ecg.Checked == true) {
                    cmd = new SqlCommand();
                    cmd.CommandText = "insert into ProjetoConsultorio.ECG values( @IDDA)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IDDA", esteID);
                    cmd.Connection = cn;
                    try{rows = cmd.ExecuteNonQuery();}
                    catch (Exception ex){
                        throw new Exception("Failed to insert ecg in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }
                }
                else if (cmcolo.Checked == true) {
                    cmd = new SqlCommand();
                    cmd.CommandText = "insert into ProjetoConsultorio.CMColo values( @IDDA, @Convencional, @MeioLiquido)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IDDA", esteID);
                    cmd.Parameters.AddWithValue("@Convencional", convencional.Text);
                    cmd.Parameters.AddWithValue("@MeioLiquido", meioliquido.Text);
                    cmd.Connection = cn;
                    try { rows = cmd.ExecuteNonQuery(); }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to insert cmColo in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }
                }
                else if (rx.Checked == true) {
                    cmd = new SqlCommand();
                    cmd.CommandText = "insert into ProjetoConsultorio.RX values( @IDDA, @Tipo)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IDDA", esteID);
                    cmd.Parameters.AddWithValue("@Tipo", rxTipo.Text);
                    cmd.Connection = cn;
                    try { rows = cmd.ExecuteNonQuery(); }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to insert rx in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }
                }
                else if (mamografia.Checked == true) {
                    cmd = new SqlCommand();
                    cmd.CommandText = "insert into ProjetoConsultorio.Mama values( @IDDA, @Mamografia, @EcografiaMamaria)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IDDA", esteID);
                    cmd.Parameters.AddWithValue("@Mamografia", mamaMamografia.Text);
                    cmd.Parameters.AddWithValue("@EcografiaMamaria", ecografiamam.Text);
                    cmd.Connection = cn;
                    try { rows = cmd.ExecuteNonQuery(); }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to insert mama in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }
                }
                else if (espermograma.Checked == true) {
                    cmd = new SqlCommand();
                    cmd.CommandText = "insert into ProjetoConsultorio.Espermograma values( @IDDA)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IDDA", esteID);
                    cmd.Connection = cn;
                    try { rows = cmd.ExecuteNonQuery(); }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to insert espermograma in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }
                }
                else if (exsudado.Checked == true) {
                    cmd = new SqlCommand();
                    cmd.CommandText = "insert into ProjetoConsultorio.Exsudado values( @IDDA, @VaginalBacteriologico, @VaginalMicologico, @VaginalParasitologico, @VaginalRetalSGB)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IDDA", esteID);
                    cmd.Parameters.AddWithValue("@VaginalBacteriologico", vagbac.Text);
                    cmd.Parameters.AddWithValue("@VaginalMicologico", vagmic.Text);
                    cmd.Parameters.AddWithValue("@VaginalParasitologico", vagparas.Text);
                    cmd.Parameters.AddWithValue("@VaginalRetalSGB", ((vagretal.Text).Equals("true", StringComparison.OrdinalIgnoreCase))?1:0);
                    cmd.Connection = cn;
                    try { rows = cmd.ExecuteNonQuery(); }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to insert exsudado in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }
                }
                else if (hsg.Checked == true) {
                    cmd = new SqlCommand();
                    cmd.CommandText = "insert into ProjetoConsultorio.HSG values( @IDDA)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IDDA", esteID);
                    cmd.Connection = cn;
                    try { rows = cmd.ExecuteNonQuery(); }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to insert hsg in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }
                }


            }
            else//update
            {
                SqlCommand cmd;
                int rows = 0;

                if (sangue.Checked == true)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "update ProjetoConsultorio.Sangue set IDDA=@IDDA, Rastreio1Trim=@Rastreio1Trim,Rastreio2Trim=@Rastreio2Trim,GrupoSanguineo=@GrupoSanguineo,CoombsIndireto=@CoombsIndireto,HemogramaHemoglobina=@HemogramaHemoglobina,Plaquetas=@Plaquetas,TP=@TP,TTP=@TTP,VS=@VS,Glicose=@Glicose,PTGO=@PTGO,Ureia=@Ureia,Creatinina=@Creatinina, AcidoUrico=@AcidoUrico,FosfataseAlcalina=@FosfataseAlcalina,BilirubinasDireta=@BilirubinasDireta,BilirubinasIndireta=@BilirubinasIndireta,TGO=@TGO,TGP=@TGP,ColestTotal=@ColestTotal,Triglicerideos=@Triglicerideos,HDL=@HDL,LDL=@LDL,VDRL=@VDRL,SerologiaCMV_IgG=@SerologiaCMV_IgG,SerologiaCMV_IgM=@SerologiaCMV_IgM,SerologiaRubeola_IgG=@SerologiaRubeola_IgG,SerologiaRubeola_IgM=@SerologiaRubeola_IgM,SerologiaToxoplasmose_IgG=@SerologiaToxoplasmose_IgG,SerologiaToxoplasmose_IgM=@SerologiaToxoplasmose_IgM,AgHBs=@AgHBs,HCV=@HCV,HIV=@HIV,TSH=@TSH,T3=@T3,T4=@T4,FSH=@FSH,LH=@LH,Estradiol=@Estradiol,Progesterona=@Progesterona,Prolactina=@Prolactina,DHEA=@DHEA,Testosterona=@Testosterona,CA125=@CA125,CA15_3=@CA153,CA19_9=@CA199,CEA=@CEA,AntiCoagulanteLupico=@AntiCoagolanteLupico,Antifosfolipidico=@Antifosfolipidico, Antitrombina=@Antitrombina,AntiDNA=@AntidDNA,AntiCardiolipina=@AntiCardiolipina,AntoGliadina=@AntoGliadina, AntiNucleares=@AntiNucleares,AntiEndomisio=@AntiEndomisio,AntiReticulina=@AntiReticulina,AntiTransglutaminase=@AntiTransglutaminase,ProteinaC=@ProteinaC, ProteinaS=@ProteinaS,Imunoglobulinas=@Imunoglobulnas, Glicoproteina=@Glicoproteina,Outros=@Outros where IDDA=@IDDA";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IDDA", ID);
                    cmd.Parameters.AddWithValue("@Rastreio1Trim", rast1trim.Text);
                    cmd.Parameters.AddWithValue("@Rastreio2Trim", rast2trim.Text);
                    cmd.Parameters.AddWithValue("@GrupoSanguineo", grupoSang.Text);
                    cmd.Parameters.AddWithValue("@CoombsIndireto", (coombsind.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@HemogramaHemoglobina", (string.IsNullOrEmpty(hemhemo.Text)) ? -1 : double.Parse(hemhemo.Text));
                    cmd.Parameters.AddWithValue("@Plaquetas", (string.IsNullOrEmpty(plaquetas.Text)) ? -1 : int.Parse(plaquetas.Text));
                    cmd.Parameters.AddWithValue("@TP", tp.Text);
                    cmd.Parameters.AddWithValue("@TTP", ttp.Text);
                    cmd.Parameters.AddWithValue("@VS", (string.IsNullOrEmpty(vs.Text)) ? -1 : double.Parse(vs.Text));
                    cmd.Parameters.AddWithValue("@Glicose", (string.IsNullOrEmpty(glicose.Text)) ? -1 : double.Parse(glicose.Text));
                    cmd.Parameters.AddWithValue("@PTGO", ptgo.Text);
                    cmd.Parameters.AddWithValue("@Ureia", (string.IsNullOrEmpty(ureia.Text)) ? -1 : double.Parse(ureia.Text));
                    cmd.Parameters.AddWithValue("@Creatinina", (string.IsNullOrEmpty(creatinina.Text)) ? -1 : double.Parse(creatinina.Text));
                    cmd.Parameters.AddWithValue("@AcidoUrico", (string.IsNullOrEmpty(acidourico.Text)) ? -1 : double.Parse(acidourico.Text));
                    cmd.Parameters.AddWithValue("@FosfataseAlcalina", (string.IsNullOrEmpty(fosfatalc.Text)) ? -1 : double.Parse(fosfatalc.Text));
                    cmd.Parameters.AddWithValue("@BilirubinasDireta", (string.IsNullOrEmpty(bilirubdir.Text)) ? -1 : double.Parse(bilirubdir.Text));
                    cmd.Parameters.AddWithValue("@BilirubinasIndireta", (string.IsNullOrEmpty(bilirubind.Text)) ? -1 : double.Parse(bilirubind.Text));
                    cmd.Parameters.AddWithValue("@TGO", (string.IsNullOrEmpty(tgo.Text)) ? -1 : double.Parse(tgo.Text));
                    cmd.Parameters.AddWithValue("@TGP", (string.IsNullOrEmpty(tgp.Text)) ? -1 : double.Parse(tgp.Text));
                    cmd.Parameters.AddWithValue("@ColestTotal", (string.IsNullOrEmpty(colesttot.Text)) ? -1 : double.Parse(colesttot.Text));
                    cmd.Parameters.AddWithValue("@Triglicerideos", (string.IsNullOrEmpty(trigliciredeos.Text)) ? -1 : double.Parse(trigliciredeos.Text));
                    cmd.Parameters.AddWithValue("@HDL", (string.IsNullOrEmpty(hdl.Text)) ? -1 : double.Parse(hdl.Text));
                    cmd.Parameters.AddWithValue("@LDL", (string.IsNullOrEmpty(ldl.Text)) ? -1 : double.Parse(ldl.Text));
                    cmd.Parameters.AddWithValue("@VDRL", (vdrl.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@SerologiaCMV_IgG", serocmvigg.Text);
                    cmd.Parameters.AddWithValue("@SerologiaCMV_IgM", serocmvigm.Text);
                    cmd.Parameters.AddWithValue("@SerologiaRubeola_IgG", serorubigg.Text);
                    cmd.Parameters.AddWithValue("@SerologiaRubeola_IgM", serorubigm.Text);
                    cmd.Parameters.AddWithValue("@SerologiaToxoplasmose_IgG", serotoxoigg.Text);
                    cmd.Parameters.AddWithValue("@SerologiaToxoplasmose_IgM", serotoxoigm.Text);
                    cmd.Parameters.AddWithValue("@AgHBs", (aghbs.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@HCV", (hcv.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@HIV", (hiv.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@TSH", (string.IsNullOrEmpty(tsh.Text)) ? -1 : double.Parse(tsh.Text));
                    cmd.Parameters.AddWithValue("@T3", (string.IsNullOrEmpty(t3.Text)) ? -1 : double.Parse(t3.Text));
                    cmd.Parameters.AddWithValue("@T4", (string.IsNullOrEmpty(t4.Text)) ? -1 : double.Parse(t4.Text));
                    cmd.Parameters.AddWithValue("@FSH", (string.IsNullOrEmpty(fsh.Text)) ? -1 : double.Parse(fsh.Text));
                    cmd.Parameters.AddWithValue("@LH", (string.IsNullOrEmpty(lh.Text)) ? -1 : double.Parse(lh.Text));
                    cmd.Parameters.AddWithValue("@Estradiol", (string.IsNullOrEmpty(estradiol.Text)) ? -1 : double.Parse(estradiol.Text));
                    cmd.Parameters.AddWithValue("@Progesterona", (string.IsNullOrEmpty(progesterona.Text)) ? -1 : double.Parse(progesterona.Text));
                    cmd.Parameters.AddWithValue("@Prolactina", (string.IsNullOrEmpty(prolactina.Text)) ? -1 : double.Parse(prolactina.Text));
                    cmd.Parameters.AddWithValue("@DHEA", (string.IsNullOrEmpty(dhea.Text)) ? -1 : double.Parse(dhea.Text));
                    cmd.Parameters.AddWithValue("@Testosterona", (string.IsNullOrEmpty(testosterona.Text)) ? -1 : double.Parse(testosterona.Text));
                    cmd.Parameters.AddWithValue("@CA125", (string.IsNullOrEmpty(ca125.Text)) ? -1 : double.Parse(ca125.Text));
                    cmd.Parameters.AddWithValue("@CA153", (string.IsNullOrEmpty(ca153.Text)) ? -1 : double.Parse(ca153.Text));
                    cmd.Parameters.AddWithValue("@CA199", (string.IsNullOrEmpty(ca199.Text)) ? -1 : double.Parse(ca199.Text));
                    cmd.Parameters.AddWithValue("@CEA", 0);
                    cmd.Parameters.AddWithValue("@AntiCoagolanteLupico", (anticoaglup.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Antifosfolipidico", (antifosfo.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Antitrombina", (antitromb.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@AntidDNA", (antidna.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@AntiCardiolipina", (anticardio.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@AntoGliadina", (antiglidina.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@AntiNucleares", (antinucleares.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@AntiEndomisio", (antiendomisio.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@AntiReticulina", (antireti.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@AntiTransglutaminase", (antitransglut.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@ProteinaC", (proteinac.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@ProteinaS", (proteinas.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Imunoglobulnas", (imunoglob.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Glicoproteina", (glicoproteina.Text.Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Outros", outros.Text);
                    cmd.Connection = cn;
                    try { rows = cmd.ExecuteNonQuery(); }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to update sangue in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }
                }
                else if (urina.Checked == true)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "update ProjetoConsultorio.Urina set IDDA=@IDDA, SumariaUrinas=@SumariaUrinas, Urocultura=@Urocultura,ValorUroc= @ValorUroc,TIG=@TIG where IDDA=@IDDA";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IDDA", ID);
                    cmd.Parameters.AddWithValue("@SumariaUrinas", sumario.Text);
                    cmd.Parameters.AddWithValue("@Urocultura", ((urocultura.Text).Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@ValorUroc", valorUroc.Text);
                    cmd.Parameters.AddWithValue("@TIG", ((tig.Text).Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Connection = cn;
                    try { rows = cmd.ExecuteNonQuery(); }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to update urina in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }
                }
                else if (ecografiaobs.Checked == true)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "update ProjetoConsultorio.EcografiaObstetrica set  IDDA=@IDDA,Semana=@Semana,Dias=@Dias where IDDA=@IDDA";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IDDA", ID);
                    cmd.Parameters.AddWithValue("@Semana", (string.IsNullOrEmpty(semanas.Text)) ? -1 : int.Parse(semanas.Text));
                    cmd.Parameters.AddWithValue("@Dias", (string.IsNullOrEmpty(dias.Text)) ? -1 : int.Parse(dias.Text));
                    cmd.Connection = cn;
                    try { rows = cmd.ExecuteNonQuery(); }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to update ecografiaObs in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }
                }
                else if (cmcolo.Checked == true)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "update ProjetoConsultorio.CMColo set IDDA= @IDDA, Convencional=@Convencional, MeioLiquido=@MeioLiquido where IDDA=@IDDA";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IDDA", ID);
                    cmd.Parameters.AddWithValue("@Convencional", convencional.Text);
                    cmd.Parameters.AddWithValue("@MeioLiquido", meioliquido.Text);
                    cmd.Connection = cn;
                    try { rows = cmd.ExecuteNonQuery(); }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to update cmColo in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }
                }
                else if (rx.Checked == true)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "update ProjetoConsultorio.RX set  IDDA=@IDDA, Tipo=@Tipo where IDDA=@IDDA";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IDDA", ID);
                    cmd.Parameters.AddWithValue("@Tipo", rxTipo.Text);
                    cmd.Connection = cn;
                    try { rows = cmd.ExecuteNonQuery(); }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to update rx in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }
                }
                else if (mamografia.Checked == true)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "update ProjetoConsultorio.Mama set IDDA=@IDDA, Mamografia=@Mamografia, EcografiaMamaria=@EcografiaMamaria where IDDA=@IDDA";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IDDA", ID);
                    cmd.Parameters.AddWithValue("@Mamografia", mamaMamografia.Text);
                    cmd.Parameters.AddWithValue("@EcografiaMamaria", ecografiamam.Text);
                    cmd.Connection = cn;
                    try { rows = cmd.ExecuteNonQuery(); }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to update mama in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }
                }
                else if (exsudado.Checked == true)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "update ProjetoConsultorio.Exsudado set  IDDA=@IDDA, VaginalBacteriologico=@VaginalBacteriologico, VaginalMicologico=@VaginalMicologico, VaginalParasitologico=@VaginalParasitologico, VaginalRetalSGB=@VaginalRetalSGB where IDDA=@IDDA";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IDDA", ID);
                    cmd.Parameters.AddWithValue("@VaginalBacteriologico", vagbac.Text);
                    cmd.Parameters.AddWithValue("@VaginalMicologico", vagmic.Text);
                    cmd.Parameters.AddWithValue("@VaginalParasitologico", vagparas.Text);
                    cmd.Parameters.AddWithValue("@VaginalRetalSGB", ((vagretal.Text).Equals("true", StringComparison.OrdinalIgnoreCase)) ? 1 : 0);
                    cmd.Connection = cn;
                    try { rows = cmd.ExecuteNonQuery(); }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to update exsudado in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }
                }

            }


            this.Hide();
            Form sistema = new Pacientes();
            sistema.ShowDialog();
            this.Close();

        }

        private void remover_Click(object sender, EventArgs e)
        {
            if (ID != -1)
            {
                if (!verifySGBDConnection())
                    return;

                SqlCommand cmd = new SqlCommand("exec deleteAnaliseProcedure "+ID, cn);

                try { int rows = cmd.ExecuteNonQuery(); }
                catch (Exception ex)
                {
                    throw new Exception("Failed to delete descricao of database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                }


                this.Hide();
                Form sistema = new Pacientes();
                sistema.ShowDialog();
                this.Close();

            }
        }
    }

}
