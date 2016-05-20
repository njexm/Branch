using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.service.branch;
using Branch.com.proem.exm.util;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Branch.com.proem.exm.window.order
{
    /// <summary>
    /// 选择单据页面
    /// </summary>
    public partial class CDQueryList : Form
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(CDQueryList));

        private MemberChoose memberChoose;

        private CustomerDelivery customerDelivery;
        /// <summary>
        /// 输入的关键词
        /// </summary>
        private string keyStr = null;
        /// <summary>
        /// 选取单元格的行
        /// </summary>
        private int row = -1;
        /// <summary>
        /// 选取单元格的列
        /// </summary>
        private int column = -1;

        /// <summary>
        /// 当前页面的工作模式
        /// 1、结算
        /// 2、退款
        /// 3、零售
        /// 定义在constant常量类中
        /// </summary>
        private string WorkMode;

        public CDQueryList()
        {
            InitializeComponent();
        }

        public CDQueryList(CustomerDelivery obj, string keyStr)
        {
            InitializeComponent();
            this.customerDelivery = obj;
            this.keyStr = keyStr;
        }

        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="choose">会员信息选择winform</param>
        /// <param name="keyStr">会员id</param>
        /// <param name="WorkMode">工作模式</param>
        /// <param name="obj">customerDelivery winform</param>
        public CDQueryList(MemberChoose choose, string keyStr, string WorkMode, CustomerDelivery obj)
        {
            InitializeComponent();
            this.memberChoose = choose;
            this.customerDelivery = obj;
            this.keyStr = keyStr;
            this.WorkMode = WorkMode;
        }

        private void CDQueryList_Load(object sender, EventArgs e)
        {
            searchTextBox.Focus();
            button1_Click(this, EventArgs.Empty);            
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            chooseDocket();
            memberChoose.Close();
            this.Close();
        }

        private void leaveButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            row = listDataGridView.CurrentCell.RowIndex;
            column = listDataGridView.CurrentCell.ColumnIndex;
        }

        private void listDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            row = listDataGridView.CurrentCell.RowIndex;
            column = listDataGridView.CurrentCell.ColumnIndex;
            chooseDocket();
            this.Close();
        }

        private void CDQueryList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Y || e.KeyCode == Keys.Enter)
            {
                okButton_Click(this, EventArgs.Empty);
            }
            if (e.KeyCode == Keys.L || e.KeyCode == Keys.Escape)
            {
                leaveButton_Click(this, EventArgs.Empty);
            }
            if(e.KeyCode == Keys.A){
                button1_Click(this, EventArgs.Empty);
            }
            if (e.KeyCode == Keys.Up)
            {
                if (listDataGridView.DataSource == null || listDataGridView.RowCount <= 1)
                {
                    return;
                }
                int index = listDataGridView.CurrentRow.Index;
                int count = listDataGridView.RowCount;
                if (index > 0)
                {
                    listDataGridView.Rows[index - 1].Selected = true;
                    listDataGridView.CurrentCell = listDataGridView.Rows[index - 1].Cells[1];
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (listDataGridView.DataSource == null || listDataGridView.RowCount <= 1)
                {
                    return;
                }
                int index = listDataGridView.CurrentRow.Index;
                int count = listDataGridView.RowCount;
                if (index < count - 1)
                {
                    listDataGridView.Rows[index + 1].Selected = true;
                    listDataGridView.CurrentCell = listDataGridView.Rows[index + 1].Cells[1];
                }
            }
        }

        /// <summary>
        /// 将选中的单据内的数据传入页面
        /// </summary>
        private void chooseDocket()
        {
            customerDelivery.initNumberAndAmount();
            string id = listDataGridView.CurrentRow.Cells[5].Value.ToString();
            BranchZcOrderTransitService branchService = new BranchZcOrderTransitService();
            ZcOrderTransit zcOrderTransit = branchService.FindById(id);
            customerDelivery.showTransitOrder(zcOrderTransit);
        }

        private void listDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //行号
            using (SolidBrush b = new SolidBrush(this.listDataGridView.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string searchText = searchTextBox.Text;
            try
            {
                string sql = "select e.id,e.ORDERNUM ,e.ORDERAMOUNT,e.CONSIGNEE,e.CANSIGNPHONE,f.ASSOCIATOR_CARDNUMBER "
                    + " From zc_order_transit e "
                    + " LEFT JOIN zc_associator_info f on e.member_id = f.id "
                    + " WHERE member_id = '" + keyStr + "' and e.orderstatus = '" + Constant.ORDER_STATUS_RECEIPT + "' and e.orderNum like '%"+searchText+"%'";
                MysqlDBHelper dbHelper = new MysqlDBHelper();
                DataSet ds = dbHelper.GetDataSet(sql, "zc_goods_master");
                listDataGridView.AutoGenerateColumns = false;
                listDataGridView.DataSource = ds;
                listDataGridView.DataMember = "zc_goods_master";
            }
            catch (Exception ex)
            {
                log.Error("加载数据源发生异常", ex);
            }
            
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            button1_Click(this, EventArgs.Empty);
        }

        private void CDQueryList_Activated(object sender, EventArgs e)
        {
            searchTextBox.Focus();
        }

        private void searchTextBox_Leave(object sender, EventArgs e)
        {
            searchTextBox.Focus();
        }
    }
}
