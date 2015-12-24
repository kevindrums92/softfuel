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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdmin));
            this.SFbtnBarraTitulo = new System.Windows.Forms.Panel();
            this.SFLogo = new System.Windows.Forms.PictureBox();
            this.SFbtnMinimizar = new System.Windows.Forms.Button();
            this.SFbtnMaximizar = new System.Windows.Forms.Button();
            this.SFLabelNombreSofstware = new System.Windows.Forms.Label();
            this.SFbtnCerrar = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SFLyContainer = new System.Windows.Forms.TableLayoutPanel();
            this.SFlyContainerDispensadores = new System.Windows.Forms.TableLayoutPanel();
            this.SFLayoutContainer = new System.Windows.Forms.TableLayoutPanel();
            this.SFPanelCara1 = new System.Windows.Forms.Panel();
            this.SFPanelCara3 = new System.Windows.Forms.Panel();
            this.SFPanelCara5 = new System.Windows.Forms.Panel();
            this.SFPanelCara7 = new System.Windows.Forms.Panel();
            this.SFPanelCara2 = new System.Windows.Forms.Panel();
            this.SFPanelCara4 = new System.Windows.Forms.Panel();
            this.SFPanelCara6 = new System.Windows.Forms.Panel();
            this.SFPanelCara8 = new System.Windows.Forms.Panel();
            this.SFPanelPOS1 = new System.Windows.Forms.Panel();
            this.SFPanelPOS2 = new System.Windows.Forms.Panel();
            this.SFPanelPOS3 = new System.Windows.Forms.Panel();
            this.SFPanelPOS4 = new System.Windows.Forms.Panel();
            this.SFPanelLog = new System.Windows.Forms.Panel();
            this.INDbtnEscanRed = new System.Windows.Forms.Button();
            this.SFbtnBuscar = new System.Windows.Forms.Button();
            this.SFtxtBuscar = new System.Windows.Forms.TextBox();
            this.SFGridLog = new System.Windows.Forms.DataGridView();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mensaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dispositivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SFbtnMaximizarMinimizar = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.SFbtnDesconectar = new System.Windows.Forms.Button();
            this.SFbtnConectar = new System.Windows.Forms.Button();
            this.SFbtnEscanearRed = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SFVersion = new System.Windows.Forms.Label();
            this.SFlbHora = new System.Windows.Forms.Label();
            this.SFlbFechaHora = new System.Windows.Forms.Label();
            this.TimerFecha = new System.Windows.Forms.Timer(this.components);
            this.SFbwCambioPrecio = new System.ComponentModel.BackgroundWorker();
            this.SFTimerCambioPrecios = new System.Windows.Forms.Timer(this.components);
            this.SFbtnBarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SFLogo)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SFLyContainer.SuspendLayout();
            this.SFlyContainerDispensadores.SuspendLayout();
            this.SFLayoutContainer.SuspendLayout();
            this.SFPanelLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SFGridLog)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // SFbtnBarraTitulo
            // 
            this.SFbtnBarraTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.SFbtnBarraTitulo.Controls.Add(this.SFLogo);
            this.SFbtnBarraTitulo.Controls.Add(this.SFbtnMinimizar);
            this.SFbtnBarraTitulo.Controls.Add(this.SFbtnMaximizar);
            this.SFbtnBarraTitulo.Controls.Add(this.SFLabelNombreSofstware);
            this.SFbtnBarraTitulo.Controls.Add(this.SFbtnCerrar);
            this.SFbtnBarraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.SFbtnBarraTitulo.Location = new System.Drawing.Point(3, 3);
            this.SFbtnBarraTitulo.Name = "SFbtnBarraTitulo";
            this.SFbtnBarraTitulo.Size = new System.Drawing.Size(994, 39);
            this.SFbtnBarraTitulo.TabIndex = 2;
            this.SFbtnBarraTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.SFbtnBarraTitulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.SFbtnBarraTitulo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            // 
            // SFLogo
            // 
            this.SFLogo.Location = new System.Drawing.Point(13, 9);
            this.SFLogo.Name = "SFLogo";
            this.SFLogo.Size = new System.Drawing.Size(18, 20);
            this.SFLogo.TabIndex = 6;
            this.SFLogo.TabStop = false;
            // 
            // SFbtnMinimizar
            // 
            this.SFbtnMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SFbtnMinimizar.FlatAppearance.BorderSize = 0;
            this.SFbtnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SFbtnMinimizar.ForeColor = System.Drawing.Color.White;
            this.SFbtnMinimizar.Location = new System.Drawing.Point(876, 1);
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
            this.SFbtnMaximizar.Location = new System.Drawing.Point(915, 1);
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
            this.SFLabelNombreSofstware.Location = new System.Drawing.Point(37, 9);
            this.SFLabelNombreSofstware.Name = "SFLabelNombreSofstware";
            this.SFLabelNombreSofstware.Size = new System.Drawing.Size(219, 20);
            this.SFLabelNombreSofstware.TabIndex = 3;
            this.SFLabelNombreSofstware.Text = "Softfuel Console Application";
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
            this.SFbtnCerrar.Location = new System.Drawing.Point(954, 1);
            this.SFbtnCerrar.Name = "SFbtnCerrar";
            this.SFbtnCerrar.Size = new System.Drawing.Size(40, 39);
            this.SFbtnCerrar.TabIndex = 0;
            this.SFbtnCerrar.Text = "X";
            this.SFbtnCerrar.UseVisualStyleBackColor = false;
            this.SFbtnCerrar.Click += new System.EventHandler(this.SFbtnCerrar_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 99.5F));
            this.tableLayoutPanel1.Controls.Add(this.SFLyContainer, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 42);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(994, 616);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // SFLyContainer
            // 
            this.SFLyContainer.BackColor = System.Drawing.Color.White;
            this.SFLyContainer.ColumnCount = 1;
            this.SFLyContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.SFLyContainer.Controls.Add(this.SFlyContainerDispensadores, 0, 0);
            this.SFLyContainer.Controls.Add(this.SFPanelLog, 0, 1);
            this.SFLyContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFLyContainer.Location = new System.Drawing.Point(7, 3);
            this.SFLyContainer.Name = "SFLyContainer";
            this.SFLyContainer.RowCount = 2;
            this.SFLyContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.98039F));
            this.SFLyContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.01961F));
            this.SFLyContainer.Size = new System.Drawing.Size(984, 610);
            this.SFLyContainer.TabIndex = 0;
            // 
            // SFlyContainerDispensadores
            // 
            this.SFlyContainerDispensadores.ColumnCount = 2;
            this.SFlyContainerDispensadores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.SFlyContainerDispensadores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.SFlyContainerDispensadores.Controls.Add(this.SFLayoutContainer, 0, 0);
            this.SFlyContainerDispensadores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFlyContainerDispensadores.Location = new System.Drawing.Point(3, 3);
            this.SFlyContainerDispensadores.Name = "SFlyContainerDispensadores";
            this.SFlyContainerDispensadores.RowCount = 1;
            this.SFlyContainerDispensadores.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.SFlyContainerDispensadores.Size = new System.Drawing.Size(973, 365);
            this.SFlyContainerDispensadores.TabIndex = 0;
            // 
            // SFLayoutContainer
            // 
            this.SFLayoutContainer.ColumnCount = 4;
            this.SFLayoutContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.SFLayoutContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.SFLayoutContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.SFLayoutContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.SFLayoutContainer.Controls.Add(this.SFPanelCara1, 0, 0);
            this.SFLayoutContainer.Controls.Add(this.SFPanelCara3, 1, 0);
            this.SFLayoutContainer.Controls.Add(this.SFPanelCara5, 2, 0);
            this.SFLayoutContainer.Controls.Add(this.SFPanelCara7, 3, 0);
            this.SFLayoutContainer.Controls.Add(this.SFPanelCara2, 0, 1);
            this.SFLayoutContainer.Controls.Add(this.SFPanelCara4, 1, 1);
            this.SFLayoutContainer.Controls.Add(this.SFPanelCara6, 2, 1);
            this.SFLayoutContainer.Controls.Add(this.SFPanelCara8, 3, 1);
            this.SFLayoutContainer.Controls.Add(this.SFPanelPOS1, 0, 2);
            this.SFLayoutContainer.Controls.Add(this.SFPanelPOS2, 1, 2);
            this.SFLayoutContainer.Controls.Add(this.SFPanelPOS3, 2, 2);
            this.SFLayoutContainer.Controls.Add(this.SFPanelPOS4, 3, 2);
            this.SFLayoutContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFLayoutContainer.Location = new System.Drawing.Point(3, 3);
            this.SFLayoutContainer.Name = "SFLayoutContainer";
            this.SFLayoutContainer.RowCount = 3;
            this.SFLayoutContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.SFLayoutContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.SFLayoutContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.SFLayoutContainer.Size = new System.Drawing.Size(675, 359);
            this.SFLayoutContainer.TabIndex = 0;
            // 
            // SFPanelCara1
            // 
            this.SFPanelCara1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelCara1.Location = new System.Drawing.Point(3, 3);
            this.SFPanelCara1.Name = "SFPanelCara1";
            this.SFPanelCara1.Size = new System.Drawing.Size(162, 113);
            this.SFPanelCara1.TabIndex = 0;
            // 
            // SFPanelCara3
            // 
            this.SFPanelCara3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelCara3.Location = new System.Drawing.Point(171, 3);
            this.SFPanelCara3.Name = "SFPanelCara3";
            this.SFPanelCara3.Size = new System.Drawing.Size(162, 113);
            this.SFPanelCara3.TabIndex = 1;
            // 
            // SFPanelCara5
            // 
            this.SFPanelCara5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelCara5.Location = new System.Drawing.Point(339, 3);
            this.SFPanelCara5.Name = "SFPanelCara5";
            this.SFPanelCara5.Size = new System.Drawing.Size(162, 113);
            this.SFPanelCara5.TabIndex = 2;
            // 
            // SFPanelCara7
            // 
            this.SFPanelCara7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelCara7.Location = new System.Drawing.Point(507, 3);
            this.SFPanelCara7.Name = "SFPanelCara7";
            this.SFPanelCara7.Size = new System.Drawing.Size(165, 113);
            this.SFPanelCara7.TabIndex = 3;
            // 
            // SFPanelCara2
            // 
            this.SFPanelCara2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelCara2.Location = new System.Drawing.Point(3, 122);
            this.SFPanelCara2.Name = "SFPanelCara2";
            this.SFPanelCara2.Size = new System.Drawing.Size(162, 113);
            this.SFPanelCara2.TabIndex = 4;
            // 
            // SFPanelCara4
            // 
            this.SFPanelCara4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelCara4.Location = new System.Drawing.Point(171, 122);
            this.SFPanelCara4.Name = "SFPanelCara4";
            this.SFPanelCara4.Size = new System.Drawing.Size(162, 113);
            this.SFPanelCara4.TabIndex = 5;
            // 
            // SFPanelCara6
            // 
            this.SFPanelCara6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelCara6.Location = new System.Drawing.Point(339, 122);
            this.SFPanelCara6.Name = "SFPanelCara6";
            this.SFPanelCara6.Size = new System.Drawing.Size(162, 113);
            this.SFPanelCara6.TabIndex = 6;
            // 
            // SFPanelCara8
            // 
            this.SFPanelCara8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelCara8.Location = new System.Drawing.Point(507, 122);
            this.SFPanelCara8.Name = "SFPanelCara8";
            this.SFPanelCara8.Size = new System.Drawing.Size(165, 113);
            this.SFPanelCara8.TabIndex = 7;
            // 
            // SFPanelPOS1
            // 
            this.SFPanelPOS1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelPOS1.Location = new System.Drawing.Point(3, 241);
            this.SFPanelPOS1.Name = "SFPanelPOS1";
            this.SFPanelPOS1.Size = new System.Drawing.Size(162, 115);
            this.SFPanelPOS1.TabIndex = 8;
            // 
            // SFPanelPOS2
            // 
            this.SFPanelPOS2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelPOS2.Location = new System.Drawing.Point(171, 241);
            this.SFPanelPOS2.Name = "SFPanelPOS2";
            this.SFPanelPOS2.Size = new System.Drawing.Size(162, 115);
            this.SFPanelPOS2.TabIndex = 9;
            // 
            // SFPanelPOS3
            // 
            this.SFPanelPOS3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelPOS3.Location = new System.Drawing.Point(339, 241);
            this.SFPanelPOS3.Name = "SFPanelPOS3";
            this.SFPanelPOS3.Size = new System.Drawing.Size(162, 115);
            this.SFPanelPOS3.TabIndex = 10;
            // 
            // SFPanelPOS4
            // 
            this.SFPanelPOS4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelPOS4.Location = new System.Drawing.Point(507, 241);
            this.SFPanelPOS4.Name = "SFPanelPOS4";
            this.SFPanelPOS4.Size = new System.Drawing.Size(165, 115);
            this.SFPanelPOS4.TabIndex = 11;
            // 
            // SFPanelLog
            // 
            this.SFPanelLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(73)))), ((int)(((byte)(92)))));
            this.SFPanelLog.Controls.Add(this.INDbtnEscanRed);
            this.SFPanelLog.Controls.Add(this.SFbtnBuscar);
            this.SFPanelLog.Controls.Add(this.SFtxtBuscar);
            this.SFPanelLog.Controls.Add(this.SFGridLog);
            this.SFPanelLog.Controls.Add(this.SFbtnMaximizarMinimizar);
            this.SFPanelLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelLog.Location = new System.Drawing.Point(3, 374);
            this.SFPanelLog.Name = "SFPanelLog";
            this.SFPanelLog.Size = new System.Drawing.Size(973, 233);
            this.SFPanelLog.TabIndex = 1;
            // 
            // INDbtnEscanRed
            // 
            this.INDbtnEscanRed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.INDbtnEscanRed.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.INDbtnEscanRed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.INDbtnEscanRed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.INDbtnEscanRed.ForeColor = System.Drawing.Color.White;
            this.INDbtnEscanRed.Location = new System.Drawing.Point(747, 3);
            this.INDbtnEscanRed.Name = "INDbtnEscanRed";
            this.INDbtnEscanRed.Size = new System.Drawing.Size(122, 30);
            this.INDbtnEscanRed.TabIndex = 4;
            this.INDbtnEscanRed.Text = "Escanear Red";
            this.INDbtnEscanRed.UseVisualStyleBackColor = true;
            this.INDbtnEscanRed.Click += new System.EventHandler(this.INDbtnEscanRed_Click);
            // 
            // SFbtnBuscar
            // 
            this.SFbtnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.SFbtnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SFbtnBuscar.ForeColor = System.Drawing.Color.White;
            this.SFbtnBuscar.Location = new System.Drawing.Point(324, 11);
            this.SFbtnBuscar.Name = "SFbtnBuscar";
            this.SFbtnBuscar.Size = new System.Drawing.Size(75, 23);
            this.SFbtnBuscar.TabIndex = 3;
            this.SFbtnBuscar.Text = "Buscar";
            this.SFbtnBuscar.UseVisualStyleBackColor = true;
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
            this.SFGridLog.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(73)))), ((int)(((byte)(92)))));
            this.SFGridLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(73)))), ((int)(((byte)(92)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(148)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SFGridLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.SFGridLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SFGridLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Fecha,
            this.Mensaje,
            this.Dispositivo});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(73)))), ((int)(((byte)(92)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SFGridLog.DefaultCellStyle = dataGridViewCellStyle2;
            this.SFGridLog.EnableHeadersVisualStyles = false;
            this.SFGridLog.Location = new System.Drawing.Point(3, 40);
            this.SFGridLog.Name = "SFGridLog";
            this.SFGridLog.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(73)))), ((int)(((byte)(92)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SFGridLog.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.SFGridLog.RowHeadersVisible = false;
            this.SFGridLog.Size = new System.Drawing.Size(967, 190);
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
            this.SFbtnMaximizarMinimizar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.SFbtnMaximizarMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SFbtnMaximizarMinimizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.SFbtnMaximizarMinimizar.ForeColor = System.Drawing.Color.White;
            this.SFbtnMaximizarMinimizar.Location = new System.Drawing.Point(875, 3);
            this.SFbtnMaximizarMinimizar.Name = "SFbtnMaximizarMinimizar";
            this.SFbtnMaximizarMinimizar.Size = new System.Drawing.Size(95, 30);
            this.SFbtnMaximizarMinimizar.TabIndex = 0;
            this.SFbtnMaximizarMinimizar.Text = "Maximizar";
            this.SFbtnMaximizarMinimizar.UseVisualStyleBackColor = true;
            this.SFbtnMaximizarMinimizar.Click += new System.EventHandler(this.SFbtnMaximizarMinimizar_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.SFbtnDesconectar, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.SFbtnConectar, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.SFbtnEscanearRed, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1, 610);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // SFbtnDesconectar
            // 
            this.SFbtnDesconectar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.SFbtnDesconectar.Enabled = false;
            this.SFbtnDesconectar.FlatAppearance.BorderSize = 0;
            this.SFbtnDesconectar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SFbtnDesconectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.SFbtnDesconectar.ForeColor = System.Drawing.Color.White;
            this.SFbtnDesconectar.Location = new System.Drawing.Point(3, 409);
            this.SFbtnDesconectar.Name = "SFbtnDesconectar";
            this.SFbtnDesconectar.Size = new System.Drawing.Size(1, 100);
            this.SFbtnDesconectar.TabIndex = 2;
            this.SFbtnDesconectar.Text = "Desconectar";
            this.SFbtnDesconectar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SFbtnDesconectar.UseVisualStyleBackColor = true;
            this.SFbtnDesconectar.Visible = false;
            this.SFbtnDesconectar.Click += new System.EventHandler(this.SFbtnDesconectar_Click);
            // 
            // SFbtnConectar
            // 
            this.SFbtnConectar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.SFbtnConectar.FlatAppearance.BorderSize = 0;
            this.SFbtnConectar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SFbtnConectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.SFbtnConectar.ForeColor = System.Drawing.Color.White;
            this.SFbtnConectar.Location = new System.Drawing.Point(3, 100);
            this.SFbtnConectar.Name = "SFbtnConectar";
            this.SFbtnConectar.Size = new System.Drawing.Size(1, 100);
            this.SFbtnConectar.TabIndex = 0;
            this.SFbtnConectar.Text = "Conectar";
            this.SFbtnConectar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SFbtnConectar.UseVisualStyleBackColor = true;
            this.SFbtnConectar.Visible = false;
            this.SFbtnConectar.Click += new System.EventHandler(this.SFbtnConectar_Click);
            // 
            // SFbtnEscanearRed
            // 
            this.SFbtnEscanearRed.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SFbtnEscanearRed.Enabled = false;
            this.SFbtnEscanearRed.FlatAppearance.BorderSize = 0;
            this.SFbtnEscanearRed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SFbtnEscanearRed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.SFbtnEscanearRed.ForeColor = System.Drawing.Color.White;
            this.SFbtnEscanearRed.Location = new System.Drawing.Point(3, 254);
            this.SFbtnEscanearRed.Name = "SFbtnEscanearRed";
            this.SFbtnEscanearRed.Size = new System.Drawing.Size(1, 100);
            this.SFbtnEscanearRed.TabIndex = 1;
            this.SFbtnEscanearRed.Text = "Buscar Red";
            this.SFbtnEscanearRed.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SFbtnEscanearRed.UseVisualStyleBackColor = true;
            this.SFbtnEscanearRed.Click += new System.EventHandler(this.SFbtnEscanearRed_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel2.Controls.Add(this.SFVersion);
            this.panel2.Controls.Add(this.SFlbHora);
            this.panel2.Controls.Add(this.SFlbFechaHora);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 658);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(994, 39);
            this.panel2.TabIndex = 4;
            // 
            // SFVersion
            // 
            this.SFVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SFVersion.AutoSize = true;
            this.SFVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.SFVersion.ForeColor = System.Drawing.Color.White;
            this.SFVersion.Location = new System.Drawing.Point(133, 9);
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
            this.SFlbHora.Location = new System.Drawing.Point(586, 9);
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
            this.SFlbFechaHora.Location = new System.Drawing.Point(701, 9);
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
            // frmAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.tableLayoutPanel1);
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
            this.tableLayoutPanel1.ResumeLayout(false);
            this.SFLyContainer.ResumeLayout(false);
            this.SFlyContainerDispensadores.ResumeLayout(false);
            this.SFLayoutContainer.ResumeLayout(false);
            this.SFPanelLog.ResumeLayout(false);
            this.SFPanelLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SFGridLog)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel SFbtnBarraTitulo;
        private System.Windows.Forms.Label SFLabelNombreSofstware;
        private System.Windows.Forms.Button SFbtnCerrar;
        private System.Windows.Forms.Button SFbtnMinimizar;
        private System.Windows.Forms.Button SFbtnMaximizar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel SFLyContainer;
        private System.Windows.Forms.TableLayoutPanel SFlyContainerDispensadores;
        private System.Windows.Forms.TableLayoutPanel SFLayoutContainer;
        private System.Windows.Forms.Panel SFPanelCara1;
        private System.Windows.Forms.Panel SFPanelCara3;
        private System.Windows.Forms.Panel SFPanelCara5;
        private System.Windows.Forms.Panel SFPanelCara7;
        private System.Windows.Forms.Panel SFPanelCara2;
        private System.Windows.Forms.Panel SFPanelCara4;
        private System.Windows.Forms.Panel SFPanelCara6;
        private System.Windows.Forms.Panel SFPanelCara8;
        private System.Windows.Forms.Panel SFPanelPOS1;
        private System.Windows.Forms.Panel SFPanelPOS2;
        private System.Windows.Forms.Panel SFPanelPOS3;
        private System.Windows.Forms.Panel SFPanelPOS4;
        private System.Windows.Forms.Panel SFPanelLog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button SFbtnConectar;
        private System.Windows.Forms.Button SFbtnEscanearRed;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label SFlbFechaHora;
        private System.Windows.Forms.Timer TimerFecha;
        private System.Windows.Forms.Label SFlbHora;
        private System.Windows.Forms.Label SFVersion;
        private System.Windows.Forms.Button SFbtnMaximizarMinimizar;
        private System.Windows.Forms.Button SFbtnDesconectar;
        private System.Windows.Forms.DataGridView SFGridLog;
        private System.Windows.Forms.Button SFbtnBuscar;
        private System.Windows.Forms.TextBox SFtxtBuscar;
        private System.ComponentModel.BackgroundWorker SFbwCambioPrecio;
        private System.Windows.Forms.Timer SFTimerCambioPrecios;
        private System.Windows.Forms.PictureBox SFLogo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mensaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dispositivo;
        private System.Windows.Forms.Button INDbtnEscanRed;
    }
}