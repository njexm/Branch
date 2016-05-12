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

            ///当前工作模式为提货状态
            if (WorkMode == Constant.PICK_UP_GOODS)
            {
                try
                {
                    //string sql = "select e.CONSIGNEE AS '姓名',e.CANSIGNPHONE AS '电话',f.ASSOCIATOR_CARDNUMBER AS '卡号',e.ORDERNUM as '订单号',e.ORDERAMOUNT as '金额'"
                    string sql = "select e.id,e.ORDERNUM ,e.ORDERAMOUNT,e.CONSIGNEE,e.CANSIGNPHONE,f.ASSOCIATOR_CARDNUMBER "
                        + " From zc_order_transit e "
                        + " LEFT JOIN zc_associator_info f on e.member_id = f.id "
                        + " WHERE member_id = '"+keyStr+"' and e.orderstatus = '" + Constant.ORDER_STATUS_RECEIPT + "' ";
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
            ///当前工作模式为退款状态
            if (WorkMode == Constant.REFUND)
            {
                try
                {
                    //string sql = "select e.CONSIGNEE AS '姓名',e.CANSIGNPHONE AS '电话',f.ASSOCIATOR_CARDNUMBER AS '卡号',e.ORDERNUM as '订单号',e.ORDERAMOUNT as '金额'"
                    string sql = "select e.id,e.ORDERNUM ,e.ORDERAMOUNT,e.CONSIGNEE,e.CANSIGNPHONE,f.ASSOCIATOR_CARDNUMBER,e.ACTUAL_MONEY "
                        + " From zc_order_history e "
                        + " LEFT JOIN zc_associator_info f on e.member_id = f.id "
                        + " WHERE  e.orderstatus not in ('" + Constant.ORDER_STATUS_ALL_REFUSE + "','" + Constant.ORDER_STATUS_ALL_REFUND + "','" + Constant.ORDER_STATUS_PART_REFUND + "') ";
                        //+ " WHERE  e.orderstatus in ('" + Constant.ORDER_STATUS_FININSH + "') ";
                    if (!keyStr.Equals(""))
                    {
                        sql += " and ( f.associator_cardnumber like '%" + keyStr + "%' or e.consignee like '%" + keyStr + "%' or e.cansignphone like '%" + keyStr + "%' ) ";
                    }
                    //MessageBox.Show(sql); 
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

                listDataGridView.CurrentCell = null;//不默认选中
            }
        }

        /// <summary>
        /// 关键词查询
        /// </summary>
        /// <returns></returns>
        private string addTextConditions()
        {
            string conditions = "";
            if (!keyStr.Equals(""))
            {
                conditions += " and f.associator_cardnumber like '%" + keyStr + "%' or e.consignee like '%" + keyStr + "%' or e.cansignphone like '%" + keyStr + "%' ";
            }
            return conditions;
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
            ///当前工作模式为提货状态
            if (WorkMode == Constant.PICK_UP_GOODS)
            {
                customerDelivery.showTransitOrder(zcOrderTransit);
            }
            /////当前工作模式为退款状态
            //if (WorkMode == Constant.REFUND)
            //{
            //    string actual_moneycard = listDataGridView.CurrentRow.Cells[5].Value.ToString();
            //    customerDelivery.setOrderNum(order_Num);
            //    customerDelivery.setinform(order_Num, actual_moneycard, name, phone, card);
            //}
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
    }
}
