﻿using Branch.com.proem.exm.window.main;
using Branch.com.proem.exm.window.receive;
using Branch.com.proem.exm.domain;
using Oracle.ManagedDataAccess.Client;
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
using Branch.com.proem.exm.service.branch;
using log4net;
using System.Text.RegularExpressions;
using Branch.com.proem.exm.service.main;
using Branch.com.proem.exm.window.util;
using Branch.com.proem.exm.service.master;
using Branch.com.proem.exm.service;
using MySql.Data.MySqlClient;
using Branch.com.proem.exm.dao.branch;
using Branch.com.proem.exm.dao.master;
using sorteSystem;
using System.Threading;

namespace Branch.com.proem.exm.window.receive
{
    /// <summary>
    /// 收货管理
    /// </summary>
    public partial class DeliveryGoods : Form
    {
        /// <summary>
        /// 日志log
        /// </summary>
        private ILog log = LogManager.GetLogger(typeof(DeliveryGoods));
        ItemInput itemsInput = null;

        private delegate void endSorteDelegate();

        Loading loading;

        private void endSorte()
        {
            loading = new Loading();
            loading.Show();
        }
        ///// <summary>
        ///// 业务员
        ///// </summary>
        //private ZcUserInfo zcUserInfo;
        ///// <summary>
        ///// 订单号
        ///// </summary>
        //private string orderNumber;
        ///// <summary>
        ///// 原始订单号
        ///// </summary>
        //private string oldOrderNumber;
        ///// <summary>
        ///// 调出分店
        ///// </summary>
        //private ZcBranchTotal branch;
        /// <summary>
        /// 选取单元格的行
        /// </summary>
        private int row = -1;
        /// <summary>
        /// 选取单元格的列
        /// </summary>
        private int column = -1;

        /// <summary>
        /// 收货标识
        /// </summary>
        private bool harvestFlag = true;

        /// <summary>
        /// 配送出库单id
        /// </summary>
        private string dispatchingId;

        public delegate void child_close();
        public event child_close delivery;

        public DeliveryGoods()
        {
            InitializeComponent();
        }

        private BranchMain branchMain;

        public DeliveryGoods(BranchMain branchMain)
        {
            InitializeComponent();
            this.branchMain = branchMain;
        }

        ///// <summary>
        ///// 主界面
        ///// </summary>
        //private BranchMain branchMain;

        ///// <summary>
        ///// 重载的构造函数
        ///// </summary>
        ///// <param name="branchMain"></param>
        //public DeliveryGoods(BranchMain branchMain)
        //{
        //    InitializeComponent();
        //    this.branchMain = branchMain;
        //}

