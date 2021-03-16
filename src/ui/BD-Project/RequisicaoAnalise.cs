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
    public partial class RequisicaoAnalise : Form
    {
        private Dictionary<string, int> meses = new Dictionary<string, int>() { { "janeiro", 1 }, { "fevereiro", 2 }, { "março", 3 }, { "abril", 4 }, { "maio", 5 }, { "junho", 6 }, { "julho", 7 }, { "agosto", 8 }, { "setembro", 9 }, { "outubro", 10 }, { "novembro", 11 }, { "dezembro", 12 } };
        private SqlConnection cn;

        public RequisicaoAnalise()
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

        private void addButton_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            int rows;
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "insert into ProjetoConsultorio.RequisicaoAnalise values(@ID,@DataR,@NIFPaciente,@NIFMedico,@Tipo)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ID", ((int)new SqlCommand("select dbo.maxReqAnalise()", cn).ExecuteScalar())+1);
            string[] datadb = Regex.Split(data.Text, "de");
            cmd.Parameters.AddWithValue("@DataR", datadb[2] + "-" + meses[datadb[1].ToLower().Trim()] + "-" + datadb[0]);
            cmd.Parameters.AddWithValue("@NIFPaciente", nifPac.Text);
            cmd.Parameters.AddWithValue("@NIFMedico", nifMed.Text);
            cmd.Parameters.AddWithValue("@Tipo", tipo.Text);
            cmd.Connection = cn;

            try
            {
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to insert requisicao in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
            }
            finally
            {

                cn.Close();

                this.Hide();
                Form sistema = new Pacientes();
                sistema.ShowDialog();
                this.Close();
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
    }
}
