namespace Branch.com.proem.exm.window.branchOut
{
    partial class BranchOutForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.main = new System.Windows.Forms.Panel();
            this.datapanel = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.serialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.goodsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.goodsSpecifications = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nums = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.goodsFileId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.checkbutton = new System.Windows.Forms.Button();
            this.savebutton = new System.Windows.Forms.Button();
            this.openbutton = new System.Windows.Forms.Button();
            this.newButton = new System.Windows.Forms.Button();
            this.numLabel = new System.Windows.Forms.Label();
            this.sumNumsTitlelabel = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.addGoodsButton = new System.Windows.Forms.Button();
            this.chooseBranchButton = new System.Windows.Forms.Button();
            this.branchTextbox = new System.Windows.Forms.TextBox();
            this.branchLabel = new System.Windows.Forms.Label();
            this.fromTextbox = new System.Windows.Forms.TextBox();
            this.fromlabel = new System.Windows.Forms.Label();
            this.remarkTextbox = new System.Windows.Forms.TextBox();
            this.remarkLabel = new System.Windows.Forms.Label();
            this.oddTextbox = new System.Windows.Forms.TextBox();
            this.oddlabel = new System.Windows.Forms.Label();
            this.logoPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.salesmanNameLabel = new System.Windows.Forms.Label();
            this.leaveButton = new System.Windows.Forms.Button();
            this.inNameLabel = new System.Windows.Forms.Label();
            this.timePanel = new System.Windows.Forms.Panel();
            this.chooseBranchDiffButton = new System.Windows.Forms.Button();
            this.main.SuspendLayout();
            this.datapanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.logoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // main
            // 
            this.main.BackgroundImage = global::Branch.Properties.Resources.login_bg_0;
            this.main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.main.Controls.Add(this.datapanel);
            this.main.Controls.Add(this.groupBox1);
            this.main.Controls.Add(this.logoPanel);
            this.main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main.Location = new System.Drawing.Point(0, 0);
            this.main.Name = "main";
            this.main.Size = new System.Drawing.Size(1024, 564);
            this.main.TabIndex = 0;
            // 
            // datapanel
            // 
            this.datapanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datapanel.Controls.Add(this.dataGridView);
            this.datapanel.Location = new System.Drawing.Point(0, 210);
            this.datapanel.Margin = new System.Windows.Forms.Padding(0);
            this.datapanel.Name = "datapanel";
            this.datapanel.Size = new System.Drawing.Size(1022, 300);
            this.datapanel.TabIndex = 32;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridView.ColumnHeadersHeight = 26;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serialNumber,
            this.goodsName,
            this.goodsSpecifications,
            this.nums,
            this.price,
            this.money,
            this.goodsFileId,
            this.id});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView.RowTemplate.Height = 26;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(1022, 300);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView_CellFormatting);
            this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
            this.dataGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView_RowPostPaint);
            // 
            // serialNumber
            // 
            this.serialNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.serialNumber.DataPropertyName = "serialNumber";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.serialNumber.DefaultCellStyle = dataGridViewCellStyle11;
            this.serialNumber.FillWeight = 150F;
            this.serialNumber.HeaderText = "货号";
            this.serialNumber.Name = "serialNumber";
            this.serialNumber.ReadOnly = true;
            // 
            // goodsName
            // 
            this.goodsName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.goodsName.DataPropertyName = "GOODS_NAME";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.goodsName.DefaultCellStyle = dataGridViewCellStyle12;
            this.goodsName.FillWeight = 200F;
            this.goodsName.HeaderText = "商品名";
            this.goodsName.Name = "goodsName";
            this.goodsName.ReadOnly = true;
            // 
            // goodsSpecifications
            // 
            this.goodsSpecifications.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.goodsSpecifications.DataPropertyName = "goods_Specifications";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.goodsSpecifications.DefaultCellStyle = dataGridViewCellStyle13;
            this.goodsSpecifications.FillWeight = 150F;
            this.goodsSpecifications.HeaderText = "规格";
            this.goodsSpecifications.Name = "goodsSpecifications";
            this.goodsSpecifications.ReadOnly = true;
            // 
            // nums
            // 
            this.nums.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nums.DataPropertyName = "nums";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.nums.DefaultCellStyle = dataGridViewCellStyle14;
            this.nums.HeaderText = "数量";
            this.nums.Name = "nums";
            // 
            // price
            // 
            this.price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.price.DataPropertyName = "price";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.price.DefaultCellStyle = dataGridViewCellStyle15;
            this.price.HeaderText = "单价";
            this.price.Name = "price";
            this.price.ReadOnly = true;
            // 
            // money
            // 
            this.money.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.money.DataPropertyName = "money";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.money.DefaultCellStyle = dataGridViewCellStyle16;
            this.money.HeaderText = "金额";
            this.money.Name = "money";
            this.money.ReadOnly = true;
            // 
            // goodsFileId
            // 
            this.goodsFileId.DataPropertyName = "goodsfile_id";
            this.goodsFileId.HeaderText = "goodsFileId";
            this.goodsFileId.Name = "goodsFileId";
            this.goodsFileId.Visible = false;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.groupBox1.Controls.Add(this.chooseBranchDiffButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.checkbutton);
            this.groupBox1.Controls.Add(this.savebutton);
            this.groupBox1.Controls.Add(this.openbutton);
            this.groupBox1.Controls.Add(this.newButton);
            this.groupBox1.Controls.Add(this.numLabel);
            this.groupBox1.Controls.Add(this.sumNumsTitlelabel);
            this.groupBox1.Controls.Add(this.deleteButton);
            this.groupBox1.Controls.Add(this.addGoodsButton);
            this.groupBox1.Controls.Add(this.chooseBranchButton);
            this.groupBox1.Controls.Add(this.branchTextbox);
            this.groupBox1.Controls.Add(this.branchLabel);
            this.groupBox1.Controls.Add(this.fromTextbox);
            this.groupBox1.Controls.Add(this.fromlabel);
            this.groupBox1.Controls.Add(this.remarkTextbox);
            this.groupBox1.Controls.Add(this.remarkLabel);
            this.groupBox1.Controls.Add(this.oddTextbox);
            this.groupBox1.Controls.Add(this.oddlabel);
            this.groupBox1.Location = new System.Drawing.Point(0, 78);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1022, 130);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(735, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 123;
            this.label1.Text = "选择差异单:";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(75)))), ((int)(((byte)(71)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(572, 84);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 30);
            this.button1.TabIndex = 122;
            this.button1.Text = "删除单据(F5)";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkbutton
            // 
            this.checkbutton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.checkbutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(75)))), ((int)(((byte)(71)))));
            this.checkbutton.FlatAppearance.BorderSize = 0;
            this.checkbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkbutton.ForeColor = System.Drawing.Color.White;
            this.checkbutton.Location = new System.Drawing.Point(272, 84);
            this.checkbutton.Name = "checkbutton";
            this.checkbutton.Size = new System.Drawing.Size(80, 30);
            this.checkbutton.TabIndex = 121;
            this.checkbutton.Text = "审核(F4)";
            this.checkbutton.UseVisualStyleBackColor = false;
            this.checkbutton.Click += new System.EventHandler(this.checkbutton_Click);
            // 
            // savebutton
            // 
            this.savebutton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.savebutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(75)))), ((int)(((byte)(71)))));
            this.savebutton.FlatAppearance.BorderSize = 0;
            this.savebutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.savebutton.ForeColor = System.Drawing.Color.White;
            this.savebutton.Location = new System.Drawing.Point(182, 84);
            this.savebutton.Name = "savebutton";
            this.savebutton.Size = new System.Drawing.Size(80, 30);
            this.savebutton.TabIndex = 120;
            this.savebutton.Text = "保存(F3)";
            this.savebutton.UseVisualStyleBackColor = false;
            this.savebutton.Click += new System.EventHandler(this.savebutton_Click);
            // 
            // openbutton
            // 
            this.openbutton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.openbutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(75)))), ((int)(((byte)(71)))));
            this.openbutton.FlatAppearance.BorderSize = 0;
            this.openbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openbutton.ForeColor = System.Drawing.Color.White;
            this.openbutton.Location = new System.Drawing.Point(96, 84);
            this.openbutton.Name = "openbutton";
            this.openbutton.Size = new System.Drawing.Size(80, 30);
            this.openbutton.TabIndex = 119;
            this.openbutton.Text = "打开(F2)";
            this.openbutton.UseVisualStyleBackColor = false;
            this.openbutton.Click += new System.EventHandler(this.openbutton_Click);
            // 
            // newButton
            // 
            this.newButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.newButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(75)))), ((int)(((byte)(71)))));
            this.newButton.FlatAppearance.BorderSize = 0;
            this.newButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newButton.ForeColor = System.Drawing.Color.White;
            this.newButton.Location = new System.Drawing.Point(10, 84);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(80, 30);
            this.newButton.TabIndex = 118;
            this.newButton.Text = "新建(F1)";
            this.newButton.UseVisualStyleBackColor = false;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // numLabel
            // 
            this.numLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numLabel.AutoSize = true;
            this.numLabel.Font = new System.Drawing.Font("宋体", 15F);
            this.numLabel.Location = new System.Drawing.Point(915, 86);
            this.numLabel.Name = "numLabel";
            this.numLabel.Size = new System.Drawing.Size(49, 20);
            this.numLabel.TabIndex = 117;
            this.numLabel.Text = "0.00";
            // 
            // sumNumsTitlelabel
            // 
            this.sumNumsTitlelabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sumNumsTitlelabel.AutoSize = true;
            this.sumNumsTitlelabel.Font = new System.Drawing.Font("宋体", 15F);
            this.sumNumsTitlelabel.Location = new System.Drawing.Point(810, 86);
            this.sumNumsTitlelabel.Name = "sumNumsTitlelabel";
            this.sumNumsTitlelabel.Size = new System.Drawing.Size(99, 20);
            this.sumNumsTitlelabel.TabIndex = 116;
            this.sumNumsTitlelabel.Text = "合计数量:";
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(75)))), ((int)(((byte)(71)))));
            this.deleteButton.FlatAppearance.BorderSize = 0;
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteButton.ForeColor = System.Drawing.Color.White;
            this.deleteButton.Location = new System.Drawing.Point(466, 84);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(100, 30);
            this.deleteButton.TabIndex = 115;
            this.deleteButton.Text = "删除商品(del)";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // addGoodsButton
            // 
            this.addGoodsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.addGoodsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(75)))), ((int)(((byte)(71)))));
            this.addGoodsButton.FlatAppearance.BorderSize = 0;
            this.addGoodsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addGoodsButton.ForeColor = System.Drawing.Color.White;
            this.addGoodsButton.Location = new System.Drawing.Point(361, 84);
            this.addGoodsButton.Name = "addGoodsButton";
            this.addGoodsButton.Size = new System.Drawing.Size(100, 30);
            this.addGoodsButton.TabIndex = 114;
            this.addGoodsButton.Text = "添加商品(A)";
            this.addGoodsButton.UseVisualStyleBackColor = false;
            this.addGoodsButton.Click += new System.EventHandler(this.addGoodsButton_Click);
            // 
            // chooseBranchButton
            // 
            this.chooseBranchButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chooseBranchButton.BackColor = System.Drawing.Color.White;
            this.chooseBranchButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.chooseBranchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chooseBranchButton.Location = new System.Drawing.Point(429, 45);
            this.chooseBranchButton.Name = "chooseBranchButton";
            this.chooseBranchButton.Size = new System.Drawing.Size(32, 20);
            this.chooseBranchButton.TabIndex = 113;
            this.chooseBranchButton.Text = "…";
            this.chooseBranchButton.UseVisualStyleBackColor = false;
            this.chooseBranchButton.Click += new System.EventHandler(this.chooseBranchButton_Click);
            // 
            // branchTextbox
            // 
            this.branchTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.branchTextbox.Font = new System.Drawing.Font("宋体", 12F);
            this.branchTextbox.Location = new System.Drawing.Point(481, 42);
            this.branchTextbox.Name = "branchTextbox";
            this.branchTextbox.ReadOnly = true;
            this.branchTextbox.Size = new System.Drawing.Size(200, 26);
            this.branchTextbox.TabIndex = 7;
            // 
            // branchLabel
            // 
            this.branchLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.branchLabel.AutoSize = true;
            this.branchLabel.Font = new System.Drawing.Font("宋体", 12F);
            this.branchLabel.Location = new System.Drawing.Point(343, 45);
            this.branchLabel.Name = "branchLabel";
            this.branchLabel.Size = new System.Drawing.Size(80, 16);
            this.branchLabel.TabIndex = 6;
            this.branchLabel.Text = "调出分店:";
            // 
            // fromTextbox
            // 
            this.fromTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.fromTextbox.Font = new System.Drawing.Font("宋体", 12F);
            this.fromTextbox.Location = new System.Drawing.Point(100, 42);
            this.fromTextbox.Name = "fromTextbox";
            this.fromTextbox.ReadOnly = true;
            this.fromTextbox.Size = new System.Drawing.Size(200, 26);
            this.fromTextbox.TabIndex = 5;
            // 
            // fromlabel
            // 
            this.fromlabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.fromlabel.AutoSize = true;
            this.fromlabel.Font = new System.Drawing.Font("宋体", 12F);
            this.fromlabel.Location = new System.Drawing.Point(10, 48);
            this.fromlabel.Name = "fromlabel";
            this.fromlabel.Size = new System.Drawing.Size(80, 16);
            this.fromlabel.TabIndex = 4;
            this.fromlabel.Text = "调出分店:";
            // 
            // remarkTextbox
            // 
            this.remarkTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.remarkTextbox.Font = new System.Drawing.Font("宋体", 12F);
            this.remarkTextbox.Location = new System.Drawing.Point(428, 10);
            this.remarkTextbox.Name = "remarkTextbox";
            this.remarkTextbox.Size = new System.Drawing.Size(350, 26);
            this.remarkTextbox.TabIndex = 3;
            // 
            // remarkLabel
            // 
            this.remarkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.remarkLabel.AutoSize = true;
            this.remarkLabel.Font = new System.Drawing.Font("宋体", 12F);
            this.remarkLabel.Location = new System.Drawing.Point(374, 15);
            this.remarkLabel.Name = "remarkLabel";
            this.remarkLabel.Size = new System.Drawing.Size(48, 16);
            this.remarkLabel.TabIndex = 2;
            this.remarkLabel.Text = "备注:";
            // 
            // oddTextbox
            // 
            this.oddTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.oddTextbox.Font = new System.Drawing.Font("宋体", 12F);
            this.oddTextbox.Location = new System.Drawing.Point(100, 10);
            this.oddTextbox.Name = "oddTextbox";
            this.oddTextbox.ReadOnly = true;
            this.oddTextbox.Size = new System.Drawing.Size(200, 26);
            this.oddTextbox.TabIndex = 1;
            // 
            // oddlabel
            // 
            this.oddlabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.oddlabel.AutoSize = true;
            this.oddlabel.Font = new System.Drawing.Font("宋体", 12F);
            this.oddlabel.Location = new System.Drawing.Point(10, 15);
            this.oddlabel.Name = "oddlabel";
            this.oddlabel.Size = new System.Drawing.Size(80, 16);
            this.oddlabel.TabIndex = 0;
            this.oddlabel.Text = "出库单号:";
            // 
            // logoPanel
            // 
            this.logoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logoPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.logoPanel.BackgroundImage = global::Branch.Properties.Resources.login_bg_2;
            this.logoPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logoPanel.Controls.Add(this.panel1);
            this.logoPanel.Controls.Add(this.panel4);
            this.logoPanel.Controls.Add(this.salesmanNameLabel);
            this.logoPanel.Controls.Add(this.leaveButton);
            this.logoPanel.Controls.Add(this.inNameLabel);
            this.logoPanel.Controls.Add(this.timePanel);
            this.logoPanel.Location = new System.Drawing.Point(0, 0);
            this.logoPanel.Margin = new System.Windows.Forms.Padding(0);
            this.logoPanel.Name = "logoPanel";
            this.logoPanel.Size = new System.Drawing.Size(1024, 75);
            this.logoPanel.TabIndex = 22;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel1.BackgroundImage = global::Branch.Properties.Resources.icon_user;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(719, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(36, 31);
            this.panel1.TabIndex = 30;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel4.BackgroundImage = global::Branch.Properties.Resources.logo;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Location = new System.Drawing.Point(20, 10);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 55);
            this.panel4.TabIndex = 29;
            // 
            // salesmanNameLabel
            // 
            this.salesmanNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.salesmanNameLabel.AutoSize = true;
            this.salesmanNameLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.salesmanNameLabel.Location = new System.Drawing.Point(764, 30);
            this.salesmanNameLabel.Name = "salesmanNameLabel";
            this.salesmanNameLabel.Size = new System.Drawing.Size(41, 12);
            this.salesmanNameLabel.TabIndex = 1;
            this.salesmanNameLabel.Text = "业务员";
            // 
            // leaveButton
            // 
            this.leaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.leaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(75)))), ((int)(((byte)(71)))));
            this.leaveButton.FlatAppearance.BorderSize = 0;
            this.leaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.leaveButton.ForeColor = System.Drawing.Color.White;
            this.leaveButton.Location = new System.Drawing.Point(932, 22);
            this.leaveButton.Name = "leaveButton";
            this.leaveButton.Size = new System.Drawing.Size(80, 30);
            this.leaveButton.TabIndex = 9;
            this.leaveButton.Text = "退出(Esc)";
            this.leaveButton.UseVisualStyleBackColor = false;
            this.leaveButton.Click += new System.EventHandler(this.leaveButton_Click);
            // 
            // inNameLabel
            // 
            this.inNameLabel.AutoSize = true;
            this.inNameLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.inNameLabel.Font = new System.Drawing.Font("宋体", 28F);
            this.inNameLabel.Location = new System.Drawing.Point(220, 16);
            this.inNameLabel.Name = "inNameLabel";
            this.inNameLabel.Size = new System.Drawing.Size(169, 38);
            this.inNameLabel.TabIndex = 20;
            this.inNameLabel.Text = "当前分店";
            // 
            // timePanel
            // 
            this.timePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.timePanel.Location = new System.Drawing.Point(584, 20);
            this.timePanel.Name = "timePanel";
            this.timePanel.Size = new System.Drawing.Size(119, 32);
            this.timePanel.TabIndex = 18;
            // 
            // chooseBranchDiffButton
            // 
            this.chooseBranchDiffButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chooseBranchDiffButton.BackColor = System.Drawing.Color.White;
            this.chooseBranchDiffButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.chooseBranchDiffButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chooseBranchDiffButton.Location = new System.Drawing.Point(837, 44);
            this.chooseBranchDiffButton.Name = "chooseBranchDiffButton";
            this.chooseBranchDiffButton.Size = new System.Drawing.Size(32, 20);
            this.chooseBranchDiffButton.TabIndex = 124;
            this.chooseBranchDiffButton.Text = "…";
            this.chooseBranchDiffButton.UseVisualStyleBackColor = false;
            this.chooseBranchDiffButton.Click += new System.EventHandler(this.chooseBranchDiffButton_Click);
            // 
            // BranchOutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 564);
            this.Controls.Add(this.main);
            this.Font = new System.Drawing.Font("宋体", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "BranchOutForm";
            this.Text = "BranchOut";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BranchOutForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BranchOutForm_KeyDown);
            this.main.ResumeLayout(false);
            this.datapanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.logoPanel.ResumeLayout(false);
            this.logoPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel main;
        private System.Windows.Forms.Panel logoPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label salesmanNameLabel;
        private System.Windows.Forms.Button leaveButton;
        private System.Windows.Forms.Label inNameLabel;
        private System.Windows.Forms.Panel timePanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label oddlabel;
        private System.Windows.Forms.TextBox oddTextbox;
        private System.Windows.Forms.Label remarkLabel;
        private System.Windows.Forms.TextBox remarkTextbox;
        private System.Windows.Forms.Label fromlabel;
        private System.Windows.Forms.TextBox fromTextbox;
        private System.Windows.Forms.Label branchLabel;
        private System.Windows.Forms.TextBox branchTextbox;
        private System.Windows.Forms.Button chooseBranchButton;
        private System.Windows.Forms.Button addGoodsButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Panel datapanel;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label sumNumsTitlelabel;
        private System.Windows.Forms.Label numLabel;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.Button openbutton;
        private System.Windows.Forms.Button savebutton;
        private System.Windows.Forms.Button checkbutton;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn goodsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn goodsSpecifications;
        private System.Windows.Forms.DataGridViewTextBoxColumn nums;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn money;
        private System.Windows.Forms.DataGridViewTextBoxColumn goodsFileId;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button chooseBranchDiffButton;
    }
}