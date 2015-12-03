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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.SFPictureBox = new System.Windows.Forms.PictureBox();
            this.SBPanelFooter = new System.Windows.Forms.Panel();
            this.SFlbNombreCara = new System.Windows.Forms.Label();
            this.SFpanelResultadosVenta = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.SFlabelDineroDesc = new System.Windows.Forms.Label();
            this.SFlbGalonesValor = new System.Windows.Forms.Label();
            this.SFlbGalonesDesc = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SFPictureBox)).BeginInit();
            this.SBPanelFooter.SuspendLayout();
            this.SFpanelResultadosVenta.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.SBPanelFooter, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(125, 112);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.SFPictureBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.SFpanelResultadosVenta, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(119, 83);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // SFPictureBox
            // 
            this.SFPictureBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.SFPictureBox.InitialImage = null;
            this.SFPictureBox.Location = new System.Drawing.Point(4, 30);
            this.SFPictureBox.Name = "SFPictureBox";
            this.SFPictureBox.Size = new System.Drawing.Size(50, 50);
            this.SFPictureBox.TabIndex = 0;
            this.SFPictureBox.TabStop = false;
            this.SFPictureBox.Resize += new System.EventHandler(this.SFPictureBox_Resize);
            // 
            // SBPanelFooter
            // 
            this.SBPanelFooter.Controls.Add(this.SFlbNombreCara);
            this.SBPanelFooter.Location = new System.Drawing.Point(3, 92);
            this.SBPanelFooter.Name = "SBPanelFooter";
            this.SBPanelFooter.Size = new System.Drawing.Size(119, 17);
            this.SBPanelFooter.TabIndex = 3;
            // 
            // SFlbNombreCara
            // 
            this.SFlbNombreCara.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFlbNombreCara.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.SFlbNombreCara.ForeColor = System.Drawing.Color.White;
            this.SFlbNombreCara.Location = new System.Drawing.Point(0, 0);
            this.SFlbNombreCara.Name = "SFlbNombreCara";
            this.SFlbNombreCara.Size = new System.Drawing.Size(119, 17);
            this.SFlbNombreCara.TabIndex = 2;
            this.SFlbNombreCara.Text = "Cara";
            this.SFlbNombreCara.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SFpanelResultadosVenta
            // 
            this.SFpanelResultadosVenta.Controls.Add(this.SFlbGalonesDesc);
            this.SFpanelResultadosVenta.Controls.Add(this.SFlbGalonesValor);
            this.SFpanelResultadosVenta.Controls.Add(this.SFlabelDineroDesc);
            this.SFpanelResultadosVenta.Controls.Add(this.label1);
            this.SFpanelResultadosVenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFpanelResultadosVenta.Location = new System.Drawing.Point(62, 3);
            this.SFpanelResultadosVenta.Name = "SFpanelResultadosVenta";
            this.SFpanelResultadosVenta.Size = new System.Drawing.Size(54, 77);
            this.SFpanelResultadosVenta.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "$0";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SFlabelDineroDesc
            // 
            this.SFlabelDineroDesc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SFlabelDineroDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SFlabelDineroDesc.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.SFlabelDineroDesc.Location = new System.Drawing.Point(0, 51);
            this.SFlabelDineroDesc.Name = "SFlabelDineroDesc";
            this.SFlabelDineroDesc.Size = new System.Drawing.Size(54, 13);
            this.SFlabelDineroDesc.TabIndex = 1;
            this.SFlabelDineroDesc.Text = "Precio:";
            this.SFlabelDineroDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SFlbGalonesValor
            // 
            this.SFlbGalonesValor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SFlbGalonesValor.ForeColor = System.Drawing.Color.White;
            this.SFlbGalonesValor.Location = new System.Drawing.Point(0, 38);
            this.SFlbGalonesValor.Name = "SFlbGalonesValor";
            this.SFlbGalonesValor.Size = new System.Drawing.Size(54, 13);
            this.SFlbGalonesValor.TabIndex = 2;
            this.SFlbGalonesValor.Text = "0";
            this.SFlbGalonesValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SFlbGalonesDesc
            // 
            this.SFlbGalonesDesc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SFlbGalonesDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SFlbGalonesDesc.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.SFlbGalonesDesc.Location = new System.Drawing.Point(0, 25);
            this.SFlbGalonesDesc.Name = "SFlbGalonesDesc";
            this.SFlbGalonesDesc.Size = new System.Drawing.Size(54, 13);
            this.SFlbGalonesDesc.TabIndex = 3;
            this.SFlbGalonesDesc.Text = "Gal:";
            this.SFlbGalonesDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctrCara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(73)))), ((int)(((byte)(92)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ctrCara";
            this.Size = new System.Drawing.Size(125, 112);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SFPictureBox)).EndInit();
            this.SBPanelFooter.ResumeLayout(false);
            this.SFpanelResultadosVenta.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox SFPictureBox;
        private System.Windows.Forms.Panel SBPanelFooter;
        private System.Windows.Forms.Label SFlbNombreCara;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel SFpanelResultadosVenta;
        private System.Windows.Forms.Label SFlbGalonesDesc;
        private System.Windows.Forms.Label SFlbGalonesValor;
        private System.Windows.Forms.Label SFlabelDineroDesc;
        private System.Windows.Forms.Label label1;

    }
}
