namespace XbeeAdminConsole
{
    partial class ctrCara
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SFPictureBox = new System.Windows.Forms.PictureBox();
            this.SBPanelFooter = new System.Windows.Forms.Panel();
            this.SFlbNombreCara = new System.Windows.Forms.Label();
            this.SFlbNombreNodo = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SFPictureBox)).BeginInit();
            this.SBPanelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.SFPictureBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.SBPanelFooter, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.SFlbNombreNodo, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(150, 150);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // SFPictureBox
            // 
            this.SFPictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SFPictureBox.Image = global::XbeeAdminConsole.Properties.Resources.dispensador;
            this.SFPictureBox.InitialImage = null;
            this.SFPictureBox.Location = new System.Drawing.Point(48, 49);
            this.SFPictureBox.Name = "SFPictureBox";
            this.SFPictureBox.Size = new System.Drawing.Size(53, 51);
            this.SFPictureBox.TabIndex = 0;
            this.SFPictureBox.TabStop = false;
            // 
            // SBPanelFooter
            // 
            this.SBPanelFooter.Controls.Add(this.SFlbNombreCara);
            this.SBPanelFooter.Location = new System.Drawing.Point(3, 123);
            this.SBPanelFooter.Name = "SBPanelFooter";
            this.SBPanelFooter.Size = new System.Drawing.Size(144, 24);
            this.SBPanelFooter.TabIndex = 3;
            // 
            // SFlbNombreCara
            // 
            this.SFlbNombreCara.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SFlbNombreCara.AutoSize = true;
            this.SFlbNombreCara.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.SFlbNombreCara.ForeColor = System.Drawing.Color.White;
            this.SFlbNombreCara.Location = new System.Drawing.Point(3, 7);
            this.SFlbNombreCara.Name = "SFlbNombreCara";
            this.SFlbNombreCara.Size = new System.Drawing.Size(29, 13);
            this.SFlbNombreCara.TabIndex = 2;
            this.SFlbNombreCara.Text = "Cara";
            // 
            // SFlbNombreNodo
            // 
            this.SFlbNombreNodo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SFlbNombreNodo.AutoSize = true;
            this.SFlbNombreNodo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.SFlbNombreNodo.ForeColor = System.Drawing.Color.White;
            this.SFlbNombreNodo.Location = new System.Drawing.Point(3, 17);
            this.SFlbNombreNodo.Name = "SFlbNombreNodo";
            this.SFlbNombreNodo.Size = new System.Drawing.Size(94, 13);
            this.SFlbNombreNodo.TabIndex = 2;
            this.SFlbNombreNodo.Text = "DISPENSADOR 1";
            // 
            // ctrCara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(73)))), ((int)(((byte)(92)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ctrCara";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SFPictureBox)).EndInit();
            this.SBPanelFooter.ResumeLayout(false);
            this.SBPanelFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox SFPictureBox;
        private System.Windows.Forms.Label SFlbNombreNodo;
        private System.Windows.Forms.Panel SBPanelFooter;
        private System.Windows.Forms.Label SFlbNombreCara;

    }
}
