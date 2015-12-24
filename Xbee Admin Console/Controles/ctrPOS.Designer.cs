namespace XbeeAdminConsole
{
    partial class ctrPOS
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.SFpcImagePOS = new System.Windows.Forms.PictureBox();
            this.SBPanelFooter = new System.Windows.Forms.Panel();
            this.SFlbNombrePOS = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SFpcImagePOS)).BeginInit();
            this.SBPanelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.SBPanelFooter, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(136, 122);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.SFpcImagePOS, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 39);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(130, 80);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // SFpcImagePOS
            // 
            this.SFpcImagePOS.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.SFpcImagePOS.Location = new System.Drawing.Point(26, 3);
            this.SFpcImagePOS.Name = "SFpcImagePOS";
            this.SFpcImagePOS.Size = new System.Drawing.Size(77, 74);
            this.SFpcImagePOS.TabIndex = 2;
            this.SFpcImagePOS.TabStop = false;
            // 
            // SBPanelFooter
            // 
            this.SBPanelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.SBPanelFooter.Controls.Add(this.SFlbNombrePOS);
            this.SBPanelFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SBPanelFooter.Location = new System.Drawing.Point(3, 3);
            this.SBPanelFooter.Name = "SBPanelFooter";
            this.SBPanelFooter.Size = new System.Drawing.Size(130, 30);
            this.SBPanelFooter.TabIndex = 3;
            // 
            // SFlbNombrePOS
            // 
            this.SFlbNombrePOS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFlbNombrePOS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.SFlbNombrePOS.ForeColor = System.Drawing.Color.White;
            this.SFlbNombrePOS.Location = new System.Drawing.Point(0, 0);
            this.SFlbNombrePOS.Name = "SFlbNombrePOS";
            this.SFlbNombrePOS.Size = new System.Drawing.Size(130, 30);
            this.SFlbNombrePOS.TabIndex = 2;
            this.SFlbNombrePOS.Text = "POS";
            this.SFlbNombrePOS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ctrPOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(73)))), ((int)(((byte)(92)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ctrPOS";
            this.Size = new System.Drawing.Size(136, 122);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SFpcImagePOS)).EndInit();
            this.SBPanelFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel SBPanelFooter;
        private System.Windows.Forms.Label SFlbNombrePOS;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox SFpcImagePOS;

    }
}
