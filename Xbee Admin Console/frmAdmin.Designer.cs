﻿namespace XbeeAdminConsole
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
            this.SFbtnBarraTitulo = new System.Windows.Forms.Panel();
            this.SFbtnMinimizar = new System.Windows.Forms.Button();
            this.SFbtnMaximizar = new System.Windows.Forms.Button();
            this.SFLabelNombreSofstware = new System.Windows.Forms.Label();
            this.SFbtnCerrar = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.SFbtnConectar = new System.Windows.Forms.Button();
            this.SFbtnEscanearRed = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SFVersion = new System.Windows.Forms.Label();
            this.SFlbHora = new System.Windows.Forms.Label();
            this.SFlbFechaHora = new System.Windows.Forms.Label();
            this.TimerFecha = new System.Windows.Forms.Timer(this.components);
            this.SFbtnBarraTitulo.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SFlyContainerDispensadores.SuspendLayout();
            this.SFLayoutContainer.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // SFbtnBarraTitulo
            // 
            this.SFbtnBarraTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.SFbtnBarraTitulo.Controls.Add(this.SFbtnMinimizar);
            this.SFbtnBarraTitulo.Controls.Add(this.SFbtnMaximizar);
            this.SFbtnBarraTitulo.Controls.Add(this.SFLabelNombreSofstware);
            this.SFbtnBarraTitulo.Controls.Add(this.SFbtnCerrar);
            this.SFbtnBarraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.SFbtnBarraTitulo.Location = new System.Drawing.Point(3, 3);
            this.SFbtnBarraTitulo.Name = "SFbtnBarraTitulo";
            this.SFbtnBarraTitulo.Size = new System.Drawing.Size(894, 39);
            this.SFbtnBarraTitulo.TabIndex = 2;
            this.SFbtnBarraTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.SFbtnBarraTitulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.SFbtnBarraTitulo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            // 
            // SFbtnMinimizar
            // 
            this.SFbtnMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SFbtnMinimizar.FlatAppearance.BorderSize = 0;
            this.SFbtnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SFbtnMinimizar.ForeColor = System.Drawing.Color.White;
            this.SFbtnMinimizar.Location = new System.Drawing.Point(776, 1);
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
            this.SFbtnMaximizar.Location = new System.Drawing.Point(815, 1);
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
            this.SFLabelNombreSofstware.Location = new System.Drawing.Point(34, 9);
            this.SFLabelNombreSofstware.Name = "SFLabelNombreSofstware";
            this.SFLabelNombreSofstware.Size = new System.Drawing.Size(195, 20);
            this.SFLabelNombreSofstware.TabIndex = 3;
            this.SFLabelNombreSofstware.Text = "Softfuel .NET Application";
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
            this.SFbtnCerrar.Location = new System.Drawing.Point(854, 1);
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 42);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(894, 566);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.SFlyContainerDispensadores, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(137, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.98039F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.01961F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(754, 560);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // SFlyContainerDispensadores
            // 
            this.SFlyContainerDispensadores.ColumnCount = 2;
            this.SFlyContainerDispensadores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.SFlyContainerDispensadores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.SFlyContainerDispensadores.Controls.Add(this.SFLayoutContainer, 0, 0);
            this.SFlyContainerDispensadores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFlyContainerDispensadores.Location = new System.Drawing.Point(3, 3);
            this.SFlyContainerDispensadores.Name = "SFlyContainerDispensadores";
            this.SFlyContainerDispensadores.RowCount = 1;
            this.SFlyContainerDispensadores.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.SFlyContainerDispensadores.Size = new System.Drawing.Size(748, 335);
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
            this.SFLayoutContainer.Size = new System.Drawing.Size(442, 329);
            this.SFLayoutContainer.TabIndex = 0;
            // 
            // SFPanelCara1
            // 
            this.SFPanelCara1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelCara1.Location = new System.Drawing.Point(3, 3);
            this.SFPanelCara1.Name = "SFPanelCara1";
            this.SFPanelCara1.Size = new System.Drawing.Size(104, 103);
            this.SFPanelCara1.TabIndex = 0;
            // 
            // SFPanelCara3
            // 
            this.SFPanelCara3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelCara3.Location = new System.Drawing.Point(113, 3);
            this.SFPanelCara3.Name = "SFPanelCara3";
            this.SFPanelCara3.Size = new System.Drawing.Size(104, 103);
            this.SFPanelCara3.TabIndex = 1;
            // 
            // SFPanelCara5
            // 
            this.SFPanelCara5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelCara5.Location = new System.Drawing.Point(223, 3);
            this.SFPanelCara5.Name = "SFPanelCara5";
            this.SFPanelCara5.Size = new System.Drawing.Size(104, 103);
            this.SFPanelCara5.TabIndex = 2;
            // 
            // SFPanelCara7
            // 
            this.SFPanelCara7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelCara7.Location = new System.Drawing.Point(333, 3);
            this.SFPanelCara7.Name = "SFPanelCara7";
            this.SFPanelCara7.Size = new System.Drawing.Size(106, 103);
            this.SFPanelCara7.TabIndex = 3;
            // 
            // SFPanelCara2
            // 
            this.SFPanelCara2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelCara2.Location = new System.Drawing.Point(3, 112);
            this.SFPanelCara2.Name = "SFPanelCara2";
            this.SFPanelCara2.Size = new System.Drawing.Size(104, 103);
            this.SFPanelCara2.TabIndex = 4;
            // 
            // SFPanelCara4
            // 
            this.SFPanelCara4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelCara4.Location = new System.Drawing.Point(113, 112);
            this.SFPanelCara4.Name = "SFPanelCara4";
            this.SFPanelCara4.Size = new System.Drawing.Size(104, 103);
            this.SFPanelCara4.TabIndex = 5;
            // 
            // SFPanelCara6
            // 
            this.SFPanelCara6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelCara6.Location = new System.Drawing.Point(223, 112);
            this.SFPanelCara6.Name = "SFPanelCara6";
            this.SFPanelCara6.Size = new System.Drawing.Size(104, 103);
            this.SFPanelCara6.TabIndex = 6;
            // 
            // SFPanelCara8
            // 
            this.SFPanelCara8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelCara8.Location = new System.Drawing.Point(333, 112);
            this.SFPanelCara8.Name = "SFPanelCara8";
            this.SFPanelCara8.Size = new System.Drawing.Size(106, 103);
            this.SFPanelCara8.TabIndex = 7;
            // 
            // SFPanelPOS1
            // 
            this.SFPanelPOS1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelPOS1.Location = new System.Drawing.Point(3, 221);
            this.SFPanelPOS1.Name = "SFPanelPOS1";
            this.SFPanelPOS1.Size = new System.Drawing.Size(104, 105);
            this.SFPanelPOS1.TabIndex = 8;
            // 
            // SFPanelPOS2
            // 
            this.SFPanelPOS2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelPOS2.Location = new System.Drawing.Point(113, 221);
            this.SFPanelPOS2.Name = "SFPanelPOS2";
            this.SFPanelPOS2.Size = new System.Drawing.Size(104, 105);
            this.SFPanelPOS2.TabIndex = 9;
            // 
            // SFPanelPOS3
            // 
            this.SFPanelPOS3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelPOS3.Location = new System.Drawing.Point(223, 221);
            this.SFPanelPOS3.Name = "SFPanelPOS3";
            this.SFPanelPOS3.Size = new System.Drawing.Size(104, 105);
            this.SFPanelPOS3.TabIndex = 10;
            // 
            // SFPanelPOS4
            // 
            this.SFPanelPOS4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SFPanelPOS4.Location = new System.Drawing.Point(333, 221);
            this.SFPanelPOS4.Name = "SFPanelPOS4";
            this.SFPanelPOS4.Size = new System.Drawing.Size(106, 105);
            this.SFPanelPOS4.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(73)))), ((int)(((byte)(92)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 344);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(748, 213);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.SFbtnConectar, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.SFbtnEscanearRed, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(128, 560);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // SFbtnConectar
            // 
            this.SFbtnConectar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SFbtnConectar.FlatAppearance.BorderSize = 0;
            this.SFbtnConectar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SFbtnConectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.SFbtnConectar.ForeColor = System.Drawing.Color.White;
            this.SFbtnConectar.Location = new System.Drawing.Point(3, 177);
            this.SFbtnConectar.Name = "SFbtnConectar";
            this.SFbtnConectar.Size = new System.Drawing.Size(122, 100);
            this.SFbtnConectar.TabIndex = 0;
            this.SFbtnConectar.Text = "Conexión";
            this.SFbtnConectar.UseVisualStyleBackColor = true;
            // 
            // SFbtnEscanearRed
            // 
            this.SFbtnEscanearRed.Dock = System.Windows.Forms.DockStyle.Top;
            this.SFbtnEscanearRed.FlatAppearance.BorderSize = 0;
            this.SFbtnEscanearRed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SFbtnEscanearRed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.SFbtnEscanearRed.ForeColor = System.Drawing.Color.White;
            this.SFbtnEscanearRed.Location = new System.Drawing.Point(3, 283);
            this.SFbtnEscanearRed.Name = "SFbtnEscanearRed";
            this.SFbtnEscanearRed.Size = new System.Drawing.Size(122, 100);
            this.SFbtnEscanearRed.TabIndex = 1;
            this.SFbtnEscanearRed.Text = "Buscar Red";
            this.SFbtnEscanearRed.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel2.Controls.Add(this.SFVersion);
            this.panel2.Controls.Add(this.SFlbHora);
            this.panel2.Controls.Add(this.SFlbFechaHora);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 608);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(894, 39);
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
            this.SFVersion.Text = "Versión: 15.11.01.01";
            // 
            // SFlbHora
            // 
            this.SFlbHora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SFlbHora.AutoSize = true;
            this.SFlbHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.SFlbHora.ForeColor = System.Drawing.Color.White;
            this.SFlbHora.Location = new System.Drawing.Point(486, 9);
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
            this.SFlbFechaHora.Location = new System.Drawing.Point(601, 9);
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
            // frmAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(900, 650);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.SFbtnBarraTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAdmin";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Softfuel .NET Application";
            this.SFbtnBarraTitulo.ResumeLayout(false);
            this.SFbtnBarraTitulo.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.SFlyContainerDispensadores.ResumeLayout(false);
            this.SFLayoutContainer.ResumeLayout(false);
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button SFbtnConectar;
        private System.Windows.Forms.Button SFbtnEscanearRed;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label SFlbFechaHora;
        private System.Windows.Forms.Timer TimerFecha;
        private System.Windows.Forms.Label SFlbHora;
        private System.Windows.Forms.Label SFVersion;
    }
}