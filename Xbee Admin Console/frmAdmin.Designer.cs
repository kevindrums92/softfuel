namespace XbeeAdminConsole
{
    partial class frmAdmin
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdmin));
            this.SFbtnBarraTitulo = new System.Windows.Forms.Panel();
            this.SFLogo = new System.Windows.Forms.PictureBox();
            this.SFbtnMinimizar = new System.Windows.Forms.Button();
            this.SFbtnMaximizar = new System.Windows.Forms.Button();
            this.SFLabelNombreSofstware = new System.Windows.Forms.Label();
            this.SFbtnCerrar = new System.Windows.Forms.Button();
            this.LayoutPanelPorcentajesCantidades = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.SFpgExtra = new XbeeAdminConsole.Controles.VerticalProgressBar();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.SFpgCorriente = new XbeeAdminConsole.Controles.VerticalProgressBar();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.SFpgACPM = new XbeeAdminConsole.Controles.VerticalProgressBar();
            this.SFPanelLog = new System.Windows.Forms.Panel();
            this.SFbtnBuscar = new System.Windows.Forms.Button();
            this.SFtxtBuscar = new System.Windows.Forms.TextBox();
            this.SFGridLog = new System.Windows.Forms.DataGridView();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mensaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dispositivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SFbtnMaximizarMinimizar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SFVersion = new System.Windows.Forms.Label();
            this.SFlbHora = new System.Windows.Forms.Label();
            this.SFlbFechaHora = new System.Windows.Forms.Label();
            this.TimerFecha = new System.Windows.Forms.Timer(this.components);
            this.SFbwCambioPrecio = new System.ComponentModel.BackgroundWorker();
            this.SFTimerCambioPrecios = new System.Windows.Forms.Timer(this.components);
            this.SFbwConsultaPorcentajesGasolina = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.TypDispensadoresRejilla = new System.Windows.Forms.TableLayoutPanel();
            this.TsPanelContainerDispensadores = new System.Windows.Forms.Panel();
            this.FloatPanelDispositivos = new System.Windows.Forms.FlowLayoutPanel();
            this.bwEstadoXamp = new System.ComponentModel.BackgroundWorker();
            this.SFbtnBarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SFLogo)).BeginInit();
            this.LayoutPanelPorcentajesCantidades.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SFPanelLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SFGridLog)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.TypDispensadoresRejilla.SuspendLayout();
            this.TsPanelContainerDispensadores.SuspendLayout();
            this.SuspendLayout();
            // 
            // SFbtnBarraTitulo
            // 
            this.SFbtnBarraTitulo.BackColor = System.Drawing.Color.Transparent;
            this.SFbtnBarraTitulo.Controls.Add(this.SFLogo);
            this.SFbtnBarraTitulo.Controls.Add(this.SFbtnMinimizar);
            this.SFbtnBarraTitulo.Controls.Add(this.SFbtnMaximizar);
            this.SFbtnBarraTitulo.Controls.Add(this.SFLabelNombreSofstware);
            this.SFbtnBarraTitulo.Controls.Add(this.SFbtnCerrar);
            this.SFbtnBarraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.SFbtnBarraTitulo.Location = new System.Drawing.Point(3, 3);
            this.SFbtnBarraTitulo.Name = "SFbtnBarraTitulo";
            this.SFbtnBarraTitulo.Size = new System.Drawing.Size(986, 39);
            this.SFbtnBarraTitulo.TabIndex = 2;
            this.SFbtnBarraTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.SFbtnBarraTitulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.SFbtnBarraTitulo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            // 
            // SFLogo
            // 
            this.SFLogo.Location = new System.Drawing.Point(11, 0);
            this.SFLogo.Name = "SFLogo";
            this.SFLogo.Size = new System.Drawing.Size(92, 39);
            this.SFLogo.TabIndex = 6;
            this.SFLogo.TabStop = false;
            // 
            // SFbtnMinimizar
            // 
            this.SFbtnMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SFbtnMinimizar.FlatAppearance.BorderSize = 0;
            this.SFbtnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SFbtnMinimizar.ForeColor = System.Drawing.Color.White;
            this.SFbtnMinimizar.Location = new System.Drawing.Point(868, 1);
            this.SFbtnMinimizar.Name = "SFbtnMinimizar";
            this.SFbtnMinimizar.Size = new System.Drawing.Size(40, 39);
            this.SFbtnMinimizar.TabIndex = 5;
            this.SFbtnMinimizar.Text = "-";
            this.SFbtnMinimizar.UseVisualStyleBackColor = true;
            this.SFbtnMinimizar.Click += new System.EventHandler(this.SFbtnMinimizar_Click);
            // 
            // SFbtnMaximizar
            // 
            this.SFbtnMaximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SFbtnMaximizar.FlatAppearance.BorderSize = 0;
            this.SFbtnMaximizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SFbtnMaximizar.ForeColor = System.Drawing.Color.White;
            this.SFbtnMaximizar.Location = new System.Drawing.Point(907, 1);
            this.SFbtnMaximizar.Name = "SFbtnMaximizar";
            this.SFbtnMaximizar.Size = new System.Drawing.Size(40, 39);
            this.SFbtnMaximizar.TabIndex = 4;
            this.SFbtnMaximizar.Text = "M";
            this.SFbtnMaximizar.UseVisualStyleBackColor = true;
            this.SFbtnMaximizar.Click += new System.EventHandler(this.SFbtnMaximizar_Click);
            // 
            // SFLabelNombreSofstware
            // 
            this.SFLabelNombreSofstware.AutoSize = true;
            this.SFLabelNombreSofstware.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.SFLabelNombreSofstware.ForeColor = System.Drawing.Color.White;
            this.SFLabelNombreSofstware.Location = new System.Drawing.Point(109, 9);
            this.SFLabelNombreSofstware.Name = "SFLabelNombreSofstware";
            this.SFLabelNombreSofstware.Size = new System.Drawing.Size(157, 20);
            this.SFLabelNombreSofstware.TabIndex = 3;
            this.SFLabelNombreSofstware.Text = "Console Application";
            this.SFLabelNombreSofstware.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.SFLabelNombreSofstware.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.SFLabelNombreSofstware.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            // 
            // SFbtnCerrar
            // 
            this.SFbtnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SFbtnCerrar.FlatAppearance.BorderSize = 0;
            this.SFbtnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SFbtnCerrar.ForeColor = System.Drawing.Color.White;
            this.SFbtnCerrar.Location = new System.Drawing.Point(946, 1);
            this.SFbtnCerrar.Name = "SFbtnCerrar";
            this.SFbtnCerrar.Size = new System.Drawing.Size(40, 39);
            this.SFbtnCerrar.TabIndex = 0;
            this.SFbtnCerrar.Text = "X";
            this.SFbtnCerrar.UseVisualStyleBackColor = false;
            this.SFbtnCerrar.Click += new System.EventHandler(this.SFbtnCerrar_Click);
            // 
            // LayoutPanelPorcentajesCantidades
            // 
            this.LayoutPanelPorcentajesCantidades.ColumnCount = 3;
            this.LayoutPanelPorcentajesCantidades.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.LayoutPanelPorcentajesCantidades.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.LayoutPanelPorcentajesCantidades.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.LayoutPanelPorcentajesCantidades.Controls.Add(this.tableLayoutPanel5, 2, 0);
            this.LayoutPanelPorcentajesCantidades.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.LayoutPanelPorcentajesCantidades.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.LayoutPanelPorcentajesCantidades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutPanelPorcentajesCantidades.Location = new System.Drawing.Point(791, 3);
            this.LayoutPanelPorcentajesCantidades.Name = "LayoutPanelPorcentajesCantidades";
            this.LayoutPanelPorcentajesCantidades.RowCount = 1;
            this.LayoutPanelPorcentajesCantidades.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanelPorcentajesCantidades.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 610F));
            this.LayoutPanelPorcentajesCantidades.Size = new System.Drawing.Size(192, 610);
            this.LayoutPanelPorcentajesCantidades.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.SFpgExtra, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(131, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(58, 604);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 543);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 61);
            this.label3.TabIndex = 1;
            this.label3.Text = "Extra";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SFpgExtra
            // 
            this.SFpgExtra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.SFpgExtra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(130)))), ((int)(((byte)(204)))));
            this.SFpgExtra.Location = new System.Drawing.Point(8, 3);
            this.SFpgExtra.Name = "SFpgExtra";
            this.SFpgExtra.Size = new System.Drawing.Size(41, 537);
            this.SFpgExtra.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.SFpgExtra.TabIndex = 2;
            this.SFpgExtra.Value = 60;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.SFpgCorriente, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(67, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(58, 604);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 543);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 61);
            this.label2.TabIndex = 1;
            this.label2.Text = "Corriente";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SFpgCorriente
            // 
            this.SFpgCorriente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.SFpgCorriente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(30)))), ((int)(((byte)(29)))));
            this.SFpgCorriente.Location = new System.Drawing.Point(8, 3);
            this.SFpgCorriente.Name = "SFpgCorriente";
            this.SFpgCorriente.Size = new System.Drawing.Size(41, 537);
            this.SFpgCorriente.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.SFpgCorriente.TabIndex = 2;
            this.SFpgCorriente.Value = 40;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.SFpgACPM, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(58, 604);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 543);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 61);
            this.label1.TabIndex = 0;
            this.label1.Text = "Acmp";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SFpgACPM
            // 
            this.SFpgACPM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.SFpgACPM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(255)))), ((int)(((byte)(65)))));
            this.SFpgACPM.Location = new System.Drawing.Point(8, 3);
            this.SFpgACPM.Maximum = 5000;
            this.SFpgACPM.Name = "SFpgACPM";
            this.SFpgACPM.Size = new System.Drawing.Size(41, 537);
            this.SFpgACPM.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.SFpgACPM.TabIndex = 1;
            this.SFpgACPM.Value = 2343;
            // 
            // SFPanelLog
            // 
            this.SFPanelLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SFPanelLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.SFPanelLog.Controls.Add(this.SFbtnBuscar);
            this.SFPanelLog.Controls.Add(this.SFtxtBuscar);
            this.SFPanelLog.Controls.Add(this.SFGridLog);
            this.SFPanelLog.Controls.Add(this.SFbtnMaximizarMinimizar);
            this.SFPanelLog.Location = new System.Drawing.Point(3, 573);
            this.SFPanelLog.Name = "SFPanelLog";
            this.SFPanelLog.Size = new System.Drawing.Size(776, 34);
            this.SFPanelLog.TabIndex = 1;
            // 
            // SFbtnBuscar
            // 
            this.SFbtnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(124)))), ((int)(((byte)(0)))));
            this.SFbtnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.SFbtnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SFbtnBuscar.ForeColor = System.Drawing.Color.White;
            this.SFbtnBuscar.Location = new System.Drawing.Point(324, 11);
            this.SFbtnBuscar.Name = "SFbtnBuscar";
            this.SFbtnBuscar.Size = new System.Drawing.Size(75, 23);
            this.SFbtnBuscar.TabIndex = 3;
            this.SFbtnBuscar.Text = "Buscar";
            this.SFbtnBuscar.UseVisualStyleBackColor = false;
            this.SFbtnBuscar.Click += new System.EventHandler(this.SFbtnBuscar_Click);
            // 
            // SFtxtBuscar
            // 
            this.SFtxtBuscar.Location = new System.Drawing.Point(6, 13);
            this.SFtxtBuscar.Name = "SFtxtBuscar";
            this.SFtxtBuscar.Size = new System.Drawing.Size(312, 20);
            this.SFtxtBuscar.TabIndex = 2;
            this.SFtxtBuscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SFtxtBuscar_KeyDown);
            this.SFtxtBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SFtxtBuscar_KeyPress);
            // 
            // SFGridLog
            // 
            this.SFGridLog.AllowUserToAddRows = false;
            this.SFGridLog.AllowUserToDeleteRows = false;
            this.SFGridLog.AllowUserToOrderColumns = true;
            this.SFGridLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SFGridLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SFGridLog.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.SFGridLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(148)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SFGridLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.SFGridLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SFGridLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Fecha,
            this.Mensaje,
            this.Dispositivo});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SFGridLog.DefaultCellStyle = dataGridViewCellStyle8;
            this.SFGridLog.EnableHeadersVisualStyles = false;
            this.SFGridLog.Location = new System.Drawing.Point(3, 40);
            this.SFGridLog.Name = "SFGridLog";
            this.SFGridLog.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(73)))), ((int)(((byte)(92)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SFGridLog.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.SFGridLog.RowHeadersVisible = false;
            this.SFGridLog.Size = new System.Drawing.Size(770, 20);
            this.SFGridLog.TabIndex = 1;
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "Fecha";
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.MinimumWidth = 50;
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            // 
            // Mensaje
            // 
            this.Mensaje.DataPropertyName = "Mensaje";
            this.Mensaje.FillWeight = 300F;
            this.Mensaje.HeaderText = "Mensaje";
            this.Mensaje.MinimumWidth = 50;
            this.Mensaje.Name = "Mensaje";
            this.Mensaje.ReadOnly = true;
            // 
            // Dispositivo
            // 
            this.Dispositivo.DataPropertyName = "Dispositivo";
            this.Dispositivo.HeaderText = "Dispositivo";
            this.Dispositivo.Name = "Dispositivo";
            // 
            // SFbtnMaximizarMinimizar
            // 
            this.SFbtnMaximizarMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SFbtnMaximizarMinimizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(124)))), ((int)(((byte)(0)))));
            this.SFbtnMaximizarMinimizar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.SFbtnMaximizarMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SFbtnMaximizarMinimizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.SFbtnMaximizarMinimizar.ForeColor = System.Drawing.Color.White;
            this.SFbtnMaximizarMinimizar.Location = new System.Drawing.Point(678, 3);
            this.SFbtnMaximizarMinimizar.Name = "SFbtnMaximizarMinimizar";
            this.SFbtnMaximizarMinimizar.Size = new System.Drawing.Size(95, 30);
            this.SFbtnMaximizarMinimizar.TabIndex = 0;
            this.SFbtnMaximizarMinimizar.Text = "Maximizar";
            this.SFbtnMaximizarMinimizar.UseVisualStyleBackColor = false;
            this.SFbtnMaximizarMinimizar.Click += new System.EventHandler(this.SFbtnMaximizarMinimizar_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.SFVersion);
            this.panel2.Controls.Add(this.SFlbHora);
            this.panel2.Controls.Add(this.SFlbFechaHora);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 658);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(986, 39);
            this.panel2.TabIndex = 4;
            // 
            // SFVersion
            // 
            this.SFVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SFVersion.AutoSize = true;
            this.SFVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.SFVersion.ForeColor = System.Drawing.Color.White;
            this.SFVersion.Location = new System.Drawing.Point(9, 9);
            this.SFVersion.Name = "SFVersion";
            this.SFVersion.Size = new System.Drawing.Size(160, 20);
            this.SFVersion.TabIndex = 5;
            this.SFVersion.Text = "Versión: 15.12.02.01";
            // 
            // SFlbHora
            // 
            this.SFlbHora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SFlbHora.AutoSize = true;
            this.SFlbHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.SFlbHora.ForeColor = System.Drawing.Color.White;
            this.SFlbHora.Location = new System.Drawing.Point(578, 9);
            this.SFlbHora.Name = "SFlbHora";
            this.SFlbHora.Size = new System.Drawing.Size(73, 20);
            this.SFlbHora.TabIndex = 4;
            this.SFlbHora.Text = "12:00pm";
            // 
            // SFlbFechaHora
            // 
            this.SFlbFechaHora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SFlbFechaHora.AutoSize = true;
            this.SFlbFechaHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.SFlbFechaHora.ForeColor = System.Drawing.Color.White;
            this.SFlbFechaHora.Location = new System.Drawing.Point(693, 9);
            this.SFlbFechaHora.Name = "SFlbFechaHora";
            this.SFlbFechaHora.Size = new System.Drawing.Size(267, 20);
            this.SFlbFechaHora.TabIndex = 3;
            this.SFlbFechaHora.Text = "domingo 29 de Noviembre de 2915";
            // 
            // TimerFecha
            // 
            this.TimerFecha.Enabled = true;
            this.TimerFecha.Interval = 1000;
            this.TimerFecha.Tick += new System.EventHandler(this.TimerFecha_Tick);
            // 
            // SFbwCambioPrecio
            // 
            this.SFbwCambioPrecio.DoWork += new System.ComponentModel.DoWorkEventHandler(this.INDbwCambioPrecio_DoWork);
            this.SFbwCambioPrecio.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.INDbwCambioPrecio_RunWorkerCompleted);
            // 
            // SFTimerCambioPrecios
            // 
            this.SFTimerCambioPrecios.Interval = 5000;
            this.SFTimerCambioPrecios.Tick += new System.EventHandler(this.SFTimerCambioPrecios_Tick);
            // 
            // SFbwConsultaPorcentajesGasolina
            // 
            this.SFbwConsultaPorcentajesGasolina.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SFbwConsultaPorcentajesGasolina_DoWork);
            this.SFbwConsultaPorcentajesGasolina.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.SFbwConsultaPorcentajesGasolina_RunWorkerCompleted);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.Controls.Add(this.LayoutPanelPorcentajesCantidades, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.TypDispensadoresRejilla, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 42);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(986, 616);
            this.tableLayoutPanel6.TabIndex = 5;
            // 
            // TypDispensadoresRejilla
            // 
            this.TypDispensadoresRejilla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TypDispensadoresRejilla.ColumnCount = 1;
            this.TypDispensadoresRejilla.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TypDispensadoresRejilla.Controls.Add(this.SFPanelLog, 0, 1);
            this.TypDispensadoresRejilla.Controls.Add(this.TsPanelContainerDispensadores, 0, 0);
            this.TypDispensadoresRejilla.Location = new System.Drawing.Point(3, 3);
            this.TypDispensadoresRejilla.Name = "TypDispensadoresRejilla";
            this.TypDispensadoresRejilla.RowCount = 2;
            this.TypDispensadoresRejilla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TypDispensadoresRejilla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.TypDispensadoresRejilla.Size = new System.Drawing.Size(782, 610);
            this.TypDispensadoresRejilla.TabIndex = 2;
            // 
            // TsPanelContainerDispensadores
            // 
            this.TsPanelContainerDispensadores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TsPanelContainerDispensadores.Controls.Add(this.FloatPanelDispositivos);
            this.TsPanelContainerDispensadores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TsPanelContainerDispensadores.Location = new System.Drawing.Point(10, 10);
            this.TsPanelContainerDispensadores.Margin = new System.Windows.Forms.Padding(10);
            this.TsPanelContainerDispensadores.Name = "TsPanelContainerDispensadores";
            this.TsPanelContainerDispensadores.Size = new System.Drawing.Size(762, 550);
            this.TsPanelContainerDispensadores.TabIndex = 2;
            // 
            // FloatPanelDispositivos
            // 
            this.FloatPanelDispositivos.AutoScroll = true;
            this.FloatPanelDispositivos.AutoSize = true;
            this.FloatPanelDispositivos.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FloatPanelDispositivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FloatPanelDispositivos.Location = new System.Drawing.Point(0, 0);
            this.FloatPanelDispositivos.Name = "FloatPanelDispositivos";
            this.FloatPanelDispositivos.Size = new System.Drawing.Size(762, 550);
            this.FloatPanelDispositivos.TabIndex = 0;
            // 
            // bwEstadoXamp
            // 
            this.bwEstadoXamp.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwEstadoXamp_DoWork);
            this.bwEstadoXamp.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwEstadoXamp_RunWorkerCompleted);
            // 
            // frmAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.ClientSize = new System.Drawing.Size(992, 700);
            this.Controls.Add(this.tableLayoutPanel6);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.SFbtnBarraTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAdmin";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Softfuel .NET Application";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAdmin_FormClosing);
            this.SFbtnBarraTitulo.ResumeLayout(false);
            this.SFbtnBarraTitulo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SFLogo)).EndInit();
            this.LayoutPanelPorcentajesCantidades.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.SFPanelLog.ResumeLayout(false);
            this.SFPanelLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SFGridLog)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.TypDispensadoresRejilla.ResumeLayout(false);
            this.TsPanelContainerDispensadores.ResumeLayout(false);
            this.TsPanelContainerDispensadores.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel SFbtnBarraTitulo;
        private System.Windows.Forms.Label SFLabelNombreSofstware;
        private System.Windows.Forms.Button SFbtnCerrar;
        private System.Windows.Forms.Button SFbtnMinimizar;
        private System.Windows.Forms.Button SFbtnMaximizar;
        private System.Windows.Forms.Panel SFPanelLog;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label SFlbFechaHora;
        private System.Windows.Forms.Timer TimerFecha;
        private System.Windows.Forms.Label SFlbHora;
        private System.Windows.Forms.Label SFVersion;
        private System.Windows.Forms.Button SFbtnMaximizarMinimizar;
        private System.Windows.Forms.DataGridView SFGridLog;
        private System.Windows.Forms.Button SFbtnBuscar;
        private System.Windows.Forms.TextBox SFtxtBuscar;
        private System.ComponentModel.BackgroundWorker SFbwCambioPrecio;
        private System.Windows.Forms.Timer SFTimerCambioPrecios;
        private System.Windows.Forms.PictureBox SFLogo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mensaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dispositivo;
        private System.Windows.Forms.TableLayoutPanel LayoutPanelPorcentajesCantidades;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private Controles.VerticalProgressBar SFpgACPM;
        private Controles.VerticalProgressBar SFpgExtra;
        private Controles.VerticalProgressBar SFpgCorriente;
        private System.ComponentModel.BackgroundWorker SFbwConsultaPorcentajesGasolina;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel TypDispensadoresRejilla;
        private System.Windows.Forms.Panel TsPanelContainerDispensadores;
        private System.Windows.Forms.FlowLayoutPanel FloatPanelDispositivos;
        private System.ComponentModel.BackgroundWorker bwEstadoXamp;
    }
}