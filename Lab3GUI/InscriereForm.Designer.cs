namespace Lab3GUI
{
    partial class InscriereForm
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
            this.dataGridProbe = new System.Windows.Forms.DataGridView();
            this.dataGridParticipanti = new System.Windows.Forms.DataGridView();
            this.textBoxNume = new System.Windows.Forms.TextBox();
            this.textBoxVarsta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelVarsta = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.checkBoxParticipantExistent = new System.Windows.Forms.CheckBox();
            this.buttonInscriere = new System.Windows.Forms.Button();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.dataGridViewProbeInscriere = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProbe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridParticipanti)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProbeInscriere)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridProbe
            // 
            this.dataGridProbe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridProbe.Location = new System.Drawing.Point(25, 23);
            this.dataGridProbe.Name = "dataGridProbe";
            this.dataGridProbe.RowHeadersVisible = false;
            this.dataGridProbe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridProbe.Size = new System.Drawing.Size(295, 188);
            this.dataGridProbe.TabIndex = 0;
            this.dataGridProbe.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridProbe_CellMouseClick);
            // 
            // dataGridParticipanti
            // 
            this.dataGridParticipanti.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridParticipanti.Location = new System.Drawing.Point(25, 241);
            this.dataGridParticipanti.Name = "dataGridParticipanti";
            this.dataGridParticipanti.RowHeadersVisible = false;
            this.dataGridParticipanti.Size = new System.Drawing.Size(394, 179);
            this.dataGridParticipanti.TabIndex = 1;
            // 
            // textBoxNume
            // 
            this.textBoxNume.Location = new System.Drawing.Point(484, 44);
            this.textBoxNume.Name = "textBoxNume";
            this.textBoxNume.Size = new System.Drawing.Size(210, 20);
            this.textBoxNume.TabIndex = 2;
            // 
            // textBoxVarsta
            // 
            this.textBoxVarsta.Location = new System.Drawing.Point(484, 77);
            this.textBoxVarsta.Name = "textBoxVarsta";
            this.textBoxVarsta.Size = new System.Drawing.Size(210, 20);
            this.textBoxVarsta.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(413, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nume";
            // 
            // labelVarsta
            // 
            this.labelVarsta.AutoSize = true;
            this.labelVarsta.Location = new System.Drawing.Point(415, 77);
            this.labelVarsta.Name = "labelVarsta";
            this.labelVarsta.Size = new System.Drawing.Size(37, 13);
            this.labelVarsta.TabIndex = 5;
            this.labelVarsta.Text = "Varsta";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(415, 128);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(35, 13);
            this.label.TabIndex = 7;
            this.label.Text = "Probe";
            // 
            // checkBoxParticipantExistent
            // 
            this.checkBoxParticipantExistent.AutoSize = true;
            this.checkBoxParticipantExistent.Location = new System.Drawing.Point(533, 280);
            this.checkBoxParticipantExistent.Name = "checkBoxParticipantExistent";
            this.checkBoxParticipantExistent.Size = new System.Drawing.Size(115, 17);
            this.checkBoxParticipantExistent.TabIndex = 8;
            this.checkBoxParticipantExistent.Text = "Participant existent";
            this.checkBoxParticipantExistent.UseVisualStyleBackColor = true;
            // 
            // buttonInscriere
            // 
            this.buttonInscriere.Location = new System.Drawing.Point(551, 326);
            this.buttonInscriere.Name = "buttonInscriere";
            this.buttonInscriere.Size = new System.Drawing.Size(75, 23);
            this.buttonInscriere.TabIndex = 9;
            this.buttonInscriere.Text = "Inscriere";
            this.buttonInscriere.UseVisualStyleBackColor = true;
            this.buttonInscriere.Click += new System.EventHandler(this.buttonInscriere_Click);
            // 
            // buttonLogout
            // 
            this.buttonLogout.Location = new System.Drawing.Point(619, 8);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(75, 23);
            this.buttonLogout.TabIndex = 10;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // dataGridViewProbeInscriere
            // 
            this.dataGridViewProbeInscriere.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProbeInscriere.Location = new System.Drawing.Point(484, 117);
            this.dataGridViewProbeInscriere.Name = "dataGridViewProbeInscriere";
            this.dataGridViewProbeInscriere.RowHeadersVisible = false;
            this.dataGridViewProbeInscriere.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewProbeInscriere.Size = new System.Drawing.Size(210, 141);
            this.dataGridViewProbeInscriere.TabIndex = 11;
            // 
            // InscriereForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 439);
            this.Controls.Add(this.dataGridViewProbeInscriere);
            this.Controls.Add(this.buttonLogout);
            this.Controls.Add(this.buttonInscriere);
            this.Controls.Add(this.checkBoxParticipantExistent);
            this.Controls.Add(this.label);
            this.Controls.Add(this.labelVarsta);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxVarsta);
            this.Controls.Add(this.textBoxNume);
            this.Controls.Add(this.dataGridParticipanti);
            this.Controls.Add(this.dataGridProbe);
            this.Name = "InscriereForm";
            this.Text = "Inscriere";
            this.Load += new System.EventHandler(this.InscriereForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProbe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridParticipanti)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProbeInscriere)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridProbe;
        private System.Windows.Forms.DataGridView dataGridParticipanti;
        private System.Windows.Forms.TextBox textBoxNume;
        private System.Windows.Forms.TextBox textBoxVarsta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelVarsta;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.CheckBox checkBoxParticipantExistent;
        private System.Windows.Forms.Button buttonInscriere;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.DataGridView dataGridViewProbeInscriere;
    }
}