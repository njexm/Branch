using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.service.branch;
using Branch.com.proem.exm.util;
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
    public partial class ResaleList : Form
    {
        private CustomerDelivery customerDelivery;

        public ResaleList()
        {
            InitializeComponent();
        }

        public ResaleList(CustomerDelivery customerDelivery)
        {
            InitializeComponent();
            this.customerDelivery = customerDelivery;
        }

        private void ResaleList_Load(object sender, EventArgs e)
        {
            button1_Click(this, EventArgs.Empty);
        }

        private void ResaleList_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape){
                this.Close();
            }else if(e.KeyCode == Keys.Enter){
                okButton_Click(this, EventArgs.Empty);
            }else if(e.KeyCode == Keys.A){
                button1_Click(this, EventArgs.Empty);
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (dataGridView1.DataSource == null || dataGridView1.RowCount <= 1)
                {
                    return;
                }
                int index = dataGridView1.CurrentRow.Index;
                int count = dataGridView1.RowCount;
                if (index > 0)
                {
                    dataGridView1.Rows[index - 1].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[index - 1].Cells[1];
                }
            }
            else if (e.KeyCode == Keys.Down)
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
                    dataGridView1.CurrentCell = dataGridView1.Rows[index + 1].Cells[1];
                }
            }
        }

        /// <summary>
        /// 确定按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            if(dataGridView1.DataSource == null || dataGridView1.RowCount == 0){
                return;
            }
            string id = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            BranchResaleService service = new BranchResaleService();
            Resale obj = service.FindById(id);
            customerDelivery.showRefundInfo(obj);
            this.Close();
        }

        /// <summary>
        /// 行号显示
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
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string search = searchTextBox.Text;
            string sql = "select a.id, a.water_number as waterNumber, a.createTime,a.actual_money as money, a.order_id, b.ASSOCIATOR_NAME as name from zc_resale a left join zc_associator_info b "
                + " on a.member_id = b.ID left join zc_order_history c on a.order_id = c.id where a.water_number not like 'KTH%' and( b.associator_Name like '%" + search + "%' or b.associator_CardNumber like '%" + search + "%' or b.associator_Mobilephone like '%" + search + "%' or a.water_number like '%" + search + "%' or c.orderNum like '%" + search + "%') order by a.createTime desc";
            MysqlDBHelper dbHelper = new MysqlDBHelper();
            DataSet ds = dbHelper.GetDataSet(sql, "zc_resale");
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "zc_resale";
        }

        private void ResaleList_Activated(object sender, EventArgs e)
        {
            searchTextBox.Focus();
        }

        private void searchTextBox_Leave(object sender, EventArgs e)
        {
            searchTextBox.Focus();
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            button1_Click(this, EventArgs.Empty);
        }
    }
}
