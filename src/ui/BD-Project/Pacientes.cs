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

namespace BD_Project
{
    public partial class Pacientes : Form
    {
        SqlConnection cn;
        public Pacientes()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            loadAll(sender, e);
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


        private void loadPac(SqlCommand cmd)
        {

            SqlDataReader reader = cmd.ExecuteReader();
            listaPacientes.Items.Clear();

            while (reader.Read())
            {
                Paciente C = new Paciente();
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
                listaPacientes.Items.Add(C);
            }

        }


        private void loadAll(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("exec searchAllPac", cn);
            loadPac(cmd);

            cn.Close();
        }


        private void Adicionar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form sistema = new InfoPaciente();
            sistema.ShowDialog();
            this.Close();
        }

        private void Editar_Click(object sender, EventArgs e)
        {
            if (listaPacientes.SelectedIndex >= 0)
            {
                this.Hide();
                Form sistema = new InfoPaciente(((Paciente)(listaPacientes.SelectedItem)).NIF);
                sistema.ShowDialog();
                this.Close();
            }
        }

        private void Remover_Click(object sender, EventArgs e)
        {
            if (listaPacientes.SelectedIndex >= 0)
            {
                if (!verifySGBDConnection())
                    return;

                SqlCommand cmd=new SqlCommand("exec deletePacProcedure "+((Paciente)listaPacientes.SelectedItem).NIF, cn);
                int rows = cmd.ExecuteNonQuery();

                Console.Write(rows);
                loadAll(sender, e);
                

                cn.Close();
            }
        }

        private void HistorialClinico_Click(object sender, EventArgs e)
        {
            if (listaPacientes.SelectedIndex >= 0)
            {
                this.Hide();
                Form sistema = new HistorialClinico(((Paciente)listaPacientes.SelectedItem).NIF);
                sistema.ShowDialog();
                this.Close();
            }

        }

        private void Medico_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form sistema = new Medico();
            sistema.ShowDialog();
            this.Close();
        }

        private void reqButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form sistema = new RequisicaoAnalise();
            sistema.ShowDialog();
            this.Close();
        }

        private void pac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (!verifySGBDConnection())
                    return;

                String aux = pac.Text;
                SqlCommand cmd;

                cmd = new SqlCommand("exec searchByName " + aux, cn);

                if (aux.All(char.IsDigit) && aux.Length==8)
                {
                    cmd = new SqlCommand("exec searchByNIF " + aux, cn);
                }
                if (aux == "")
                {
                    cmd = new SqlCommand("exec searchAllPac", cn);
                }
                if(string.Equals(aux,"ADSE", StringComparison.OrdinalIgnoreCase) || string.Equals(aux,"PSP", StringComparison.OrdinalIgnoreCase) 
                    || string.Equals(aux,"GNR", StringComparison.OrdinalIgnoreCase) || string.Equals(aux, "SSMJ", StringComparison.OrdinalIgnoreCase))
                {
                    cmd = new SqlCommand("exec searchBySubsist " + aux, cn);
                }
                
                loadPac(cmd);

                cn.Close();
            }
        }
    }
}
