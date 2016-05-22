using Branch.com.proem.exm.window.main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Branch.com.proem.exm.util;
using MySql.Data.MySqlClient;
using Branch.com.proem.exm.window.util;
using log4net;
using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.service.main;
using Branch.com.proem.exm.service;
using Branch.com.proem.exm.service.master;
using Branch.com.proem.exm.service.branch;
using Branch.com.proem.exm.window.order.controller;
using System.Drawing.Printing;
using Branch.com.proem.exm.dao.branch;
using WxPayAPI;

namespace Branch.com.proem.exm.window.order
{
    /// <summary>
    /// 客户提货
    /// </summary>
    public partial class CustomerDelivery : Form
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(CustomerDelivery));

        /// <summary>
        /// 当前页面的工作模式
        /// 1、结算
        /// 2、退款
        /// 3、零售
        /// 定义在constant常量类中
        /// </summary>
        private string WorkMode;

        /// <summary>
        /// 扫码添加还是删除
        /// true 默认添加
        /// false 减去
        /// </summary>
        private bool AddOrDelete = true;

        /// <summary>
        /// 待打印的商品列表
        /// </summary>
        private List<GoodsPrint> printObjectlist = new List<GoodsPrint>();


        //ItemInput itemsInput = null;

        /// <summary>
        /// 选取单元格的行
        /// </summary>
        private int row = -1;
        /// <summary>
        /// 选取单元格的列
        /// </summary>
        private int column = -1;
        /// <summary>
        /// 数量合计
        /// </summary>
        private int totalSumValue = 0;
        /// <summary>
        /// 金额合计
        /// </summary>
        private float totalAmountValue = 0;
        /// <summary>
        /// 退款数量合计
        /// </summary>
        private int returnSum = 0;
        /// <summary>
        /// 退款金额合计
        /// </summary>
        private float returnAmount = 0;

        /// <summary>
        /// 实际付款总金额
        /// </summary>
        public string actualTotalMoney;

        ///// <summary>
        ///// 输入的关键词
        ///// </summary>
        //private string keyStr = null;
        /// <summary>
        /// 订单号
        /// </summary>
        private string order_Num = null;
        /// <summary>
        /// 字符串追加
        /// </summary>
        string conditon = "where 1=1 ";

        /// <summary>
        /// 标识是否处于零售状态
        /// true表示正在零售
        /// false标识不在零售
        /// </summary>
        private bool isResale = false;

        /// <summary>
        /// transit_id
        /// </summary>
        private string zc_order_transit_id;

        /// <summary>
        /// 会员信息类
        /// </summary>
        private AssociatorInfo associatorInfo;

        public delegate void child_close();
        public event child_close customer;

        public CustomerDelivery()
        {
            InitializeComponent();
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CustomerDelivery_Load(object sender, EventArgs e)
        {
            numberTextBox.Focus();
            resaleInit();

            inNameLabel.Text = LoginUserInfo.branchName;
            Times times = new Times();
            times.TopLevel = false;
            this.timePanel.Controls.Add(times);
            times.Show();
        }

        /// <summary>
        /// 行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //行号
            using (SolidBrush b = new SolidBrush(this.itemDataGridView.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 5, e.RowBounds.Location.Y + 4);
            }
        }

        ///// <summary>
        ///// 客户提货完成，保存事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void saveButton_Click(object sender, EventArgs e)
        //{
        //    string cardId = this.queryTextBox.Text.ToString().Trim();
        //    string name = this.cardNameTextBox.Text.ToString().Trim();
        //    string phone = this.phoneTextBox.Text.ToString().Trim();
        //    if (cardId == "" && phone == "" && name == "")
        //    {
        //        MessageBox.Show("没有检索到用户");
        //        return;
        //    }
        //    if(itemDataGridView.DataSource == null)
        //    {
        //        MessageBox.Show("该用户今天没有任何订单需要收取");
        //    }
        //    string coniditon = " where 1=1 ";
        //    if (cardId != "")
        //    {
        //        coniditon += " and b.associator_cardnumber = '"+cardId+"' ";
        //    }
        //    if (name != "")
        //    {
        //        coniditon += " and a.consignee = '"+name+"' ";
        //    }
        //    if (phone != "")
        //    {
        //        coniditon += " and a.cansignphone ='"+phone+"' ";
        //    }
        //    string sql = "select a.id from zc_order_transit a LEFT JOIN zc_associator_info b on a.member_id = b.id "+coniditon;
        //    CustomerDeliveryService customerDeliveryService = new CustomerDeliveryService();
        //    List<string> list = customerDeliveryService.FindOrderIdBySql(sql);
        //    //将用户提货完成后的订单和订单详情 挪动到history表中
        //    customerDeliveryService.UpdateStatusAndMoveOrder(list);

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        public void itemInput(string num)
        {
            this.itemDataGridView[4, row].Value = num;
            ////this.itemDataGridView.CurrentCell = itemDataGridView[4, row]; //单元格设置可编辑状态-
            ////itemDataGridView.BeginEdit(true);

            ////itemsInput.sureflag = false;
            //if (column == 7 && row != -1)
            //{
            //    itemDataGridView[column, row].Value = num;
            //}
            //else
            //{
            //    //string bar = numberTextBox.Text;
            //    //QueryGoods query = new QueryGoods();
            //    //int item = query.queryExistGood(bar, "", this, log);//查询后获得
            //    //if (item == 0)
            //    //{
            //    //    MessageBox.Show("商品不存在,请重新输入", "提示", MessageBoxButtons.OK);
            //    //}
            //    //else if (item == 1)
            //    //{
            //    //    numberTextBox.Text = "";
            //    //}
            //    //else if (item > 1 && item < 50)
            //    //{
            //    //    DGSGood dGSGood = new DGSGood(this, bar, "");
            //    //    dGSGood.ShowDialog();
            //    //    numberTextBox.Text = "";
            //    //}
            //    //else if (item >= 50)
            //    //{
            //    //    MessageBox.Show("匹配的商品记录数大于[50]条，请输入更多内容以减少匹配范围", "提示", MessageBoxButtons.OK);
            //    //}
            //}
            column = -1;
            row = -1;
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void queryButton_Click(object sender, EventArgs e)
        {
            ///当前工作模式为提货状态
            if (WorkMode == Constant.PICK_UP_GOODS)
            {
                ///获取查询条件
                //string searchString = queryTextBox.Text.ToString().Trim();
                string searchString = "";
                CustomerDeliveryController controller = new CustomerDeliveryController();
                int orderCounts = controller.GetOrderCounts(searchString);
                if (orderCounts == 0)
                {
                    MessageBox.Show("暂无待客户提货的订单!");
                    return;
                }
                else if (orderCounts == 1)
                {
                    //controller.GetOrderInfo(searchString, this);
                    /////直接根据orderNumber获取此订单下的商品展示
                    //DataSet ds = controller.GetGoodDataSetById(id_.Text.ToString());
                    //itemDataGridView.AutoGenerateColumns = false;
                    //itemDataGridView.DataSource = ds;
                    //itemDataGridView.DataMember = "zc_order_transit";
                    //itemDataGridView.CurrentCell = null;//不默认选中
                    /////初始化显示份数差异
                    //initDifference();
                }
                else
                {
                    //CDQueryList cdQueryList = new CDQueryList(this, searchString, WorkMode);
                    //cdQueryList.ShowDialog();
                }
                ///焦点移动到扫码框内
                numberTextBox.Focus();
            }

            ///当前工作模式为退款状态时，在状态为已完成的单据当日单据内查询
            if (WorkMode == Constant.REFUND)
            {//GetReturnOrderCounts
                ///获取查询条件
                //string searchString = queryTextBox.Text.ToString().Trim();
                string searchString = "";
                CustomerDeliveryController controller = new CustomerDeliveryController();
                int orderCounts = controller.GetReturnOrderCounts(searchString);
                if (orderCounts == 0)
                {
                    MessageBox.Show("该关键词下暂无已结算的订单!");
                    return;
                }
                else if (orderCounts == 1)
                {
                    controller.GetReturnOrderInfo(searchString, this);
                    ///直接根据orderNumber获取此订单下的商品展示
                    //DataSet ds = controller.GetReturnGoodDataSetById(id_.Text.ToString());
                    //listDataGridView.AutoGenerateColumns = false;
                    //listDataGridView.DataSource = ds;
                    //listDataGridView.DataMember = "zc_order_history";
                    //listDataGridView.CurrentCell = null;//不默认选中
                }
                else
                {
                    //CDQueryList cdQueryList = new CDQueryList(this, searchString, WorkMode);
                    //cdQueryList.ShowDialog();
                }
                ///焦点移动到扫码框内
                numberTextBox.Focus();
            }
        }

        private int queryNum()
        {
            int con = 0;
            MysqlDBHelper dbHelper = new MysqlDBHelper();
            MySqlConnection conn = dbHelper.GetConnection();
            string sql = null;
            string num = null;
            string amount = null;
            string name = null;
            string phone = null;
            string card = null;
            try
            {
                //sql = "select COUNT(1) "
                sql = "select e.ORDERNUM ,e.ORDERAMOUNT,e.CONSIGNEE,e.CANSIGNPHONE,f.ASSOCIATOR_CARDNUMBER "
                    + " From zc_order_transit e "
                    + " LEFT JOIN zc_associator_info f on e.member_id = f.id "
                + conditon;
                //MessageBox.Show("sql:"+sql);

                MySqlDataReader reader = dbHelper.GetReader(sql, conn);
                while (reader.Read())
                {
                    con += 1;
                    if (con == 1)
                    {
                        num = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                        amount = reader.IsDBNull(1) ? string.Empty : reader.GetString(0);
                        name = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        phone = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        card = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        //MessageBox.Show(num);
                    }
                    //alreadyTotalAmount += float.Parse(reader.IsDBNull(1) ? string.Empty : reader.GetString(1));
                }
                if (con == 1)
                {
                    order_Num = num;
                    setinform(num, amount, name, phone, card);
                }
            }
            catch (Exception ex)
            {
                log.Error("获取订单数量信息失败", ex);
            }
            finally
            {
                dbHelper.CloseConnection(conn);
            }
            return con;
        }

        private void queryOrderList()
        {
            ///当前工作模式为提货状态
            if (WorkMode == Constant.PICK_UP_GOODS)
            {
                string sql = "select sum(nums) as nums,name,sum(g_price*nums) as totalprice,classify_name,goods_unit,delFlag,goods_specifications,serialNumber,g_price as actualnums,goodsfile_id,goods_class_id,g_price,orderNum from "
                    + " (select a.goods_state,a.name,a.nums,b.goods_specifications,b.goods_unit,a.g_price,b.id as goodsfile_id,b.delFlag,b.serialNumber,c.classify_name,b.goods_class_id,b.goods_supplier_id,e.orderNum "
                    + "from zc_order_transit e  "
                    + "LEFT JOIN zc_associator_info f on e.member_id = f.id "
                    + " LEFT JOIN zc_order_transit_item a on e.id = a.order_id "
                    + " left join zc_goods_master b on a.goodsfile_id = b.id "
                    + " left join zc_classify_info c on b.goods_class_id = c.id "
                    + " where e.orderNum='" + order_Num + "'"
                    + " )as d group by name,delFlag,classify_name,goods_unit,goods_specifications,serialNumber,g_price,goodsfile_id,goods_class_id ";

                MysqlDBHelper dbHelper = new MysqlDBHelper();
                DataSet ds = dbHelper.GetDataSet(sql, "zc_order_transit");
                itemDataGridView.AutoGenerateColumns = false;
                itemDataGridView.DataSource = ds;
                itemDataGridView.DataMember = "zc_order_transit";
                itemDataGridView.CurrentCell = null;//不默认选中
                initDifference();
            }
            ///当前工作模式为退款状态
            if (WorkMode == Constant.REFUND)
            {
                //string sql = "select sum(nums) as nums,name,sum(g_price*nums) as totalprice,classify_name,goods_unit,delFlag,goods_specifications,serialNumber,g_price as actualnums,goodsfile_id,goods_class_id,g_price,orderNum ,actual_nums,actual_money from "
                //    + " (select a.goods_state,a.name,a.nums,b.goods_specifications,b.goods_unit,a.g_price,b.id as goodsfile_id,b.delFlag,b.serialNumber,c.classify_name,b.goods_class_id,b.goods_supplier_id,e.orderNum,a.actual_nums,a.actual_money "
                //    + "from zc_order_history e  "
                //    + "LEFT JOIN zc_associator_info f on e.member_id = f.id "
                //    + " LEFT JOIN zc_order_history_item a on e.id = a.order_id "
                //    + " left join zc_goods_master b on a.goodsfile_id = b.id "
                //    + " left join zc_classify_info c on b.goods_class_id = c.id "
                //    + " where e.orderNum='" + order_Num + "' and e.orderstatus not in ('" + Constant.ORDER_STATUS_ALL_REFUSE + "','" + Constant.ORDER_STATUS_ALL_REFUND + "','" + Constant.ORDER_STATUS_PART_REFUND + "')"
                //    + " )as d group by name,delFlag,classify_name,goods_unit,goods_specifications,serialNumber,g_price,goodsfile_id,goods_class_id ";

                //MysqlDBHelper dbHelper = new MysqlDBHelper();
                //DataSet ds = dbHelper.GetDataSet(sql, "zc_order_history");
                //listDataGridView.AutoGenerateColumns = false;
                //listDataGridView.DataSource = ds;
                //listDataGridView.DataMember = "zc_order_history";
                //listDataGridView.CurrentCell = null;//不默认选中

            }
        }

        /// <summary>
        /// 初始化显示差异difference
        /// </summary>
        private void initDifference()
        {
            for (int i = 0; i < itemDataGridView.RowCount; i++)
            {
                int orderNums = Convert.ToInt32(itemDataGridView[3, i].Value == null ? "0" : itemDataGridView[3, i].Value);
                itemDataGridView[5, i].Value = orderNums;
            }
        }

        /// <summary>
        /// 快捷键绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerDelivery_KeyDown(object sender, KeyEventArgs e)
        {
            ///快捷键F1功能提示
            if(e.KeyCode == Keys.F1){
                HelpMessage help = new HelpMessage();
                help.Show();
            }else if(e.KeyCode == Keys.T)
            {
                pickUp();
            }else if(e.KeyCode == Keys.L)
            {
                resaleInit();
            }else if(e.KeyCode == Keys.F2 && WorkMode.Equals(Constant.RETAIL))
            {
                MemberChoose memberChoose = new MemberChoose(this, WorkMode);
                memberChoose.Show();
            }
            else if (e.KeyCode == Keys.Subtract)
            {
                setFlagLabel();
            }
            else if (e.KeyCode == Keys.Multiply)
            {
                openChangeNums();
            }else if(e.KeyCode == Keys.U && WorkMode.Equals(Constant.RETAIL))
            {
                deleteResaleGoods();
            }else if(e.KeyCode == Keys.Add)
            {
                ///按space进入结算
                settlement();
            }else if(e.KeyCode == Keys.A){
                //退货
                returnOfGoodsInit();
            }else if(e.KeyCode == Keys.R && (WorkMode.Equals(Constant.PICK_UP_GOODS) || WorkMode.Equals(Constant.REFUND))){
                if (itemDataGridView.CurrentCell == null)
                {
                    return;
                }
                else 
                {
                    if (WorkMode.Equals(Constant.PICK_UP_GOODS))
                    {
                        RefuseReason reason = new RefuseReason(this, itemDataGridView.CurrentRow.Cells[1].Value == null ? string.Empty : itemDataGridView.CurrentRow.Cells[1].Value.ToString(), false);
                        reason.Show();
                    }
                    else
                    {
                        RefuseReason reason = new RefuseReason(this, itemDataGridView.CurrentRow.Cells[1].Value == null ? string.Empty : itemDataGridView.CurrentRow.Cells[1].Value.ToString(), WorkMode);
                        reason.Show();
                    }
                    
                    //InputReason inputReason = new InputReason(this.WorkMode, this);
                    //inputReason.Show();
                }
            }

            ///扫码
            if (numberTextBox.Focused == true && e.KeyCode == Keys.Enter)
            {
                if (WorkMode.Equals(Constant.RETAIL))
                {
                    AddResaleGoods();
                }
                else if (WorkMode.Equals(Constant.PICK_UP_GOODS))
                {
                    AddOrderGoods();
                }else if(WorkMode.Equals(Constant.REFUND)){
                    AddReFundGoods();
                }
            }
            //快捷键返回上层
            if(e.KeyCode == Keys.Escape){
                this.Close();
            }




            /////快捷键点击退款
            //if (e.KeyCode == Keys.F2)
            //{
            //    refund_Click(this, EventArgs.Empty);
            //}
            ////balance_Click(object sender, EventArgs e)
            //if (e.KeyCode == Keys.F3)
            //{
            //    balance_Click(this, EventArgs.Empty);
            //}
            ////快捷键零售
            //if(e.KeyCode == Keys.F4){
            //    resaleButton_Click(this, EventArgs.Empty);
            //}
            ////快捷进入会员信息选择
            //if(isResale && e.KeyCode == Keys.F5)
            //{
            //    memberChooseButton_Click(this, EventArgs.Empty);
            //}
            /////点击添加扫码的零售商品
            ////if(isResale && resaleNumberTextBox.Focused && e.KeyCode == Keys.Enter){
            ////    AddResaleGoods();
            ////}
            //if(isResale && e.KeyCode == Keys.A)
            //{
            //    AddOrReduceButton_Click(this, EventArgs.Empty);
            //}
            /////零售商品数量修改
            ////if(isResale & e.KeyCode == Keys.B)
            ////{
            ////    button1_Click(this, EventArgs.Empty);
            ////}
            /////零售商品删除
            //if (isResale & e.KeyCode == Keys.C)
            //{
            //    button2_Click(this, EventArgs.Empty);
            //}

            /////查询键点击查询
            ////if (queryTextBox.Focused == true && e.KeyCode == Keys.Enter)
            ////{
            ////    queryButton_Click(this, e);
            ////}else 

            //if (numberTextBox.Focused == true && e.KeyCode == Keys.Enter)
            //{
            //    equalButton_Click(this, EventArgs.Empty);
            //}


        }

        /// <summary>
        /// 结算
        /// </summary>
        private void settlement()
        {
            if(WorkMode.Equals(Constant.RETAIL))
            {
                PayForm pay = new PayForm();
                pay.totalAmount = totalAmount.Text;
                pay.memberId = associatorInfo == null ? string.Empty : associatorInfo.Id;
                ResaleWaterNumber = "LS" + DateTime.Now.ToString("yyyyMMddhhmmss") + LoginUserInfo.street;
                pay.orderId = ResaleWaterNumber;
                pay.ModeFlag = 2;
                pay.customerDelivery = this;
                pay.ShowDialog();
            }else if(WorkMode.Equals(Constant.PICK_UP_GOODS))
            {
                pickUpSettlement();
            }else if(WorkMode.Equals(Constant.REFUND)){
                refundSettlement();
            }
        }

        

        

        /// <summary>
        /// 初始化提货显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pickUp()
        {
            this.workModelabel.Text = "客户提货";
            this.WorkMode = Constant.PICK_UP_GOODS;
            zc_order_transit_id = "";
            serialnumber.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            serialnumber.Width = 90;
            goods_name.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            goods_name.Width = 200;
            goods_price.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            goods_price.Width = 90;
            goodAmount.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            goodAmount.Width = 120;
            nums.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            nums.Width = 120;
            actualQuantity.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            actualQuantity.Width = 120;

            nums.Visible = true;
            actualQuantity.Visible = true;
            diffenence.Visible = true;
            orderAmount.Visible = true;
            receiveAmount.Visible = true;
            goods_specifications.Visible = true;
            goods_unit.Visible = true;
            orderNum.Visible = true;
            refuseReason.Visible = true;
            nums.HeaderText = "订单份数";
            actualQuantity.HeaderText = "实际份数";

            refundReason.Visible = false;
            resale_nums.Visible = false;
            resale_money.Visible = false;

            refund_weight.Visible = false;
            receive_money.Visible = false;
            refund_money.Visible = false;
            itemDataGridView.DataSource = null;
            itemDataGridView.Rows.Clear();

            ///初始化会员信息
            memberName.Text = "";
            memberPhone.Text = "";
            memberCard.Text = "";

            ///初始化合计
            totalSum.Text = MoneyFormat.RountFormat(0);
            totalAmount.Text = MoneyFormat.RountFormat(0);

            //点击提货直接进入会员选择界面
            MemberChoose memberChoose = new MemberChoose(this, WorkMode);
            memberChoose.Show();
        }

        /// <summary>
        /// 初始化零售显示
        /// </summary>
        private void resaleInit()
        {
            this.workModelabel.Text = "零售";
            this.WorkMode = Constant.RETAIL;
            zc_order_transit_id = "";
            serialnumber.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            serialnumber.FillWeight = 0.2F;
            goods_name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            goods_name.FillWeight = 0.35F;
            goods_price.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            goods_price.FillWeight = 0.1F;
            goodAmount.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            goodAmount.FillWeight = 0.1F;

            nums.Visible = false;
            actualQuantity.Visible = false;
            diffenence.Visible = false;
            orderAmount.Visible = false;
            receiveAmount.Visible = false;
            goods_specifications.Visible = false;
            goods_unit.Visible = false;
            orderNum.Visible = false;
            refuseReason.Visible = false;
            refundReason.Visible = false;

            resale_nums.Visible = true;
            resale_money.Visible = true;

            refund_weight.Visible = false;
            receive_money.Visible = false;
            refund_money.Visible = false;

            itemDataGridView.DataSource = null;
            itemDataGridView.Rows.Clear();
            ///初始化会员信息
            memberName.Text = "";
            memberPhone.Text = "";
            memberCard.Text = "";

            ///初始化合计
            totalSum.Text = MoneyFormat.RountFormat(0);
            totalAmount.Text = MoneyFormat.RountFormat(0);
        }

        /// <summary>
        /// 退货初始化
        /// </summary>
        private void returnOfGoodsInit()
        {
            this.workModelabel.Text = "退货";
            this.WorkMode = Constant.REFUND;
            zc_order_transit_id = "";

            serialnumber.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            serialnumber.Width = 90;
            goods_name.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            goods_name.Width = 200;
            goods_price.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            goods_price.Width = 90;
            goodAmount.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            goodAmount.Width = 120;
            nums.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            nums.Width = 120;
            actualQuantity.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            actualQuantity.Width = 120;


            nums.Visible = true;
            actualQuantity.Visible = true;
            nums.HeaderText = "购买数量";
            actualQuantity.HeaderText = "退货数量";
            diffenence.Visible = false;
            orderAmount.Visible = false;
            receiveAmount.Visible = false;
            goods_specifications.Visible = false;
            goods_unit.Visible = false;
            orderNum.Visible = false;
            refuseReason.Visible = false;
            refundReason.Visible = true;
            resale_nums.Visible = false;
            resale_money.Visible = false;

            refund_weight.Visible = true;
            receive_money.Visible = true;
            refund_money.Visible = true;

            itemDataGridView.DataSource = null;
            itemDataGridView.Rows.Clear();

            ///初始化会员信息
            memberName.Text = "";
            memberPhone.Text = "";
            memberCard.Text = "";

            ///初始化合计
            totalSum.Text = MoneyFormat.RountFormat(0);
            totalAmount.Text = MoneyFormat.RountFormat(0);

            ResaleList resaleListForm = new ResaleList(this);
            resaleListForm.ShowDialog();
        }

        /// <summary>
        /// 获取订单号
        /// </summary>
        /// <param name="orderNum">单号</param>
        public void setOrderNum(string orderNum)
        {
            this.order_Num = orderNum;
            queryOrderList();
        }

        /// <summary>
        /// 右侧展示订单信息
        /// </summary>
        /// <param name="orderNum">单号</param>
        /// <param name="name">姓名</param>
        /// <param name="phone">电话</param>
        /// <param name="card">卡号</param>
        public void setinform(string orderNum, string order_Amount, string name, string phone, string card)
        {
            //id_.Text = orderNum;
            //name_label.Text = name;
            //inform_label.Text = phone;
            //card_label.Text = card;
            //amount_TextBox.Text = order_Amount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="good_id">货号</param>
        /// <param name="good_num">份数</param>
        /// <param name="good_wei">重量</param>
        public void setbar(string good_id, string good_num, string good_wei)
        {

        }

        /// <summary>
        /// 合计
        /// </summary>
        private void Calculate()
        {
            initNumberAndAmount();
            try
            {
                for (int i = 0; i < itemDataGridView.RowCount; i++)
                {
                    totalSumValue += Convert.ToInt32((itemDataGridView[4, i].Value == null || itemDataGridView[4, i].Value.ToString().Trim() == "") ? "0" : itemDataGridView[4, i].Value.ToString());
                    totalAmountValue += float.Parse((itemDataGridView[7, i].Value == null || itemDataGridView[7, i].Value.ToString().Trim() == "") ? "0" : itemDataGridView[7, i].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                log.Error("类型转换异常", ex);
            }
            this.totalSum.Text = MoneyFormat.RountFormat(totalSumValue);
            this.totalAmount.Text = MoneyFormat.RountFormat(totalAmountValue);
        }

        /// <summary>
        /// 检测输入是否为数字的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (itemDataGridView.CurrentCell.ColumnIndex == 4)
            {
                e.Control.KeyPress -= new KeyPressEventHandler(checkInput);
                e.Control.KeyPress += new KeyPressEventHandler(checkInput);
            }
            if (itemDataGridView.CurrentCell.ColumnIndex == 7)
            {
                //7 只输入数字和点 退回
                e.Control.KeyPress -= new KeyPressEventHandler(checkInput2);
                e.Control.KeyPress += new KeyPressEventHandler(checkInput2);
            }
        }

        /// <summary>
        /// 检测输入输入是否是数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkInput2(object sender, KeyPressEventArgs e)
        {
            DataGridViewTextBoxEditingControl s = sender as DataGridViewTextBoxEditingControl;
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
            {
                e.Handled = true;
                //MessageBox.Show("数据验证");
            }
        }

        /// <summary>
        /// 检测输入输入是否是数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkInput(object sender, KeyPressEventArgs e)
        {
            DataGridViewTextBoxEditingControl s = sender as DataGridViewTextBoxEditingControl;
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 数据量变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 4)
            {
                try
                {
                    initNumberAndAmount();
                    int num = itemDataGridView.Rows[e.RowIndex].Cells[4].Value == null ? 0 : Convert.ToInt32(itemDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString());
                    Double price = Convert.ToDouble(itemDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString());
                    itemDataGridView.Rows[e.RowIndex].Cells[5].Value = Convert.ToInt32(itemDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString()) - num;
                    //itemDataGridView.Rows[e.RowIndex].Cells[7].Value = Math.Round(num * price, 2).ToString("0.00");
                    //Calculate();
                }
                catch (Exception ex)
                {
                    log.Error("自动计算金额错误", ex);
                }
            }
            if (e.RowIndex != -1 && e.ColumnIndex == 5 && WorkMode.Equals(Constant.PICK_UP_GOODS))
            {
                int cot = Convert.ToInt32(itemDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString());
                if (cot != 0)
                {
                    this.itemDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(201, 67, 65);//Color.Red;
                }
                else
                {
                    this.itemDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(51, 153, 255);//Color.Blue;
                }
            }
            ///订单金额进行了手动修改
            if (e.RowIndex != -1 && e.ColumnIndex == 7)
            {
                initNumberAndAmount();
                Calculate();
            }
        }

        private void itemDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            row = itemDataGridView.CurrentCell.RowIndex;
            column = itemDataGridView.CurrentCell.ColumnIndex;
        }

        /// <summary>
        /// 
        /// </summary>
        public void balance_data()
        {
            string orderId = itemDataGridView.SelectedRows[0].Cells[12].Value.ToString();
            BranchZcOrderTransitService transitService = new BranchZcOrderTransitService();
            BranchZcOrderTransitItemService transitItemService = new BranchZcOrderTransitItemService();
            BranchZcOrderHistoryService historyService = new BranchZcOrderHistoryService();
            BranchZcOrderHistoryItemService historyItemService = new BranchZcOrderHistoryItemService();
            ZcOrderTransit zcTransit = transitService.FindById(orderId);
            zcTransit.OrderStatus = Constant.ORDER_STATUS_FININSH;
            ZcOrderHistory history = ZcOrderHelper.Tranform(zcTransit);
            List<ZcOrderTransitItem> itemlist = transitItemService.FindByOrderId(orderId);
            List<ZcOrderHistoryItem> historylist = ZcOrderHelper.Transform(itemlist);
            historyItemService.AddZcOrderHistoryItem(historylist);
            historyService.AddZcOrderHistory(history);
            transitItemService.DeleteByOrderId(orderId);
            transitService.DeleteById(orderId);
        }

        /// <summary>
        /// 初始化份数还有金额
        /// </summary>
        public void initNumberAndAmount()
        {
            this.totalSumValue = 0;
            this.totalAmountValue = 0;
            totalSum.Text = MoneyFormat.RountFormat(totalSumValue);
            totalAmount.Text = MoneyFormat.RountFormat(totalAmountValue);
        }

        /// <summary>
        /// DatagridView 显示格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (itemDataGridView.Columns[e.ColumnIndex].Name == "goods_specifications")
            {
                if (e.Value == null || e.Value.ToString() == "")
                {
                    return;
                }
                string str = e.Value.ToString();
                e.Value = str.Replace("商品规格：", "");
            }
            if (itemDataGridView.Columns[e.ColumnIndex].Name == "goods_price")
            {
                if (e.Value == null || e.Value.ToString() == "")
                {
                    return;
                }
                float price = e.Value == null ? 0 : float.Parse(e.Value.ToString());
                e.Value = MoneyFormat.RountFormat(price);
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void initData()
        {
            zc_order_transit_id = "";
            memberName.Text = "";
            memberCard.Text = "";
            memberPhone.Text = "";
            orderNumerLabel.Text = "";
            totalSum.Text = MoneyFormat.RountFormat(0);
            totalAmount.Text = MoneyFormat.RountFormat(0);
            itemDataGridView.DataSource = null;
            itemDataGridView.Rows.Clear();
        }

        /// <summary>
        /// 提货模式下的扫码
        /// </summary>
        private void AddOrderGoods()
        {
            string bar = numberTextBox.Text;
            numberTextBox.Text = "";
            string serial = "";
            string weight = "";
            if (string.IsNullOrEmpty(bar) || (!bar.StartsWith("28") && !bar.StartsWith("69")) || (bar.Length != 18 && bar.Length != 13))
            {
                MessageBox.Show("扫描的条码不正确，请重新扫描", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (bar.Length == 18)
            {
                serial = bar.Substring(2, 5);
            }
            else if (bar.StartsWith("28"))
            {
                serial = bar.Substring(2, 5);
            }
            else
            {
                serial = bar;
            }
            BranchZcGoodsMasterService branchGoodsService = new BranchZcGoodsMasterService();
            ZcGoodsMaster zcGoodsMaster = branchGoodsService.FindBySerialNumber(serial);
            if (zcGoodsMaster == null)
            {
                MessageBox.Show("没有此货号对应的商品信息，请检查后重新操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool isWeightGoods = branchGoodsService.IsWeightGoods(zcGoodsMaster.Id);
            if (isWeightGoods)
            {
                if (bar.Length == 18)
                {
                    weight = float.Parse(bar.Substring(12, 5).Insert(2, ".")).ToString();
                }
                else
                {
                    weight = float.Parse(bar.Substring(7, 5).Insert(2, ".")).ToString();
                }
            }
            DataSet ds = (DataSet)itemDataGridView.DataSource;
            if (ds == null || itemDataGridView.RowCount == 0)
            {
                MessageBox.Show("当前未选择任意订单，请先选择一条订单再进行扫码操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool flag = false;
            for (int i = 0; i < itemDataGridView.RowCount; i++)
            {
                if (itemDataGridView[0, i].Value.ToString().Equals(serial))
                {
                    flag = true;
                    ///价格
                    float goodsPrice = float.Parse(itemDataGridView[2, i].Value == null ? "0" : itemDataGridView[2, i].Value.ToString());
                    GoodsPrint goodsPrint = new GoodsPrint();
                    goodsPrint.SerialNumber = serial;
                    goodsPrint.Name = itemDataGridView[1, i].Value == null ? "" : itemDataGridView[1, i].Value.ToString();
                    goodsPrint.Nums = 1;
                    goodsPrint.Price = float.Parse(itemDataGridView[2, i].Value == null ? "0" : itemDataGridView[2, i].Value.ToString());
                    goodsPrint.Unit = itemDataGridView[10, i].Value == null ? "" : itemDataGridView[10, i].Value.ToString();
                    goodsPrint.GoodsFileId = zcGoodsMaster.Id;
                    goodsPrint.BarCode = bar;
                    if(isWeightGoods){
                        goodsPrint.Weight = float.Parse(weight);
                    }else{
                    }
                    if (AddOrDelete)
                    {
                        itemDataGridView[4, i].Value = Convert.ToInt32(itemDataGridView[4, i].Value == null ? "0" : itemDataGridView[4, i].Value.ToString()) + 1;
                        if (isWeightGoods)
                        {
                            itemDataGridView[7, i].Value = MoneyFormat.RountFormat((float.Parse(itemDataGridView[7, i].Value == null ? "0" : itemDataGridView[7, i].Value.ToString())) + goodsPrice * float.Parse(weight));
                            itemDataGridView[8, i].Value = (float.Parse(itemDataGridView[8, i].Value == null ? "0" : itemDataGridView[8, i].Value.ToString()) +  float.Parse(weight)).ToString("0.000");
                        }
                        else
                        {
                            itemDataGridView[7, i].Value = MoneyFormat.RountFormat(float.Parse(itemDataGridView[7, i].Value == null ? "0" : itemDataGridView[7, i].Value.ToString()) + goodsPrice * 1);
                            itemDataGridView[8, i].Value = (float.Parse(itemDataGridView[8, i].Value == null ? "0" : itemDataGridView[8, i].Value.ToString()) + 1).ToString("0.000");
                        }
                        bool isExist = false;
                        for (int j = 0; j < printObjectlist.Count; j++)
                        {
                            GoodsPrint obj = printObjectlist[j];
                            
                            if(obj.SerialNumber.Equals(goodsPrint.SerialNumber))
                            {
                                ///存在商品
                                isExist = true;
                                ///称重商品
                                if (isWeightGoods)
                                {
                                    printObjectlist.Add(goodsPrint);
                                    break;
                                }
                                else ///非称重商品，数量自增
                                {
                                    obj.Nums += 1;
                                    break;
                                }
                            }
                        }
                        ///不存在   都要添加
                        if(!isExist)
                        {
                            printObjectlist.Add(goodsPrint);
                        }
                        
                    }
                    else
                    {
                        if (Convert.ToInt32(itemDataGridView[4, i].Value == null ? "0" : itemDataGridView[4, i].Value.ToString()) == 0)
                        {
                            MessageBox.Show("此商品的份数为0，无法进行减去扫码操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            setFlagLabel();
                            return;
                        }
                        bool isDone = false;
                        for (int j = 0; j < printObjectlist.Count; j++ )
                        {
                            GoodsPrint obj = printObjectlist[j];
                            if(goodsPrint.SerialNumber.Equals(obj.SerialNumber))
                            {
                                if (isWeightGoods)
                                {
                                    if (obj.Weight == float.Parse(weight))
                                    {
                                        printObjectlist.RemoveAt(j);
                                        isDone = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    isDone = true;
                                    if (obj.Nums == 1)
                                    {
                                        printObjectlist.RemoveAt(j);
                                        break;
                                    }
                                    else
                                    {
                                        obj.Nums = obj.Nums - 1;
                                        break;
                                    }
                                }
                            }
                        }
                        if( !isDone)
                        {
                            MessageBox.Show("此商品之前未曾进行过扫码操作，无法执行减去扫码操作!", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            setFlagLabel();
                            return;
                        }
                        itemDataGridView[4, i].Value = Convert.ToInt32(itemDataGridView[4, i].Value == null ? "0" : itemDataGridView[4, i].Value.ToString()) - 1;
                        if (isWeightGoods)
                        {
                            itemDataGridView[7, i].Value = MoneyFormat.RountFormat((float.Parse(itemDataGridView[7, i].Value == null ? "0" : itemDataGridView[7, i].Value.ToString())) - goodsPrice * float.Parse(weight));
                            itemDataGridView[8, i].Value = (float.Parse(itemDataGridView[8, i].Value == null ? "0" : itemDataGridView[8, i].Value.ToString()) - float.Parse(weight)).ToString("0.000");
                        }
                        else
                        {
                            itemDataGridView[7, i].Value = MoneyFormat.RountFormat(float.Parse(itemDataGridView[7, i].Value == null ? "0" : itemDataGridView[7, i].Value.ToString()) - goodsPrice * 1);
                            itemDataGridView[8, i].Value = (float.Parse(itemDataGridView[8, i].Value == null ? "0" : itemDataGridView[8, i].Value.ToString()) - 1).ToString("0.000");
                        }
                        setFlagLabel();
                    }
                    Calculate();
                    itemDataGridView.Rows[i].Selected = true;
                    itemDataGridView.CurrentCell = itemDataGridView.Rows[i].Cells[0];
                }
            }

            if (!flag)
            {
                MessageBox.Show("客户订单中没有此商品，请与客户确认", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 退货扫码
        /// </summary>
        public void AddReFundGoods() 
        {
            string bar = numberTextBox.Text;
            numberTextBox.Text = "";
            string serial = "";
            string weight = "";
            if (string.IsNullOrEmpty(bar) || (!bar.StartsWith("28") && !bar.StartsWith("69")) || (bar.Length != 18 && bar.Length != 13))
            {
                MessageBox.Show("扫描的条码不正确，请重新扫描", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (bar.Length == 18)
            {
                serial = bar.Substring(2, 5);
            }
            else if (bar.StartsWith("28"))
            {
                serial = bar.Substring(2, 5);
            }
            else
            {
                serial = bar;
            }
            BranchZcGoodsMasterService branchGoodsService = new BranchZcGoodsMasterService();
            ZcGoodsMaster zcGoodsMaster = branchGoodsService.FindBySerialNumber(serial);
            if (zcGoodsMaster == null)
            {
                MessageBox.Show("没有此货号对应的商品信息，请检查后重新操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool isWeightGoods = branchGoodsService.IsWeightGoods(zcGoodsMaster.Id);
            if (isWeightGoods)
            {
                if (bar.Length == 18)
                {
                    weight = float.Parse(bar.Substring(12, 5).Insert(2, ".")).ToString();
                }
                else
                {
                    weight = float.Parse(bar.Substring(7, 5).Insert(2, ".")).ToString();
                }
            }
            DataSet ds = (DataSet)itemDataGridView.DataSource;
            if (ds == null || itemDataGridView.RowCount == 0)
            {
                MessageBox.Show("当前未选择任意订单，请先选择一条订单再进行扫码操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool flag = false;
            for (int i = 0; i < itemDataGridView.RowCount; i++)
            {
                if (itemDataGridView[0, i].Value.ToString().Equals(serial))
                {
                    float price = itemDataGridView.Rows[i].Cells[2].Value == null ? 0 : float.Parse(itemDataGridView.Rows[i].Cells[2].Value.ToString());
                    float oldNums = itemDataGridView.Rows[i].Cells[3].Value == null ? 0 : float.Parse(itemDataGridView.Rows[i].Cells[3].Value.ToString());
                    float refundNums = itemDataGridView.Rows[i].Cells[4].Value == null ? 0 : float.Parse(itemDataGridView.Rows[i].Cells[4].Value.ToString());
                    float oldWeight = itemDataGridView.Rows[i].Cells[8].Value == null ? 0 : float.Parse(itemDataGridView.Rows[i].Cells[8].Value.ToString());
                    float refundWeight = itemDataGridView.Rows[i].Cells[17].Value == null ? 0 : float.Parse(itemDataGridView.Rows[i].Cells[17].Value.ToString());
                    GoodsPrint goodsPrint = new GoodsPrint();
                    goodsPrint.SerialNumber = serial;
                    goodsPrint.Name = itemDataGridView[1, i].Value == null ? "" : itemDataGridView[1, i].Value.ToString();
                    goodsPrint.Nums = 1;
                    goodsPrint.Price = price;
                    goodsPrint.GoodsFileId = zcGoodsMaster.Id;
                    goodsPrint.BarCode = bar;
                    if (isWeightGoods)
                    {
                        goodsPrint.Weight = float.Parse(weight);
                    }
                    if(AddOrDelete){
                        if (refundNums + 1 > oldNums)
                        {
                            MessageBox.Show("退货数量不能超过销售数量", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            if (isWeightGoods)
                            {
                                if (oldWeight < float.Parse(weight) + refundWeight)
                                {
                                    MessageBox.Show("退货重量不能超过销售重量", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else 
                                {
                                    itemDataGridView.Rows[i].Cells[4].Value = refundNums + 1;
                                    itemDataGridView.Rows[i].Cells[17].Value = refundWeight + float.Parse(weight);
                                    itemDataGridView.Rows[i].Cells[19].Value = MoneyFormat.RountFormat((refundWeight + float.Parse(weight)) * price);
                                    itemDataGridView.Rows[i].Selected = true;
                                    itemDataGridView.CurrentCell = itemDataGridView.Rows[i].Cells[0];
                                    printObjectlist.Add(goodsPrint);
                                    flag = true;
                                }
                            }
                            else 
                            {
                                itemDataGridView.Rows[i].Cells[4].Value = refundNums + 1;
                                itemDataGridView.Rows[i].Cells[19].Value = MoneyFormat.RountFormat((refundNums + 1) * price);
                                foreach(GoodsPrint obj in printObjectlist){
                                    if(obj.SerialNumber.Equals(serial)){
                                        obj.Nums += 1;
                                    }else{
                                        continue;
                                    }
                                }
                                flag = true;
                            }
                        }
                    }else
                    {
                        setFlagLabel();
                        if (refundNums == 0)
                        {
                            MessageBox.Show("退货数量已经为0，无法进行扫码减去操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else if (refundNums == 1)
                        {
                            if(isWeightGoods){
                                bool isDone = false;
                                foreach (GoodsPrint obj in printObjectlist)
                                {
                                    if (obj.SerialNumber.Equals(serial) && obj.Weight == float.Parse(weight))
                                    {
                                        printObjectlist.Remove(obj);
                                        isDone = true;
                                        flag = true;
                                        itemDataGridView.Rows[i].Cells[4].Value = 0;
                                        itemDataGridView.Rows[i].Cells[17].Value = 0;
                                        itemDataGridView.Rows[i].Cells[19].Value = MoneyFormat.RountFormat(0);
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                if(! isDone){
                                    MessageBox.Show("没有此商品对应的退货扫码记录，无法进行扫码减去操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }else{
                                foreach (GoodsPrint obj in printObjectlist)
                                {
                                    if (obj.SerialNumber.Equals(serial))
                                    {
                                        printObjectlist.Remove(obj);
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                flag = true;
                                itemDataGridView.Rows[i].Cells[4].Value = 0;
                                itemDataGridView.Rows[i].Cells[19].Value = MoneyFormat.RountFormat(0);
                            }
                        }
                        else 
                        {
                            if(isWeightGoods){
                                bool isDone = false;
                                foreach (GoodsPrint obj in printObjectlist)
                                {
                                    if (obj.SerialNumber.Equals(serial) && obj.Weight == float.Parse(weight))
                                    {
                                        printObjectlist.Remove(obj);
                                        isDone = true;
                                        flag = true;
                                        itemDataGridView.Rows[i].Cells[4].Value = refundNums - 1;
                                        itemDataGridView.Rows[i].Cells[17].Value = refundWeight - float.Parse(weight);
                                        itemDataGridView.Rows[i].Cells[19].Value = MoneyFormat.RountFormat((refundWeight - float.Parse(weight)) * price);
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                if (!isDone)
                                {
                                    MessageBox.Show("没有此商品对应的退货扫码记录，无法进行扫码减去操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }else{
                                foreach (GoodsPrint obj in printObjectlist)
                                {
                                    if (obj.SerialNumber.Equals(serial))
                                    {
                                        obj.Nums -= 1;
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                flag = true;
                                itemDataGridView.Rows[i].Cells[4].Value = refundNums - 1;
                                itemDataGridView.Rows[i].Cells[19].Value = MoneyFormat.RountFormat((refundNums - 1) * price);
                            }
                        }
                    }
                    refundCalculate();
                }
                else {
                    continue;
                }
            }
            if(!flag){
                MessageBox.Show("没有此商品对应的销售信息记录，无法进行退货", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// DatagridView结束编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ///当前工作模式为提货状态时， 焦点在实际份数和扫码框之前切换
            if (WorkMode == Constant.PICK_UP_GOODS)
            {
                numberTextBox.Focus();
            }

        }

        private void CustomerDelivery_FormClosed(object sender, FormClosedEventArgs e)
        {
            customer();
        }

        /// <summary>
        /// 结算点击事件
        /// </summary>
        private void pickUpSettlement()
        {
            if (itemDataGridView.DataSource == null || itemDataGridView.RowCount == 0)
            {
                MessageBox.Show("当前没有要结算的订单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ///存在拒收商品标识
            bool existRefuse = false;
            ///整单商品拒收标识
            bool allRefuse = true;
            for (int i = 0; i < itemDataGridView.Rows.Count; i++)
            {
                if (Convert.ToInt32(itemDataGridView[5, i].Value) != 0)
                {
                    existRefuse = true;
                    if (Convert.ToInt32(itemDataGridView[5, i].Value) != Convert.ToInt32(itemDataGridView[3, i].Value))
                    {
                        allRefuse = false;
                    }
                }
                else
                {
                    allRefuse = false;
                }
            }

            if (!existRefuse)
            {
                ///不存在拒收情况，直接进入结算  
                PayForm pay = new PayForm();
                pay.totalAmount = totalAmount.Text;
                pay.memberId = associatorInfo == null ? string.Empty : associatorInfo.Id;
                ResaleWaterNumber = "TH" + DateTime.Now.ToString("yyyyMMddhhmmss") + LoginUserInfo.street;
                pay.orderId = ResaleWaterNumber;
                pay.ModeFlag = 0;
                pay.customerDelivery = this;
                pay.ShowDialog();
            }
            else
            {
                if (allRefuse)
                {
                    DialogResult dr = MessageBox.Show("当前客户未提取商品,是否整单拒收？", "标题", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (dr == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < itemDataGridView.Rows.Count; i++)
                        {
                            string reason = itemDataGridView[13, i].Value == null ? string.Empty : itemDataGridView[13, i].Value.ToString();
                            if (Convert.ToInt32(itemDataGridView[5, i].Value) != 0 && !string.IsNullOrEmpty(reason))
                            {
                                
                                RefuseReason refuseReason = new RefuseReason(this, itemDataGridView[1, i].Value.ToString(), i, true);
                                refuseReason.ShowDialog();
                            }
                        }

                        actualTotalMoney = "0";
                        saveRefuseInform(Constant.ORDER_STATUS_ALL_REFUSE, "", "");
                        MessageBox.Show("整单拒收成功");
                        initData();
                    }
                }
                else
                {
                    ///部分拒收    
                    DialogResult dr = MessageBox.Show("当前订单中存在未收商品,是否部分拒收,结算？", "标题", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (dr == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < itemDataGridView.Rows.Count; i++)
                        {
                            string reason = itemDataGridView[13, i].Value == null ? string.Empty : itemDataGridView[13, i].Value.ToString();
                            if (Convert.ToInt32(itemDataGridView[5, i].Value) != 0 && !string.IsNullOrEmpty(reason))
                            {
                                RefuseReason refuseReason = new RefuseReason(this, itemDataGridView[1, i].Value.ToString(), i, true);
                                refuseReason.ShowDialog();
                            }
                        }
                        ///先结算  
                        PayForm pay = new PayForm();
                        pay.totalAmount = totalAmount.Text;
                        pay.memberId = associatorInfo == null ? string.Empty : associatorInfo.Id;
                        ResaleWaterNumber = "TH" + DateTime.Now.ToString("yyyyMMddhhmmss") + LoginUserInfo.street;
                        pay.orderId = ResaleWaterNumber;
                        pay.ModeFlag = 1;
                        pay.customerDelivery = this;
                        pay.ShowDialog();

                    }
                }
            }
        }

        /// <summary>
        /// 零售流水号
        /// </summary>
        private string ResaleWaterNumber;

        /// <summary>
        /// 添加拒收原因
        /// </summary>
        /// <param name="index"></param>
        /// <param name="p"></param>
        public void AddRefuseReason(int index, string reason, bool flag)
        {
            if (flag)
            {
                itemDataGridView[13, index].Value = reason;
            }
            else 
            {
                itemDataGridView.CurrentRow.Cells[13].Value = reason;
            }
        }

        /// <summary>
        /// 保存拒收信息和明细
        /// </summary>
        public void saveRefuseInform(string orderstatus, string waterNumber, string payInfoId)
        {
            List<UploadInfo> uploadList = new List<UploadInfo>();
            if (orderstatus.Equals(Constant.ORDER_STATUS_PART_REFUSE))
            {
                ///零售主表
                Resale resale = new Resale();
                resale.Id = Guid.NewGuid().ToString();
                resale.CreateTime = DateTime.Now;
                resale.UpdateTime = DateTime.Now;
                resale.WaterNumber = waterNumber;
                resale.Nums = totalSum.Text;
                resale.Money = totalAmount.Text;
                ///TODO  暂时未加入折扣，优惠金额等算法
                resale.ActualMoney = totalAmount.Text;
                resale.BranchId = LoginUserInfo.branchId;
                resale.SaleManId = LoginUserInfo.id;
                resale.memberId = associatorInfo == null ? string.Empty : associatorInfo.Id;
                resale.OrderId = zc_order_transit_id;
                resale.WaterNumber = waterNumber;
                resale.PayInfoId = payInfoId;

                List<ResaleItem> list = new List<ResaleItem>();
                for (int i = 0; i < printObjectlist.Count; i++)
                {
                    GoodsPrint goodsPrint = printObjectlist[i];
                    ResaleItem obj = new ResaleItem();
                    obj.Id = Guid.NewGuid().ToString();
                    obj.CreateTime = DateTime.Now;
                    obj.UpdateTime = DateTime.Now;
                    obj.ResaleId = resale.Id;
                    obj.GoodsFileId = goodsPrint.GoodsFileId;
                    obj.Nums = goodsPrint.Nums.ToString();
                    if (goodsPrint.Weight != 0)
                    {
                        obj.weight = goodsPrint.Weight.ToString();
                        obj.Money = MoneyFormat.RountFormat(goodsPrint.Price * goodsPrint.Weight);
                    }
                    else 
                    {
                        obj.Money = MoneyFormat.RountFormat(goodsPrint.Price * goodsPrint.Nums);
                    }
                    ///TODO  暂时未加入折扣，优惠金额等算法
                    obj.ActualMoney = obj.Money;
                    obj.BarCode = goodsPrint.BarCode;
                    obj.Price = goodsPrint.Price.ToString();
                    list.Add(obj);
                }
                BranchResaleItemService branchItemService = new BranchResaleItemService();
                BranchResaleService branchService = new BranchResaleService();
                ResaleItemService itemServcie = new ResaleItemService();
                ResaleService service = new ResaleService();
                branchService.AddResale(resale);
                branchItemService.AddResaleItem(list);
                
                ///上传零售信息
                if (PingTask.IsConnected)
                {
                    ///连网状态
                    service.AddResale(resale);
                    itemServcie.AddResaleItem(list);
                    
                }
                else
                {
                    //断网状态

                    UploadInfo obj1 = new UploadInfo();
                    obj1.Id = resale.Id;
                    obj1.CreateTime = DateTime.Now;
                    obj1.UpdateTime = DateTime.Now;
                    obj1.Type = Constant.ZC_RESALE;
                    uploadList.Add(obj1);
                    foreach (ResaleItem obj in list)
                    {
                        UploadInfo obj2 = new UploadInfo();
                        obj2.Id = obj.Id;
                        obj2.CreateTime = DateTime.Now;
                        obj2.UpdateTime = DateTime.Now;
                        obj2.Type = Constant.ZC_RESALE_ITME;
                        uploadList.Add(obj2);
                    }
                }
                ///库存的减少
                BranchZcStoreHouseService branchStoreService = new BranchZcStoreHouseService();
                List<ZcStoreHouse> storeList = new List<ZcStoreHouse>();
                for (int i = 0; i < itemDataGridView.RowCount; i++)
                {
                    if (itemDataGridView.Rows[i].Cells[4].Value == null || string.IsNullOrEmpty(itemDataGridView.Rows[i].Cells[4].Value.ToString()) || float.Parse(itemDataGridView.Rows[i].Cells[4].Value.ToString()) == 0)
                    {
                        continue;
                    }
                    String goodsFileId = itemDataGridView.Rows[i].Cells[14].Value.ToString();
                    float nums = float.Parse(itemDataGridView.Rows[i].Cells[4].Value.ToString());
                    string weightString = itemDataGridView.Rows[i].Cells[8].Value == null ? string.Empty : itemDataGridView.Rows[i].Cells[8].Value.ToString();
                    ZcStoreHouse storeHouse = branchStoreService.FindByGoodsFileIdAndBranchId(goodsFileId, LoginUserInfo.branchId);
                    if (storeHouse != null)
                    {
                        float oldNums = float.Parse(storeHouse.Store);
                        float oldMoney = float.Parse(storeHouse.StoreMoney);
                        storeHouse.UpdateTime = DateTime.Now;
                        storeHouse.Store = (oldNums - nums).ToString();
                        storeHouse.StoreMoney = (oldMoney * (oldNums - nums) / oldNums).ToString();
                        if (!string.IsNullOrEmpty(weightString))
                        {
                            float oldWeight = float.Parse(storeHouse.weight);
                            storeHouse.weight = (oldWeight - float.Parse(weightString)).ToString();
                        }
                        storeList.Add(storeHouse);
                    }
                    else
                    {
                        log.Error("不存在" + itemDataGridView.Rows[i].Cells[1].Value.ToString() + "的库存信息", new Exception("不存在" + itemDataGridView.Rows[i].Cells[1].Value.ToString() + "的库存信息"));
                    }

                }
                branchStoreService.updateStoreHouse(storeList);
                ZcStoreHouseService storeService = new ZcStoreHouseService();
                if (PingTask.IsConnected)
                {
                    storeService.updateStoreHouse(storeList);
                }
                else
                {
                    for (int i = 0; i < storeList.Count; i++)
                    {
                        ZcStoreHouse obj = storeList[i];
                        UploadInfo info = new UploadInfo();
                        info.Id = obj.Id;
                        info.CreateTime = DateTime.Now;
                        info.UpdateTime = DateTime.Now;
                        info.Type = Constant.ZC_STOREHOUSE_UPDATE;
                        uploadList.Add(info);
                    }
                }
            }
            

            BranchZcOrderTransitService transitService = new BranchZcOrderTransitService();
            ZcOrderTransit tran = transitService.FindById(zc_order_transit_id);
            ZcOrderRefuse refuse = new ZcOrderRefuse();
            refuse.id = Guid.NewGuid().ToString();
            refuse.createTime = DateTime.Now;
            refuse.updateTime = DateTime.Now;
            refuse.orderId = tran.Id;
            refuse.orderAmount = tran.OrderAmount.ToString("0.00");
            refuse.actualAmount = totalAmount.Text.ToString();
            List<ZcOrderRefuseItem> refuseItemList = new List<ZcOrderRefuseItem>();
            for (int i = 0; i < itemDataGridView.RowCount; i++)
            {
                string refuseNums = itemDataGridView[5, i].Value == null ? "0" : itemDataGridView[5, i].Value.ToString();
                if (refuseNums == "0")
                {
                    continue;
                }
                ZcOrderRefuseItem obj = new ZcOrderRefuseItem();
                obj.id = Guid.NewGuid().ToString();
                obj.createTime = DateTime.Now;
                obj.updateTime = DateTime.Now;
                obj.serialNumber = itemDataGridView[0, i].Value == null ? "" : itemDataGridView[0, i].Value.ToString();
                obj.orderId = itemDataGridView[12, i].Value == null ? "" : itemDataGridView[12, i].Value.ToString();
                obj.orderNums = itemDataGridView[3, i].Value == null ? "" : itemDataGridView[3, i].Value.ToString();
                obj.refuseNums = itemDataGridView[5, i].Value == null ? "0" : itemDataGridView[5, i].Value.ToString();
                obj.price = itemDataGridView[2, i].Value == null ? "0" : itemDataGridView[2, i].Value.ToString();
                obj.refuseAmount = ((float.Parse(obj.price)) * (Convert.ToInt32(obj.refuseNums))).ToString("0.00");
                obj.salesmanId = LoginUserInfo.id;
                obj.handDate = DateTime.Now;
                obj.street = LoginUserInfo.street;
                obj.goodsFileId = itemDataGridView[14, i].Value == null ? "" : itemDataGridView[14, i].Value.ToString();
                obj.refuseId = refuse.id;
                obj.refuseReason = itemDataGridView[13, i].Value == null ? "" : itemDataGridView[13, i].Value.ToString();
                refuseItemList.Add(obj);
            }
            //获取原来的订单信息， 将订单状态修改为全部拒收  移动到history表中
            BranchZcOrderTransitItemService itemService = new BranchZcOrderTransitItemService();
            List<ZcOrderTransitItem> transitItemlist = itemService.FindByOrderId(tran.Id);
            tran.OrderStatus = orderstatus;
            ZcOrderHistory history = ZcOrderHelper.Tranform(tran);
            history.ActualMoney = actualTotalMoney;
            history.ActualNums = totalSum.Text;
            List<ZcOrderHistoryItem> historyItemlist = ZcOrderHelper.Transform(transitItemlist);
            for (int i = 0; i < itemDataGridView.RowCount; i++)
            {
                string goodsFileId = itemDataGridView[14, i].Value == null ? "" : itemDataGridView[14, i].Value.ToString();
                for (int j = 0; j < historyItemlist.Count; j++)
                {
                    ZcOrderHistoryItem item = historyItemlist[j];
                    if (goodsFileId.Equals(item.GoodsFileId))
                    {
                        item.actualNums = itemDataGridView[4, i].Value == null ? "0" : itemDataGridView[4, i].Value.ToString();
                        item.actualMoney = itemDataGridView[7, i].Value == null ? "" : itemDataGridView[7, i].Value.ToString();
                    }
                }
            }
            BranchZcOrderHistoryItemService historyItemService = new BranchZcOrderHistoryItemService();
            BranchZcOrderHistoryService historyService = new BranchZcOrderHistoryService();
            //本地添加history表中相关数据
            historyService.AddZcOrderHistory(history);
            historyItemService.AddZcOrderHistoryItem(historyItemlist);
            //上传history数据
            
            if (PingTask.IsConnected)
            {
                ZcOrderHistoryService masterhistoryService = new ZcOrderHistoryService();
                ZcOrderHistoryItemService masterHistoryItemService = new ZcOrderHistoryItemService();
                masterhistoryService.AddZcOrderHistory(history);
                masterHistoryItemService.AddZcOrderHistoryItem(historyItemlist);
            }
            else
            {
                UploadInfo obj1 = new UploadInfo();
                obj1.Id = history.Id;
                obj1.CreateTime = DateTime.Now;
                obj1.UpdateTime = DateTime.Now;
                obj1.Type = Constant.ZC_ORDER_HISTORY;
                uploadList.Add(obj1);
                foreach (ZcOrderHistoryItem obj in historyItemlist)
                {
                    UploadInfo obj2 = new UploadInfo();
                    obj2.Id = obj.Id;
                    obj2.CreateTime = DateTime.Now;
                    obj2.UpdateTime = DateTime.Now;
                    obj2.Type = Constant.ZC_ORDER_HISTORY_ITEM;
                    uploadList.Add(obj2);
                }
            }


            //删除本地transit相关表数据
            itemService.DeleteByOrderId(tran.Id);
            transitService.DeleteById(tran.Id);
            //上传transit删除信息
            if (PingTask.IsConnected)
            {
                ZcOrderTransitService masterTransitService = new ZcOrderTransitService();
                ZcOrderTransitItemService masterTransitItemService = new ZcOrderTransitItemService();
                masterTransitService.DeleteByOrderId(tran.Id);
                masterTransitItemService.DeleteByOrderId(tran.Id);
            }
            else
            {
                UploadInfo obj1 = new UploadInfo();
                obj1.Id = tran.Id;
                obj1.CreateTime = DateTime.Now;
                obj1.UpdateTime = DateTime.Now;
                obj1.Type = Constant.ZC_ORDER_TRANSIT_DELETE;
                uploadList.Add(obj1);

                UploadInfo obj2 = new UploadInfo();
                obj2.Id = tran.Id;
                obj2.CreateTime = DateTime.Now;
                obj2.UpdateTime = DateTime.Now;
                obj2.Type = Constant.ZC_ORDER_TRANSIT_ITEM_DELETE;
                uploadList.Add(obj1);

            }

            ///添加拒收明细
            BranchZcOrderRefuseService refuseService = new BranchZcOrderRefuseService();
            BranchZcOrderRefuseItemService refuseItemService = new BranchZcOrderRefuseItemService();
            refuseService.AddZcOrderRefuse(refuse);
            refuseItemService.AddZcOrderRefuseItems(refuseItemList);

            //上传拒收信息
            if (PingTask.IsConnected)
            {
                ZcOrderRefuseService masterRefuseService = new ZcOrderRefuseService();
                ZcOrderRefuseItemService masterRefuseItemService = new ZcOrderRefuseItemService();
                masterRefuseService.AddZcOrderRefuse(refuse);
                masterRefuseItemService.AddZcOrderRefuseItem(refuseItemList);
            }
            else
            {
                UploadInfo obj1 = new UploadInfo();
                obj1.Id = refuse.id;
                obj1.CreateTime = DateTime.Now;
                obj1.UpdateTime = DateTime.Now;
                obj1.Type = Constant.ZC_ORDER_REFUSE;
                uploadList.Add(obj1);
                foreach (ZcOrderRefuseItem obj in refuseItemList)
                {
                    UploadInfo obj2 = new UploadInfo();
                    obj2.Id = obj.id;
                    obj2.CreateTime = DateTime.Now;
                    obj2.UpdateTime = DateTime.Now;
                    obj2.Type = Constant.ZC_ORDER_REFUSE_ITEM;
                    uploadList.Add(obj2);
                }
            }

            UploadDao uploadDao = new UploadDao();
            uploadDao.AddUploadInfo(uploadList);
            
            ///初始化到零售
            resaleInit();
        }

        /// <summary>
        /// 全部收取，没有拒收
        /// </summary>
        public void saveAllPay(string orderstatus, string waterNumber, string payInfoId)
        {
            ///零售主表
            Resale resale = new Resale();
            resale.Id = Guid.NewGuid().ToString();
            resale.CreateTime = DateTime.Now;
            resale.UpdateTime = DateTime.Now;
            resale.WaterNumber = waterNumber;
            resale.Nums = totalSum.Text;
            resale.Money = totalAmount.Text;
            ///TODO  暂时未加入折扣，优惠金额等算法
            resale.ActualMoney = totalAmount.Text;
            resale.BranchId = LoginUserInfo.branchId;
            resale.SaleManId = LoginUserInfo.id;
            resale.memberId = associatorInfo == null ? string.Empty : associatorInfo.Id;
            resale.OrderId = zc_order_transit_id;
            resale.WaterNumber = waterNumber;
            resale.PayInfoId = payInfoId;

            List<ResaleItem> list = new List<ResaleItem>();
            for (int i = 0; i < printObjectlist.Count; i++)
            {
                GoodsPrint goodsPrint = printObjectlist[i];
                ResaleItem obj = new ResaleItem();
                obj.Id = Guid.NewGuid().ToString();
                obj.CreateTime = DateTime.Now;
                obj.UpdateTime = DateTime.Now;
                obj.ResaleId = resale.Id;
                obj.GoodsFileId = goodsPrint.GoodsFileId;
                obj.Nums = goodsPrint.Nums.ToString();
                obj.BarCode = goodsPrint.BarCode;
                obj.Price = goodsPrint.Price.ToString();
                if (goodsPrint.Weight != 0)
                {
                    obj.weight = goodsPrint.Weight.ToString();
                    obj.Money = MoneyFormat.RountFormat(goodsPrint.Weight * goodsPrint.Price);
                }
                else
                {
                    obj.Money = MoneyFormat.RountFormat(goodsPrint.Price * goodsPrint.Nums);
                }
                ///TODO  暂时未加入折扣，优惠金额等算法
                obj.ActualMoney = obj.Money;
                list.Add(obj);
            }
            BranchResaleItemService branchItemService = new BranchResaleItemService();
            BranchResaleService branchService = new BranchResaleService();
            ResaleItemService itemServcie = new ResaleItemService();
            ResaleService service = new ResaleService();
            branchService.AddResale(resale);
            branchItemService.AddResaleItem(list);
            List<UploadInfo> uploadList = new List<UploadInfo>();
            ///上传零售信息
            if (PingTask.IsConnected)
            {
                ///连网状态
                service.AddResale(resale);
                itemServcie.AddResaleItem(list);
            }
            else
            {
                //断网状态
                
                UploadInfo obj1 = new UploadInfo();
                obj1.Id = resale.Id;
                obj1.CreateTime = DateTime.Now;
                obj1.UpdateTime = DateTime.Now;
                obj1.Type = Constant.ZC_RESALE;
                uploadList.Add(obj1);
                foreach (ResaleItem obj in list)
                {
                    UploadInfo obj2 = new UploadInfo();
                    obj2.Id = obj.Id;
                    obj2.CreateTime = DateTime.Now;
                    obj2.UpdateTime = DateTime.Now;
                    obj2.Type = Constant.ZC_RESALE_ITME;
                    uploadList.Add(obj2);
                }
            }
            ///库存的减少
            BranchZcStoreHouseService branchStoreService = new BranchZcStoreHouseService();
            List<ZcStoreHouse> storeList = new List<ZcStoreHouse>();
            for (int i = 0; i < itemDataGridView.RowCount; i++)
            {
                String goodsFileId = itemDataGridView.Rows[i].Cells[14].Value.ToString();
                float nums = float.Parse(itemDataGridView.Rows[i].Cells[4].Value.ToString());
                string weightString = itemDataGridView.Rows[i].Cells[8].Value == null ? string.Empty : itemDataGridView.Rows[i].Cells[8].Value.ToString();
                ZcStoreHouse storeHouse = branchStoreService.FindByGoodsFileIdAndBranchId(goodsFileId, LoginUserInfo.branchId);
                if (storeHouse != null)
                {
                    float oldNums = float.Parse(storeHouse.Store);
                    float oldMoney = float.Parse(storeHouse.StoreMoney);
                    storeHouse.UpdateTime = DateTime.Now;
                    storeHouse.Store = (oldNums - nums).ToString();
                    storeHouse.StoreMoney = (oldMoney * (oldNums - nums) / oldNums).ToString();
                    if (!string.IsNullOrEmpty(weightString))
                    {
                        float oldWeight = float.Parse(storeHouse.weight);
                        storeHouse.weight = (oldWeight - float.Parse(weightString)).ToString();
                    }
                    storeList.Add(storeHouse);
                }
                else
                {
                    log.Error("不存在" + itemDataGridView.Rows[i].Cells[1].Value.ToString() + "的库存信息", new Exception("不存在" + itemDataGridView.Rows[i].Cells[1].Value.ToString() + "的库存信息"));
                }

            }
            branchStoreService.updateStoreHouse(storeList);
            ZcStoreHouseService storeService = new ZcStoreHouseService();
            if (PingTask.IsConnected)
            {
                storeService.updateStoreHouse(storeList);
            }
            else
            {
                for (int i = 0; i < storeList.Count; i++)
                {
                    ZcStoreHouse obj = storeList[i];
                    UploadInfo info = new UploadInfo();
                    info.Id = obj.Id;
                    info.CreateTime = DateTime.Now;
                    info.UpdateTime = DateTime.Now;
                    info.Type = Constant.ZC_STOREHOUSE_UPDATE;
                    uploadList.Add(info);
                }
            }

            BranchZcOrderTransitService transitService = new BranchZcOrderTransitService();
            ZcOrderTransit tran = transitService.FindById(this.zc_order_transit_id);
            BranchZcOrderTransitItemService itemService = new BranchZcOrderTransitItemService();
            List<ZcOrderTransitItem> transitItemlist = itemService.FindByOrderId(tran.Id);
            BranchZcOrderHistoryItemService historyItemService = new BranchZcOrderHistoryItemService();
            BranchZcOrderHistoryService historyService = new BranchZcOrderHistoryService();
            tran.OrderStatus = orderstatus;
            ZcOrderHistory history = ZcOrderHelper.Tranform(tran);
            List<ZcOrderHistoryItem> historyItemlist = ZcOrderHelper.Transform(transitItemlist);
            history.ActualMoney = actualTotalMoney;
            history.ActualNums = totalSum.Text;
            for (int i = 0; i < itemDataGridView.RowCount; i++)
            {
                string goodsFileId = itemDataGridView[14, i].Value == null ? "" : itemDataGridView[14, i].Value.ToString();
                for (int j = 0; j < historyItemlist.Count; j++)
                {
                    ZcOrderHistoryItem item = historyItemlist[j];
                    if (goodsFileId.Equals(item.GoodsFileId))
                    {
                        item.actualNums = itemDataGridView[4, i].Value == null ? "0" : itemDataGridView[4, i].Value.ToString();
                        item.actualMoney = itemDataGridView[7, i].Value == null ? "" : itemDataGridView[7, i].Value.ToString();
                    }
                }
            }
            historyService.AddZcOrderHistory(history);
            historyItemService.AddZcOrderHistoryItem(historyItemlist);

            //上传history数据
            if (PingTask.IsConnected)
            {
                ZcOrderHistoryService masterhistoryService = new ZcOrderHistoryService();
                ZcOrderHistoryItemService masterHistoryItemService = new ZcOrderHistoryItemService();
                masterhistoryService.AddZcOrderHistory(history);
                masterHistoryItemService.AddZcOrderHistoryItem(historyItemlist);
            }
            else
            {
                UploadInfo obj1 = new UploadInfo();
                obj1.Id = history.Id;
                obj1.CreateTime = DateTime.Now;
                obj1.UpdateTime = DateTime.Now;
                obj1.Type = Constant.ZC_ORDER_HISTORY;
                uploadList.Add(obj1);
                foreach (ZcOrderHistoryItem obj in historyItemlist)
                {
                    UploadInfo obj2 = new UploadInfo();
                    obj2.Id = obj.Id;
                    obj2.CreateTime = DateTime.Now;
                    obj2.UpdateTime = DateTime.Now;
                    obj2.Type = Constant.ZC_ORDER_HISTORY_ITEM;
                    uploadList.Add(obj2);
                }
            }

            itemService.DeleteByOrderId(tran.Id);
            transitService.DeleteById(tran.Id);
            //上传transit删除信息
            if (PingTask.IsConnected)
            {
                ZcOrderTransitService masterTransitService = new ZcOrderTransitService();
                ZcOrderTransitItemService masterTransitItemService = new ZcOrderTransitItemService();
                masterTransitService.DeleteByOrderId(tran.Id);
                masterTransitItemService.DeleteByOrderId(tran.Id);
            }
            else
            {
                UploadInfo obj1 = new UploadInfo();
                obj1.Id = tran.Id;
                obj1.CreateTime = DateTime.Now;
                obj1.UpdateTime = DateTime.Now;
                obj1.Type = Constant.ZC_ORDER_TRANSIT_DELETE;
                uploadList.Add(obj1);

                UploadInfo obj2 = new UploadInfo();
                obj2.Id = tran.Id;
                obj2.CreateTime = DateTime.Now;
                obj2.UpdateTime = DateTime.Now;
                obj2.Type = Constant.ZC_ORDER_TRANSIT_ITEM_DELETE;
                uploadList.Add(obj1);

            }
            UploadDao uploadDao = new UploadDao();
            uploadDao.AddUploadInfo(uploadList);

            ///初始化到零售
            resaleInit();
        }

        private void numberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 实际付款金额
        /// </summary>
        private float actualPayAmount;

        /// <summary>
        /// 打印小票
        /// </summary>
        /// <param name="p"></param>
        public void printTicket(float p)
        {
            actualPayAmount = p;

            //PrintPreviewDialog ppd = new PrintPreviewDialog();
            PrintDocument pd = new PrintDocument();
            //设置边距

            Margins margin = new Margins(20, 20, 20, 20);

            pd.DefaultPageSettings.Margins = margin;
            ////纸张设置默认


            //打印事件设置            


            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            try
            {
                //ppd.Document = pd;

                //ppd.ShowDialog();
                pd.Print();
                ///打印完成后再进行初始化数据
            }

            catch (Exception ex)
            {

                log.Error("打印出错，检查打印机是否连通", ex);
                MessageBox.Show("付款成功,打印出错,请检查打印机是否正确连接!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                initData();
            }
        }

        public string[] GetPrintStr()
        {

            StringBuilder sb = new StringBuilder();

            string tou = "宜鲜美配送有限公司";

            string address = "南京市江宁区东麒路66号";
            string saleID = ResaleWaterNumber;
            

            //string item = "品名";

            //decimal price = 25.00M;

            //int count = 5;

            //decimal total = 0.00M;

            //decimal fukuan = 500.00M;



            sb.Append("            " + tou + "     \n");

            sb.Append("-----------------------------------------------------------------\n");

            sb.Append("日期:" + DateTime.Now.ToShortDateString() + "  " + "单号:" + saleID + "\n");

            sb.Append("-----------------------------------------------------------------\n");

            sb.Append("品名" + "\t\t" + "单价" + "\t" + "重/量" + "\t" + "小计" + "\n");
            //for (int i = 0; i < itemDataGridView.RowCount; i++)
            //{
            //    int actualnums = itemDataGridView[4, i].Value == null ? 0 : Convert.ToInt32(itemDataGridView[4, i].Value);
            //    if (actualnums != 0)
            //    {
            //        string name = itemDataGridView[1, i].Value == null ? "" : itemDataGridView[1, i].Value.ToString();
            //        if (name.Length < 4)
            //        {
            //            name += "\t\t";
            //        }
            //        else if (name.Length <= 6)
            //        {
            //            name += "\t";
            //        }
            //        else
            //        {
            //            name = name.Substring(0, 6) + "... ";
            //        }
            //        string price = itemDataGridView[2, i].Value == null ? "" : itemDataGridView[2, i].Value.ToString();
            //        price = float.Parse(price).ToString("0.00");
            //        string amount = itemDataGridView[7, i].Value == null ? "" : itemDataGridView[7, i].Value.ToString();
            //        amount = float.Parse(amount).ToString("0.00");
            //        sb.Append(name + price + "\t" + actualnums + "\t" + amount + "\n");
            //    }
            //}
            for (int i = 0; i < printObjectlist.Count; i++ )
            {
                GoodsPrint obj = printObjectlist[i];
                string name = obj.Name;
                if (name.Length < 4)
                {
                    name += "\t\t";
                }
                else if (name.Length <= 6)
                {
                    name += "\t";
                }
                else
                {
                    name = name.Substring(0, 6) + "... ";
                }
                if (obj.SerialNumber.Length == 5)
                {
                    sb.Append(name + obj.Price.ToString("0.00") + "\t" + obj.Weight.ToString("0.000") + "\t" + (obj.Price * obj.Weight).ToString("0.00") + "\n");
                }
                else
                {
                    sb.Append(name + obj.Price.ToString("0.00") + "\t" + obj.Nums + "\t" + (obj.Price*obj.Nums).ToString("0.00") + "\n");
                }
               
            }


            sb.Append("-----------------------------------------------------------------\n");
            if(isResale)
            {
                //sb.Append("数量: " + resaletotalNumlabel.Text + "\t\t 合计: " + resaletotalsum.Text + "\n");
            }else
            {
                sb.Append("数量: " + totalSum.Text + "\t\t 合计: " + totalAmount.Text + "\n");
            }


            sb.Append("实际付款金额:" + " " + actualPayAmount + "\n");

            sb.Append("-----------------------------------------------------------------\n");

            sb.Append("地址：" + address + "\n");
            sb.Append("             谢谢惠顾欢迎下次光临                ");

            Console.WriteLine(sb.ToString());
            return sb.ToString().Split('\n');

        }

        private int index;

        private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font InvoiceFont = new Font("Arial", 10, FontStyle.Regular);
            SolidBrush GrayBrush = new SolidBrush(Color.Black);
            string[] strs = GetPrintStr();
            int y = 0;
            while (index < strs.Length)
            {
                g.DrawString(GetPrintStr()[index++], InvoiceFont, GrayBrush, 5, 5 + y);
                y += 15;
                if (y > e.PageBounds.Height)
                {
                    e.HasMorePages = true;
                    return;
                }

            }
            index = 0;
            e.HasMorePages = false;
        }


        /// <summary>
        /// 设置会员信息类
        /// </summary>
        /// <param name="obj"></param>
        public void SetAssociatorInfo(AssociatorInfo obj)
        {
            associatorInfo = obj;
            memberName.Text = obj.Name;
            memberCard.Text = obj.CardNumber;
            memberPhone.Text = obj.MobilePhone;
        }

        /// <summary>
        /// 扫码添加零售商品
        /// </summary>
        private void AddResaleGoods()
        {
            string numberString = numberTextBox.Text;
            numberTextBox.Text = "";
            if (string.IsNullOrEmpty(numberString) || (!numberString.StartsWith("28") && !numberString.StartsWith("69")) || (numberString.Length != 18 && numberString.Length != 13))
            {
                MessageBox.Show("扫描的条码不正确，请重新扫描", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string serial = "";
            string weight = "";
            if (numberString.Length == 18)
            {
                serial = numberString.Substring(2, 5);
            }
            else if (numberString.StartsWith("28"))
            {
                serial = numberString.Substring(2, 5);
            }
            else
            {
                serial = numberString;
            }
            BranchZcGoodsMasterService branchGoodsService = new BranchZcGoodsMasterService();
            ZcGoodsMaster obj = branchGoodsService.FindBySerialNumber(serial);
            if (obj == null)
            {
                MessageBox.Show("没有此货号对应的商品信息，请检查后重新操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool isWeightGoods = branchGoodsService.IsWeightGoods(obj.Id);
            if (isWeightGoods)
            {
                if (numberString.Length == 18)
                {
                    weight = float.Parse(numberString.Substring(12, 5).Insert(2, ".")).ToString();
                }
                else 
                {
                    weight = float.Parse(numberString.Substring(7, 5).Insert(2, ".")).ToString();
                }
            }

            GoodsPrint goodsPrint = new GoodsPrint();
            bool isExist = false;
            for (int i = 0; i < itemDataGridView.Rows.Count; i++)
            {
                if (serial.Equals(itemDataGridView[0, i].Value.ToString()))
                {
                    isExist = true;
                    if (AddOrDelete)
                    {
                        float nums = float.Parse(itemDataGridView[15, i].Value.ToString());
                        float price = float.Parse(itemDataGridView[2, i].Value == null ? "0" : itemDataGridView[2, i].Value.ToString());
                        if (isWeightGoods)
                        {
                            float old_weight = string.IsNullOrEmpty(itemDataGridView[8, i].Value.ToString()) ? 0 : float.Parse(itemDataGridView[8, i].Value.ToString());
                            itemDataGridView[8, i].Value = old_weight + float.Parse(weight);
                            itemDataGridView[16, i].Value = MoneyFormat.RountFormat((old_weight + float.Parse(weight)) * price);
                        }
                        else 
                        {
                            itemDataGridView[16, i].Value = MoneyFormat.RountFormat((nums + 1) * price);
                        }
                        itemDataGridView[15, i].Value = nums + 1;
                        itemDataGridView.Rows[i].Selected = true;
                        itemDataGridView.CurrentCell = itemDataGridView.Rows[i].Cells[0];

                        if (!isWeightGoods)
                        {
                            foreach (GoodsPrint gp in printObjectlist)
                            {
                                if (gp.SerialNumber.Equals(serial))
                                {
                                    gp.Nums += 1;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            goodsPrint.SerialNumber = serial;
                            goodsPrint.Name = obj.GoodsName;
                            goodsPrint.Nums = 1;
                            goodsPrint.Price = obj.GoodsPrice;
                            goodsPrint.Weight = float.Parse(weight);
                            goodsPrint.BarCode = numberString;
                            goodsPrint.GoodsFileId = obj.Id;
                            printObjectlist.Add(goodsPrint);
                        }
                        break;
                    }
                    else
                    {
                        float nums = float.Parse(itemDataGridView[15, i].Value.ToString());
                        float old_weight = string.IsNullOrEmpty(itemDataGridView[8, i].Value.ToString()) ? 0 : float.Parse(itemDataGridView[8, i].Value.ToString());
                        if (isWeightGoods)
                        {
                            if (float.Parse(weight) > old_weight)
                            {
                                MessageBox.Show("扫码减去的商品重量不能大于已扫描的商品重量", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                setFlagLabel();
                                return;
                            }
                        }

                        if (nums == 1)
                        {
                            bool isHave = false;
                            foreach (GoodsPrint gp in printObjectlist)
                            {
                                if (gp.SerialNumber.Equals(serial))
                                {
                                    if (!isWeightGoods)
                                    {
                                        printObjectlist.Remove(gp);
                                        isHave = true;
                                        break;
                                    }
                                    else 
                                    {
                                        if (gp.Weight == float.Parse(weight))
                                        {
                                            printObjectlist.Remove(gp);
                                            isHave = true;
                                            break;
                                        }
                                        else 
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    continue;
                                    
                                }
                            }
                            if( !isHave)
                            {
                                MessageBox.Show("没有此商品对应的扫码销售信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                setFlagLabel();
                                return;
                            }
                            itemDataGridView.Rows.RemoveAt(i);
                            setFlagLabel();
                        }
                        else
                        {
                            if (!isWeightGoods)
                            {
                                foreach (GoodsPrint gp in printObjectlist)
                                {
                                    if (gp.SerialNumber.Equals(serial))
                                    {
                                        gp.Nums -= 1;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                bool isHaveResaleGoods = false;
                                foreach (GoodsPrint gp in printObjectlist)
                                {
                                    if (gp.SerialNumber.Equals(serial) && gp.Weight == float.Parse(weight))
                                    {
                                        printObjectlist.Remove(gp);
                                        isHaveResaleGoods = true;
                                        break;
                                    }
                                }
                                if (!isHaveResaleGoods)
                                {
                                    MessageBox.Show("没有此商品对应的扫码销售信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    setFlagLabel();
                                    return;
                                }
                            }
                            float price = float.Parse(itemDataGridView[2, i].Value == null ? "0" : itemDataGridView[2, i].Value.ToString());
                            itemDataGridView[15, i].Value = nums - 1;
                            if (isWeightGoods)
                            {
                                itemDataGridView[8, i].Value = old_weight - float.Parse(weight);
                                itemDataGridView[16, i].Value = MoneyFormat.RountFormat((old_weight - float.Parse(weight)) * price);
                            }
                            else 
                            {
                                itemDataGridView[16, i].Value = MoneyFormat.RountFormat((nums - 1) * price);
                            }
                            itemDataGridView.Rows[i].Selected = true;
                            itemDataGridView.CurrentCell = itemDataGridView.Rows[i].Cells[0];
                            setFlagLabel();
                            break;
                        }
                    }
                }
            }
            if (!isExist)
            {
                if (AddOrDelete)
                {
                    if (isWeightGoods)
                    {
                        itemDataGridView.Rows.Add(new Object[] { serial, obj.GoodsName, obj.GoodsPrice, "", "", "", "", "", weight, "", "", "", "", "", obj.Id, 1, (obj.GoodsPrice*float.Parse(weight)).ToString("0.00") });
                    }
                    else
                    {
                        itemDataGridView.Rows.Add(new Object[] { serial, obj.GoodsName, obj.GoodsPrice, "", "", "", "", "", weight, "", "", "", "", "", obj.Id, 1, obj.GoodsPrice });
                    }
                    
                    itemDataGridView.Rows[itemDataGridView.Rows.Count - 1].Selected = true;
                    itemDataGridView.CurrentCell = itemDataGridView.Rows[itemDataGridView.Rows.Count - 1].Cells[0];
                    ///将待打印的商品添加进去
                    goodsPrint.SerialNumber = serial;
                    goodsPrint.Name = obj.GoodsName;
                    goodsPrint.Nums = 1;
                    goodsPrint.Weight = isWeightGoods ? float.Parse(weight) : 0;
                    goodsPrint.Price = obj.GoodsPrice;
                    goodsPrint.BarCode = numberString;
                    goodsPrint.GoodsFileId = obj.Id;
                    printObjectlist.Add(goodsPrint);
                }
                else
                {
                    MessageBox.Show("已扫的商品中没有此商品，无法进行减去扫码操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    setFlagLabel();
                    return;
                }
            }
            ///计算数量小计和金额总计
            ResaleCalculate();
        }

        /// <summary>
        /// 限制输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resaleNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 点击数量修改按钮
        /// </summary>
        private void openChangeNums()
        {
            if (itemDataGridView.RowCount == 0)
            {
                return;
            }
            string goodsFileId = itemDataGridView.CurrentRow.Cells[14].Value.ToString();
            BranchZcGoodsMasterService branchService = new BranchZcGoodsMasterService();
            bool isWeightGoods = branchService.IsWeightGoods(goodsFileId);
            if (isWeightGoods)
            {
                ///说明是称重的商品，无法进行数量修改
                MessageBox.Show("称重商品无法手动修改数量", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                InputNums obj = new InputNums(this);
                obj.Show();
            }
        }

        /// <summary>
        /// 修改不需称重商品数量
        /// </summary>
        /// <param name="num"></param>
        public void ResaleChangeNums(string num)
        {
            int index = itemDataGridView.CurrentRow.Index;
            itemDataGridView.Rows[index].Cells[15].Value = num;
            itemDataGridView.Rows[index].Cells[16].Value = MoneyFormat.RountFormat(float.Parse(itemDataGridView.Rows[index].Cells[2].Value.ToString()) * float.Parse(num));
           foreach(GoodsPrint obj in printObjectlist)
           {
               if (obj.SerialNumber.Equals(itemDataGridView.Rows[index].Cells[0].Value.ToString()))
               {
                   obj.Nums = float.Parse(num);
                   break;
               }
           }
            //计算总的数量和金额的算法
            ResaleCalculate();
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        private void deleteResaleGoods()
        {
            if (itemDataGridView.RowCount == 0)
            {
                return;
            }
            string name = itemDataGridView.CurrentRow.Cells[1].Value.ToString();
            DialogResult dr = MessageBox.Show("确定删除" + name + "?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                int index = itemDataGridView.CurrentRow.Index;
                List<GoodsPrint> deleteList = new List<GoodsPrint>();
                foreach (GoodsPrint obj in printObjectlist)
                {
                    if (obj.SerialNumber.Equals(itemDataGridView.Rows[index].Cells[0].Value.ToString()))
                    {
                        deleteList.Add(obj);
                    }
                }
                foreach(GoodsPrint obj in deleteList)
                {
                    printObjectlist.Remove(obj);
                }
                itemDataGridView.Rows.RemoveAt(index);
                //计算总的数量和金额的算法
                ResaleCalculate();
            }
        }

        /// <summary>
        /// 零售数量金额合计计算计算
        /// </summary>
        private void ResaleCalculate()
        {
            float totalSum = 0;
            float totalMoney = 0;
            for (int i = 0; i < itemDataGridView.RowCount; i++)
            {
                float nums = float.Parse(itemDataGridView.Rows[i].Cells[15].Value.ToString());
                float money = float.Parse(itemDataGridView.Rows[i].Cells[16].Value.ToString());
                totalSum += nums;
                totalMoney += money;
            }

            this.totalSum.Text = MoneyFormat.RountFormat(totalSum);
            this.totalAmount.Text = MoneyFormat.RountFormat(totalMoney);
        }

        /// <summary>
        /// 退货数量金额合计计算
        /// </summary>
        private void refundCalculate()
        {
            float totalSum = 0;
            float totalMoney = 0;
            for (int i = 0; i < itemDataGridView.RowCount; i++)
            {
                float nums = itemDataGridView.Rows[i].Cells[4].Value == null ? 0 : float.Parse(itemDataGridView.Rows[i].Cells[4].Value.ToString());
                float money = itemDataGridView.Rows[i].Cells[19].Value == null ? 0 : float.Parse(itemDataGridView.Rows[i].Cells[19].Value.ToString());
                totalSum += nums;
                totalMoney += money;
            }

            this.totalSum.Text = MoneyFormat.RountFormat(totalSum);
            this.totalAmount.Text = MoneyFormat.RountFormat(totalMoney);
        }

        /// <summary>
        /// 保存零售信息
        /// </summary>
        /// <param name="waterNumber"></param>
        /// <param name="payInfoId">
        /// 支付主表id
        /// </param>
        public void saveResaleInform(string waterNumber, string payInfoId)
        {
            ///零售主表
            Resale resale = new Resale();
            resale.Id = Guid.NewGuid().ToString();
            resale.CreateTime = DateTime.Now;
            resale.UpdateTime = DateTime.Now;
            resale.WaterNumber = waterNumber;
            resale.Nums = totalSum.Text;
            resale.Money = totalAmount.Text;
            ///TODO  暂时未加入折扣，优惠金额等算法
            resale.ActualMoney = totalAmount.Text;
            resale.BranchId = LoginUserInfo.branchId;
            resale.SaleManId = LoginUserInfo.id;
            resale.memberId = associatorInfo == null ? string.Empty : associatorInfo.Id;
            resale.WaterNumber = waterNumber;
            resale.PayInfoId = payInfoId;
            
            List<ResaleItem> list = new List<ResaleItem>();
            for (int i = 0; i < printObjectlist.Count; i++)
            {
                GoodsPrint goodsPrint = printObjectlist[i];
                ResaleItem obj = new ResaleItem();
                obj.Id = Guid.NewGuid().ToString();
                obj.CreateTime = DateTime.Now;
                obj.UpdateTime = DateTime.Now;
                obj.ResaleId = resale.Id;
                obj.GoodsFileId = goodsPrint.GoodsFileId;
                obj.Nums = goodsPrint.Nums.ToString();
                obj.Price = goodsPrint.Price.ToString();
                obj.BarCode = goodsPrint.BarCode;
                if (goodsPrint.Weight != 0)
                {
                    obj.weight = goodsPrint.Weight.ToString();
                    obj.Money = MoneyFormat.RountFormat(goodsPrint.Weight * goodsPrint.Price);
                }
                else
                {
                    obj.Money = MoneyFormat.RountFormat(goodsPrint.Price * goodsPrint.Nums);
                }
                ///TODO  暂时未加入折扣，优惠金额等算法
                obj.ActualMoney = obj.Money;
                list.Add(obj);
            }
            BranchResaleItemService branchItemService = new BranchResaleItemService();
            BranchResaleService branchService = new BranchResaleService();
            ResaleItemService itemServcie = new ResaleItemService();
            ResaleService service = new ResaleService();
            branchService.AddResale(resale);
            branchItemService.AddResaleItem(list);
            ///上传零售信息
            if (PingTask.IsConnected)
            {
                ///连网状态
                service.AddResale(resale);
                itemServcie.AddResaleItem(list);
            }
            else
            {
                //断网状态
                List<UploadInfo> uploadList = new List<UploadInfo>();
                UploadInfo obj1 = new UploadInfo();
                obj1.Id = resale.Id;
                obj1.CreateTime = DateTime.Now;
                obj1.UpdateTime = DateTime.Now;
                obj1.Type = Constant.ZC_RESALE;
                uploadList.Add(obj1);
                foreach (ResaleItem obj in list)
                {
                    UploadInfo obj2 = new UploadInfo();
                    obj2.Id = obj.Id;
                    obj2.CreateTime = DateTime.Now;
                    obj2.UpdateTime = DateTime.Now;
                    obj2.Type = Constant.ZC_RESALE_ITME;
                    uploadList.Add(obj2);
                }
                UploadDao uploadDao = new UploadDao();
                uploadDao.AddUploadInfo(uploadList);
            }
            ///库存的减少
            BranchZcStoreHouseService branchStoreService = new BranchZcStoreHouseService();
            List<ZcStoreHouse> storeList = new List<ZcStoreHouse>();
            for (int i = 0; i < itemDataGridView.RowCount; i++)
            {
                String goodsFileId = itemDataGridView.Rows[i].Cells[14].Value.ToString();
                float nums = float.Parse(itemDataGridView.Rows[i].Cells[15].Value.ToString());
                string weightString = itemDataGridView.Rows[i].Cells[8].Value == null ? string.Empty : itemDataGridView.Rows[i].Cells[8].Value.ToString();
                ZcStoreHouse storeHouse = branchStoreService.FindByGoodsFileIdAndBranchId(goodsFileId, LoginUserInfo.branchId);
                if(storeHouse != null){
                    float oldNums = float.Parse(storeHouse.Store);
                    float oldMoney = float.Parse(storeHouse.StoreMoney);
                    storeHouse.UpdateTime = DateTime.Now;
                    storeHouse.Store = (oldNums - nums).ToString();
                    storeHouse.StoreMoney = (oldMoney * (oldNums - nums) / oldNums).ToString();
                    if(!string.IsNullOrEmpty(weightString)){
                        float oldWeight = float.Parse(storeHouse.weight);
                        storeHouse.weight = (oldWeight - float.Parse(weightString)).ToString();
                    }
                    storeList.Add(storeHouse);
                }else
                {
                    log.Error("不存在" + itemDataGridView.Rows[i].Cells[1].Value.ToString() + "的库存信息", new Exception("不存在" + itemDataGridView.Rows[i].Cells[1].Value.ToString() + "的库存信息"));
                }
                
            }
            branchStoreService.updateStoreHouse(storeList);
            ZcStoreHouseService storeService = new ZcStoreHouseService();
            if (PingTask.IsConnected)
            {
                storeService.updateStoreHouse(storeList);
            }
            else 
            {
                List<UploadInfo> uploadList = new List<UploadInfo>();
                for (int i = 0; i < storeList.Count; i++) {
                    ZcStoreHouse obj = storeList[i];
                    UploadInfo info = new UploadInfo();
                    info.Id = obj.Id;
                    info.CreateTime = DateTime.Now;
                    info.UpdateTime = DateTime.Now;
                    info.Type = Constant.ZC_STOREHOUSE_UPDATE;
                    uploadList.Add(info);
                }
                UploadDao uploadDao = new UploadDao();
                uploadDao.AddUploadInfo(uploadList);
            }

            //初始化
            resaleInit();
        }

        /// <summary>
        /// 焦点离开numberTextbox事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numberTextBox_Leave(object sender, EventArgs e)
        {
            numberTextBox.Focus();
        }

        /// <summary>
        /// 窗体激活事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerDelivery_Activated(object sender, EventArgs e)
        {
            numberTextBox.Focus();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="p"></param>
        public void showTransitOrder(ZcOrderTransit obj)
        {
            zc_order_transit_id = obj.Id;
            CustomerDeliveryController controller = new CustomerDeliveryController();
            DataSet ds = controller.GetGoodDataSetById(obj.Id);
            itemDataGridView.AutoGenerateColumns = false;
            itemDataGridView.DataSource = ds;
            itemDataGridView.DataMember = "zc_order_transit";
            orderNumerLabel.Text = obj.OrderNum;
            initDifference();
        }

        private Resale resaleObj;

        /// <summary>
        /// 加载退货信息
        /// </summary>
        /// <param name="resale"></param>
        public void showRefundInfo(Resale resale)
        {
            BranchAssociatorInfoService branchAssociatorInfoService = new BranchAssociatorInfoService();
            associatorInfo = branchAssociatorInfoService.FindById(resale.memberId);
            memberCard.Text = associatorInfo.CardNumber;
            memberName.Text = associatorInfo.Name;
            memberPhone.Text = associatorInfo.MobilePhone;
            this.resaleObj = resale;
            if (!string.IsNullOrEmpty(resale.OrderId))
            {
                zc_order_transit_id = resale.OrderId;
                BranchZcOrderHistoryService branchhistoryService = new BranchZcOrderHistoryService();
                ZcOrderHistory history = branchhistoryService.FindById(resale.OrderId);
                orderNumerLabel.Text = history.OrderNum;
                workModelabel.Text = "按单退货";
            }
            else
            {
                workModelabel.Text = "零售退货";
            }
            MysqlDBHelper dbHelper = new MysqlDBHelper();
            string sql = "select c.id as goodsfile_id,c.SERIALNUMBER,c.GOODS_NAME as name , a.price as g_price,sum(a.nums) as nums, sum(a.weight) as weight, a.money as receive_money "
                +" from zc_resale_item a left join zc_resale b on a.resale_id = b.id "
                + " left join zc_goods_master c on a.goodsFile_id = c.ID where a.resale_id = '" + resale.Id + "' group by c.SERIALNUMBER";
            DataSet ds = dbHelper.GetDataSet(sql, "zc_resale_item");
            itemDataGridView.AutoGenerateColumns = false;
            itemDataGridView.DataSource = ds;
            itemDataGridView.DataMember = "zc_resale_item";
        }

        /// <summary>
        /// 退货支付
        /// </summary>
        private void refundSettlement()
        {
            bool isHave = false;
            for (int i = 0; i < itemDataGridView.RowCount; i++)
            {
                float nums = itemDataGridView.Rows[i].Cells[4].Value == null ? 0 : float.Parse(itemDataGridView.Rows[i].Cells[4].Value.ToString());
                if (nums != 0)
                {
                    isHave = true;
                }
                else
                {
                    continue;
                }
            }
            if (!isHave)
            {
                MessageBox.Show("当前没有货物需要进行退货", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                return;
            }
            BranchPayInfoService branchPayInfoService = new BranchPayInfoService();
            BranchPayInfoItemService branchPayInfoItemService = new BranchPayInfoItemService();
            PayInfo payhistory = branchPayInfoService.FindById(resaleObj.PayInfoId);
            ///默认只有一种支付
            List<PayInfoItem> payList = branchPayInfoItemService.FindByPayId(resaleObj.PayInfoId);
            PayInfoItem payInfoItem = payList[0];
            string showstring = "";
            if (payInfoItem.PayMode == BranchPay.money)
            {
                showstring = "退货金额为：" + totalAmount.Text + ",退款方式为现金退款,是否确认退货?";
            }
            else if (payInfoItem.PayMode == BranchPay.WxPay)
            {
                showstring = "退货金额为：" + totalAmount.Text + ",退款方式为微信退款,是否确认退货?";
            }
            else if (payInfoItem.PayMode == BranchPay.ZFBPay)
            {
                showstring = "退货金额为：" + totalAmount.Text + ",退款方式为支付宝退款,是否确认退货?";
            }

            DialogResult dr = MessageBox.Show(showstring, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                ///原路返回金额
                if (payInfoItem.PayMode == BranchPay.WxPay)
                {
                    string amount = float.Parse(resaleObj.ActualMoney).ToString("0.00").Replace(".", "");
                    WxPayData result = Refund.Run(null, resaleObj.WaterNumber, amount, float.Parse(totalAmount.Text).ToString("0.00").Replace(".", ""));
                    if ("SUCCESS".Equals(result.GetValue("result_code")))
                    {
                        MessageBox.Show("微信支付退款成功!");
                    }
                    else
                    {
                        if (result.IsSet("err_code_des"))
                        {
                            MessageBox.Show(result.GetValue("err_code_des").ToString());
                            return;
                        }
                        else
                        {
                            MessageBox.Show("未知错误!");
                            return;
                        }

                    }
                }

                PayInfoService payInfoService = new PayInfoService();
                PayInfoItemService payInfoItemService = new PayInfoItemService();
                PayInfo payInfo = new PayInfo();
                payInfo.Id = Guid.NewGuid().ToString();
                payInfo.CreateTime = DateTime.Now;
                payInfo.UpdateTime = DateTime.Now;
                payInfo.MemberId = associatorInfo == null ? string.Empty : associatorInfo.Id;
                payInfo.Money = totalAmount.Text;
                payInfo.BranchId = LoginUserInfo.branchId;
                payInfo.salesmanId = LoginUserInfo.id;
                List<PayInfoItem> payInfoItemList = new List<PayInfoItem>();
                for (int i = 0; i < itemDataGridView.RowCount; i++)
                {
                    float nums = itemDataGridView.Rows[i].Cells[4].Value == null ? 0 : float.Parse(itemDataGridView.Rows[i].Cells[4].Value.ToString());
                    if (nums != 0)
                    {
                        PayInfoItem itemPay = new PayInfoItem();
                        itemPay.Id = Guid.NewGuid().ToString();
                        itemPay.CreateTime = DateTime.Now;
                        itemPay.UpdateTime = DateTime.Now;
                        itemPay.PayInfoId = payInfo.Id;
                        itemPay.PayMode = payInfoItem.PayMode;
                        itemPay.Money = itemDataGridView.Rows[i].Cells[19].Value.ToString();
                        payInfoItemList.Add(itemPay);
                    }
                }
                branchPayInfoService.AddPayInfo(payInfo);
                branchPayInfoItemService.AddPayInfoItem(payInfoItemList);

                ///保存流水信息
                ResaleWaterNumber = "KTH" + DateTime.Now.ToString("yyyyMMddhhmmss") + LoginUserInfo.street; ;
                Resale resale = new Resale();
                resale.Id = Guid.NewGuid().ToString();
                resale.CreateTime = DateTime.Now;
                resale.UpdateTime = DateTime.Now;
                resale.WaterNumber = ResaleWaterNumber;
                resale.Nums = totalSum.Text;
                resale.Money = MoneyFormat.RountFormat(-float.Parse(totalAmount.Text));
                ///TODO  暂时未加入折扣，优惠金额等算法
                resale.ActualMoney = resale.Money;
                resale.BranchId = LoginUserInfo.branchId;
                resale.SaleManId = LoginUserInfo.id;
                resale.memberId = associatorInfo == null ? string.Empty : associatorInfo.Id;
                resale.PayInfoId = payInfo.Id;
                if(!string.IsNullOrEmpty(resaleObj.OrderId)){
                    resale.OrderId = resaleObj.OrderId;
                }
                List<ResaleItem> list = new List<ResaleItem>();
                for (int i = 0; i < printObjectlist.Count; i++)
                {
                    GoodsPrint goodsPrint = printObjectlist[i];
                    float nums = goodsPrint.Nums;
                    if (nums == 0)
                    {
                        continue;
                    }
                    ResaleItem obj = new ResaleItem();
                    obj.Id = Guid.NewGuid().ToString();
                    obj.CreateTime = DateTime.Now;
                    obj.UpdateTime = DateTime.Now;
                    obj.ResaleId = resale.Id;
                    obj.GoodsFileId = goodsPrint.GoodsFileId;
                    obj.BarCode = goodsPrint.BarCode;
                    obj.Nums = goodsPrint.Nums.ToString();
                    obj.Price = goodsPrint.Price.ToString();
                    if(goodsPrint.Weight != 0){
                        obj.weight = goodsPrint.Weight.ToString();
                        obj.Money = MoneyFormat.RountFormat(-goodsPrint.Weight * goodsPrint.Price);
                    }else
                    {
                        obj.Money = MoneyFormat.RountFormat(-goodsPrint.Nums * goodsPrint.Price);
                    }
                    ///TODO  暂时未加入折扣，优惠金额等算法
                    obj.ActualMoney = obj.Money;
                    list.Add(obj);
                }
                BranchResaleItemService branchItemService = new BranchResaleItemService();
                BranchResaleService branchService = new BranchResaleService();
                ResaleItemService itemServcie = new ResaleItemService();
                ResaleService service = new ResaleService();
                branchItemService.AddResaleItem(list);
                branchService.AddResale(resale);
                List<UploadInfo> uploadList = new List<UploadInfo>();
                if(!string.IsNullOrEmpty(resaleObj.OrderId))
                {
                    BranchZcOrderHistoryService branchZcOrderHistoryService = new BranchZcOrderHistoryService();
                    ZcOrderHistory zcOrderHistory = branchZcOrderHistoryService.FindById(zc_order_transit_id);

                    ZcOrderRefund zcOrderRefund = new ZcOrderRefund();
                    zcOrderRefund.Id = Guid.NewGuid().ToString();
                    zcOrderRefund.CreateTime = DateTime.Now;
                    zcOrderRefund.UpdateTime = DateTime.Now;
                    zcOrderRefund.Order_id = zcOrderHistory.Id;
                    zcOrderRefund.Order_amount = zcOrderHistory.OrderAmount.ToString("0.00");
                    zcOrderRefund.Actual_amount = (float.Parse(zcOrderHistory.ActualMoney == null ? "0" : zcOrderHistory.ActualMoney) - returnAmount).ToString("0.00");
                    zcOrderRefund.Order_refund_status = "1";//退款单未审批
                    zcOrderRefund.Hand_date = DateTime.Now;

                    //MessageBox.Show("‘ID’:" + zcOrderRefund.Id + "‘UpdateTime’:" + zcOrderRefund.UpdateTime + "‘Order_id’:" + zcOrderRefund.Order_id + "‘Order_amount’:" + zcOrderRefund.Order_amount + "‘Actual_amount’:" + zcOrderRefund.Actual_amount);

                    
                    List<ZcOrderRefundItem> refundlist = new List<ZcOrderRefundItem>();
                    for (int i = 0; i < itemDataGridView.RowCount; i++)
                    {
                        float nums = itemDataGridView.Rows[i].Cells[4].Value == null ? 0 : float.Parse(itemDataGridView.Rows[i].Cells[4].Value.ToString());
                        if(nums == 0){
                            continue;
                        }
                        ZcOrderRefundItem zcOrderRefundItem = new ZcOrderRefundItem();
                        zcOrderRefundItem.Id = Guid.NewGuid().ToString();
                        zcOrderRefundItem.CreateTime = DateTime.Now;
                        zcOrderRefundItem.UpdateTime = DateTime.Now;
                        zcOrderRefundItem.SerialNumber = itemDataGridView.Rows[i].Cells[0].Value.ToString();
                        zcOrderRefundItem.Order_id = zc_order_transit_id;
                        zcOrderRefundItem.Order_nums = itemDataGridView.Rows[i].Cells[3].Value.ToString();
                        zcOrderRefundItem.Refund_nums = itemDataGridView.Rows[i].Cells[4].Value.ToString();
                        zcOrderRefundItem.Price = itemDataGridView.Rows[i].Cells[2].Value.ToString();
                        zcOrderRefundItem.Refund_amount = itemDataGridView.Rows[i].Cells[19].Value.ToString();
                        zcOrderRefundItem.Salesman_id = LoginUserInfo.id;
                        zcOrderRefundItem.Hand_date = DateTime.Now;
                        zcOrderRefundItem.Street = LoginUserInfo.street;
                        zcOrderRefundItem.GoodsFile_id = itemDataGridView.Rows[i].Cells[14].Value.ToString();
                        zcOrderRefundItem.Refund_id = zcOrderRefund.Id;
                        zcOrderRefundItem.Refund_reason = itemDataGridView.Rows[i].Cells[20].Value == null ? string.Empty : itemDataGridView.Rows[i].Cells[20].Value.ToString();
                        refundlist.Add(zcOrderRefundItem);
                        if (PingTask.IsConnected)
                        {
                            //上传退款表数据
                            RefundInfoService refundInfoService = new RefundInfoService();
                            refundInfoService.AddZcOrderRefund(zcOrderRefund);
                            refundInfoService.AddZcOrderRefundItem(refundlist);
                            //上传历史表状态更新
                            ZcOrderHistoryService zcOrderHistoryService = new ZcOrderHistoryService();
                            zcOrderHistoryService.UpdateOrderStatusByIds(zc_order_transit_id, Constant.ORDER_STATUS_REFUND);
                        }
                        else
                        {
                            UploadInfo obj1 = new UploadInfo();
                            obj1.Id = zcOrderRefund.Id;
                            obj1.CreateTime = DateTime.Now;
                            obj1.UpdateTime = DateTime.Now;
                            obj1.Type = Constant.ZC_ORDER_REFUND;
                            uploadList.Add(obj1);
                            foreach (ZcOrderRefundItem obj in refundlist)
                            {
                                UploadInfo obj2 = new UploadInfo();
                                obj2.Id = obj.Id;
                                obj2.CreateTime = DateTime.Now;
                                obj2.UpdateTime = DateTime.Now;
                                obj2.Type = Constant.ZC_ORDER_REFUND_ITEM;
                                uploadList.Add(obj2);
                            }
                            UploadInfo obj3 = new UploadInfo();
                            obj3.Id = zcOrderRefund.Id;
                            obj3.CreateTime = DateTime.Now;
                            obj3.UpdateTime = DateTime.Now;
                            obj3.Type = Constant.ZC_ORDER_HISTORY_UPDATE;
                            uploadList.Add(obj3);
                        }
                    }

                    //添加退款明细
                    BranchZcOrderRefundService branchZcOrderRefundService = new BranchZcOrderRefundService();
                    BranchZcOrderRefundItemService branchZcOrderRefundItemService = new BranchZcOrderRefundItemService();
                    branchZcOrderRefundService.AddZcOrderRefund(zcOrderRefund);
                    branchZcOrderRefundItemService.AddZcOrderRefundItem(refundlist);
                    //修改本地历史订单状态
                    branchZcOrderHistoryService.UpdateOrderStatusByIds(zc_order_transit_id, Constant.ORDER_STATUS_REFUND);
                    //
                    MessageBox.Show("退款单已生成，请等待处理！");

                   
                }

                ///上传零售信息
                if (PingTask.IsConnected)
                {
                    ///连网状态
                    itemServcie.AddResaleItem(list);
                    service.AddResale(resale);
                    payInfoService.AddPayInfo(payInfo);
                    payInfoItemService.AddPayInfoItem(payInfoItemList);
                }
                else
                {
                    //断网状态

                    UploadInfo obj1 = new UploadInfo();
                    obj1.Id = resale.Id;
                    obj1.CreateTime = DateTime.Now;
                    obj1.UpdateTime = DateTime.Now;
                    obj1.Type = Constant.ZC_RESALE;
                    uploadList.Add(obj1);
                    foreach (ResaleItem obj in list)
                    {
                        UploadInfo obj2 = new UploadInfo();
                        obj2.Id = obj.Id;
                        obj2.CreateTime = DateTime.Now;
                        obj2.UpdateTime = DateTime.Now;
                        obj2.Type = Constant.ZC_RESALE_ITME;
                        uploadList.Add(obj2);
                    }
                    UploadInfo obj3 = new UploadInfo();
                    obj3.Id = resale.Id;
                    obj3.CreateTime = DateTime.Now;
                    obj3.UpdateTime = DateTime.Now;
                    obj3.Type = Constant.PAY_INFO;
                    uploadList.Add(obj1);
                    foreach (PayInfoItem obj in payInfoItemList)
                    {
                        UploadInfo obj2 = new UploadInfo();
                        obj2.Id = obj.Id;
                        obj2.CreateTime = DateTime.Now;
                        obj2.UpdateTime = DateTime.Now;
                        obj2.Type = Constant.PAY_INFO_ITEM;
                        uploadList.Add(obj2);
                    }
                    UploadDao uploadDao = new UploadDao();
                    uploadDao.AddUploadInfo(uploadList);
                }
                
            }
            
            printTicket(float.Parse(totalAmount.Text));
            ///初始化数据
            returnOfGoodsInit();
        }

        public void writeReason(string reason)
        {
            if (this.WorkMode.Equals(Constant.PICK_UP_GOODS))
            {
                itemDataGridView.CurrentRow.Cells[13].Value = reason;
            }
            else if (this.WorkMode.Equals(Constant.REFUND))
            {
                itemDataGridView.CurrentRow.Cells[20].Value = reason;
            }
        }

        public void AddRefundReason(string p)
        {
            itemDataGridView.CurrentRow.Cells[20].Value = p;
        }

        /// <summary>
        /// 显示当前扫码的模式
        /// </summary>
        private void setFlagLabel()
        {
            if (AddOrDelete)
            {
                AddOrDelete = false;
                flaglabel.Text = "(-)";
            }
            else
            {
                AddOrDelete = true;
                flaglabel.Text = "(+)";
            }
        }

    }
}
