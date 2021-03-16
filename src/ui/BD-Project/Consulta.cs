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
    public partial class Consulta : Form
    {
        Dictionary<string, int> meses = new Dictionary<string, int>() { { "janeiro", 1 }, { "fevereiro", 2 }, { "março", 3 }, { "abril", 4 }, { "maio", 5 }, { "junho", 6 }, { "julho", 7 }, { "agosto", 8 }, { "setembro", 9 }, { "outubro", 10 }, { "novembro", 11 }, { "dezembro", 12 } };
        private int idConsulta=-1;
        private int nifPac = -1;
        private SqlConnection cn;

        public Consulta()
        {
            InitializeComponent();

        }

        public Consulta(int c)
        {
            InitializeComponent();
            nifPac = c;
        }

        public Consulta(int c, int a)
        {
            InitializeComponent();
            nifPac = c;
            idConsulta = a;
        }

        private void conClick(object sender, EventArgs e)
        {
            allConsOff();
            RadioButton rb = (RadioButton)sender;

            switch (rb.Name)
            {
                case "ginec":
                    this.Controls.Find("conGinec", true)[0].BringToFront();
                    this.Controls.Find("conGinec", true)[0].Show();
                    break;
                case "obst":
                    this.Controls.Find("conObs", true)[0].BringToFront();
                    this.Controls.Find("conObs", true)[0].Show();
                    break;
            }
        }

        private void allConsOff()
        {
            this.Controls.Find("conGinec", true)[0].Hide();
            this.Controls.Find("conObs", true)[0].Hide();
        }

        private void obsClick(object sender, EventArgs e)
        {
            allObsOff();
            RadioButton rb = (RadioButton)sender;

            switch (rb.Name)
            {
                case "obstNormal":
                    this.Controls.Find("normal", true)[0].BringToFront();
                    this.Controls.Find("normal", true)[0].Show();
                    break;
                case "obstParto":
                    this.Controls.Find("parto", true)[0].BringToFront();
                    this.Controls.Find("parto", true)[0].Show();
                    break;
                case "obstPuer":
                    this.Controls.Find("puerperio", true)[0].BringToFront();
                    this.Controls.Find("puerperio", true)[0].Show();
                    break;
            }
        }

        private void allObsOff()
        {
            this.Controls.Find("normal", true)[0].Hide();
            this.Controls.Find("parto", true)[0].Hide();
            this.Controls.Find("puerperio", true)[0].Hide();
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form sistema = new Pacientes();
            sistema.ShowDialog();
            this.Close();
        }

        private void analiseButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form sistema = new DescricaoAnalise(-1, idConsulta);
            sistema.ShowDialog();
            this.Close();
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
            ComboBox tb1 = new ComboBox() { Items = { "Eutocico" , "Ventosa" , "Forceps" , "Cesariana" , "Pelvico" }, Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left), Font = new Font("Microsft Sans Serif", 12) };
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
            ComboBox tb4 = new ComboBox() { Items = { "Masculino","Feminino","Indeterminado"}, Name = "sexoBebe", Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left), Font = new Font("Microsft Sans Serif", 12) };
            tb4.KeyDown += new KeyEventHandler(sexoBebe_Click);
            panel.Controls.Add(tb4, 3, 1);
            panel.Controls.Add(new Label() { Text = "Indicação", Font = new Font("Microsft Sans Serif", 12), Anchor = (AnchorStyles.Top | AnchorStyles.Right) }, 2, 2);
            TextBox tb5 = new TextBox() { Name = "indicacao", Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left), Font = new Font("Microsft Sans Serif", 12), Multiline = true };
            tb5.KeyDown += new KeyEventHandler(indicacao_Click);
            panel.Controls.Add(tb5, 3, 2);
            return panel;
        }

        //para inserçoes
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
            if (e.KeyCode == Keys.Enter)
            {
                Console.Write(((TextBox)sender).Text);
            }
        }
        //ate aqui

        private void bebes_TextChanged(object sender, KeyEventArgs e)
        {
            if (idConsulta == -1)
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
        }

        private void Consulta_Load(object sender, EventArgs e)
        {

            cn = getSGBDConnection();

            if (nifPac != -1)
            {
                if (!verifySGBDConnection())
                    return;

                SqlDataReader reader;

                normalIg.Text = new SqlCommand("select dbo.IG(" + nifPac + ")", cn).ExecuteScalar().ToString();

                reader = new SqlCommand("select * from dbo.ecoObsPac(" + nifPac + ")", cn).ExecuteReader();

                EcografiaObs eo;
                normalIGEco.Items.Clear();
                while (reader.Read())
                {
                    eo = new EcografiaObs();
                    eo.Id = int.Parse(reader["IDDA"].ToString());
                    eo.Data = reader["DataE"].ToString();

                    normalIGEco.Items.Add(eo);
                }

                cn.Close();
            }

            if (idConsulta != -1)
            {
                if (!verifySGBDConnection())
                    return;

                SqlCommand cmd = new SqlCommand("select dbo.tipoConsulta("+idConsulta+")", cn);
                if (((int)cmd.ExecuteScalar()) == 0)
                {
                    ginec.Checked = true;
                    conGinec.BringToFront();
                    conGinec.Show();
                    ConsultaGinec cg = new ConsultaGinec();
                    SqlDataReader reader = new SqlCommand("select * from dbo.searchConGinByID(" + idConsulta+")", cn).ExecuteReader();

                    while (reader.Read())
                    {
                        cg.ID = int.Parse(reader["ID"].ToString());
                        cg.Descricao = reader["Descricao"].ToString();
                        cg.Data = reader["DataC"].ToString();
                        cg.Hora = Convert.ToDouble(reader["Hora"].ToString());
                    }

                    cn.Close();

                    descricao.Text = cg.Descricao;
                    data.Text = cg.Data;
                    hora.Text = cg.Hora.ToString();

                }
                else
                {
                    obst.Checked = true;
                    conObs.BringToFront();
                    conObs.Show();
                    ConsultaObst co = new ConsultaObst();
                    SqlDataReader reader = new SqlCommand("select * from dbo.searchConObsByID(" + idConsulta + ")", cn).ExecuteReader();

                    while (reader.Read())
                    {
                        co.ID = int.Parse(reader["ID"].ToString());
                        co.GravidezPac = int.Parse(reader["GravidezPac"].ToString());
                        co.NumeroGravidez= int.Parse(reader["GravidezNum"].ToString());
                        co.Tipo = reader["Tipo"].ToString();
                        co.AFU = (reader["AFU"]==DBNull.Value)? -1:int.Parse(reader["AFU"].ToString());
                        co.MAF = (reader["MAF"]==DBNull.Value)?false : bool.Parse(reader["MAF"].ToString());
                        co.Foco = (reader["Foco"] == DBNull.Value) ? false : bool.Parse(reader["Foco"].ToString());
                        co.Peso = double.Parse(reader["Peso"].ToString());
                        co.TA = reader["TA"].ToString();
                        co.ExGin = reader["ExGin"].ToString();
                        co.Queixas = reader["Queixas"].ToString();
                        co.Medicacao = reader["Medicacao"].ToString();
                        co.Obs = reader["Obs"].ToString();
                        co.PuerAmamentacao = reader["PuerAmamentacao"].ToString();
                        co.PuerMenstruacao = reader["PuerMenstruacao"].ToString();
                        co.PuerObs = reader["PuerObservacoes"].ToString();
                        co.Data = reader["DataC"].ToString();
                        co.Hora = Convert.ToDouble(reader["Hora"].ToString());
                    }

                    reader.Close();
                    

                    switch (co.Tipo)
                    {
                        case "Normal":
                            obstNormal.Checked = true;
                            normal.BringToFront();
                            normal.Show();

                            normalNumGrav.Text=co.NumeroGravidez.ToString();
                            normalGrav.Text = co.NumeroGravidez.ToString();
                            gravidez.Text = co.NumeroGravidez.ToString();
                            normalAfu.Text = co.AFU.ToString();
                            normalMaf.Text = co.MAF.ToString();
                            normalFoco.Text = co.Foco.ToString();
                            normalPeso.Text = co.Peso.ToString();
                            normalTa.Text = co.TA;
                            normalExgin.Text = co.ExGin;
                            normalQueixas.Text = co.Queixas;
                            normalMedicacao.Text = co.Medicacao;
                            normalObs.Text = co.Obs;
                            data.Text = co.Data;
                            hora.Text = co.Hora.ToString();


                            break;
                        case "Parto":
                            obstParto.Checked = true;
                            parto.BringToFront();
                            parto.Show();

                            data.Text = co.Data;
                            hora.Text = co.Hora.ToString();
                            gravidez.Text = co.NumeroGravidez.ToString();

                            bebes.Text = new SqlCommand("select dbo.getNumBebesByNIFnGrav(" + nifPac + ", " + co.NumeroGravidez.ToString() + ")", cn).ExecuteScalar().ToString();

                            reader.Close();
                            reader = new SqlCommand("exec getBebeInfoByNIFnGrav "+ nifPac + ", " + co.NumeroGravidez.ToString(), cn).ExecuteReader();
                            flowLayoutPanel.Controls.Clear();
                            while (reader.Read())
                            {
                                TableLayoutPanel tb = createTL();
                                tb.GetControlFromPosition(1,0).Text = reader["Parto"].ToString();
                                tb.GetControlFromPosition(1,1).Text = reader["APGAR"].ToString();
                                tb.GetControlFromPosition(3,0).Text = reader["PesoBebe"].ToString();
                                tb.GetControlFromPosition(3,1).Text = reader["SexoBebe"].ToString();
                                tb.GetControlFromPosition(3,2).Text = reader["Indicacao"].ToString();
                                flowLayoutPanel.Controls.Add(tb);
                            }
                            

                            break;
                        case "Puer":
                            obstPuer.Checked = true;
                            puerperio.BringToFront();
                            puerperio.Show();
                            puerperioAmam.Text= co.PuerAmamentacao;
                            puerperioMens.Text = co.PuerMenstruacao;
                            puerperioObs.Text = co.PuerObs;
                            data.Text = co.Data;
                            hora.Text = co.Hora.ToString();
                            gravidez.Text = co.NumeroGravidez.ToString();
                            break;
                    }

                    cn.Close();
                }



            }
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
            Form sistema;
            if (nifPac == -1)
            {
                MessageBox.Show("Nenhum paciente associado");
                this.Hide();
                sistema = new Pacientes();
                sistema.ShowDialog();
                this.Close(); 
                return;
            }

            if (idConsulta == -1)
            {
                int rows = 0;

                if (!verifySGBDConnection())
                    return;

                SqlCommand cmd = new SqlCommand("select dbo.maxConsulta()", cn);

                int esteID = ((int)cmd.ExecuteScalar()) + 1;
                idConsulta = esteID;

                cmd = new SqlCommand();

                cmd.CommandText = "insert into ProjetoConsultorio.Consulta values( @ID, @DataC, @Hora, @NIFPaciente)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID", esteID);
                string[] datadb = Regex.Split(data.Text, "de");
                cmd.Parameters.AddWithValue("@DataC", datadb[2] + "-" + meses[datadb[1].ToLower().Trim()] + "-" + datadb[0]);
                cmd.Parameters.AddWithValue("@Hora", (string.IsNullOrEmpty(hora.Text))? DateTime.Now.Hour:int.Parse(hora.Text));
                cmd.Parameters.AddWithValue("@NIFPaciente", nifPac);
                cmd.Connection = cn;

                try
                {
                    rows = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to insert consulta in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                }

                if (ginec.Checked == true)
                {


                    cmd = new SqlCommand();

                    cmd.CommandText = "insert into ProjetoConsultorio.ConsultaGinecologia values( @ID, @Descricao)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", esteID);
                    cmd.Parameters.AddWithValue("@Descricao", descricao.Text);
                    cmd.Connection = cn;

                    try
                    {
                        rows = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to insert consultaGinecologia in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }


                }
                else if (obst.Checked == true)
                {
                    string tipo="Normal";
                    if (obstNormal.Checked == true)
                    {
                        tipo = "Normal";
                    }
                    else if (obstParto.Checked == true)
                    {
                        tipo = "Parto";
                    }
                    else if (obstPuer.Checked == true)
                    {
                        tipo = "Puerperio";
                    }

                    if(((int)new SqlCommand("select dbo.gravidezPacienteExists("+nifPac+","+ int.Parse(gravidez.Text) + ")", cn).ExecuteScalar())==0)
                    {
                        cmd = new SqlCommand();
                        cmd.CommandText = "insert into ProjetoConsultorio.Gravidez values( @NIFPac, @NumeroGravidez, @NumeroConsultas)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@NIFPac", nifPac);
                        cmd.Parameters.AddWithValue("@NumeroGravidez", int.Parse(gravidez.Text));
                        cmd.Parameters.AddWithValue("@NumeroConsultas", 1);
                        cmd.Connection = cn;
                        try
                        {
                            rows = cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Failed to insert gravidez in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                        }
                    }else
                    {
                        cmd = new SqlCommand();
                        cmd.CommandText = "update ProjetoConsultorio.Gravidez set NumeroConsultas=@NumeroConsultas where NIFPac=@NIFPac";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@NIFPac", nifPac);
                        cmd.Parameters.AddWithValue("@NumeroConsultas", ((int)new SqlCommand("select dbo.numConsultasGravidez(" + nifPac + "," + int.Parse(gravidez.Text) + ")", cn).ExecuteScalar())+1);
                        cmd.Connection = cn;
                        try
                        {
                            rows = cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Failed to insert gravidez in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                        }
                    }


                    cmd = new SqlCommand();

                    cmd.CommandText = "insert into ProjetoConsultorio.ConsultaObstetricia values( @ID, @GravidezPac, @GravidezNum, @Tipo, @AFU, @MAF, @Foco, @Peso, @TA, @ExGin, @Queixas, @Medicacao, @Obs, @PuerAmamentacao, @PuerMenstruacao, @PuerObservacoes)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", esteID);
                    cmd.Parameters.AddWithValue("@GravidezPac", nifPac);
                    cmd.Parameters.AddWithValue("@GravidezNum", int.Parse(gravidez.Text));
                    cmd.Parameters.AddWithValue("@Tipo", tipo);
                    cmd.Parameters.AddWithValue("@AFU", (normalAfu.Text.Equals(""))?-1:int.Parse(normalAfu.Text));
                    cmd.Parameters.AddWithValue("@MAF", (normalMaf.Text).Equals("true", StringComparison.OrdinalIgnoreCase)?1:0);
                    cmd.Parameters.AddWithValue("@Foco", (normalFoco.Text).Equals("true", StringComparison.OrdinalIgnoreCase) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Peso", (normalPeso.Text.Equals("")) ? -1 : double.Parse(normalPeso.Text));
                    cmd.Parameters.AddWithValue("@TA", normalTa.Text);
                    cmd.Parameters.AddWithValue("@ExGin", normalExgin.Text);
                    cmd.Parameters.AddWithValue("@Queixas", normalQueixas.Text);
                    cmd.Parameters.AddWithValue("@Medicacao", normalMedicacao.Text);
                    cmd.Parameters.AddWithValue("@Obs", normalObs.Text);
                    cmd.Parameters.AddWithValue("@PuerAmamentacao", puerperioAmam.Text);
                    cmd.Parameters.AddWithValue("@PuerMenstruacao", puerperioMens.Text);
                    cmd.Parameters.AddWithValue("@PuerObservacoes", puerperioObs.Text);
                    cmd.Connection = cn;

                    try
                    {
                        rows = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to insert consultaGinecologia in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }

                }
            }
            else if (idConsulta != -1)
            {
                int rows = 0;

                if (!verifySGBDConnection())
                    return;

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "update ProjetoConsultorio.Consulta set ID= @ID,DataC= @DataC,Hora= @Hora,NIFPaciente= @NIFPaciente where ID=@ID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID", idConsulta);
                string[] datadb = Regex.Split(data.Text, "de");
                cmd.Parameters.AddWithValue("@DataC", datadb[2] + "-" + meses[datadb[1].ToLower().Trim()] + "-" + datadb[0]);
                cmd.Parameters.AddWithValue("@Hora", (string.IsNullOrEmpty(hora.Text)) ? DateTime.Now.Hour : int.Parse(hora.Text));
                cmd.Parameters.AddWithValue("@NIFPaciente", nifPac);
                cmd.Connection = cn;

                try
                {
                    rows = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to update consulta in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                }

                if (ginec.Checked == true)
                {

                    cmd = new SqlCommand();

                    cmd.CommandText = "update ProjetoConsultorio.ConsultaGinecologia set ID= @ID, Descricao=@Descricao where ID=@ID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", idConsulta);
                    cmd.Parameters.AddWithValue("@Descricao", descricao.Text);
                    cmd.Connection = cn;

                    try
                    {
                        rows = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to insert consultaGinecologia in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }


                }
                else if (obst.Checked == true)
                {

                    if (((int)new SqlCommand("select dbo.gravidezPacienteExists(" + nifPac + "," + int.Parse(gravidez.Text) + ")", cn).ExecuteScalar()) == 0)
                    {
                        cmd = new SqlCommand();
                        cmd.CommandText = "insert into ProjetoConsultorio.Gravidez values( @NIFPac, @NumeroGravidez, @NumeroConsultas)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@NIFPac", nifPac);
                        cmd.Parameters.AddWithValue("@NumeroGravidez", int.Parse(gravidez.Text));
                        cmd.Parameters.AddWithValue("@NumeroConsultas", 1);
                        cmd.Connection = cn;
                        try
                        {
                            rows = cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Failed to insert gravidez in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                        }
                    }

                    string tipo = "Normal";
                    if (obstNormal.Checked == true)
                    {
                        tipo = "Normal";
                    }
                    else if (obstParto.Checked == true)
                    {
                        tipo = "Parto";
                    }
                    else if (obstPuer.Checked == true)
                    {
                        tipo = "Puerperio";
                    }


                    cmd = new SqlCommand();

                    cmd.CommandText = "update ProjetoConsultorio.ConsultaObstetricia set ID=@ID, GravidezPac=@GravidezPac, GravidezNum=@GravidezNum, Tipo=@Tipo, AFU=@AFU, MAF=@MAF, Foco=@Foco, Peso=@Peso,TA= @TA, ExGin=@ExGin, Queixas=@Queixas,Medicacao= @Medicacao,Obs= @Obs, PuerAmamentacao=@PuerAmamentacao, PuerMenstruacao=@PuerMenstruacao, PuerObservacoes=@PuerObservacoes where ID=@ID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", idConsulta);
                    cmd.Parameters.AddWithValue("@GravidezPac", nifPac);
                    cmd.Parameters.AddWithValue("@GravidezNum", int.Parse(gravidez.Text));
                    cmd.Parameters.AddWithValue("@Tipo", tipo);
                    cmd.Parameters.AddWithValue("@AFU", (normalAfu.Text.Equals("")) ? -1 : int.Parse(normalAfu.Text));
                    cmd.Parameters.AddWithValue("@MAF", (normalMaf.Text).Equals("true", StringComparison.OrdinalIgnoreCase) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Foco", (normalFoco.Text).Equals("true", StringComparison.OrdinalIgnoreCase) ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Peso", (normalPeso.Text.Equals("")) ? -1 : double.Parse(normalPeso.Text));
                    cmd.Parameters.AddWithValue("@TA", normalTa.Text);
                    cmd.Parameters.AddWithValue("@ExGin", normalExgin.Text);
                    cmd.Parameters.AddWithValue("@Queixas", normalQueixas.Text);
                    cmd.Parameters.AddWithValue("@Medicacao", normalMedicacao.Text);
                    cmd.Parameters.AddWithValue("@Obs", normalObs.Text);
                    cmd.Parameters.AddWithValue("@PuerAmamentacao", puerperioAmam.Text);
                    cmd.Parameters.AddWithValue("@PuerMenstruacao", puerperioMens.Text);
                    cmd.Parameters.AddWithValue("@PuerObservacoes", puerperioObs.Text);
                    cmd.Connection = cn;

                    try
                    {
                        rows = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to insert consultaGinecologia in database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                    }


                }
            }

            this.Hide();
            sistema = new Pacientes();
            sistema.ShowDialog();
            this.Close();

        }

        private void remover_Click(object sender, EventArgs e)
        {
            if (idConsulta != -1)
            {
                if (!verifySGBDConnection())
                    return;

                int tipo;

                if (ginec.Checked == true)
                {
                    tipo = 0;
                }
                else
                {
                    tipo = 1;
                }

                SqlCommand cmd = new SqlCommand("exec deleteConProcedure " + tipo + ", " + idConsulta, cn);

                try { int rows = cmd.ExecuteNonQuery(); }
                catch (Exception ex)
                {
                    throw new Exception("Failed to delete consulta of database. \n ERROR MESSAGE: \n" + ex.Message.ToString());
                }


                this.Hide();
                Form sistema = new Pacientes();
                sistema.ShowDialog();
                this.Close();
            }
        }

        private void normalIGEco_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;

            string s = new SqlCommand("select dbo.IGEco(" + ((EcografiaObs)normalIGEco.SelectedItem).Id + ")", cn).ExecuteScalar().ToString();
            BeginInvoke((MethodInvoker)delegate { normalIGEco.Text = s; });
        }
    }
}
