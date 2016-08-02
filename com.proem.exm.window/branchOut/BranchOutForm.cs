using Branch.com.proem.exm.dao.branch;
using Branch.com.proem.exm.dao.master;
using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.service.branch;
using Branch.com.proem.exm.service.master;
using Branch.com.proem.exm.util;
using Branch.com.proem.exm.window.main;
using Branch.com.proem.exm.window.retreat;
using Branch.com.proem.exm.window.util;
using log4net;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Branch.com.proem.exm.window.branchOut
{
    public partial class BranchOutForm : Form
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(BranchOutForm));

        /// <summary>
        /// 主表id
        /// </summary>
        private string branchOutId;

        /// <summary>
        /// 分店id
        /// </summary>
        private string branchId;

        /// <summary>
        /// 亭点出库单状态
        /// </summary>
        private string status;

        /// <summary>
        /// 主界面
        /// </summary>
        private BranchMain branchMain;

        public BranchOutForm()
        {
            InitializeComponent();
        }

        public BranchOutForm(BranchMain branchMain)
        {
            InitializeComponent();
            this.branchMain = branchMain;
        }

        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //行号
            using (SolidBrush b = new SolidBrush(this.dataGridView.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 5, e.RowBounds.Location.Y + 4);
            }
        }

        private void BranchOutForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape){
                leaveButton_Click(this, EventArgs.Empty);
            }
            if(e.KeyCode == Keys.A){
                //添加商品选择
                addGoodsButton_Click(this, EventArgs.Empty);
            }
            if(e.KeyCode == Keys.Delete){
                //删除
                deleteButton_Click(this, EventArgs.Empty);
            }
            if(e.KeyCode == Keys.F1){
                newButton_Click(this, EventArgs.Empty);
            }
            if(e.KeyCode == Keys.F2){
                openbutton_Click(this, EventArgs.Empty);
            }
            if(e.KeyCode == Keys.F3){
                savebutton_Click(this, EventArgs.Empty);
            }
            if(e.KeyCode == Keys.F4){
                checkbutton_Click(this, EventArgs.Empty);
            }
            if (e.KeyCode == Keys.F5) {
                button1_Click(this, EventArgs.Empty);
            }
        }

        private void BranchOutForm_Load(object sender, EventArgs e)
        {
            Times times = new Times();
            times.TopLevel = false;
            this.timePanel.Controls.Add(times);
            times.Show();

            inNameLabel.Text = LoginUserInfo.branchName;
            salesmanNameLabel.Text = LoginUserInfo.name;
            
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addGoodsButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(status) && "2".Equals(status))
            {
                MessageBox.Show("改亭点出库单已经审核，无法进行操作", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return;
            }
            GoodsChoose goodsChoose = new GoodsChoose(this);
            goodsChoose.Show();
        }

        public void AddGoods(List<ZcGoodsMaster> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                bool isExist = false;
                ZcGoodsMaster zcGoodsMaster = list[i];
                for (int j = 0; j < dataGridView.RowCount; j++)
                {
                    if (dataGridView[0, j].Value.ToString().Equals(zcGoodsMaster.SerialNumber))
                    {
                        MessageBox.Show("货号为" + zcGoodsMaster.SerialNumber + "的" + zcGoodsMaster.GoodsName + "已经在第" + (j + 1) + "行，请不要重复添加!");
                        return;
                    }

                }
                if (!isExist)
                {
                    if (string.IsNullOrEmpty(branchOutId))
                    {
                        dataGridView.Rows.Add(new Object[] { zcGoodsMaster.SerialNumber, zcGoodsMaster.GoodsName, zcGoodsMaster.GoodsSpecifications, 1.0, zcGoodsMaster.GoodsPrice, zcGoodsMaster.GoodsPrice * 1.0, zcGoodsMaster.Id, Guid.NewGuid().ToString().Replace("-", "") });
                    }
                    else
                    {
                        BranchOutItemDao dao = new BranchOutItemDao();
                        BranchOutItem item = new BranchOutItem();
                        item.id = Guid.NewGuid().ToString().Replace("-", "");
                        item.goodsFile_id = zcGoodsMaster.Id;
                        item.nums = "1";
                        item.price = zcGoodsMaster.GoodsPrice.ToString();
                        item.money = item.price;
                        item.createTime = DateTime.Now;
                        item.updateTime = DateTime.Now;
                        item.branchOut_id = branchOutId;
                        dao.addObj(item);

                        //重新加载数据

                    }
                }

            }
            calcauteNums();
        }

        private void leaveButton_Click(object sender, EventArgs e)
        {
            branchMain.Show();
            this.Close();
        }

        /// <summary>
        /// 新增按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newButton_Click(object sender, EventArgs e)
        {
            clearData();
            branchOutId = string.Empty;
            branchId = string.Empty;
            status = string.Empty;
            fromTextbox.Text = LoginUserInfo.branchName;
            oddTextbox.Text = "TDCKD" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            dataGridView.DataSource = null;
            dataGridView.Rows.Clear();
        }

        /// <summary>
        /// 清除数据
        /// </summary>
        private void clearData()
        {
            branchOutId = string.Empty;
            branchId = string.Empty;
            status = string.Empty;
            branchDiffId = string.Empty;
            fromTextbox.Text = "";
            oddTextbox.Text = "";
            remarkTextbox.Text = "";
            branchTextbox.Text = "";
            numLabel.Text = "0.00";
            dataGridView.DataSource = null;
            dataGridView.Rows.Clear();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(status) && "2".Equals(status))
            {
                MessageBox.Show("改亭点出库单已经审核，无法进行商品删除!", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return;
            }
            string name =  dataGridView.CurrentRow.Cells[1].Value.ToString();
            string id = dataGridView.CurrentRow.Cells[7].Value.ToString();
            DialogResult dr = MessageBox.Show("确定要删除" + name + "吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                BranchOutItemDao itemDao = new BranchOutItemDao();
                itemDao.deleteById(id);
                loadData();
            }
        }

        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openbutton_Click(object sender, EventArgs e)
        {
            BranchOutChoose choose = new BranchOutChoose(this);
            choose.ShowDialog();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void savebutton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(status) && "2".Equals(status))
            {
                MessageBox.Show("改亭点出库单已经审核，无法进行保存!", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return;
            }
            saveOrUpdate();
            MessageBox.Show("保存成功!");
        }

        public void saveOrUpdate() 
        {
            if (string.IsNullOrEmpty(branchOutId))
            {
                //新增
                BranchOutDao dao = new BranchOutDao();
                BranchOutItemDao itemDao = new BranchOutItemDao();
                BranchOut obj = new BranchOut();
                obj.id = Guid.NewGuid().ToString().Replace("-", "");
                obj.createTime = DateTime.Now;
                obj.updateTime = DateTime.Now;
                obj.OutOdd = oddTextbox.Text;
                if(! string.IsNullOrEmpty(branchDiffId)){
                    obj.branchDiff_id = branchDiffId;
                }
                obj.nums = numLabel.Text;
                obj.branch_id = LoginUserInfo.branchId;
                obj.branch_to_id = this.branchId;
                obj.user_id = LoginUserInfo.id;
                obj.status = "0";
                obj.remark = remarkTextbox.Text;

                if (dataGridView.DataSource == null && dataGridView.RowCount == 0)
                {
                    return;
                }
                float money = 0;
                List<BranchOutItem> list = new List<BranchOutItem>();
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    BranchOutItem item = new BranchOutItem();
                    item.id = dataGridView[7, i].Value.ToString();
                    item.createTime = DateTime.Now;
                    item.updateTime = DateTime.Now;
                    item.branchOut_id = obj.id;
                    item.goodsFile_id = dataGridView[0, i].Value.ToString();
                    item.price = dataGridView[4, i].Value.ToString();
                    item.nums = dataGridView[3, i].Value == null ? "0" : dataGridView[3, i].Value.ToString();
                    money += float.Parse(item.price) * float.Parse(item.nums);
                    item.money = (float.Parse(item.price) * float.Parse(item.nums)).ToString();
                    list.Add(item);
                }
                itemDao.addList(list);
                obj.money = money.ToString();
                dao.addObj(obj);
                branchId = obj.id;
                status = "0";
            }
            else
            {
                //更新
                BranchOutDao dao = new BranchOutDao();
                BranchOutItemDao itemDao = new BranchOutItemDao();
                BranchOut obj = new BranchOut();
                obj.id = branchOutId;
                obj.updateTime = DateTime.Now;
                obj.nums = numLabel.Text;
                obj.branch_to_id = this.branchId;
                obj.remark = remarkTextbox.Text;
                if (dataGridView.RowCount == 0)
                {
                    return;
                }
                float money = 0;
                List<BranchOutItem> list = new List<BranchOutItem>();
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    BranchOutItem item = new BranchOutItem();
                    item.id = dataGridView[7, i].Value.ToString();
                    item.updateTime = DateTime.Now;
                    item.price = dataGridView[4, i].Value == null ? "0" : dataGridView[4, i].Value.ToString();
                    item.nums = dataGridView[3, i].Value == null ? "0" : dataGridView[3, i].Value.ToString();
                    money += float.Parse(item.price) * float.Parse(item.nums);
                    item.money = (float.Parse(item.price) * float.Parse(item.nums)).ToString();
                    list.Add(item);
                }
                itemDao.updateList(list);
                obj.money = money.ToString();
                dao.updateObj(obj);
            }
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkbutton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(status) && "2".Equals(status))
            {
                MessageBox.Show("改亭点出库单已经审核，无法重复审核!", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return;
            }
            saveOrUpdate();
            BranchOutDao dao = new BranchOutDao();
            dao.updateStatus(branchId, "2");
            ///上传数据
            BranchOut obj = dao.FindById(branchId);
            BranchOutItemDao itemDao = new BranchOutItemDao();
            List<BranchOutItem> list = itemDao.FindByOutId(branchId);
            if (PingTask.IsConnected)
            {
                MasterBranchOutDao masterDao = new MasterBranchOutDao();
                MasterBranchOutItemDao masterItemDao = new MasterBranchOutItemDao();
                masterDao.addObj(obj);
                masterItemDao.addList(list);
            }
            else {
                List<UploadInfo> uplist = new List<UploadInfo>();
                UploadInfo obj1 = new UploadInfo();
                obj1.Id = obj.id;
                obj1.CreateTime = DateTime.Now;
                obj1.UpdateTime = DateTime.Now;
                obj1.Type = Constant.ZC_BRANCH_OUT;
                uplist.Add(obj1);
                for (int i = 0; i < list.Count; i++ )
                {
                    UploadInfo obj2 = new UploadInfo();
                    obj2.Id = list[i].id;
                    obj2.CreateTime = DateTime.Now;
                    obj2.UpdateTime = DateTime.Now;
                    obj2.Type = Constant.ZC_BRANCH_OUT_ITEM;
                    uplist.Add(obj2);
                }
                UploadDao uploadDao = new UploadDao();
                uploadDao.AddUploadInfo(uplist);
            }


            ///库存的更新
            if(!string.IsNullOrEmpty(branchDiffId)){
                BranchZcGoodsMasterService branchGoodsService = new BranchZcGoodsMasterService();
                BranchZcStoreHouseService branchStoreService = new BranchZcStoreHouseService();
                List<ZcStoreHouse> storeList = new List<ZcStoreHouse>();
                ZcStoreHouseService storeService = new ZcStoreHouseService();
                foreach (BranchOutItem dg in list)
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
                            UploadDao uploaddao = new UploadDao();
                            uploaddao.AddUploadInfo(info);
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
                        ZcStoreHouse zcobj = storeList[i];
                        UploadInfo info = new UploadInfo();
                        info.Id = zcobj.Id;
                        info.CreateTime = DateTime.Now;
                        info.UpdateTime = DateTime.Now;
                        info.Type = Constant.ZC_STOREHOUSE_UPDATE;
                        uploadList.Add(info);
                    }
                    UploadDao uploadDao = new UploadDao();
                    uploadDao.AddUploadInfo(uploadList);
                }
            }

            MessageBox.Show("审核成功!");
            status = "2";
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].Name == "money")
            {
                if (e.Value == null || e.Value.ToString() == "")
                {
                    return;
                }
                float money = float.Parse(e.Value.ToString());
                e.Value = MoneyFormat.RountFormat(money);
            }
        }

        /// <summary>
        /// 点击选择分店按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chooseBranchButton_Click(object sender, EventArgs e)
        {
            RGSupplierChoose choose = new RGSupplierChoose(this);
            choose.ShowDialog();
        }

        /// <summary>
        /// 选择完的分店
        /// </summary>
        /// <param name="zcBranchTotal"></param>
        public void AddBranchTotalInfo(ZcBranchTotal zcBranchTotal)
        {
            branchTextbox.Text = zcBranchTotal.BranchName;
            branchId = zcBranchTotal.Id;
        }

        /// <summary>
        /// 计算合计数量
        /// </summary>
        private void calcauteNums()
        {
            if (dataGridView.DataSource == null && dataGridView.RowCount == 0)
            {
                return;
            }
            float nums = 0;
            for (int i = 0; i < dataGridView.RowCount; i++ )
            {
                float num = (dataGridView[3, i].Value == null || string.IsNullOrEmpty(dataGridView[3, i].Value.ToString())) ? 0 : float.Parse(dataGridView[3, i].Value.ToString());
                nums += num;
            }
            numLabel.Text = MoneyFormat.RountFormat(nums);
        }

        public void openBranchOut(string id)
        {
            clearData();
            BranchOutDao dao = new BranchOutDao();
            BranchOut obj = dao.FindById(id);
            this.status = obj.status;
            oddTextbox.Text = obj.OutOdd;
            branchOutId = obj.id;
            branchId = obj.branch_to_id;
            BranchZcBranchTotalDao branchDao = new BranchZcBranchTotalDao();
            ZcBranchTotal branchtotal = branchDao.FindById(obj.branch_to_id);
            branchTextbox.Text = branchtotal.BranchName;
            remarkTextbox.Text = obj.remark;
            loadData();
        }

        private void loadData()
        {
            string sql = "select a.id, a.nums, a.price, a.money , a.goodsFile_id, b.serialNumber, b.GOODS_SPECIFICATIONS, b.GOODS_NAME  from zc_branch_out_item a left join zc_goods_master b on a.goodsFile_id = b.id where 1=1 and a.branchOut_id = '"+branchOutId+"'";
            MySqlConnection conn = null;
            MySqlCommand cmd = new MySqlCommand();
            MysqlDBHelper dbHelper = new MysqlDBHelper();
            try
            {
                conn = dbHelper.GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "zc_branch_out_item");
                dataGridView.AutoGenerateColumns = false;
                dataGridView.DataSource = ds;
                dataGridView.DataMember = "zc_branch_out_item";
            }
            catch (Exception ex)
            {
                log.Error("加载出库单明细", ex);
            }
            finally
            {
                dbHelper.CloseConnection(conn);
                cmd.Dispose();
            }
            calcauteNums();
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            calcauteNums();
            if (dataGridView.DataSource == null && dataGridView.RowCount == 0)
            {
                return;
            }
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                float num = (dataGridView[3, i].Value == null || string.IsNullOrEmpty(dataGridView[3, i].Value.ToString())) ? 0 : float.Parse(dataGridView[3, i].Value.ToString());
                float price = (dataGridView[4, i].Value == null || string.IsNullOrEmpty(dataGridView[4, i].Value.ToString())) ? 0 : float.Parse(dataGridView[4, i].Value.ToString());
                dataGridView[5, i].Value = MoneyFormat.RountFormat(num * price);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(status) && "2".Equals(status))
            {
                MessageBox.Show("改亭点出库单已经审核，无法进行删除!", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return;
            }
            DialogResult dr = MessageBox.Show("确定要删除亭点出库单吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                BranchOutItemDao itemDao = new BranchOutItemDao();
                itemDao.deleteByOutId(branchOutId);
                BranchOutDao outDao = new BranchOutDao();
                outDao.deleteById(branchOutId);
                clearData();
            }
        }

        private void chooseBranchDiffButton_Click(object sender, EventArgs e)
        {
            BranchDiifForm choose = new BranchDiifForm();
            choose.ShowDialog();
        }

        /// <summary>
        /// 差异单号
        /// </summary>
        private string branchDiffId;

        public void openBranchDiff(string id)
        {
            branchDiffId = id;
            BranchDiffItemDao dao = new BranchDiffItemDao();
            BranchZcGoodsMasterDao zcGoodsMasterDao = new BranchZcGoodsMasterDao();
            List<BranchDiffItem> list = dao.FindByDiffId(id);
            for (int i = 0; i < list.Count; i++ )
            {
                BranchDiffItem diffitem = list[i];
                ZcGoodsMaster zcGoodsMaster = zcGoodsMasterDao.FindById(diffitem.goodsFile_id);
                if (zcGoodsMaster!= null)
                {
                    dataGridView.Rows.Add(new Object[] { zcGoodsMaster.SerialNumber, zcGoodsMaster.GoodsName, zcGoodsMaster.GoodsSpecifications, 1.0, diffitem.price, diffitem.price, zcGoodsMaster.Id, Guid.NewGuid().ToString().Replace("-", "") });
                }
            }
        }
    }
}
