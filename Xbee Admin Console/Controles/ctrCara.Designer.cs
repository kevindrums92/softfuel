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
            this.SFpanelResultadosVenta = new System.Windows.Forms.Panel();
            this.SFlbGalonesDesc = new System.Windows.Forms.Label();
            this.SFlbGalonesValor = new System.Windows.Forms.Label();
            this.SFlabelDineroDesc = new System.Windows.Forms.Label();
            this.SFlbDineroValor = new System.Windows.Forms.Label();
            this.SFpcImageDispensador = new System.Windows.Forms.PictureBox();
            this.SBPanelFooter = new System.Windows.Forms.Panel();
            this.SFlbNombreCara = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SFpanelResultadosVenta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SFpcImageDispensador)).BeginInit();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(155, 93);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.SFpanelResultadosVenta, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.SFpcImageDispensador, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 30);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(149, 60);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // SFpanelResultadosVenta
            // 
            this.SFpanelResultadosVenta.Controls.Add(this.SFlbGalonesDesc);
            this.SFpanelResultadosVenta.Controls.Add(this.SFlbGalonesValor);
            this.SFpanelResultadosVenta.Controls.Add(this.SFlabelDineroDesc);
            this.SFpanelResultadosVenta.Controls.Add(this.SFlbDineroValor);
            this.SFpanelResultadosVenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFpanelResultadosVenta.Location = new System.Drawing.Point(77, 3);
            this.SFpanelResultadosVenta.Name = "SFpanelResultadosVenta";
            this.SFpanelResultadosVenta.Size = new System.Drawing.Size(69, 54);
            this.SFpanelResultadosVenta.TabIndex = 1;
            // 
            // SFlbGalonesDesc
            // 
            this.SFlbGalonesDesc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SFlbGalonesDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SFlbGalonesDesc.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.SFlbGalonesDesc.Location = new System.Drawing.Point(0, 2);
            this.SFlbGalonesDesc.Name = "SFlbGalonesDesc";
            this.SFlbGalonesDesc.Size = new System.Drawing.Size(69, 13);
            this.SFlbGalonesDesc.TabIndex = 3;
            this.SFlbGalonesDesc.Text = "Gal:";
            this.SFlbGalonesDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SFlbGalonesValor
            // 
            this.SFlbGalonesValor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SFlbGalonesValor.ForeColor = System.Drawing.Color.White;
            this.SFlbGalonesValor.Location = new System.Drawing.Point(0, 15);
            this.SFlbGalonesValor.Name = "SFlbGalonesValor";
            this.SFlbGalonesValor.Size = new System.Drawing.Size(69, 13);
            this.SFlbGalonesValor.TabIndex = 2;
            this.SFlbGalonesValor.Text = "0";
            this.SFlbGalonesValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SFlabelDineroDesc
            // 
            this.SFlabelDineroDesc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SFlabelDineroDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SFlabelDineroDesc.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.SFlabelDineroDesc.Location = new System.Drawing.Point(0, 28);
            this.SFlabelDineroDesc.Name = "SFlabelDineroDesc";
            this.SFlabelDineroDesc.Size = new System.Drawing.Size(69, 13);
            this.SFlabelDineroDesc.TabIndex = 1;
            this.SFlabelDineroDesc.Text = "Precio:";
            this.SFlabelDineroDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SFlbDineroValor
            // 
            this.SFlbDineroValor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SFlbDineroValor.ForeColor = System.Drawing.Color.White;
            this.SFlbDineroValor.Location = new System.Drawing.Point(0, 41);
            this.SFlbDineroValor.Name = "SFlbDineroValor";
            this.SFlbDineroValor.Size = new System.Drawing.Size(69, 13);
            this.SFlbDineroValor.TabIndex = 0;
            this.SFlbDineroValor.Text = "$0";
            this.SFlbDineroValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SFpcImageDispensador
            // 
            this.SFpcImageDispensador.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.SFpcImageDispensador.Location = new System.Drawing.Point(12, 5);
            this.SFpcImageDispensador.Name = "SFpcImageDispensador";
            this.SFpcImageDispensador.Size = new System.Drawing.Size(49, 52);
            this.SFpcImageDispensador.TabIndex = 2;
            this.SFpcImageDispensador.TabStop = false;
            // 
            // SBPanelFooter
            // 
            this.SBPanelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.SBPanelFooter.Controls.Add(this.SFlbNombreCara);
            this.SBPanelFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SBPanelFooter.Location = new System.Drawing.Point(3, 3);
            this.SBPanelFooter.Name = "SBPanelFooter";
            this.SBPanelFooter.Size = new System.Drawing.Size(149, 21);
            this.SBPanelFooter.TabIndex = 3;
            // 
            // SFlbNombreCara
            // 
            this.SFlbNombreCara.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.SFlbNombreCara.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFlbNombreCara.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.SFlbNombreCara.ForeColor = System.Drawing.Color.White;
            this.SFlbNombreCara.Location = new System.Drawing.Point(0, 0);
            this.SFlbNombreCara.Name = "SFlbNombreCara";
            this.SFlbNombreCara.Size = new System.Drawing.Size(149, 21);
            this.SFlbNombreCara.TabIndex = 2;
            this.SFlbNombreCara.Text = "Cara";
            this.SFlbNombreCara.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ctrCara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(195)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ctrCara";
            this.Size = new System.Drawing.Size(155, 93);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.SFpanelResultadosVenta.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SFpcImageDispensador)).EndInit();
            this.SBPanelFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel SBPanelFooter;
        private System.Windows.Forms.Label SFlbNombreCara;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel SFpanelResultadosVenta;
        private System.Windows.Forms.Label SFlbGalonesDesc;
        private System.Windows.Forms.Label SFlbGalonesValor;
        private System.Windows.Forms.Label SFlabelDineroDesc;
        private System.Windows.Forms.Label SFlbDineroValor;
        private System.Windows.Forms.PictureBox SFpcImageDispensador;

    }
}
