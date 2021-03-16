namespace BD_Project
{
    partial class GravidezInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.gravNum = new System.Windows.Forms.TextBox();
            this.home = new System.Windows.Forms.Button();
            this.bebes = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.consultas = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gravidez";
            // 
            // gravNum
            // 
            this.gravNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gravNum.Location = new System.Drawing.Point(141, 24);
            this.gravNum.Name = "gravNum";
            this.gravNum.Size = new System.Drawing.Size(55, 26);
            this.gravNum.TabIndex = 1;
            // 
            // home
            // 
            this.home.Location = new System.Drawing.Point(895, 18);
            this.home.Name = "home";
            this.home.Size = new System.Drawing.Size(92, 41);
            this.home.TabIndex = 2;
            this.home.Text = "Home";
            this.home.UseVisualStyleBackColor = true;
            this.home.Click += new System.EventHandler(this.home_Click);
            // 
            // bebes
            // 
            this.bebes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bebes.Location = new System.Drawing.Point(150, 89);
            this.bebes.Name = "bebes";
            this.bebes.Size = new System.Drawing.Size(82, 26);
            this.bebes.TabIndex = 43;
            this.bebes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bebes_TextChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(25, 92);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(119, 20);
            this.label27.TabIndex = 44;
            this.label27.Text = "Número Bebés:";
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.Location = new System.Drawing.Point(29, 140);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(958, 366);
            this.flowLayoutPanel.TabIndex = 23;
            this.flowLayoutPanel.WrapContents = false;
            // 
            // consultas
            // 
            this.consultas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consultas.Location = new System.Drawing.Point(517, 24);
            this.consultas.Name = "consultas";
            this.consultas.Size = new System.Drawing.Size(82, 26);
            this.consultas.TabIndex = 45;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(367, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 20);
            this.label2.TabIndex = 46;
            this.label2.Text = "Número Consultas:";
            // 
            // GravidezInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 537);
            this.Controls.Add(this.consultas);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bebes);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.home);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.gravNum);
            this.Controls.Add(this.label1);
            this.Name = "GravidezInfo";
            this.Text = "GravidezInfo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.GravidezInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox gravNum;
        private System.Windows.Forms.Button home;
        private System.Windows.Forms.TextBox bebes;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.TextBox consultas;
        private System.Windows.Forms.Label label2;
    }
}