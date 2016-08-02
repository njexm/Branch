using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.service.branch;
using Branch.com.proem.exm.util;
using Branch.com.proem.exm.window.branchOut;
using Branch.com.proem.exm.window.require;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Branch.com.proem.exm.window.retreat
{
    /// <summary>
    /// 选择供应商
    /// </summary>
    public partial class RGSupplierChoose : Form
    {
        public RGSupplierChoose()
        {
            InitializeComponent();
        }

        private BranchOutForm branchOutForm;

        public RGSupplierChoose(BranchOutForm obj)
        {
            InitializeComponent();
            this.branchOutForm = obj;
        }

        /// <summary>
        /// 要货单
        /// </summary>
        private BranchZcRequire branchZcRequire;

        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="obj"></param>
        public RGSupplierChoose(BranchZcRequire obj)
        {
            InitializeComponent();
            this.branchZcRequire = obj;
        }
        /// <summary>
        /// 已有订单
        /// </summary>
        private RGReturnList rGReturnList;

        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="obj"></param>
        public RGSupplierChoose(RGReturnList obj)
        {
            InitializeComponent();
            this.rGReturnList = obj;
        }
        /// <summary>
        /// 重载确认收货
        /// </summary>
        private ChooserCommitList chooserCommitList;

        public RGSupplierChoose(ChooserCommitList obj)
        {
            InitializeComponent();
            this.chooserCommitList = obj;
        }
        /// <summary>
        /// 选择分店信息返回到表中
        /// </summary>
        private void chooseBranchTotalInfo() {

            ZcBranchTotal zcBranchTotal = new ZcBranchTotal();
            zcBranchTotal.Id = supplierDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            zcBranchTotal.BranchCode = supplierDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            zcBranchTotal.BranchName = supplierDataGridView.SelectedRows[0].Cells[2].Value.ToString();

            if (branchZcRequire != null)
            {
                try
                {
                    this.branchZcRequire.addBranchToTalInfo(zcBranchTotal);
                }
                catch (NullReferenceException e)
                {
                    chooseBranchTotalInfo2();
                }
            }
            else if (branchOutForm != null)
            {
                this.branchOutForm.AddBranchTotalInfo(zcBranchTotal);
            }
            
            
           

        }
        private void chooseBranchTotalInfo2()
        {

            ZcBranchTotal zcBranchTotal = new ZcBranchTotal();
            zcBranchTotal.Id = supplierDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            zcBranchTotal.BranchCode = supplierDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            zcBranchTotal.BranchName = supplierDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            this.rGReturnList.addBranchInfo(zcBranchTotal);

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            chooseBranchTotalInfo();
            this.Close();
        }

        private void leaveButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RGSupplierChoose_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Q)
            //{
            //    queryButton_Click(this, EventArgs.Empty);
            //}
            if (e.KeyCode == Keys.Y)
            {
                okButton_Click(this, EventArgs.Empty);
            }
            if (e.KeyCode == Keys.L)
            {
                leaveButton_Click(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// 关键词点击搜索，如果关键词不为空就根据参数查，如果不为空就查所有
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void queryButton_Click(object sender, EventArgs e)
        {
            //if (supplierTextBox.Text.Equals(""))
            //{
            //    BranchZcRequireService branchZcRequireService = new BranchZcRequireService();
            //    branchZcRequireService.findAllBranchInfo();
            //}
            //else {
            //    BranchZcRequireService branchZcRequireService = new BranchZcRequireService();
            //    branchZcRequireService.findZcBranchTotalByParams(findBranchTotalById());
            //}

            supplierDataGridView.DataSource = GetData(); ;
            supplierDataGridView.DataMember = "zc_branch_total";
        }
        /// <summary>
        /// 关键词输入返回方法
        /// </summary>
        /// <returns></returns>
        private string findBranchTotalById() {
            string str = supplierTextBox.Text;
            string conditions = "";
            if (!str.Equals(""))
            {
                conditions += " and zbt.branch_name like '%" + supplierTextBox.Text + "%' or zbt.branch_code like '%" + supplierTextBox.Text + "%'";
                
            }
            return conditions;
           
        }

       

        private void supplierTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 供应商窗口加载的时候就展示所有分店信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RGSupplierChoose_Load(object sender, EventArgs e)
        {
            //BranchZcRequireService branchZcRequireService = new BranchZcRequireService();
            //List<ZcBranchTotal> list = branchZcRequireService.findAllBranchInfo();
            //for (int i = 0; i < list.Count; i++)
            //{
            //    bool isExist = false;
            //    ZcBranchTotal zcBranchTotal = list[i];
            //    for (int j = 0; j < supplierDataGridView.RowCount; j++)
            //    {
            //        if (supplierDataGridView[0, j].Value.ToString().Equals(zcBranchTotal.BranchCode))
            //        {
            //            MessageBox.Show("编号为" + zcBranchTotal.BranchCode + "的" + zcBranchTotal.BranchName + "已经在第" + (j + 1) + "行，请不要重复添加!");
            //            return;
            //        }

            //    }
            //    if (!isExist)
            //    {

            //        supplierDataGridView.Rows.Add(new Object[] { zcBranchTotal.BranchCode, zcBranchTotal.BranchName, Guid.NewGuid().ToString(), zcBranchTotal.Id });


            //    }

            //}

            DataSet ds = GetData();
            supplierDataGridView.DataSource = ds;
            supplierDataGridView.DataMember = "zc_branch_total";
        }

        private DataSet GetData()
        {
            DataSet ds = new DataSet();
            OracleUtil oracleUtil = new OracleUtil();
            string searchString = supplierTextBox.Text;
            string sql = "select id,branch_code, branch_name from zc_branch_total ";
            if(! string.IsNullOrEmpty(searchString.Trim()))
            {
                sql += " where branch_code like '%"+searchString+"%' or branch_name like '%"+searchString+"%'"; 
            }
            ds = oracleUtil.GetDataSet(sql, "zc_branch_total");
            return ds;
        }
        /// <summary>
        /// 用户双击选中行的时候调用确定按钮的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void supplierDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            okButton_Click(okButton,new EventArgs());
        }
    }
}
