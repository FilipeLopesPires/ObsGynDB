namespace BD_Project
{
    partial class Pacientes
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
            this.listaPacientes = new System.Windows.Forms.ListBox();
            this.Adicionar = new System.Windows.Forms.Button();
            this.Editar = new System.Windows.Forms.Button();
            this.Remover = new System.Windows.Forms.Button();
            this.pac = new System.Windows.Forms.TextBox();
            this.Paciente = new System.Windows.Forms.Label();
            this.HistorialClinico = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.updatePacientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.médicoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reqButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listaPacientes
            // 
            this.listaPacientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listaPacientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listaPacientes.FormattingEnabled = true;
            this.listaPacientes.ItemHeight = 20;
            this.listaPacientes.Location = new System.Drawing.Point(13, 65);
            this.listaPacientes.Name = "listaPacientes";
            this.listaPacientes.Size = new System.Drawing.Size(531, 264);
            this.listaPacientes.TabIndex = 0;
            this.listaPacientes.DoubleClick += new System.EventHandler(this.Editar_Click);
            // 
            // Adicionar
            // 
            this.Adicionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Adicionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Adicionar.Location = new System.Drawing.Point(562, 39);
            this.Adicionar.Name = "Adicionar";
            this.Adicionar.Size = new System.Drawing.Size(92, 30);
            this.Adicionar.TabIndex = 1;
            this.Adicionar.Text = "Adicionar";
            this.Adicionar.UseVisualStyleBackColor = true;
            this.Adicionar.Click += new System.EventHandler(this.Adicionar_Click);
            // 
            // Editar
            // 
            this.Editar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Editar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Editar.Location = new System.Drawing.Point(562, 75);
            this.Editar.Name = "Editar";
            this.Editar.Size = new System.Drawing.Size(92, 30);
            this.Editar.TabIndex = 2;
            this.Editar.Text = "Editar";
            this.Editar.UseVisualStyleBackColor = true;
            this.Editar.Click += new System.EventHandler(this.Editar_Click);
            // 
            // Remover
            // 
            this.Remover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Remover.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Remover.Location = new System.Drawing.Point(562, 111);
            this.Remover.Name = "Remover";
            this.Remover.Size = new System.Drawing.Size(92, 30);
            this.Remover.TabIndex = 3;
            this.Remover.Text = "Remover";
            this.Remover.UseVisualStyleBackColor = true;
            this.Remover.Click += new System.EventHandler(this.Remover_Click);
            // 
            // pac
            // 
            this.pac.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pac.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pac.Location = new System.Drawing.Point(87, 36);
            this.pac.Name = "pac";
            this.pac.Size = new System.Drawing.Size(457, 26);
            this.pac.TabIndex = 5;
            this.pac.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pac_KeyDown);
            // 
            // Paciente
            // 
            this.Paciente.AutoSize = true;
            this.Paciente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Paciente.Location = new System.Drawing.Point(12, 39);
            this.Paciente.Name = "Paciente";
            this.Paciente.Size = new System.Drawing.Size(79, 20);
            this.Paciente.TabIndex = 6;
            this.Paciente.Text = "Paciente: ";
            // 
            // HistorialClinico
            // 
            this.HistorialClinico.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.HistorialClinico.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HistorialClinico.Location = new System.Drawing.Point(562, 147);
            this.HistorialClinico.Name = "HistorialClinico";
            this.HistorialClinico.Size = new System.Drawing.Size(92, 30);
            this.HistorialClinico.TabIndex = 7;
            this.HistorialClinico.Text = "Historial Clínico";
            this.HistorialClinico.UseVisualStyleBackColor = true;
            this.HistorialClinico.Click += new System.EventHandler(this.HistorialClinico_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(15, 9);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(73, 25);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updatePacientesToolStripMenuItem,
            this.médicoToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(65, 21);
            this.toolStripMenuItem1.Text = "Opções";
            // 
            // updatePacientesToolStripMenuItem
            // 
            this.updatePacientesToolStripMenuItem.Name = "updatePacientesToolStripMenuItem";
            this.updatePacientesToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.updatePacientesToolStripMenuItem.Text = "Update Pacientes";
            this.updatePacientesToolStripMenuItem.Click += new System.EventHandler(this.loadAll);
            // 
            // médicoToolStripMenuItem
            // 
            this.médicoToolStripMenuItem.Name = "médicoToolStripMenuItem";
            this.médicoToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.médicoToolStripMenuItem.Text = "Médico";
            this.médicoToolStripMenuItem.Click += new System.EventHandler(this.Medico_Click);
            // 
            // reqButton
            // 
            this.reqButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.reqButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reqButton.Location = new System.Drawing.Point(562, 280);
            this.reqButton.Name = "reqButton";
            this.reqButton.Size = new System.Drawing.Size(92, 49);
            this.reqButton.TabIndex = 10;
            this.reqButton.Text = "Requisitar Análise";
            this.reqButton.UseVisualStyleBackColor = true;
            this.reqButton.Click += new System.EventHandler(this.reqButton_Click);
            // 
            // Pacientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(673, 347);
            this.Controls.Add(this.reqButton);
            this.Controls.Add(this.pac);
            this.Controls.Add(this.HistorialClinico);
            this.Controls.Add(this.Paciente);
            this.Controls.Add(this.Remover);
            this.Controls.Add(this.Editar);
            this.Controls.Add(this.Adicionar);
            this.Controls.Add(this.listaPacientes);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Pacientes";
            this.Text = "Pacientes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listaPacientes;
        private System.Windows.Forms.Button Adicionar;
        private System.Windows.Forms.Button Editar;
        private System.Windows.Forms.Button Remover;
        private System.Windows.Forms.TextBox pac;
        private System.Windows.Forms.Label Paciente;
        private System.Windows.Forms.Button HistorialClinico;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem updatePacientesToolStripMenuItem;
        private System.Windows.Forms.Button reqButton;
        private System.Windows.Forms.ToolStripMenuItem médicoToolStripMenuItem;
    }
}

