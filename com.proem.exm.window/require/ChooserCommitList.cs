using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.util;
using Branch.com.proem.exm.window.require;
using Branch.com.proem.exm.window.retreat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Branch.com.proem.exm.window.require
{
    /// <summary>
    /// 打开已有退货单
    /// </summary>
    public partial class ChooserCommitList : Form
    {
        public ChooserCommitList()
        {
            InitializeComponent();
        }

        

        private BranchZcRequire branchZcRequire;

        public ChooserCommitList(BranchZcRequire obj)
        {
            InitializeComponent();
            this.branchZcRequire = obj;
        }
        
        /// <summary>
        /// 分店id
        /// </summary>
        private string branchId = "";
        /// <summary>
        /// 添加分店信息
        /// </summary>
        /// <param name="zcBranchTotal"></param>
        public void addBranchInfo(ZcBranchTotal zcBranchTotal)
        {
            textBox1.Text = zcBranchTotal.Id;
            supplierIdTextBox.Text = zcBranchTotal.BranchName;
            branchId = zcBranchTotal.Id;
        }

        public void addUserInfo(ZcUserInfo zcUserInfo) {
            operatorTextBox.Text = zcUserInfo.UserName;
        }


        /// <summary>
        /// 将表格内的数据传入ReturnGoods页面
        /// </summary>
        private void choosegoods()
        {
            ZcRequire zcRequire = new ZcRequire();
            zcRequire.id = returnDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            zcRequire.yhdNumber = returnDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            ///创建时间的转换，但是传到页面处理是报null；
            string str=returnDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            DateTime time = Convert.ToDateTime(str);
            zcRequire.createTime = time;
           
            zcRequire.nums = returnDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            zcRequire.money = returnDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            zcRequire.checkMan = returnDataGridView.SelectedRows[0].Cells[5].Value.ToString();
            
            zcRequire.remark = returnDataGridView.SelectedRows[0].Cells[6].Value.ToString();

            zcRequire.branchId = returnDataGridView.SelectedRows[0].Cells[7].Value.ToString();

            zcRequire.calloutBranchId = returnDataGridView.SelectedRows[0].Cells[9].Value.ToString();

            zcRequire.userId = returnDataGridView.SelectedRows[0].Cells[8].Value.ToString();

            zcRequire.status = Int32.Parse( returnDataGridView.SelectedRows[0].Cells[10].Value.ToString());
            zcRequire.reason =  returnDataGridView.SelectedRows[0].Cells[11].Value.ToString();
            this.branchZcRequire.AddZcRequireInfo(zcRequire);

        }

        private void supplierButton_Click(object sender, EventArgs e)
        {
            RGSupplierChoose rGSupplierChoose = new RGSupplierChoose(this);
            rGSupplierChoose.ShowDialog();//选择供应商
        }

        private void operatorButton_Click(object sender, EventArgs e)
        {
            RGROperatorChoose rGROperatorChoose = new RGROperatorChoose(this);
            rGROperatorChoose.Show();//选择操作员
        }

        private void RGReturnList_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Y)
            {
                okButton_Click(this, EventArgs.Empty);
            }
            if (e.KeyCode == Keys.L)
            {
                leaveButton_Click(this, EventArgs.Empty);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {

            choosegoods();
            this.Close();
            
            
        }

        private void leaveButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void queryButton_Click(object sender, EventArgs e)
        {
           
            returnDataGridView.DataSource = GetData();
            returnDataGridView.DataMember = "zc_require";
        }

        

        private DataSet GetData()
        {
            DataSet ds = new DataSet();
            OracleUtil oracleUtil = new OracleUtil();
            string searchString = docketIdTextBox.Text.ToString();
            string startTime = startDateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss ");
            
            string endTime = endDateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss ");
            
            string LocalBranchId = LoginUserInfo.branchId.ToString();
            string requestBrach = textBox1.Text.ToString();
            float minMoney = 0;
            if (moneyTextBox.Text.ToString().Equals(""))
            {
                minMoney = 0;
            }
            else {
                minMoney = float.Parse(moneyTextBox.Text.ToString());
            }
           
            string doMember = operatorTextBox.Text.ToString();
            string sql = "select id, createTime,YHD_NUMBER, nums, money,check_man,remark,user_id, branch_id,callout_branch_id,status,reason FROM zc_require zr where zr.status = 2 ";

            if (!string.IsNullOrEmpty(searchString.Trim()))
            {
                sql += " and zr.createtime between to_date('" + startTime + "','yyyy-mm-dd hh24:mi:ss')  and to_date( '" + endTime + "','yyyy-mm-dd hh24:mi:ss')  AND zr.YHD_NUMBER like '%" + searchString + "%' AND zr.BRANCH_ID like '%" + LocalBranchId + "%' AND zr.callout_branch_id like '%" + requestBrach + "%' AND zr.user_id like '%" + doMember + "%' and zr.money>=" + minMoney + "";
            }
            ds = oracleUtil.GetDataSet(sql, "zc_require");
            return ds;
        }

        private void startDateTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void warehouseButton_Click(object sender, EventArgs e)
        {

        }

        private void RGReturnList_Load(object sender, EventArgs e)
        {
            warehouseTextBox.Text = LoginUserInfo.branchName;
            returnDataGridView.DataSource = GetData();
            returnDataGridView.DataMember = "zc_require";
        }


        private void returnDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e == null || e.Value == null || !(sender is DataGridView))
            {
                return;
            }

            DataGridView view = (DataGridView)sender;
            try { 
                if(view.Columns[e.ColumnIndex].DataPropertyName=="status"){
                    int val = Convert.ToInt32(e.Value);
                    switch(val){
                        case 0:
                            e.Value="待提交";
                            break;
                        case 1:
                            e.Value="待审核";
                            break;
                        case 2:
                            e.Value="审核通过";
                            break;
                        case 3:
                            e.Value="待处理";
                            break;
                        case 4:
                            e.Value="完成";
                            break;
                        case 5:
                            e.Value="作废";
                            break;
                        e.FormattingApplied = true ;

                    }
                }
            }catch(System.Exception ex){
                e.FormattingApplied = false;
                MessageBox.Show(ToString());

            }finally{
            
            }
        }
        /// <summary>
        /// 双击一行是触发确定点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void returnDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            okButton_Click(okButton,new EventArgs());
        }
    }
}
