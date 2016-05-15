using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.service.branch;
using Branch.com.proem.exm.util;
using Branch.com.proem.exm.window.order.controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Branch.com.proem.exm.window.order
{
    /// <summary>
    /// 会员信息
    /// </summary>
    public partial class MemberChoose : Form
    {
        /// <summary>
        /// 零售界面
        /// </summary>
        private CustomerDelivery customerDelivery;

        /// <summary>
        /// 工作模式
        /// </summary>
        private string workMode;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MemberChoose()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 重载构造函数
        /// </summary>
        /// <param name="customerDelivery"></param>
        public MemberChoose(CustomerDelivery customerDelivery, string workMode) 
        {
            InitializeComponent();
            this.customerDelivery = customerDelivery;
            this.workMode = workMode;
        }

        /// <summary>
        /// 页面初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MemberChoose_Load(object sender, EventArgs e)
        {
            searchTextbox.Focus();
        }

        /// <summary>
        /// 搜索会员信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string searchString = searchTextbox.Text;
            string sql = "select id, ASSOCIATOR_CARDNUMBER as cardNumber,ASSOCIATOR_NAME as name, ASSOCIATOR_MOBILEPHONE as phone "+
                "from ZC_ASSOCIATOR_INFO where ASSOCIATOR_NAME like '%" + searchString + "%' or ASSOCIATOR_MOBILEPHONE like '%" + searchString + "%' or ASSOCIATOR_CARDNUMBER like '%" + searchString + "%' order by id limit 50 ";
            MysqlDBHelper dbHelper = new MysqlDBHelper();
            DataSet ds = dbHelper.GetDataSet(sql, "ZC_ASSOCIATOR_INFO");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "ZC_ASSOCIATOR_INFO";
            dataGridView1.Focus();
        }

        /// <summary>
        /// 添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //行号
            using (SolidBrush b = new SolidBrush(this.dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 5, e.RowBounds.Location.Y + 4);
            }
        }

        /// <summary>
        /// 页面快捷键注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MemberChoose_KeyDown(object sender, KeyEventArgs e)
        {
            if(searchTextbox.Focused && e.KeyCode == Keys.Enter){
                button1_Click(this, EventArgs.Empty);
            }else if(dataGridView1.Focused && e.KeyCode == Keys.Enter){
                confirmButton_Click(this, EventArgs.Empty);
            }else if(e.KeyCode == Keys.F1){
                confirmButton_Click(this, EventArgs.Empty);
            }else if(e.KeyCode == Keys.Escape){
                cancelButton_Click(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 取消按钮， 关闭会员信息页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 确定按钮选择会员信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null || dataGridView1.RowCount == 0)
            {
                return;
            }
            AssociatorInfo obj = new AssociatorInfo();
            obj.Id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            obj.CardNumber = dataGridView1.CurrentRow.Cells[1].Value == null ? string.Empty : dataGridView1.CurrentRow.Cells[1].Value.ToString();
            obj.Name = dataGridView1.CurrentRow.Cells[2].Value == null ? string.Empty : dataGridView1.CurrentRow.Cells[2].Value.ToString();
            obj.MobilePhone = dataGridView1.CurrentRow.Cells[3].Value == null ? string.Empty : dataGridView1.CurrentRow.Cells[3].Value.ToString();
            if(workMode.Equals(Constant.RETAIL)){
                ///nothing todo 
            }else if(workMode.Equals(Constant.PICK_UP_GOODS)){
                ///查询该用户的订单有几条
                BranchZcOrderTransitService branchTransitService = new BranchZcOrderTransitService();
                int orderCounts = branchTransitService.GetOrderCount(obj.Id);
                if (orderCounts == 0)
                {
                    MessageBox.Show("暂无" + obj.Name + "提货的订单!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (orderCounts == 1)
                {
                    List<ZcOrderTransit> list = branchTransitService.FindByMemberId(obj.Id);
                    ZcOrderTransit zcOrderTransit = list[0];
                    customerDelivery.showTransitOrder(zcOrderTransit);
                }
                else
                {
                    CDQueryList cdQueryList = new CDQueryList(this, obj.Id, workMode, customerDelivery);
                    cdQueryList.ShowDialog();
                }
            }else if(workMode.Equals(Constant.REFUND))
            {
                BranchResaleService branchResaleService = new BranchResaleService();
                int count = branchResaleService.FindCountByMemberId(obj.Id);
                if(count == 0){
                    MessageBox.Show("暂无" + obj.Name + "的销售流水信息!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (count == 1)
                {
                    List<Resale> list = branchResaleService.FindByMemberId(obj.Id);
                    Resale resale = list[0];
                    customerDelivery.showRefundInfo(resale);
                }
                else 
                {
                    ResaleList resaleListForm = new ResaleList(this, obj.Id, customerDelivery);
                    resaleListForm.ShowDialog();
                }
            }
            customerDelivery.SetAssociatorInfo(obj);
            this.Close();
        }
    }
}
