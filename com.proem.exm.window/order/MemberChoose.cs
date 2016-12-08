using Branch.com.proem.exm.dao.branch;
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
            button1_Click(this, EventArgs.Empty);
        }

        /// <summary>
        /// 搜索会员信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string searchString = searchTextbox.Text;
            string sql = "select id, MEMBERNAME as name, MEMBERMOBILE as phone " +
                "from zc_member where MEMBERNAME like '%" + searchString + "%' or MEMBERMOBILE like '%" + searchString + "%' order by id limit 50 ";
            MysqlDBHelper dbHelper = new MysqlDBHelper();
            DataSet ds = dbHelper.GetDataSet(sql, "zc_member");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "zc_member";
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
            if(e.KeyCode == Keys.Enter){
                confirmButton_Click(this, EventArgs.Empty);
            }else if(e.KeyCode == Keys.F1){
                confirmButton_Click(this, EventArgs.Empty);
            }else if(e.KeyCode == Keys.Escape){
                cancelButton_Click(this, EventArgs.Empty);
            }else if(e.KeyCode == Keys.Up)
            {
                if (dataGridView1.DataSource == null || dataGridView1.RowCount <= 1)
                {
                    return;
                }
                int index = dataGridView1.CurrentRow.Index;
                int count = dataGridView1.RowCount;
                if(index > 0){
                    dataGridView1.Rows[index-1].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[index - 1].Cells[2];
                }
            }else if(e.KeyCode == Keys.Down)
            {
                if (dataGridView1.DataSource == null || dataGridView1.RowCount <= 1)
                {
                    return;
                }
                int index = dataGridView1.CurrentRow.Index;
                int count = dataGridView1.RowCount;
                if (index < count - 1)
                {
                    dataGridView1.Rows[index + 1].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[index + 1].Cells[2];
                }
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
                if (workMode.Equals(Constant.PICK_UP_GOODS))
                {
                    BranchZcOrderTransitService branchTransitService = new BranchZcOrderTransitService();
                    int counts = branchTransitService.getCountBy(searchTextbox.Text);
                    if (counts == 0)
                    {
                        MessageBox.Show("暂无" + searchTextbox.Text + "需要提货的订单!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (counts == 1)
                    {
                        List<ZcOrderTransit> list = branchTransitService.FindByCondition(searchTextbox.Text);
                        ZcOrderTransit zcOrderTransit = list[0];
                        customerDelivery.showTransitOrder(zcOrderTransit);
                        this.Close();
                    }
                    else
                    {
                        CDQueryList cdQueryList = new CDQueryList(this, searchTextbox.Text, workMode, customerDelivery, 1);
                        cdQueryList.ShowDialog();
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
               // AssociatorInfo obj = new AssociatorInfo();
                ZcMember obj = new ZcMember();
                obj.id = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                BranchZcMemberDao branchZcMemberDao = new BranchZcMemberDao();
                obj = branchZcMemberDao.FindById(obj.id);
                //obj.CardNumber = dataGridView1.CurrentRow.Cells[1].Value == null ? string.Empty : dataGridView1.CurrentRow.Cells[1].Value.ToString();
                //obj.memberName = dataGridView1.CurrentRow.Cells[2].Value == null ? string.Empty : dataGridView1.CurrentRow.Cells[2].Value.ToString();
                //obj.memberMobile = dataGridView1.CurrentRow.Cells[3].Value == null ? string.Empty : dataGridView1.CurrentRow.Cells[3].Value.ToString();
                if (workMode.Equals(Constant.RETAIL))
                {
                    ///nothing todo 
                }
                else if (workMode.Equals(Constant.PICK_UP_GOODS))
                {
                    ///查询该用户的订单有几条
                    BranchZcOrderTransitService branchTransitService = new BranchZcOrderTransitService();
                    int orderCounts = branchTransitService.GetOrderCount(obj.id);
                    if (orderCounts == 0)
                    {
                        int counts = branchTransitService.getCountBy(searchTextbox.Text);
                        if (counts == 0)
                        {
                            MessageBox.Show("暂无" + searchTextbox.Text + "需要提货的订单!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else if (counts == 1)
                        {
                            List<ZcOrderTransit> list = branchTransitService.FindByCondition(searchTextbox.Text);
                            ZcOrderTransit zcOrderTransit = list[0];
                            customerDelivery.showTransitOrder(zcOrderTransit);
                            this.Close();
                        }
                        else
                        {
                            CDQueryList cdQueryList = new CDQueryList(this, searchTextbox.Text, workMode, customerDelivery, 1);
                            cdQueryList.ShowDialog();
                        }
                    }
                    else if (orderCounts == 1)
                    {
                        List<ZcOrderTransit> list = branchTransitService.FindByMemberId(obj.id);
                        ZcOrderTransit zcOrderTransit = list[0];
                        customerDelivery.showTransitOrder(zcOrderTransit);
                    }
                    else
                    {
                        CDQueryList cdQueryList = new CDQueryList(this, obj.id, workMode, customerDelivery, 0);
                        cdQueryList.ShowDialog();
                    }
                }
                else if (workMode.Equals(Constant.REFUND))
                {
                    BranchResaleService branchResaleService = new BranchResaleService();
                    int count = branchResaleService.FindCountByMemberId(obj.id);
                    if (count == 0)
                    {
                        MessageBox.Show("暂无" + obj.memberName + "的销售流水信息!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (count == 1)
                    {
                        List<Resale> list = branchResaleService.FindByMemberId(obj.id);
                        Resale resale = list[0];
                        customerDelivery.showRefundInfo(resale);
                    }
                    else
                    {

                    }
                }
                customerDelivery.SetAssociatorInfo(obj);
                this.Close();
            }
           
        }

        private void searchTextbox_TextChanged(object sender, EventArgs e)
        {
            button1_Click(this, EventArgs.Empty);
        }

        private void MemberChoose_Activated(object sender, EventArgs e)
        {
            searchTextbox.Focus();
        }

        private void searchTextbox_Leave(object sender, EventArgs e)
        {
            searchTextbox.Focus();
        }
    }
}
