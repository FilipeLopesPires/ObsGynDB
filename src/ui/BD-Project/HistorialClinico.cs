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
    public partial class HistorialClinico : Form
    {
        Dictionary<string, int> meses = new Dictionary<string, int>() { { "janeiro", 1 }, { "fevereiro", 2 }, { "março", 3 }, { "abril", 4 }, { "maio", 5 }, { "junho", 6 }, { "julho", 7 }, { "agosto", 8 }, { "setembro", 9 }, { "outubro", 10 }, { "novembro", 11 }, { "dezembro", 12 } };
        private SqlConnection cn;
        private int PacNif=-1;
        private HistClinico HC;

        public HistorialClinico()
        {
            InitializeComponent();
        }

        public HistorialClinico(int nf)
        {
            InitializeComponent();
            PacNif = nf;
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form sistema = new Pacientes();
            sistema.ShowDialog();
            this.Close();
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form sistema = new InfoPaciente(PacNif);
            sistema.ShowDialog();
            this.Close();
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

        private void HistorialClinico_Load(object sender, EventArgs e)
        {

            if (PacNif != -1)
            {
                cn = getSGBDConnection();
                loadHC();

                gruposang.Text = HC.GrupoSanguineo;
                alcool.Text = HC.Alcool;
                tabaco.Text = HC.Tabaco;
                alergias.Text = HC.Alergias;
                antFam.Text = HC.AntecedentesFamiliares;
                antPess.Text = HC.AntecedentesPessoais;
                cirurgias.Text = HC.Cirurgias;
                altura.Text = HC.Altura.ToString();
                peso.Text = HC.Peso.ToString();
                transSang.Text = HC.TranferenciaSanguinea.ToString();
                medicamentos.Text = HC.Medicamentos;
                menopausa.Text = (HC.Menopausa == -1) ? "" : HC.Menopausa.ToString();
                coitarca.Text = (HC.Coitarca == -1) ? "" : HC.Coitarca.ToString();
                menarca.Text = (HC.Menarca == -1) ? "" : HC.Menarca.ToString();
                partos.Text = (HC.Partos == -1) ? "" : HC.Partos.ToString();
                gravidezes.Text = (HC.Gravidezes == -1) ? "" : HC.Gravidezes.ToString();
                ciclos.Text = HC.Ciclos;
                dum.Text = (HC.DUM == "") ? "1753-1-1" : HC.DUM;
                dpp.Text = (HC.DPP == "") ? "1753-1-1" : HC.DPP;
                histObstetricia.Text = HC.HistoriaObs;
                contracecao.Text = HC.Contracecao;
            }


        }



        private void loadHC()
        {

            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("exec searchPacHC " + PacNif, cn);
            SqlDataReader reader = cmd.ExecuteReader();
            HC = new HistClinico();
            while (reader.Read())
            {
                HC.NIF = int.Parse(reader["PacNIF"].ToString());
                HC.GrupoSanguineo = reader["GrupoSanguineo"].ToString();
                HC.Alcool = reader["Alcool"].ToString();
                HC.Tabaco = reader["Tabaco"].ToString();
                HC.Alergias = reader["Alergias"].ToString();
                HC.Cirurgias = reader["Cirurgias"].ToString();
                HC.Altura = double.Parse(reader["Altura"].ToString());
                HC.Peso = double.Parse(reader["Peso"].ToString());
                HC.AntecedentesFamiliares = reader["AntFamiliares"].ToString();
                HC.AntecedentesPessoais = reader["AntPessoais"].ToString();
                HC.TranferenciaSanguinea = bool.Parse(reader["TransfSanguineas"].ToString());
                HC.Medicamentos = reader["Medicamentos"].ToString();
                HC.Menarca = (reader["Menarca"] == DBNull.Value)? -1 :  int.Parse(reader["Menarca"].ToString());
                HC.Coitarca = (reader["Coitarca"] == DBNull.Value) ? -1 : int.Parse(reader["Coitarca"].ToString());
                HC.Ciclos = reader["Ciclos"].ToString();
                HC.Gravidezes = (reader["Gravidezes"] == DBNull.Value) ? -1 : int.Parse(reader["Gravidezes"].ToString());
                HC.Partos = (reader["Partos"] == DBNull.Value) ? -1 : int.Parse(reader["Partos"].ToString());
                HC.HistoriaObs = reader["HistoriaObstetrica"].ToString();
                HC.DUM = reader["DUM"].ToString();
                HC.DPP = reader["DPP"].ToString();
                HC.Menopausa = (reader["Menopausa"] == DBNull.Value) ? -1 : int.Parse(reader["Menopausa"].ToString());
                HC.Contracecao = reader["Contracecao"].ToString();


            }


            reader.Close();


            cmd = new SqlCommand("exec searchByNIF " + PacNif, cn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                nif.Text = reader["PacNIF"].ToString();
                nome.Text = reader["Nome"].ToString();
                dataNasc.Text = reader["DataNascimento"].ToString();
                estCivil.Text = reader["EstadoCivil"].ToString();


            }


            cn.Close();
        }







        private void editButton_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;

            if ((int)new SqlCommand("select dbo.gotHC("+PacNif+")", cn).ExecuteScalar() == 0)
            {
                int rows = 0;

                
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "insert into ProjetoConsultorio.HistorialClinico values( @PacNIF,@GrupoSanguineo,@Altura,@Peso,@AntFamiliares,@AntPessoais,@Cirurgias,@Alergias,@TransfSanguineas,@Tabaco,@Alcool,@Medicamentos,@Menarca,@Coitarca,@Ciclos,@Contracecao,@Gravidezes,@Partos,@HistoriaObstetrica,@DUM,@DPP,@Menopausa)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PacNIF", int.Parse(nif.Text));
                cmd.Parameters.AddWithValue("@GrupoSanguineo", gruposang.Text);
                cmd.Parameters.AddWithValue("@Altura", string.IsNullOrEmpty(altura.Text)?-1:double.Parse(altura.Text));
                cmd.Parameters.AddWithValue("@Peso", string.IsNullOrEmpty(peso.Text) ? -1 : double.Parse(peso.Text));
                cmd.Parameters.AddWithValue("@AntFamiliares", antFam.Text);
                cmd.Parameters.AddWithValue("@AntPessoais", antPess.Text);
                cmd.Parameters.AddWithValue("@Cirurgias", cirurgias.Text);
                cmd.Parameters.AddWithValue("@Alergias", alergias.Text);
                cmd.Parameters.AddWithValue("@TransfSanguineas", transSang.Text.Equals("true", StringComparison.OrdinalIgnoreCase) ? 1 : 0);
                cmd.Parameters.AddWithValue("@Tabaco", tabaco.Text);
                cmd.Parameters.AddWithValue("@Alcool", alcool.Text);
                cmd.Parameters.AddWithValue("@Medicamentos", medicamentos.Text);
                cmd.Parameters.AddWithValue("@Menarca", string.IsNullOrEmpty(menarca.Text) ? -1 : int.Parse(menarca.Text));
                cmd.Parameters.AddWithValue("@Coitarca", string.IsNullOrEmpty(coitarca.Text) ? -1 : int.Parse(coitarca.Text));
                cmd.Parameters.AddWithValue("@Ciclos", ciclos.Text);
                cmd.Parameters.AddWithValue("@Contracecao", contracecao.Text);
                cmd.Parameters.AddWithValue("@Gravidezes", string.IsNullOrEmpty(gravidezes.Text) ? 0 : int.Parse(gravidezes.Text));
                cmd.Parameters.AddWithValue("@Partos", string.IsNullOrEmpty(partos.Text) ? 0 : int.Parse(partos.Text));
                cmd.Parameters.AddWithValue("@HistoriaObstetrica", histObstetricia.Text);
                string[] datadbdum = Regex.Split(dum.Text, "de");
                cmd.Parameters.AddWithValue("@DUM", datadbdum[2] + "-" + meses[datadbdum[1].ToLower().Trim()] + "-" + datadbdum[0]);
                string[] datadbdpp = Regex.Split(dpp.Text, "de");
                cmd.Parameters.AddWithValue("@DPP", datadbdpp[2] + "-" + meses[datadbdpp[1].ToLower().Trim()] + "-" + datadbdpp[0]);
                cmd.Parameters.AddWithValue("@Menopausa", string.IsNullOrEmpty(menopausa.Text) ? -1 : int.Parse(menopausa.Text));
                cmd.Connection = cn;
                try
                {
                    rows = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to insert historial in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                }
            }
            else
            {

                int rows = 0;

                if (!verifySGBDConnection())
                    return;
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "update ProjetoConsultorio.HistorialClinico set PacNIF=@PacNIF,GrupoSanguineo=@GrupoSanguineo,Altura=@Altura,Peso=@Peso,AntFamiliares=@AntFamiliares,AntPessoais=@AntPessoais,Cirurgias=@Cirurgias,Alergias=@Alergias,TransfSanguineas=@TransfSanguineas,Tabaco=@Tabaco,Alcool=@Alcool,Medicamentos=@Medicamentos,Menarca=@Menarca,Coitarca=@Coitarca,Ciclos=@Ciclos,Contracecao=@Contracecao,Gravidezes=@Gravidezes,Partos=@Partos,HistoriaObstetrica=@HistoriaObstetrica,DUM=@DUM,DPP=@DPP,Menopausa=@Menopausa where PacNIF=@PacNIF";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PacNIF", int.Parse(nif.Text));
                cmd.Parameters.AddWithValue("@GrupoSanguineo", gruposang.Text);
                cmd.Parameters.AddWithValue("@Altura", string.IsNullOrEmpty(altura.Text) ? -1 : double.Parse(altura.Text));
                cmd.Parameters.AddWithValue("@Peso", string.IsNullOrEmpty(peso.Text) ? -1 : double.Parse(peso.Text));
                cmd.Parameters.AddWithValue("@AntFamiliares", antFam.Text);
                cmd.Parameters.AddWithValue("@AntPessoais", antPess.Text);
                cmd.Parameters.AddWithValue("@Cirurgias", cirurgias.Text);
                cmd.Parameters.AddWithValue("@Alergias", alergias.Text);
                cmd.Parameters.AddWithValue("@TransfSanguineas", transSang.Text.Equals("true", StringComparison.OrdinalIgnoreCase) ? 1 : 0);
                cmd.Parameters.AddWithValue("@Tabaco", tabaco.Text);
                cmd.Parameters.AddWithValue("@Alcool", alcool.Text);
                cmd.Parameters.AddWithValue("@Medicamentos", medicamentos.Text);
                cmd.Parameters.AddWithValue("@Menarca", string.IsNullOrEmpty(menarca.Text) ? -1 : int.Parse(menarca.Text));
                cmd.Parameters.AddWithValue("@Coitarca", string.IsNullOrEmpty(coitarca.Text) ? -1 : int.Parse(coitarca.Text));
                cmd.Parameters.AddWithValue("@Ciclos", ciclos.Text);
                cmd.Parameters.AddWithValue("@Contracecao", contracecao.Text);
                cmd.Parameters.AddWithValue("@Gravidezes", string.IsNullOrEmpty(gravidezes.Text) ? 0 : int.Parse(gravidezes.Text));
                cmd.Parameters.AddWithValue("@Partos", string.IsNullOrEmpty(partos.Text) ? 0 : int.Parse(partos.Text));
                cmd.Parameters.AddWithValue("@HistoriaObstetrica", histObstetricia.Text);
                string[] datadbdum = Regex.Split(dum.Text, "de");
                cmd.Parameters.AddWithValue("@DUM", datadbdum[2] + "-" + meses[datadbdum[1].ToLower().Trim()] + "-" + datadbdum[0]);
                string[] datadbdpp = Regex.Split(dpp.Text, "de");
                cmd.Parameters.AddWithValue("@DPP", datadbdpp[2] + "-" + meses[datadbdpp[1].ToLower().Trim()] + "-" + datadbdpp[0]);
                cmd.Parameters.AddWithValue("@Menopausa", string.IsNullOrEmpty(menopausa.Text) ? -1 : int.Parse(menopausa.Text));
                cmd.Connection = cn;
                try
                {
                    rows = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to update historial in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                }

            }


            cn.Close();
            this.Hide();
            Form sistema = new Pacientes();
            sistema.ShowDialog();
            this.Close();


        }
    }
}
