namespace Branch.com.proem.exm.window.retreat
{
    partial class RGSupplierChoose
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
            this.supplierPanel = new System.Windows.Forms.Panel();
            this.queryResultGroupBox = new System.Windows.Forms.GroupBox();
            this.okButton = new System.Windows.Forms.Button();
            this.leaveButton = new System.Windows.Forms.Button();
            this.supplierTablePanel = new System.Windows.Forms.Panel();
            this.supplierDataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.queryConditionGroupBox = new System.Windows.Forms.GroupBox();
            this.queryButton = new System.Windows.Forms.Button();
            this.supplierTextBox = new System.Windows.Forms.TextBox();
            this.supplierLabel = new System.Windows.Forms.Label();
            this.supplierPanel.SuspendLayout();
            this.queryResultGroupBox.SuspendLayout();
            this.supplierTablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.supplierDataGridView)).BeginInit();
            this.queryConditionGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // supplierPanel
            // 
            this.supplierPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.supplierPanel.Controls.Add(this.queryResultGroupBox);
            this.supplierPanel.Controls.Add(this.queryConditionGroupBox);
            this.supplierPanel.Location = new System.Drawing.Point(0, 0);
            this.supplierPanel.Name = "supplierPanel";
            this.supplierPanel.Size = new System.Drawing.Size(583, 360);
            this.supplierPanel.TabIndex = 2;
            // 
            // queryResultGroupBox
            // 
            this.queryResultGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.queryResultGroupBox.Controls.Add(this.okButton);
            this.queryResultGroupBox.Controls.Add(this.leaveButton);
            this.queryResultGroupBox.Controls.Add(this.supplierTablePanel);
            this.queryResultGroupBox.Location = new System.Drawing.Point(5, 65);
            this.queryResultGroupBox.Name = "queryResultGroupBox";
            this.queryResultGroupBox.Size = new System.Drawing.Size(572, 292);
            this.queryResultGroupBox.TabIndex = 1;
            this.queryResultGroupBox.TabStop = false;
            this.queryResultGroupBox.Text = "查询结果";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(440, 264);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(60, 22);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "确定(Y)";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // leaveButton
            // 
            this.leaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.leaveButton.Location = new System.Drawing.Point(506, 264);
            this.leaveButton.Name = "leaveButton";
            this.leaveButton.Size = new System.Drawing.Size(60, 22);
            this.leaveButton.TabIndex = 1;
            this.leaveButton.Text = "退出(L)";
            this.leaveButton.UseVisualStyleBackColor = true;
            this.leaveButton.Click += new System.EventHandler(this.leaveButton_Click);
            // 
            // supplierTablePanel
            // 
            this.supplierTablePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.supplierTablePanel.Controls.Add(this.supplierDataGridView);
            this.supplierTablePanel.Location = new System.Drawing.Point(4, 19);
            this.supplierTablePanel.Name = "supplierTablePanel";
            this.supplierTablePanel.Size = new System.Drawing.Size(564, 240);
            this.supplierTablePanel.TabIndex = 0;
            // 
            // supplierDataGridView
            // 
            this.supplierDataGridView.AllowUserToAddRows = false;
            this.supplierDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.supplierDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.id});
            this.supplierDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.supplierDataGridView.Location = new System.Drawing.Point(0, 0);
            this.supplierDataGridView.MultiSelect = false;
            this.supplierDataGridView.Name = "supplierDataGridView";
            this.supplierDataGridView.RowTemplate.Height = 23;
            this.supplierDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.supplierDataGridView.Size = new System.Drawing.Size(564, 240);
            this.supplierDataGridView.TabIndex = 0;
            this.supplierDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.supplierDataGridView_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "branch_code";
            this.Column1.HeaderText = "分店编号";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "branch_name";
            this.Column2.FillWeight = 0.5F;
            this.Column2.HeaderText = "分店名称";
            this.Column2.Name = "Column2";
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // queryConditionGroupBox
            // 
            this.queryConditionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.queryConditionGroupBox.Controls.Add(this.queryButton);
            this.queryConditionGroupBox.Controls.Add(this.supplierTextBox);
            this.queryConditionGroupBox.Controls.Add(this.supplierLabel);
            this.queryConditionGroupBox.Location = new System.Drawing.Point(5, 2);
            this.queryConditionGroupBox.Name = "queryConditionGroupBox";
            this.queryConditionGroupBox.Size = new System.Drawing.Size(573, 58);
            this.queryConditionGroupBox.TabIndex = 0;
            this.queryConditionGroupBox.TabStop = false;
            this.queryConditionGroupBox.Text = "查询条件";
            // 
            // queryButton
            // 
            this.queryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.queryButton.Font = new System.Drawing.Font("宋体", 14F);
            this.queryButton.Location = new System.Drawing.Point(481, 15);
            this.queryButton.Name = "queryButton";
            this.queryButton.Size = new System.Drawing.Size(55, 29);
            this.queryButton.TabIndex = 30;
            this.queryButton.Text = "查询";
            this.queryButton.UseVisualStyleBackColor = true;
            this.queryButton.Click += new System.EventHandler(this.queryButton_Click);
            // 
            // supplierTextBox
            // 
            this.supplierTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.supplierTextBox.Font = new System.Drawing.Font("宋体", 14F);
            this.supplierTextBox.Location = new System.Drawing.Point(109, 15);
            this.supplierTextBox.Name = "supplierTextBox";
            this.supplierTextBox.Size = new System.Drawing.Size(354, 29);
            this.supplierTextBox.TabIndex = 29;
            this.supplierTextBox.TextChanged += new System.EventHandler(this.supplierTextBox_TextChanged);
            // 
            // supplierLabel
            // 
            this.supplierLabel.AutoSize = true;
            this.supplierLabel.Font = new System.Drawing.Font("宋体", 14F);
            this.supplierLabel.Location = new System.Drawing.Point(7, 20);
            this.supplierLabel.Name = "supplierLabel";
            this.supplierLabel.Size = new System.Drawing.Size(104, 19);
            this.supplierLabel.TabIndex = 28;
            this.supplierLabel.Text = "要货分店：";
            // 
            // RGSupplierChoose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.supplierPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RGSupplierChoose";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择要货分店";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.RGSupplierChoose_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RGSupplierChoose_KeyDown);
            this.supplierPanel.ResumeLayout(false);
            this.queryResultGroupBox.ResumeLayout(false);
            this.supplierTablePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.supplierDataGridView)).EndInit();
            this.queryConditionGroupBox.ResumeLayout(false);
            this.queryConditionGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel supplierPanel;
        private System.Windows.Forms.GroupBox queryResultGroupBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button leaveButton;
        private System.Windows.Forms.Panel supplierTablePanel;
        private System.Windows.Forms.DataGridView supplierDataGridView;
        private System.Windows.Forms.GroupBox queryConditionGroupBox;
        private System.Windows.Forms.Button queryButton;
        private System.Windows.Forms.TextBox supplierTextBox;
        private System.Windows.Forms.Label supplierLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
    }
}