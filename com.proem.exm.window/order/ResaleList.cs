using Branch.com.proem.exm.domain;
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
        private MemberChoose memberChoose;

        private CustomerDelivery customerDelivery;

        private string memberId;

        public ResaleList()
        {
            InitializeComponent();
        }

        public ResaleList(MemberChoose memberChoose, string memberId, CustomerDelivery customerDelivery)
        {
            InitializeComponent();
            this.memberChoose = memberChoose;
            this.customerDelivery = customerDelivery;
            this.memberId = memberId;
        }

        private void ResaleList_Load(object sender, EventArgs e)
        {
            string sql = "select a.id, a.water_number as waterNumber, a.createTime,a.actual_money as money, a.order_id, b.ASSOCIATOR_NAME as name from zc_resale a left join zc_associator_info b "
                +" on a.member_id = b.ID where a.member_id = '"+memberId+"' order by a.createTime desc";
            MysqlDBHelper dbHelper = new MysqlDBHelper();
            DataSet ds = dbHelper.GetDataSet(sql, "zc_resale");
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "zc_resale";
        }

        private void ResaleList_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Y){
                okButton_Click(this, EventArgs.Empty);
            }else if(e.KeyCode == Keys.L){
                this.Close();
            }else if(e.KeyCode == Keys.Enter){
                okButton_Click(this, EventArgs.Empty);
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
            Resale obj = new Resale();
            obj.Id = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            obj.OrderId = dataGridView1.CurrentRow.Cells[3].Value == null ? string.Empty : dataGridView1.CurrentRow.Cells[3].Value.ToString();
            customerDelivery.showRefundInfo(obj);
            memberChoose.Close();
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
    }
}
