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
    public partial class InfoPaciente : Form
    {
        private int pacNif=-1;
        private SqlConnection cn;
        private Paciente C;

        public InfoPaciente()
        {
            InitializeComponent();
        }

        public InfoPaciente(int pn)
        {
            InitializeComponent();
            pacNif = pn;
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form sistema = new Pacientes();
            sistema.ShowDialog();
            this.Close();
        }

        private void histClinButton_Click(object sender, EventArgs e)
        {
            if (pacNif != -1)
            {
                this.Hide();
                Form sistema = new HistorialClinico(pacNif);
                sistema.ShowDialog();
                this.Close();
            }
        }

        private void conButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form sistema = new Consulta(pacNif);
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

        private void InfoPaciente_Load(object sender, EventArgs e)
        {
            if (pacNif != -1)
            {
                cn = getSGBDConnection();
                loadPac();
                loadCon();
                loadGrav();
                loadBebe();
                loadAnalise();

                nome.Text = C.Nome;
                nif.Text = C.NIF.ToString();
                contactos.Text = C.Contactos;
                codPost.Text = C.CodigoPostal;
                residencia.Text = C.Residencia;
                dataNasc.Text = C.DataNascimento;
                profissao.Text = C.Profissao;
                estCivil.Text = C.EstadoCivil;
                subsistema.Text = C.Subsistema;
                ss.Text = C.SS.ToString();
                sns.Text = C.SNS.ToString();
                mail.Text = C.Mail;
            }

        }

        private void loadGrav()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("exec searchGravByNIF " + pacNif, cn);
            SqlDataReader reader = cmd.ExecuteReader();
            gravidezes.Items.Clear();
            while (reader.Read())
            {
                Gravidez g = new Gravidez();
                g.NIF = int.Parse(reader["NIFPac"].ToString());
                g.NumConsultas = int.Parse(reader["NumeroConsultas"].ToString());
                g.NumGravidez = int.Parse(reader["NumeroGravidez"].ToString());
                gravidezes.Items.Add(g);

            }

            cn.Close();
        }

        private void loadAnalise()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("exec searchAnaliseByNIF " + pacNif, cn);
            SqlDataReader reader = cmd.ExecuteReader();
            analises.Items.Clear();
            while (reader.Read())
            {
                Analise a = new Analise();
                a.Id=int.Parse(reader["IDDA"].ToString());
                a.Requisicao = int.Parse(reader["IDDA"].ToString());
                a.Data = reader["DataDA"].ToString();
                a.Descricao = reader["Descricao"].ToString();
                analises.Items.Add(a);

            }

            cn.Close();
        }

        private void loadBebe()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("exec searchBebeByNIF " + pacNif, cn);
            SqlDataReader reader = cmd.ExecuteReader();
            bebes.Items.Clear();
            while (reader.Read())
            {
                Bebe b = new Bebe();
                b.NumBebe=int.Parse(reader["Bebe"].ToString());
                b.Sexo = reader["SexoBebe"].ToString();
                b.Peso = Convert.ToDouble(reader["PesoBebe"].ToString());
                b.Indicacao = reader["Indicacao"].ToString();
                b.Parto = reader["Parto"].ToString();
                b.APGAR = reader["APGAR"].ToString();
                bebes.Items.Add(b);

            }

            cn.Close();
        }

        private void loadCon()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("exec searchConByNIF " + pacNif, cn);
            SqlDataReader reader = cmd.ExecuteReader();
            consultas.Items.Clear();
            while (reader.Read())
            {
                ConsultaPac c = new ConsultaPac();

                c.ID = int.Parse(reader["idconsulta"].ToString());
                c.Data = reader["dataconsulta"].ToString();
                c.Tipo = reader["tipoconsulta"].ToString();
                consultas.Items.Add(c);
            }

            cn.Close();
        }

        private void loadPac()
        {

            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("exec searchByNIF "+pacNif, cn);
            SqlDataReader reader = cmd.ExecuteReader();
            C = new Paciente();
            while (reader.Read())
            {
                C.Nome = reader["Nome"].ToString();
                C.NIF = int.Parse(reader["PacNIF"].ToString());
                C.Contactos = reader["Contactos"].ToString();
                C.Mail = reader["Mail"].ToString();
                C.CodigoPostal = reader["CodigoPostal"].ToString();
                C.Residencia = reader["Residencia"].ToString();
                C.DataNascimento = reader["DataNascimento"].ToString();
                C.Profissao = reader["Profissao"].ToString();
                C.EstadoCivil = reader["EstadoCivil"].ToString();
                C.Subsistema = reader["Subsistema"].ToString();
                C.SS = int.Parse(reader["SS"].ToString());
                C.SNS = int.Parse(reader["SNS"].ToString());
            }

            cn.Close();
        }

        private void consultas_DoubleClick(object sender, EventArgs e)
        {
            if (consultas.SelectedIndex >= 0)
            {
                this.Hide();
                Form sistema = new Consulta(pacNif, ((ConsultaPac)consultas.SelectedItem).ID);
                sistema.ShowDialog();
                this.Close();
            }
            
        }

        private void analises_DoubleClick(object sender, EventArgs e)
        {
            if (analises.SelectedIndex >= 0)
            {
                this.Hide();
                Form sistema = new DescricaoAnalise(((Analise)analises.SelectedItem).Id);
                sistema.ShowDialog();
                this.Close();
            }
        }

        private void submeterButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> meses = new Dictionary<string, int>(){ {"janeiro", 1},{"fevereiro", 2 },{"março",3 },{"abril",4 },{"maio", 5 },{"junho", 6 },{"julho",7 },{"agosto", 8 },{"setembro", 9 },{"outubro", 10 },{"novembro", 11 },{"dezembro", 12 } };
            Paciente p = new Paciente();

            p.Nome = nome.Text;
            p.NIF = int.Parse(nif.Text);
            p.Contactos = contactos.Text;
            p.CodigoPostal = codPost.Text;
            p.Residencia = residencia.Text;
            string[] datadb = Regex.Split(dataNasc.Text, "de");
            p.DataNascimento = datadb[2] + "-" + meses[datadb[1].ToLower().Trim()] + "-" + datadb[0];
            p.Profissao = profissao.Text;
            p.EstadoCivil = estCivil.Text;
            p.Subsistema = subsistema.Text;
            p.SS = String.IsNullOrEmpty(ss.Text) ? -1 : int.Parse(ss.Text);
            p.SNS = String.IsNullOrEmpty(sns.Text) ? -1 : int.Parse(sns.Text);
            p.Mail = mail.Text;


            if (pacNif != -1)
            {
                updatePac(p);
            }
            else
            {
                insertPac(p);
            }
        }

        private void insertPac(Paciente p)
        {
            int rows = 0;

            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "insert into ProjetoConsultorio.Paciente values( @PacNIF,@Nome, @DataNascimento, @Residencia, @CodigoPostal, @Contactos, @Mail, @Profissao, @EstadoCivil,@SS,@SNS,@Subsistema)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PacNIF", p.NIF);
            cmd.Parameters.AddWithValue("@Nome", p.Nome);
            cmd.Parameters.AddWithValue("@DataNascimento", p.DataNascimento);
            cmd.Parameters.AddWithValue("@Residencia", p.Residencia);
            cmd.Parameters.AddWithValue("@CodigoPostal", p.CodigoPostal);
            cmd.Parameters.AddWithValue("@Contactos", p.Contactos);
            cmd.Parameters.AddWithValue("@Mail", p.Mail);
            cmd.Parameters.AddWithValue("@Profissao", p.Profissao);
            cmd.Parameters.AddWithValue("@EstadoCivil", p.EstadoCivil);
            cmd.Parameters.AddWithValue("@SS", p.SS);
            cmd.Parameters.AddWithValue("@SNS", p.SNS);
            cmd.Parameters.AddWithValue("@Subsistema", p.Subsistema);
            cmd.Connection = cn;

            try
            {
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to insert paciente in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
            }
            finally
            {

                cn.Close();

                this.Hide();
                Form sistema = new Pacientes();
                sistema.ShowDialog();
                this.Close();
            }
        }

        private void updatePac(Paciente p)
        {
            int rows = 0;

            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "UPDATE ProjetoConsultorio.Paciente SET PacNIF = @PacNIF, Nome = @Nome,  DataNascimento = @DataNascimento,  Residencia = @Residencia,  CodigoPostal = @CodigoPostal, Contactos = @Contactos, Mail = @Mail, Profissao = @Profissao, EstadoCivil = @EstadoCivil, SS = @SS, SNS = @SNS, Subsistema=@Subsistema WHERE PacNIF = @PacNIF";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PacNIF", p.NIF);
            cmd.Parameters.AddWithValue("@Nome", p.Nome);
            cmd.Parameters.AddWithValue("@DataNascimento", p.DataNascimento);
            cmd.Parameters.AddWithValue("@Residencia", p.Residencia);
            cmd.Parameters.AddWithValue("@CodigoPostal", p.CodigoPostal);
            cmd.Parameters.AddWithValue("@Contactos", p.Contactos);
            cmd.Parameters.AddWithValue("@Mail", p.Mail);
            cmd.Parameters.AddWithValue("@Profissao", p.Profissao);
            cmd.Parameters.AddWithValue("@EstadoCivil", p.EstadoCivil);
            cmd.Parameters.AddWithValue("@SS", p.SS);
            cmd.Parameters.AddWithValue("@SNS", p.SNS);
            cmd.Parameters.AddWithValue("@Subsistema", p.Subsistema);
            cmd.Connection = cn;

            try
            {
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update paciente in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
            }
            finally
            {
                
                cn.Close();

                this.Hide();
                Form sistema = new Pacientes();
                sistema.ShowDialog();
                this.Close();
            }
        }

        private void gravidezes_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
            Form sistema = new GravidezInfo(pacNif, ((Gravidez)gravidezes.SelectedItem).NumGravidez, ((Gravidez)gravidezes.SelectedItem).NumConsultas);
            sistema.ShowDialog();
            this.Close();
        }
    }
}
