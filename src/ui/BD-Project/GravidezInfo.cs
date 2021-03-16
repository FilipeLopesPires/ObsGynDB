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
    public partial class GravidezInfo : Form
    {
        private SqlConnection cn;
        private int pacNif, gravidez, consult;

        public GravidezInfo()
        {
            InitializeComponent();
        }

        public GravidezInfo(int nif, int grav, int c)
        {
            InitializeComponent();
            pacNif = nif;
            gravidez = grav;
            consult = c;
        }

        private void home_Click(object sender, EventArgs e)
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


        private TableLayoutPanel createTL()
        {
            TableLayoutPanel panel = new TableLayoutPanel();
            panel.Width = (int)(flowLayoutPanel.Width * 0.9);
            panel.Height = 110;
            panel.ColumnCount = 4;
            panel.RowCount = 3;
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            panel.Controls.Add(new Label() { Text = "Parto", Font = new Font("Microsft Sans Serif", 12), Anchor = (AnchorStyles.Bottom | AnchorStyles.Right) }, 0, 0);
            ComboBox tb1 = new ComboBox() { Items = { "Eutocico", "Ventosa", "Forceps", "Cesariana", "Pelvico" }, Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left), Font = new Font("Microsft Sans Serif", 12) };
            tb1.KeyDown += new KeyEventHandler(tipoParto_KeyDown);
            tb1.Name = "tipoParto";
            panel.Controls.Add(tb1, 1, 0);
            panel.Controls.Add(new Label() { Text = "Peso Bébe", Font = new Font("Microsft Sans Serif", 12), Anchor = (AnchorStyles.Bottom | AnchorStyles.Right) }, 2, 0);
            TextBox tb2 = new TextBox() { Name = "pesoBebe", Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left), Font = new Font("Microsft Sans Serif", 12) };
            tb2.KeyDown += new KeyEventHandler(pesoBebe_KeyDown);
            panel.Controls.Add(tb2, 3, 0);
            panel.Controls.Add(new Label() { Text = "APGAR", Font = new Font("Microsft Sans Serif", 12), Anchor = (AnchorStyles.Bottom | AnchorStyles.Right) }, 0, 1);
            TextBox tb3 = new TextBox() { Name = "apgar", Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left), Font = new Font("Microsft Sans Serif", 12) };
            tb3.KeyDown += new KeyEventHandler(apgar_KeyDown);
            panel.Controls.Add(tb3, 1, 1);
            panel.Controls.Add(new Label() { Text = "Sexo Bébe", Font = new Font("Microsft Sans Serif", 12), Anchor = (AnchorStyles.Bottom | AnchorStyles.Right) }, 2, 1);
            ComboBox tb4 = new ComboBox() { Items = { "Masculino", "Feminino", "Indeterminado" }, Name = "sexoBebe", Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left), Font = new Font("Microsft Sans Serif", 12) };
            tb4.KeyDown += new KeyEventHandler(sexoBebe_Click);
            panel.Controls.Add(tb4, 3, 1);
            panel.Controls.Add(new Label() { Text = "Indicação", Font = new Font("Microsft Sans Serif", 12), Anchor = (AnchorStyles.Top | AnchorStyles.Right) }, 2, 2);
            TextBox tb5 = new TextBox() { Name = "indicacao", Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left), Font = new Font("Microsft Sans Serif", 12), Multiline = true };
            tb5.KeyDown += new KeyEventHandler(indicacao_Click);
            panel.Controls.Add(tb5, 3, 2);
            return panel;
        }

        private void indicacao_Click(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void sexoBebe_Click(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void apgar_KeyDown(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void pesoBebe_KeyDown(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void tipoParto_KeyDown(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }


        private void bebes_TextChanged(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                flowLayoutPanel.Controls.Clear();
                int i = int.Parse(bebes.Text);
                for (int o = 0; o < i; o++)
                {
                    flowLayoutPanel.Controls.Add(new Label() { Text = "Bebe " + (o + 1), Font = new Font("Microsft Sans Serif", 12) });
                    flowLayoutPanel.Controls.Add(createTL());
                }
            }
        }

        private void GravidezInfo_Load(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;

            gravNum.Text = gravidez.ToString();
            consultas.Text = consult.ToString();


            SqlDataReader reader;

            bebes.Text = new SqlCommand("select dbo.getNumBebesByNIFnGrav(" + pacNif + ", " + gravidez + ")", cn).ExecuteScalar().ToString();

            reader = new SqlCommand("exec getBebeInfoByNIFnGrav " + pacNif + ", " +gravidez, cn).ExecuteReader();
            flowLayoutPanel.Controls.Clear();
            while (reader.Read())
            {
                TableLayoutPanel tb = createTL();
                tb.GetControlFromPosition(1, 0).Text = reader["Parto"].ToString();
                tb.GetControlFromPosition(1, 1).Text = reader["APGAR"].ToString();
                tb.GetControlFromPosition(3, 0).Text = reader["PesoBebe"].ToString();
                tb.GetControlFromPosition(3, 1).Text = reader["SexoBebe"].ToString();
                tb.GetControlFromPosition(3, 2).Text = reader["Indicacao"].ToString();
                flowLayoutPanel.Controls.Add(tb);
            }

        }
    }
}
