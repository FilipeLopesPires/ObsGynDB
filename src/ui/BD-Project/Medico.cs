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
    public partial class Medico : Form
    {
        Dictionary<string, int> meses = new Dictionary<string, int>() { { "janeiro", 1 }, { "fevereiro", 2 }, { "março", 3 }, { "abril", 4 }, { "maio", 5 }, { "junho", 6 }, { "julho", 7 }, { "agosto", 8 }, { "setembro", 9 }, { "outubro", 10 }, { "novembro", 11 }, { "dezembro", 12 } };
        private SqlConnection cn;

        public Medico()
        {
            InitializeComponent();
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form sistema = new Pacientes();
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


        private void loadAllMed(SqlCommand cmd)
        {
            if (!verifySGBDConnection())
                return;

            SqlDataReader reader = cmd.ExecuteReader();
            medicos.Items.Clear();

            while (reader.Read())
            {
                MedicoInfo m = new MedicoInfo();
                m.Nome = reader["Nome"].ToString();
                m.NIF = int.Parse(reader["MedNIF"].ToString());
                m.Espacialidade = reader["Especialidade"].ToString();
                m.HorasSemanais = int.Parse(reader["HorasSemanais"].ToString());
                m.Mail = reader["Mail"].ToString();
                m.DataInicio = reader["DataInicio"].ToString();
                m.Salario = double.Parse(reader["Salario"].ToString());

                medicos.Items.Add(m);
            }


            cn.Close();
        }

        private void showMed(int Mnif)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("exec searchMedByNIF "+Mnif, cn);
            SqlDataReader reader = cmd.ExecuteReader();
            medicos.Items.Clear();
            MedicoInfo m = new MedicoInfo();
            while (reader.Read())
            {
                m.Nome = reader["Nome"].ToString();
                m.NIF = int.Parse(reader["MedNIF"].ToString());
                m.Espacialidade = reader["Especialidade"].ToString();
                m.HorasSemanais = int.Parse(reader["HorasSemanais"].ToString());
                m.Mail = reader["Mail"].ToString();
                m.DataInicio = reader["DataInicio"].ToString();
                m.Salario = double.Parse(reader["Salario"].ToString());
            }

            cn.Close();

            nome.Text = m.Nome;
            mail.Text = m.Mail;
            dataInicio.Text = m.DataInicio;
            especialidade.Text = m.Espacialidade;
            nif.Text = (m.NIF).ToString();
            salario.Text = m.Salario.ToString();
            horasSemanais.Text = m.HorasSemanais.ToString();
            medicos.Refresh();

        }


        private void searchmedico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!verifySGBDConnection())
                    return;

                String aux = searchmedico.Text;
                SqlCommand cmd;

                cmd = new SqlCommand("exec searchMedByName " + aux, cn);

                if (aux.All(char.IsDigit) && aux.Length == 8)
                {
                    cmd = new SqlCommand("exec searchMedByNIF " + aux, cn);
                }
                if (aux == "")
                {
                    cmd = new SqlCommand("exec searchMed", cn);
                }
                if (string.Equals(aux, "ginecologia", StringComparison.OrdinalIgnoreCase) || string.Equals(aux, "obstetricia", StringComparison.OrdinalIgnoreCase))
                {
                    cmd = new SqlCommand("exec searchMedByEspec " + aux, cn);
                }

                loadAllMed(cmd);
            }
        }

        private void Medico_Load(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            loadAllMed(new SqlCommand("exec searchMed", cn));
        }

        private void editButton_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty( nif.Text) && (nif.Text).All(char.IsDigit) && (nif.Text).Length==8)
            {

                int rows = 0;

                if (!verifySGBDConnection())
                    return;
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE ProjetoConsultorio.Medico SET MedNIF = @MedNIF, Nome = @Nome, Mail = @Mail, Especialidade = @Especialidade, HorasSemanais = @HorasSemanais, DataInicio = @DataInicio, Salario = @Salario WHERE MedNIF = @MedNIF";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@MedNIF", int.Parse(nif.Text));
                cmd.Parameters.AddWithValue("@Nome", nome.Text);
                cmd.Parameters.AddWithValue("@Mail", mail.Text);
                cmd.Parameters.AddWithValue("@Especialidade", especialidade.Text);
                cmd.Parameters.AddWithValue("@HorasSemanais", int.Parse(horasSemanais.Text));
                string[] datadb = Regex.Split(dataInicio.Text, "de");
                cmd.Parameters.AddWithValue("@DataInicio", datadb[2] + "-" + meses[datadb[1].ToLower().Trim()] + "-" + datadb[0]);
                cmd.Parameters.AddWithValue("@Salario", double.Parse(salario.Text));
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


        }

        private void medicos_DoubleClick(object sender, EventArgs e)
        {
            if (medicos.SelectedIndex >= 0)
            {
                showMed(((MedicoInfo)medicos.SelectedItem).NIF);
                loadAllMed(new SqlCommand("exec searchMed", cn));
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(nif.Text) && (nif.Text).All(char.IsDigit) && (nif.Text).Length == 8)
            {

                int rows = 0;

                if (!verifySGBDConnection())
                    return;
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "insert into ProjetoConsultorio.Medico values(@MedNIF,@Nome,@Mail,@Especialidade,@HorasSemanais,@DataInicio,@Salario)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@MedNIF", int.Parse(nif.Text));
                cmd.Parameters.AddWithValue("@Nome", nome.Text);
                cmd.Parameters.AddWithValue("@Mail", mail.Text);
                cmd.Parameters.AddWithValue("@Especialidade", especialidade.Text);
                cmd.Parameters.AddWithValue("@HorasSemanais", int.Parse(horasSemanais.Text));
                string[] datadb = Regex.Split(dataInicio.Text, "de");
                cmd.Parameters.AddWithValue("@DataInicio", datadb[2] + "-" + meses[datadb[1].ToLower().Trim()] + "-" + datadb[0]);
                cmd.Parameters.AddWithValue("@Salario", Convert.ToSingle(salario.Text));
                cmd.Connection = cn;

                try
                {
                    rows = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to insert medico in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
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
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (medicos.SelectedIndex >= 0)
            {
                int nif=((MedicoInfo)medicos.SelectedItem).NIF;

                if (!verifySGBDConnection())
                    return;
                SqlCommand cmd = new SqlCommand("exec deleteMedProcedure "+ nif, cn);
                int rows = cmd.ExecuteNonQuery();
                cn.Close();

                this.Hide();
                Form sistema = new Pacientes();
                sistema.ShowDialog();
                this.Close();

            }
        }
    }
}
