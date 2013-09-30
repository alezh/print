namespace WindowsFormsApplication1
{
    partial class DeliveryPrint
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeliveryPrint));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtPrintPiece = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbShopMethod = new System.Windows.Forms.ComboBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOrderNo = new System.Windows.Forms.TextBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnSetSingle = new System.Windows.Forms.Button();
            this.cmbSelect = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_setmulfalse = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_mynum = new System.Windows.Forms.Label();
            this.label_printuser = new System.Windows.Forms.Label();
            this.button_getallprintok = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label_oknum = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label_allnum = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.EMS1 = new WindowsFormsApplication1.EMS();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button_jiechu = new System.Windows.Forms.Button();
            this.button_download = new System.Windows.Forms.Button();
            this.button_gethdname = new System.Windows.Forms.Button();
            this.comboBox_hdname = new System.Windows.Forms.ComboBox();
            this.label_hdname = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_reprint = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.活动信息设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.检查自动发货ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.没点发货汇总ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.库存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.库存与订单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.出库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新增用户ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.子账号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.密码修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.订单修改与废弃ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(12, 301);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(550, 470);
            this.panel1.TabIndex = 7;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column6,
            this.Column7,
            this.Column8});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 10;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(550, 470);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Column5
            // 
            this.Column5.HeaderText = "序号";
            this.Column5.Name = "Column5";
            this.Column5.Width = 36;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "订单号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.Width = 110;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "快递单号";
            this.Column2.Name = "Column2";
            this.Column2.Width = 110;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "是否完成";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 60;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "买家ID";
            this.Column4.Name = "Column4";
            this.Column4.Width = 70;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "商品";
            this.Column6.Name = "Column6";
            this.Column6.Width = 60;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "数量";
            this.Column7.Name = "Column7";
            this.Column7.Width = 60;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "时间";
            this.Column8.Name = "Column8";
            // 
            // txtPrintPiece
            // 
            this.txtPrintPiece.Location = new System.Drawing.Point(117, 14);
            this.txtPrintPiece.Name = "txtPrintPiece";
            this.txtPrintPiece.Size = new System.Drawing.Size(43, 23);
            this.txtPrintPiece.TabIndex = 6;
            this.txtPrintPiece.Text = "100";
            this.txtPrintPiece.TextChanged += new System.EventHandler(this.txtPrintPiece_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "设置打印张数：";
            // 
            // cmbShopMethod
            // 
            this.cmbShopMethod.FormattingEnabled = true;
            this.cmbShopMethod.Items.AddRange(new object[] {
            "中通快递",
            "EMS",
            "邮政国内小包"});
            this.cmbShopMethod.Location = new System.Drawing.Point(117, 81);
            this.cmbShopMethod.Name = "cmbShopMethod";
            this.cmbShopMethod.Size = new System.Drawing.Size(121, 22);
            this.cmbShopMethod.TabIndex = 4;
            this.cmbShopMethod.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnPrint.Location = new System.Drawing.Point(4, 51);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(122, 23);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.Text = "打印面单(&P)";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.textBox1.Location = new System.Drawing.Point(119, 21);
            this.textBox1.MaxLength = 200;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(113, 23);
            this.textBox1.TabIndex = 12;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSave.Location = new System.Drawing.Point(4, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(123, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "保存快递单号(&S)";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 14);
            this.label3.TabIndex = 15;
            this.label3.Text = "录入快递单号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 14);
            this.label4.TabIndex = 16;
            this.label4.Text = "订单号码：";
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.Location = new System.Drawing.Point(94, 21);
            this.txtOrderNo.MaxLength = 200;
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Size = new System.Drawing.Size(121, 23);
            this.txtOrderNo.TabIndex = 17;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(221, 20);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(86, 23);
            this.btnQuery.TabIndex = 18;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSelect.Location = new System.Drawing.Point(119, 50);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(113, 23);
            this.btnSelect.TabIndex = 19;
            this.btnSelect.Text = "浏览(EMS)";
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnSetSingle
            // 
            this.btnSetSingle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnSetSingle.Location = new System.Drawing.Point(192, 16);
            this.btnSetSingle.Name = "btnSetSingle";
            this.btnSetSingle.Size = new System.Drawing.Size(99, 23);
            this.btnSetSingle.TabIndex = 21;
            this.btnSetSingle.Text = "单条为否(&R)";
            this.btnSetSingle.UseVisualStyleBackColor = false;
            this.btnSetSingle.Click += new System.EventHandler(this.btnSetSingle_Click);
            // 
            // cmbSelect
            // 
            this.cmbSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelect.FormattingEnabled = true;
            this.cmbSelect.Items.AddRange(new object[] {
            "100",
            "200",
            "300",
            "400",
            "500"});
            this.cmbSelect.Location = new System.Drawing.Point(117, 19);
            this.cmbSelect.Name = "cmbSelect";
            this.cmbSelect.Size = new System.Drawing.Size(121, 22);
            this.cmbSelect.TabIndex = 23;
            this.cmbSelect.SelectedIndexChanged += new System.EventHandler(this.cmbSelect_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 14);
            this.label5.TabIndex = 25;
            this.label5.Text = "设置显示条数：";
            // 
            // button_setmulfalse
            // 
            this.button_setmulfalse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button_setmulfalse.Location = new System.Drawing.Point(299, 17);
            this.button_setmulfalse.Name = "button_setmulfalse";
            this.button_setmulfalse.Size = new System.Drawing.Size(101, 23);
            this.button_setmulfalse.TabIndex = 26;
            this.button_setmulfalse.Text = "多条为否";
            this.button_setmulfalse.UseVisualStyleBackColor = false;
            this.button_setmulfalse.Click += new System.EventHandler(this.button_setmulfalse_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox1.Controls.Add(this.label_mynum);
            this.groupBox1.Controls.Add(this.label_printuser);
            this.groupBox1.Controls.Add(this.button_getallprintok);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label_oknum);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label_allnum);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(568, 199);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(535, 43);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印数量提示：";
            // 
            // label_mynum
            // 
            this.label_mynum.AutoSize = true;
            this.label_mynum.Location = new System.Drawing.Point(355, 20);
            this.label_mynum.Name = "label_mynum";
            this.label_mynum.Size = new System.Drawing.Size(14, 14);
            this.label_mynum.TabIndex = 6;
            this.label_mynum.Text = "0";
            // 
            // label_printuser
            // 
            this.label_printuser.AutoSize = true;
            this.label_printuser.Location = new System.Drawing.Point(262, 21);
            this.label_printuser.Name = "label_printuser";
            this.label_printuser.Size = new System.Drawing.Size(0, 14);
            this.label_printuser.TabIndex = 5;
            // 
            // button_getallprintok
            // 
            this.button_getallprintok.BackColor = System.Drawing.Color.OrangeRed;
            this.button_getallprintok.ForeColor = System.Drawing.Color.Black;
            this.button_getallprintok.Location = new System.Drawing.Point(399, 11);
            this.button_getallprintok.Name = "button_getallprintok";
            this.button_getallprintok.Size = new System.Drawing.Size(105, 26);
            this.button_getallprintok.TabIndex = 31;
            this.button_getallprintok.Text = "查看所有订单";
            this.button_getallprintok.UseVisualStyleBackColor = false;
            this.button_getallprintok.Click += new System.EventHandler(this.button_getallprintok_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(310, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 14);
            this.label8.TabIndex = 4;
            this.label8.Text = "打印：";
            // 
            // label_oknum
            // 
            this.label_oknum.AutoSize = true;
            this.label_oknum.Location = new System.Drawing.Point(201, 21);
            this.label_oknum.Name = "label_oknum";
            this.label_oknum.Size = new System.Drawing.Size(14, 14);
            this.label_oknum.TabIndex = 3;
            this.label_oknum.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(137, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 14);
            this.label7.TabIndex = 2;
            this.label7.Text = "已经打印：";
            // 
            // label_allnum
            // 
            this.label_allnum.AutoSize = true;
            this.label_allnum.Location = new System.Drawing.Point(79, 21);
            this.label_allnum.Name = "label_allnum";
            this.label_allnum.Size = new System.Drawing.Size(14, 14);
            this.label_allnum.TabIndex = 1;
            this.label_allnum.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 14);
            this.label6.TabIndex = 0;
            this.label6.Text = "订单总数：";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.SlateGray;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView2.Location = new System.Drawing.Point(3, 60);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(530, 87);
            this.dataGridView2.TabIndex = 32;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Controls.Add(this.btnQuery);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtOrderNo);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(567, 43);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(536, 150);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "订单详细信息";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox3.Controls.Add(this.crystalReportViewer1);
            this.groupBox3.Location = new System.Drawing.Point(568, 249);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(535, 522);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "打印预览";
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = 0;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(3, 17);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ReportSource = this.EMS1;
            this.crystalReportViewer1.Size = new System.Drawing.Size(529, 502);
            this.crystalReportViewer1.TabIndex = 9;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox4.Controls.Add(this.button_jiechu);
            this.groupBox4.Controls.Add(this.button_download);
            this.groupBox4.Controls.Add(this.button_gethdname);
            this.groupBox4.Controls.Add(this.comboBox_hdname);
            this.groupBox4.Controls.Add(this.cmbShopMethod);
            this.groupBox4.Controls.Add(this.label_hdname);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.cmbSelect);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.ForeColor = System.Drawing.Color.Black;
            this.groupBox4.Location = new System.Drawing.Point(13, 41);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(546, 112);
            this.groupBox4.TabIndex = 35;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "第1步";
            // 
            // button_jiechu
            // 
            this.button_jiechu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button_jiechu.Location = new System.Drawing.Point(439, 73);
            this.button_jiechu.Name = "button_jiechu";
            this.button_jiechu.Size = new System.Drawing.Size(101, 31);
            this.button_jiechu.TabIndex = 31;
            this.button_jiechu.Text = "解除锁定";
            this.button_jiechu.UseVisualStyleBackColor = false;
            this.button_jiechu.Click += new System.EventHandler(this.button_jiechu_Click);
            // 
            // button_download
            // 
            this.button_download.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button_download.Location = new System.Drawing.Point(265, 73);
            this.button_download.Name = "button_download";
            this.button_download.Size = new System.Drawing.Size(136, 32);
            this.button_download.TabIndex = 30;
            this.button_download.Text = "下载并锁定订单(&X)";
            this.button_download.UseVisualStyleBackColor = false;
            this.button_download.Click += new System.EventHandler(this.button_download_Click);
            // 
            // button_gethdname
            // 
            this.button_gethdname.Location = new System.Drawing.Point(439, 41);
            this.button_gethdname.Name = "button_gethdname";
            this.button_gethdname.Size = new System.Drawing.Size(86, 23);
            this.button_gethdname.TabIndex = 29;
            this.button_gethdname.Text = "刷新活动名称";
            this.button_gethdname.UseVisualStyleBackColor = true;
            this.button_gethdname.Click += new System.EventHandler(this.button_gethdname_Click);
            // 
            // comboBox_hdname
            // 
            this.comboBox_hdname.FormattingEnabled = true;
            this.comboBox_hdname.Location = new System.Drawing.Point(117, 45);
            this.comboBox_hdname.MaxLength = 500;
            this.comboBox_hdname.Name = "comboBox_hdname";
            this.comboBox_hdname.Size = new System.Drawing.Size(316, 22);
            this.comboBox_hdname.TabIndex = 27;
            this.comboBox_hdname.SelectedIndexChanged += new System.EventHandler(this.comboBox_hdname_SelectedIndexChanged);
            // 
            // label_hdname
            // 
            this.label_hdname.AutoSize = true;
            this.label_hdname.Location = new System.Drawing.Point(8, 50);
            this.label_hdname.Name = "label_hdname";
            this.label_hdname.Size = new System.Drawing.Size(105, 14);
            this.label_hdname.TabIndex = 28;
            this.label_hdname.Text = "选择活动名称：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 14);
            this.label9.TabIndex = 15;
            this.label9.Text = "选择快递类型：";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnPrint);
            this.groupBox8.Controls.Add(this.label1);
            this.groupBox8.Controls.Add(this.txtPrintPiece);
            this.groupBox8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox8.ForeColor = System.Drawing.Color.Black;
            this.groupBox8.Location = new System.Drawing.Point(13, 159);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(207, 82);
            this.groupBox8.TabIndex = 39;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "第2步";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btnSave);
            this.groupBox9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox9.ForeColor = System.Drawing.Color.Black;
            this.groupBox9.Location = new System.Drawing.Point(12, 247);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(135, 48);
            this.groupBox9.TabIndex = 40;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "第4步";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.label2);
            this.groupBox10.Controls.Add(this.textBox_reprint);
            this.groupBox10.Controls.Add(this.btnSetSingle);
            this.groupBox10.Controls.Add(this.button_setmulfalse);
            this.groupBox10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox10.ForeColor = System.Drawing.Color.Black;
            this.groupBox10.Location = new System.Drawing.Point(153, 249);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(406, 46);
            this.groupBox10.TabIndex = 41;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "重新打印";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 28;
            this.label2.Text = "订单号：";
            // 
            // textBox_reprint
            // 
            this.textBox_reprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox_reprint.Location = new System.Drawing.Point(73, 16);
            this.textBox_reprint.MaxLength = 200;
            this.textBox_reprint.Name = "textBox_reprint";
            this.textBox_reprint.Size = new System.Drawing.Size(113, 23);
            this.textBox_reprint.TabIndex = 27;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.SteelBlue;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.帮助ToolStripMenuItem,
            this.检查自动发货ToolStripMenuItem,
            this.库存ToolStripMenuItem,
            this.用户管理ToolStripMenuItem,
            this.订单修改与废弃ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1028, 25);
            this.menuStrip1.TabIndex = 42;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.活动信息设置ToolStripMenuItem,
            this.帮助信息ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "菜单";
            // 
            // 活动信息设置ToolStripMenuItem
            // 
            this.活动信息设置ToolStripMenuItem.Name = "活动信息设置ToolStripMenuItem";
            this.活动信息设置ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.活动信息设置ToolStripMenuItem.Text = "活动信息设置";
            this.活动信息设置ToolStripMenuItem.Click += new System.EventHandler(this.活动信息设置ToolStripMenuItem_Click);
            // 
            // 帮助信息ToolStripMenuItem
            // 
            this.帮助信息ToolStripMenuItem.Name = "帮助信息ToolStripMenuItem";
            this.帮助信息ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.帮助信息ToolStripMenuItem.Text = "帮助信息";
            this.帮助信息ToolStripMenuItem.Click += new System.EventHandler(this.帮助信息ToolStripMenuItem_Click);
            // 
            // 检查自动发货ToolStripMenuItem
            // 
            this.检查自动发货ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.没点发货汇总ToolStripMenuItem});
            this.检查自动发货ToolStripMenuItem.Name = "检查自动发货ToolStripMenuItem";
            this.检查自动发货ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.检查自动发货ToolStripMenuItem.Text = "检查自动发货";
            // 
            // 没点发货汇总ToolStripMenuItem
            // 
            this.没点发货汇总ToolStripMenuItem.Name = "没点发货汇总ToolStripMenuItem";
            this.没点发货汇总ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.没点发货汇总ToolStripMenuItem.Text = "没点发货汇总";
            this.没点发货汇总ToolStripMenuItem.Click += new System.EventHandler(this.没点发货汇总ToolStripMenuItem_Click);
            // 
            // 库存ToolStripMenuItem
            // 
            this.库存ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.库存与订单ToolStripMenuItem,
            this.出库ToolStripMenuItem});
            this.库存ToolStripMenuItem.Name = "库存ToolStripMenuItem";
            this.库存ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.库存ToolStripMenuItem.Text = "库存";
            // 
            // 库存与订单ToolStripMenuItem
            // 
            this.库存与订单ToolStripMenuItem.Name = "库存与订单ToolStripMenuItem";
            this.库存与订单ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.库存与订单ToolStripMenuItem.Text = "库存与订单";
            this.库存与订单ToolStripMenuItem.Click += new System.EventHandler(this.formkucunMenuItem_Click);
            // 
            // 出库ToolStripMenuItem
            // 
            this.出库ToolStripMenuItem.Name = "出库ToolStripMenuItem";
            this.出库ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.出库ToolStripMenuItem.Text = "出库";
            // 
            // 用户管理ToolStripMenuItem
            // 
            this.用户管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新增用户ToolStripMenuItem,
            this.子账号ToolStripMenuItem,
            this.密码修改ToolStripMenuItem});
            this.用户管理ToolStripMenuItem.Name = "用户管理ToolStripMenuItem";
            this.用户管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.用户管理ToolStripMenuItem.Text = "用户管理";
            // 
            // 新增用户ToolStripMenuItem
            // 
            this.新增用户ToolStripMenuItem.Name = "新增用户ToolStripMenuItem";
            this.新增用户ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.新增用户ToolStripMenuItem.Text = "新增用户";
            // 
            // 子账号ToolStripMenuItem
            // 
            this.子账号ToolStripMenuItem.Name = "子账号ToolStripMenuItem";
            this.子账号ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.子账号ToolStripMenuItem.Text = "新增子账号";
            // 
            // 密码修改ToolStripMenuItem
            // 
            this.密码修改ToolStripMenuItem.Name = "密码修改ToolStripMenuItem";
            this.密码修改ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.密码修改ToolStripMenuItem.Text = "密码修改";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 730);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1028, 23);
            this.statusStrip1.TabIndex = 43;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(53, 18);
            this.toolStripStatusLabel1.Text = "start---";
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.textBox1);
            this.groupBox5.Controls.Add(this.btnSelect);
            this.groupBox5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox5.Location = new System.Drawing.Point(226, 160);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(333, 81);
            this.groupBox5.TabIndex = 44;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "第3步";
            // 
            // 订单修改与废弃ToolStripMenuItem
            // 
            this.订单修改与废弃ToolStripMenuItem.Name = "订单修改与废弃ToolStripMenuItem";
            this.订单修改与废弃ToolStripMenuItem.Size = new System.Drawing.Size(104, 21);
            this.订单修改与废弃ToolStripMenuItem.Text = "订单修改与废弃";
            this.订单修改与废弃ToolStripMenuItem.Click += new System.EventHandler(this.订单修改与废弃ToolStripMenuItem_Click);
            // 
            // DeliveryPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1028, 753);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimizeBox = false;
            this.Name = "DeliveryPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "柠檬绿茶团购打印20130517";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.DeliveryPrint_Resize);
            this.ResizeEnd += new System.EventHandler(this.DeliveryPrint_ResizeEnd);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtPrintPiece;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbShopMethod;
        private System.Windows.Forms.Button btnPrint;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private WindowsFormsApplication1.EMS EMS1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOrderNo;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnSetSingle;
        private System.Windows.Forms.ComboBox cmbSelect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_setmulfalse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_allnum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_mynum;
        private System.Windows.Forms.Label label_printuser;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label_oknum;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_getallprintok;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.TextBox textBox_reprint;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助信息ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox_hdname;
        private System.Windows.Forms.Label label_hdname;
        private System.Windows.Forms.Button button_gethdname;
        private System.Windows.Forms.Button button_download;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_jiechu;
        private System.Windows.Forms.ToolStripMenuItem 活动信息设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 检查自动发货ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 没点发货汇总ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 库存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 库存与订单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 出库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 用户管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新增用户ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 子账号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 密码修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 订单修改与废弃ToolStripMenuItem;
    }
}