        private void DeliveryGoods_Load(object sender, EventArgs e)
        {
            ////初始化当前分店
            //inNameTextBox.Text = LoginUserInfo.branchName;
            ////以下为暂时的默认数据
            //salesmanNameTextBox.Text = LoginUserInfo.name;
            //初始化当前分店
            inNameLabel.Text = LoginUserInfo.branchName;
            //以下为暂时的默认数据
            salesmanNameLabel.Text = LoginUserInfo.name;

            //itemsInput = new ItemInput(this, log);
            //itemsInput.TopLevel = false;
            //this.itemInputPanel.Controls.Add(itemsInput);
            //itemsInput.Show();

            BranchDispatchingWarehouseDao dao = new BranchDispatchingWarehouseDao();
            List<string> list = dao.getCountToday();
            if (list == null || list.Count == 0)
            {
                MessageBox.Show("今天无连锁配送单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (list.Count == 1)
            {
                ///加载配送单汇总数据
                loadGoods(list[0]);
                initDifference(list[0]);
            }
            else 
            {
                ///打开选择配送单界面
                DispatchChoose choose = new DispatchChoose(this);
                choose.ShowDialog();
            }

            //DownloadIdentifyService service = new DownloadIdentifyService();
            /////判断亭点是否收获
            //if (service.IsHarvest())
            //{
            //    harvestFlag = false;
            //    ///已经已收货
            //    loadHarvestGoods();
            //    loadTotal();
            //}
            //else    ///未收货
            //{
            //    loadGoods();
            //    initDifference();
            //}

            Times times = new Times();
            times.TopLevel = false;
            this.timePanel.Controls.Add(times);
            times.Show();
            ///初始化焦点事件  将焦点放到扫描框
            numberTextBox.Focus();
        }

        /// <summary>
        /// 加载
        /// </summary>
        private void loadTotal()
        {
            float totalSum = 0;
            float totalAmount = 0;
            try
            {
                for (int i = 0; i < itemDataGridView.RowCount; i++)
                {
                    totalSum += Convert.ToInt32((itemDataGridView[8, i].Value == null || itemDataGridView[8, i].Value.ToString().Trim() == "") ? "0" : itemDataGridView[8, i].Value.ToString());
                    totalAmount += float.Parse((itemDataGridView[10, i].Value == null || itemDataGridView[10, i].Value.ToString().Trim() == "") ? "0" : itemDataGridView[10, i].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                log.Error("类型转换异常", ex);
            }
            this.totalSum.Text = MoneyFormat.RountFormat(totalSum);
            this.totalAmount.Text = MoneyFormat.RountFormat(totalAmount);
        }

        /// <summary>
        /// 初始化显示差异difference
        /// </summary>
        private void initDifference(string id)
        {
            BranchInDao dao = new BranchInDao();
            List<string> list = dao.getByDispatchingId(id);
            if (list != null && list.Count == 1)
            {
                harvestFlag = true;
                for (int i = 0; i < itemDataGridView.RowCount; i++)
                {
                    float nums = float.Parse(string.IsNullOrEmpty(itemDataGridView[3, i].Value.ToString()) ? "0" : itemDataGridView[3, i].Value.ToString());
                    float actual = float.Parse(string.IsNullOrEmpty(itemDataGridView[4, i].Value.ToString()) ? "0" : itemDataGridView[4, i].Value.ToString());
                    itemDataGridView[5, i].Value = (nums - actual).ToString("0.00");
                }
            }
            else 
            {
                harvestFlag = false;
                for (int i = 0; i < itemDataGridView.RowCount; i++)
                {
                    float nums = float.Parse(string.IsNullOrEmpty(itemDataGridView[3, i].Value.ToString()) ? "0" : itemDataGridView[3, i].Value.ToString());
                    itemDataGridView[4, i].Value = nums.ToString("0.00");
                    itemDataGridView[5, i].Value = "0";
                }
            }
        }

        /// <summary>
        /// 加载收货过的商品信息列表
        /// </summary>
        //private void loadHarvestGoods()
        //{
        //    DateTime first = DateTime.Today;
        //    DateTime last = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
        //    MysqlDBHelper dbHelper = new MysqlDBHelper();
        //    MySqlConnection conn = null;
        //    string sql = "select serialNumber , name, classify, goods_unit, goods_specifications, goods_price as g_price, nums, actual_quantity, order_amount, receive_amount,sortenum  from daily_receive_goods where createTime between @first and @last ";
        //    MySqlCommand cmd = new MySqlCommand();
        //    try
        //    {
        //        conn = dbHelper.GetConnection();
        //        cmd.Connection = conn;
        //        cmd.CommandText = sql;
        //        cmd.Parameters.AddWithValue("@first", first);
        //        cmd.Parameters.AddWithValue("@last", last);
        //        DataSet ds = new DataSet();
        //        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        //        da.Fill(ds, "daily_receive_goods");
        //        receiveAmount.DataPropertyName = "receive_amount";
        //        actualQuantity.DataPropertyName = "actual_quantity";
        //        orderAmount.DataPropertyName = "order_amount";
        //        classify.DataPropertyName = "classify";
        //        actualQuantity.ReadOnly = true;

        //        itemDataGridView.DataSource = ds;
        //        itemDataGridView.DataMember = "daily_receive_goods";
        //        itemDataGridView.CurrentCell = null;//不默认选中
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("获取当天收货后的商品信息失败", ex);
        //    }
        //    finally
        //    {
        //        cmd.Dispose();
        //        dbHelper.CloseConnection(conn);
        //    }
        //}

        private void returnButton_Click(object sender, EventArgs e)
        {
            //BranchMain branchMain = new BranchMain();
            //branchMain.Show();
            this.Close();
        }

        private void scanButton_Click(object sender, EventArgs e)
        {
            Scan scan = new Scan(this);
            scan.ShowDialog();
            //扫描
        }

        private void deliveryButton_Click(object sender, EventArgs e)
        {
            if (!harvestFlag)
            {
                MessageBox.Show("当前货物已确认收货，无法重复确认收货!", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            bool deli = false;
            bool prompt = false;
            if (itemDataGridView.Rows.Count == 0)
            {
                MessageBox.Show("当前并无需要提交的收货单！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            for (int i = 0; i < itemDataGridView.Rows.Count; i++)
            {
                if (float.Parse(itemDataGridView[4, i].Value == null ? "0" : itemDataGridView[4, i].Value.ToString()) == 0)
                {
                    deli = true;
                }

                if (float.Parse(itemDataGridView[5, i].Value == null ? "0" : itemDataGridView[5, i].Value.ToString()) != 0)
                {
                    prompt = true;
                }
            }
            if (deli || prompt)
            {
                string sInput = "";
                DGPrompt dGPrompt = new DGPrompt();
                if (dGPrompt.ShowDialog() == DialogResult.OK)
                {//
                    sInput = dGPrompt.inputMess;
                    //MessageBox.Show(sInput);
                }
                if (!sInput.Equals("y"))
                {
                    return;
                }
            }


            endSorteDelegate endDelegate = endSorte;
            this.BeginInvoke(endDelegate);
            Thread objThread = new Thread(new ThreadStart(delegate
            {

                BranchInDao branchInDao = new BranchInDao();
                BranchInItemDao branchInItemDao = new BranchInItemDao();

                BranchIn branchIn = new BranchIn();
                branchIn.id = Guid.NewGuid().ToString().Replace("-", "");
                branchIn.createTime = DateTime.Now;
                branchIn.updateTime = DateTime.Now;
                branchIn.InOdd = "TDRKD" + DateTime.Now.ToString("yyyyMMddHHmmssSSS");
                branchIn.dispatching_id = dispatchingId;
                branchIn.branch_id = LoginUserInfo.branchId;
                branchIn.user_id = LoginUserInfo.id;
                float number = 0;
                float weight = 0;
                float money = 0;
                List<BranchInItem> list = new List<BranchInItem>();
                //确定收货
                for (int i = 0; i < itemDataGridView.RowCount; i++)
                {
                    BranchInItem obj = new BranchInItem();
                    float nums = float.Parse(string.IsNullOrEmpty(itemDataGridView[4, i].Value.ToString()) ? "0" : itemDataGridView[4, i].Value.ToString());
                    if (nums == 0)
                    {
                        continue;
                    }
                    number += nums;
                    obj.id = Guid.NewGuid().ToString().Replace("-", "");
                    obj.createTime = DateTime.Now;
                    obj.updateTime = DateTime.Now;
                    obj.branchIn_id = branchIn.id;
                    obj.nums = string.IsNullOrEmpty(itemDataGridView[4, i].Value.ToString()) ? "0" : itemDataGridView[4, i].Value.ToString();
                    obj.weight = string.IsNullOrEmpty(itemDataGridView[6, i].Value.ToString()) ? "0" : itemDataGridView[6, i].Value.ToString();
                    weight += float.Parse(obj.weight);
                    obj.money = string.IsNullOrEmpty(itemDataGridView[7, i].Value.ToString()) ? "0" : itemDataGridView[7, i].Value.ToString();
                    money += float.Parse(obj.money);
                    obj.goodsFile_id = itemDataGridView[8, i].Value.ToString();
                    obj.price = string.IsNullOrEmpty(itemDataGridView[2, i].Value.ToString()) ? "0" : itemDataGridView[2, i].Value.ToString();
                    list.Add(obj);
                }
                branchIn.nums = number.ToString();
                branchIn.weight = weight.ToString();
                branchIn.money = money.ToString();

                if (prompt)
                {
                    BranchDiff diff = new BranchDiff();
                    diff.id = Guid.NewGuid().ToString().Replace("-", "");
                    diff.createTime = DateTime.Now;
                    diff.updateTime = DateTime.Now;
                    diff.DiffOdd = "TDCYD" + DateTime.Now.ToString("yyyyMMddHHmmssSSS");
                    diff.branchIn_id = branchIn.id;
                    diff.branch_id = LoginUserInfo.branchId;
                    diff.user_id = LoginUserInfo.id;
                    List<BranchDiffItem> diffList = new List<BranchDiffItem>();
                    float diffNums = 0;
                    for (int i = 0; i < itemDataGridView.RowCount; i++)
                    {
                        if (float.Parse(itemDataGridView[5, i].Value == null ? "0" : itemDataGridView[5, i].Value.ToString()) != 0)
                        {
                            BranchDiffItem diffItem = new BranchDiffItem();
                            diffItem.id = Guid.NewGuid().ToString().Replace("-", "");
                            diffItem.branchDiff_id = diff.id;
                            diffItem.createTime = DateTime.Now;
                            diffItem.updateTime = DateTime.Now;
                            diffItem.nums = itemDataGridView[5, i].Value.ToString();
                            diffNums += float.Parse(diffItem.nums);
                            diffItem.goodsFile_id = itemDataGridView[8, i].Value.ToString();
                            diffItem.price = string.IsNullOrEmpty(itemDataGridView[2, i].Value.ToString()) ? "0" : itemDataGridView[2, i].Value.ToString();
                            diffList.Add(diffItem);
                        }
                    }
                    diff.nums = diffNums.ToString();

                    BranchDiffDao diffDao = new BranchDiffDao();
                    BranchDiffItemDao diffItemDao = new BranchDiffItemDao();

                    //存入本地差异单表
                    diffDao.addObj(diff);
                    diffItemDao.addList(diffList);

                    if (PingTask.IsConnected)
                    {
                        MasterBranchDiffDao masterDiffDao = new MasterBranchDiffDao();
                        masterDiffDao.addObj(diff);
                        MasterBranchDiffItemDao masterDiffItemDao = new MasterBranchDiffItemDao();
                        masterDiffItemDao.addList(diffList);
                    }
                    else
                    {
                        List<UploadInfo> uploadList = new List<UploadInfo>();
                        UploadInfo uploadInfo1 = new UploadInfo();
                        uploadInfo1.Id = diff.id;
                        uploadInfo1.Type = Constant.ZC_BRANCH_DIFF;
                        uploadInfo1.CreateTime = DateTime.Now;
                        uploadInfo1.UpdateTime = DateTime.Now;
                        uploadList.Add(uploadInfo1);
                        foreach (BranchDiffItem obj in diffList)
                        {
                            UploadInfo uploadInfo = new UploadInfo();
                            uploadInfo.Id = obj.id;
                            uploadInfo.CreateTime = DateTime.Now;
                            uploadInfo.UpdateTime = DateTime.Now;
                            uploadInfo.Type = Constant.ZC_BRANCH_DIFF_ITEM;
                            uploadList.Add(uploadInfo);
                        }
                        UploadDao uploadDao = new UploadDao();
                        uploadDao.AddUploadInfo(uploadList);
                    }
                }



                //存入本地数据库
                branchInDao.addObj(branchIn);
                branchInItemDao.addList(list);

                //将全部订单改为待提货  
                BranchZcOrderTransitService branchZcOrderTransitService = new BranchZcOrderTransitService();
                branchZcOrderTransitService.UpdateOrderStatus(Constant.ORDER_STATUS_RECEIPT);
                //将亭点收获标识更新为true
                DownloadIdentifyService downloadService = new DownloadIdentifyService();
                downloadService.UpdateHarvestFlag(Constant.HARVEST_YES);

                List<ZcOrderTransit> tranlist = branchZcOrderTransitService.FindAll();
                ///上传数据处理
                if (PingTask.IsConnected)
                {
                    //上传每日收货数据
                    MasterBranchInDao masterBranchInDao = new MasterBranchInDao();
                    masterBranchInDao.addObj(branchIn);
                    MasterBranchInItemDao masterBranchInItemDao = new MasterBranchInItemDao();
                    masterBranchInItemDao.addList(list);

                    ZcOrderTransitService zcOderTransitService = new ZcOrderTransitService();
                    zcOderTransitService.UpdateStatus(tranlist);
                }
                else
                {
                    List<UploadInfo> uploadList = new List<UploadInfo>();
                    UploadInfo uploadInfo1 = new UploadInfo();
                    uploadInfo1.Id = branchIn.id;
                    uploadInfo1.Type = Constant.ZC_BRANCH_IN;
                    uploadInfo1.CreateTime = DateTime.Now;
                    uploadInfo1.UpdateTime = DateTime.Now;
                    uploadList.Add(uploadInfo1);
                    foreach (BranchInItem obj in list)
                    {
                        UploadInfo uploadInfo = new UploadInfo();
                        uploadInfo.Id = obj.id;
                        uploadInfo.CreateTime = DateTime.Now;
                        uploadInfo.UpdateTime = DateTime.Now;
                        uploadInfo.Type = Constant.ZC_BRANCH_IN_ITEM;
                        uploadList.Add(uploadInfo);
                    }
                    foreach (ZcOrderTransit obj in tranlist)
                    {
                        UploadInfo uploadInfo = new UploadInfo();
                        uploadInfo.Id = obj.Id;
                        uploadInfo.CreateTime = DateTime.Now;
                        uploadInfo.UpdateTime = DateTime.Now;
                        uploadInfo.Type = Constant.ZC_ORDER_TRANSIT_UPDATE;
                        uploadList.Add(uploadInfo);
                    }

                    UploadDao uploadDao = new UploadDao();
                    uploadDao.AddUploadInfo(uploadList);
                }

                ///库存的更新
                BranchZcGoodsMasterService branchGoodsService = new BranchZcGoodsMasterService();
                BranchZcStoreHouseService branchStoreService = new BranchZcStoreHouseService();
                List<ZcStoreHouse> storeList = new List<ZcStoreHouse>();
                ZcStoreHouseService storeService = new ZcStoreHouseService();
                foreach (BranchInItem dg in list)
                {
                    String goodsFileId = dg.goodsFile_id;
                    float nums = float.Parse(dg.nums);
                    bool isGoodsWeight = branchGoodsService.IsWeightGoods(dg.goodsFile_id);
                    ZcStoreHouse storeHouse = branchStoreService.FindByGoodsFileIdAndBranchId(goodsFileId, LoginUserInfo.branchId);
                    if (storeHouse != null)
                    {
                        bool zeroFlag = string.IsNullOrEmpty(storeHouse.Store) || storeHouse.Store.Equals("0");
                        float oldNums = string.IsNullOrEmpty(storeHouse.Store) ? 0F : float.Parse(storeHouse.Store);
                        float oldMoney = string.IsNullOrEmpty(storeHouse.Store) ? 0F : float.Parse(storeHouse.StoreMoney);
                        storeHouse.UpdateTime = DateTime.Now;
                        storeHouse.Store = (oldNums - nums).ToString();
                        storeHouse.StoreMoney = zeroFlag ? "0" : (oldMoney * (oldNums - nums) / oldNums).ToString();
                        storeList.Add(storeHouse);
                    }
                    else
                    {
                        ///TODO
                        BranchZcBranchInfoService branchInfoService = new BranchZcBranchInfoService();
                        storeHouse = new ZcStoreHouse();
                        storeHouse.Id = Guid.NewGuid().ToString();
                        storeHouse.CreateTime = DateTime.Now;
                        storeHouse.UpdateTime = DateTime.Now;
                        storeHouse.Store = nums.ToString();
                        storeHouse.StoreMoney = dg.money;
                        storeHouse.BranchId = branchInfoService.FindIdByBranchTotalId(LoginUserInfo.branchId);
                        storeHouse.CreateUserId = LoginUserInfo.id;
                        storeHouse.GoodsFileId = dg.goodsFile_id;
                        branchStoreService.AddZcStoreHouse(storeHouse);
                        if (PingTask.IsConnected)
                        {
                            storeService.AddZcStoreHouseII(storeHouse);
                        }
                        else
                        {
                            UploadInfo info = new UploadInfo();
                            info.Id = storeHouse.Id;
                            info.CreateTime = DateTime.Now;
                            info.UpdateTime = DateTime.Now;
                            info.Type = Constant.ZC_STORE_HOSUE;
                            UploadDao dao = new UploadDao();
                            dao.AddUploadInfo(info);
                        }
                    }

                }
                branchStoreService.updateStoreHouse(storeList);

                if (PingTask.IsConnected)
                {
                    storeService.updateStoreHouse(storeList);
                }
                else
                {
                    List<UploadInfo> uploadList = new List<UploadInfo>();
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
                    UploadDao uploadDao = new UploadDao();
                    uploadDao.AddUploadInfo(uploadList);
                }
                loading.Close();
                MessageBox.Show("收货成功！");
                this.branchMain.Show();
                this.Close();
            }));
            objThread.Start();
        }

        

        /// <summary>
        /// 加载商品
        /// </summary>
        private void loadGoods(string id)
        {
            dispatchingId = id;
            string sql = "select m.*,n.actual_nums from (select sum(a.nums) as nums, sum(a.weight) as weight, sum(a.money) as money, a.goods_name, a.serialNumber, "
                + " a.goodsPrice, a.dispatchingWarehouseId, b.branch_total_id, c.id as goodsFile_id "
                + " from zc_dispatching_warehouse_items a left join zc_dispatching_warehouse b on a.dispatchingWarehouseId = b.id "
                + " left join zc_goods_master c on a.goodsFile_id = b.id where 1=1 and b.id = '" + id + "' group by a.goods_name, c.id)m "
                + " left join (select a.nums as actual_nums,a.goodsfile_id from zc_branch_in_item a left join zc_branch_in b on a.branchin_id = b.id) n "
                + " on m.goodsFile_id = n.goodsfile_id where 1=1 order by m.serialNumber asc";
            MySqlConnection conn = null;
            MysqlDBHelper dbHelper = new MysqlDBHelper();
            try
            {
                conn = dbHelper.GetConnection();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "zc_dispatching_warehouse_items");
                itemDataGridView.AutoGenerateColumns = false;
                itemDataGridView.DataSource = ds;
                itemDataGridView.DataMember = "zc_dispatching_warehouse_items";
                itemDataGridView.CurrentCell = null;//不默认选中
            }
            catch (Exception ex)
            {
                log.Error("加载订单中商品时发生错误", ex);
            }
            finally
            {
                dbHelper.CloseConnection(conn);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="num"></param>
        public void itemInput(string num)
        {
            this.itemDataGridView[4, row].Value = num;
            ////this.itemDataGridView[4, row].Value = num;
            ////this.itemDataGridView.CurrentCell = itemDataGridView[4, row]; //单元格设置可编辑状态-
            ////itemDataGridView.BeginEdit(true);
            ////itemsInput.sureflag = false;

            //if (column == 4 && row != -1)
            //{
            //    //itemDataGridView[column, row].Value = num;
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
        /// 添加商品
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="num">商品份数</param>
        public void AddGoods(ZcGoodsMaster obj, string num)
        {
            DataSet ds = (DataSet)itemDataGridView.DataSource;

            bool flag = false;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][0].ToString().Equals(obj.SerialNumber))
                {
                    string nums = itemDataGridView[4, i].Value.ToString();
                    itemDataGridView[4, i].Value = Convert.ToInt32(nums) + Convert.ToInt32(num);
                    return;
                }
                else
                {

                }
                if (itemDataGridView[0, i].Value.ToString().Equals(obj.SerialNumber))//
                {//
                    row = i;//
                    column = 5;//
                    //itemsInput.sureflag = true;//
                    flag = true;//
                    this.itemDataGridView.CurrentCell = itemDataGridView[4, row]; //单元格设置可编辑状态-
                    itemDataGridView.BeginEdit(true);
                }//
            }
            if (!flag)
            {
                MessageBox.Show("今天收货订单中没有此商品，请与总部联系");
                return;
            }
            //自动计算
            Calculate();
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    if (ds.Tables[0].Rows[i][0].ToString().ToString().Equals(obj.SerialNumber))
            //    {
            //        string nums = itemDataGridView[5, i].Value.ToString();
            //        itemDataGridView[5, i].Value = Convert.ToInt32(nums) + Convert.ToInt32(num);
            //        return;
            //    }
            //}
            //ds.Tables[0].Rows.Add(obj.SerialNumber, obj.GoodsName, obj.GoodsUnit, obj.GoodsSpecifications, num, obj.GoodsPrice, (Convert.ToInt32(num) * obj.GoodsPrice).ToString("0.00"), obj.Remark, Guid.NewGuid().ToString(), obj.Id);
            //Calculate();
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

        private void itemDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            row = itemDataGridView.CurrentCell.RowIndex;
            column = itemDataGridView.CurrentCell.ColumnIndex;
            //MessageBox.Show(column + "/" + row);
            //choose = true;
        }

        private void itemCountPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// 合计金额，并计算份数差异
        /// </summary>
        private void Calculate()
        {
            float totalSum = 0;
            //float totalAmount = 0;
            try
            {
                for (int i = 0; i < itemDataGridView.RowCount; i++)
                {
                    totalSum += float.Parse((itemDataGridView[4, i].Value == null || itemDataGridView[4, i].Value.ToString().Trim() == "") ? "0" : itemDataGridView[4, i].Value.ToString());
                    //totalAmount += float.Parse((itemDataGridView[8, i].Value == null || itemDataGridView[8, i].Value.ToString().Trim() == "") ? "0" : itemDataGridView[8, i].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                log.Error("类型转换异常", ex);
            }
            this.totalSum.Text = MoneyFormat.RountFormat(totalSum);
            //this.totalAmount.Text = MoneyFormat.RountFormat(totalAmount);
        }

        private void DeliveryGoods_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 && deliveryButton.Enabled == true)
            {
                deliveryButton_Click(this, EventArgs.Empty);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                returnButton_Click(this, EventArgs.Empty);
            }
            if (e.Control && e.KeyCode == Keys.T)
            {
                if (itemsInput.Focused)
                {
                    this.Focus();
                }
                else
                {
                    itemsInput.Focus();

                }
            }
            if (numberTextBox.Focused && e.KeyCode == Keys.Enter)
            {
                equalButton_Click(this, EventArgs.Empty);
            }
        }

        private void itemDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //行号
            using (SolidBrush b = new SolidBrush(this.itemDataGridView.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 5, e.RowBounds.Location.Y + 4);
            }
        }

        /// <summary>
        /// 检测输入是否为数字的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (itemDataGridView.CurrentCell.ColumnIndex == 5)
            {
                e.Control.KeyPress += new KeyPressEventHandler(checkInput);
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
            if (e.RowIndex != -1 && e.ColumnIndex == 5)
            {
                try
                {
                    float num = (itemDataGridView.Rows[e.RowIndex].Cells[4].Value == null || string.IsNullOrEmpty(itemDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString())) ? 0 : float.Parse(itemDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString());
                    //float price =float.Parse(itemDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString());
                    float difference = float.Parse(itemDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString());
                    if (difference != 0)
                    {
                        itemDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(201, 67, 65);// Color.Red;
                    }
                    else
                    {
                        itemDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(51, 153, 255);// Color.Blue;//51,153,255
                    }
                    Calculate();
                }
                catch (Exception ex)
                {
                    log.Error("自动计算金额错误", ex);
                }
            }
        }

        private void itemDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //choose = false;
            //itemsInput.sureflag = true;
        }

        /// <summary>
        /// 显示数据格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (itemDataGridView.Columns[e.ColumnIndex].Name == "weight")
            {
                if (e.Value == null || e.Value.ToString() == "")
                {
                    return;
                }
                float weight = float.Parse(e.Value.ToString());
                e.Value = weight.ToString("0.0000");
            }
            if (itemDataGridView.Columns[e.ColumnIndex].Name == "goodsprice")
            {
                if (e.Value == null || e.Value.ToString() == "")
                {
                    e.Value = "0.00";
                }
                else 
                {
                    float price = float.Parse(e.Value.ToString());
                    e.Value = MoneyFormat.RountFormat(price);
                }
            }
            if (itemDataGridView.Columns[e.ColumnIndex].Name == "money")
            {
                if (e.Value == null || e.Value.ToString() == "")
                {
                    e.Value = "0.00";
                }
                else
                {
                    float money = float.Parse(e.Value.ToString());
                    e.Value = MoneyFormat.RountFormat(money);
                }
            }
        }

        private void oneButton_Click(object sender, EventArgs e)
        {
            numberTextBox.Text += "1";
        }

        private void twoButton_Click(object sender, EventArgs e)
        {
            numberTextBox.Text += "2";
        }

        private void threeButton_Click(object sender, EventArgs e)
        {
            numberTextBox.Text += "3";
        }

        private void fourButton_Click(object sender, EventArgs e)
        {
            numberTextBox.Text += "4";
        }

        private void fiveButton_Click(object sender, EventArgs e)
        {
            numberTextBox.Text += "5";
        }

        private void sixButton_Click(object sender, EventArgs e)
        {
            numberTextBox.Text += "6";
        }

        private void sevenButton_Click(object sender, EventArgs e)
        {
            numberTextBox.Text += "7";
        }

        private void eightButton_Click(object sender, EventArgs e)
        {
            numberTextBox.Text += "8";
        }

        private void nineButton_Click(object sender, EventArgs e)
        {
            numberTextBox.Text += "9";
        }

        private void zeroButton_Click(object sender, EventArgs e)
        {
            numberTextBox.Text += "0";
        }

        private void dashButton_Click(object sender, EventArgs e)
        {
            numberTextBox.Text += "-";
        }

        private void decimalButton_Click(object sender, EventArgs e)
        {
            numberTextBox.Text += ".";
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            if (numberTextBox.Text.Length > 0)
            {
                numberTextBox.Text = numberTextBox.Text.Remove(numberTextBox.Text.Length - 1, 1);
            }
        }

        private void clearBotton_Click(object sender, EventArgs e)
        {
            numberTextBox.Text = "";
        }

        private void equalButton_Click(object sender, EventArgs e)
        {
            if(!harvestFlag)
            {
                ///今天已经亭长收货,无法继续
                MessageBox.Show("亭长今天已经收货，无法进行扫码!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numberTextBox.Text = "";
                return;
            }
            string bar = numberTextBox.Text;
            numberTextBox.Text = "";
            string serial = "";
            string weight = "";
            if (string.IsNullOrEmpty(bar) || (!bar.StartsWith("28") && !bar.StartsWith("69")) || (bar.Length != 18 && bar.Length != 13))
            {
                MessageBox.Show("扫码的条码不正确,请重新扫码!", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
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
            ZcGoodsMaster obj = branchGoodsService.FindBySerialNumber(serial);
            if (obj == null)
            {
                MessageBox.Show("没有此货号对应的商品信息，请检查后重新操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool isWeightGoods = branchGoodsService.IsWeightGoods(obj.Id);
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
            bool flag = false;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (itemDataGridView[0, i].Value.ToString().Equals(serial))
                {
                    row = i;//
                    column = 5;//
                    flag = true;
                    //itemDataGridView[5, i].Value = good_num;
                    this.itemDataGridView.CurrentCell = itemDataGridView[4, row]; //单元格设置可编辑状态-
                    itemDataGridView.BeginEdit(true);
                }
            }

            if (!flag)
            {
                MessageBox.Show("客户订单中没有此商品，请与客户确认", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
        }

        private void itemDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            numberTextBox.Focus();
        }

        private void DeliveryGoods_FormClosed(object sender, FormClosedEventArgs e)
        {
            delivery();
        }

        /// <summary>
        /// 限制条码框输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        private void itemDataGridView_Paint(object sender, PaintEventArgs e)
        {
            //itemDataGridView.CurrentCell = null;
            itemDataGridView.ClearSelection();
        }


        public void loadById(string id)
        {
            loadGoods(id);
            initDifference(id);
        }
    }
}