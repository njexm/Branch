namespace Branch.com.proem.exm.window.order
{
    partial class CDQueryList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cDQueryListPanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.listTablePanel = new System.Windows.Forms.Panel();
            this.listDataGridView = new System.Windows.Forms.DataGridView();
            this.ORDERNUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDERAMOUNT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CONSIGNEE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CANSIGNPHONE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ASSOCIATOR_CARDNUMBER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leaveButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.cDQueryListPanel.SuspendLayout();
            this.listTablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // cDQueryListPanel
            // 
            this.cDQueryListPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cDQueryListPanel.BackgroundImage = global::Branch.Properties.Resources.login_bg_0;
            this.cDQueryListPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cDQueryListPanel.Controls.Add(this.button1);
            this.cDQueryListPanel.Controls.Add(this.label1);
            this.cDQueryListPanel.Controls.Add(this.searchTextBox);
            this.cDQueryListPanel.Controls.Add(this.listTablePanel);
            this.cDQueryListPanel.Controls.Add(this.leaveButton);
            this.cDQueryListPanel.Controls.Add(this.okButton);
            this.cDQueryListPanel.Location = new System.Drawing.Point(0, 0);
            this.cDQueryListPanel.Name = "cDQueryListPanel";
            this.cDQueryListPanel.Size = new System.Drawing.Size(784, 461);
            this.cDQueryListPanel.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(67)))), ((int)(((byte)(65)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(253, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 34);
            this.button1.TabIndex = 5;
            this.button1.Text = "查询A";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(67)))), ((int)(((byte)(65)))));
            this.label1.Location = new System.Drawing.Point(352, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "输入单号查询";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // searchTextBox
            // 
            this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.searchTextBox.Font = new System.Drawing.Font("宋体", 12F);
            this.searchTextBox.Location = new System.Drawing.Point(20, 17);
            this.searchTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(200, 26);
            this.searchTextBox.TabIndex = 3;
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            this.searchTextBox.Leave += new System.EventHandler(this.searchTextBox_Leave);
            // 
            // listTablePanel
            // 
            this.listTablePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listTablePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(234)))), ((int)(((byte)(243)))));
            this.listTablePanel.Controls.Add(this.listDataGridView);
            this.listTablePanel.Location = new System.Drawing.Point(0, 60);
            this.listTablePanel.Name = "listTablePanel";
            this.listTablePanel.Size = new System.Drawing.Size(784, 321);
            this.listTablePanel.TabIndex = 2;
            // 
            // listDataGridView
            // 
            this.listDataGridView.AllowUserToAddRows = false;
            this.listDataGridView.AllowUserToDeleteRows = false;
            this.listDataGridView.AllowUserToResizeColumns = false;
            this.listDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.listDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.listDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.listDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.listDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.listDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("宋体", 16F);
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.listDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.listDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.listDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ORDERNUM,
            this.ORDERAMOUNT,
            this.CONSIGNEE,
            this.CANSIGNPHONE,
            this.ASSOCIATOR_CARDNUMBER,
            this.id});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.listDataGridView.DefaultCellStyle = dataGridViewCellStyle15;
            this.listDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listDataGridView.EnableHeadersVisualStyles = false;
            this.listDataGridView.Location = new System.Drawing.Point(0, 0);
            this.listDataGridView.MultiSelect = false;
            this.listDataGridView.Name = "listDataGridView";
            this.listDataGridView.ReadOnly = true;
            this.listDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.listDataGridView.RowTemplate.Height = 23;
            this.listDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.listDataGridView.Size = new System.Drawing.Size(784, 321);
            this.listDataGridView.TabIndex = 0;
            this.listDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.listDataGridView_CellClick);
            this.listDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.listDataGridView_CellDoubleClick);
            this.listDataGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.listDataGridView_RowPostPaint);
            // 
            // ORDERNUM
            // 
            this.ORDERNUM.DataPropertyName = "ORDERNUM";
            this.ORDERNUM.HeaderText = "订单号";
            this.ORDERNUM.Name = "ORDERNUM";
            this.ORDERNUM.ReadOnly = true;
            this.ORDERNUM.Width = 200;
            // 
            // ORDERAMOUNT
            // 
            this.ORDERAMOUNT.DataPropertyName = "ORDERAMOUNT";
            this.ORDERAMOUNT.HeaderText = "金额";
            this.ORDERAMOUNT.Name = "ORDERAMOUNT";
            this.ORDERAMOUNT.ReadOnly = true;
            this.ORDERAMOUNT.Width = 120;
            // 
            // CONSIGNEE
            // 
            this.CONSIGNEE.DataPropertyName = "CONSIGNEE";
            this.CONSIGNEE.HeaderText = "姓名";
            this.CONSIGNEE.Name = "CONSIGNEE";
            this.CONSIGNEE.ReadOnly = true;
            this.CONSIGNEE.Width = 120;
            // 
            // CANSIGNPHONE
            // 
            this.CANSIGNPHONE.DataPropertyName = "CANSIGNPHONE";
            this.CANSIGNPHONE.HeaderText = "电话";
            this.CANSIGNPHONE.Name = "CANSIGNPHONE";
            this.CANSIGNPHONE.ReadOnly = true;
            this.CANSIGNPHONE.Width = 120;
            // 
            // ASSOCIATOR_CARDNUMBER
            // 
            this.ASSOCIATOR_CARDNUMBER.DataPropertyName = "ASSOCIATOR_CARDNUMBER";
            this.ASSOCIATOR_CARDNUMBER.HeaderText = "卡号";
            this.ASSOCIATOR_CARDNUMBER.Name = "ASSOCIATOR_CARDNUMBER";
            this.ASSOCIATOR_CARDNUMBER.ReadOnly = true;
            this.ASSOCIATOR_CARDNUMBER.Width = 180;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // leaveButton
            // 
            this.leaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.leaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(173)))), ((int)(((byte)(173)))));
            this.leaveButton.FlatAppearance.BorderSize = 0;
            this.leaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.leaveButton.ForeColor = System.Drawing.Color.White;
            this.leaveButton.Location = new System.Drawing.Point(407, 390);
            this.leaveButton.Name = "leaveButton";
            this.leaveButton.Size = new System.Drawing.Size(80, 35);
            this.leaveButton.TabIndex = 1;
            this.leaveButton.Text = "退出(L)";
            this.leaveButton.UseVisualStyleBackColor = false;
            this.leaveButton.Click += new System.EventHandler(this.leaveButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(67)))), ((int)(((byte)(65)))));
            this.okButton.FlatAppearance.BorderSize = 0;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okButton.ForeColor = System.Drawing.Color.White;
            this.okButton.Location = new System.Drawing.Point(297, 390);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(80, 35);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "确定(Y)";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // CDQueryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.cDQueryListPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CDQueryList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择单据";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.CDQueryList_Activated);
            this.Load += new System.EventHandler(this.CDQueryList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CDQueryList_KeyDown);
            this.cDQueryListPanel.ResumeLayout(false);
            this.cDQueryListPanel.PerformLayout();
            this.listTablePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel cDQueryListPanel;
        private System.Windows.Forms.Panel listTablePanel;
        private System.Windows.Forms.Button leaveButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.DataGridView listDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDERNUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDERAMOUNT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CONSIGNEE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CANSIGNPHONE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ASSOCIATOR_CARDNUMBER;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}