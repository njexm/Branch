using Branch.com.proem.exm.dao.branch;
using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.service.branch;
using Branch.com.proem.exm.service.master;
using Branch.com.proem.exm.util;
using Branch.com.proem.exm.window.main;
using Branch.com.proem.exm.window.retreat;
using Branch.com.proem.exm.window.util;
using log4net;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Branch.com.proem.exm.window.require
{
    public partial class BranchZcRequire : Form
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(BranchZcRequire));

        public BranchZcRequire()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  添加商品
        /// </summary>
        /// <param name="obj"></param>
        public void AddGoods(ZcGoodsMaster obj)
        {
            DataSet ds = (DataSet)dataGridView1.DataSource;
            MessageBox.Show("货号为" + obj.SerialNumber + "的" + obj.GoodsName);
            bool isExist = false;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1[0, i].Value.ToString().Equals(obj.SerialNumber))
                {
                    MessageBox.Show("货号为" + obj.SerialNumber + "的" + obj.GoodsName + "已经在第" + (i + 1) + "行，请不要重复添加!");
                    return;
                }
            }
            if (!isExist)
            {
                dataGridView1.Rows.Add(new Object[] { obj.Id, obj.SerialNumber, obj.GoodsName, obj.GoodsUnit, obj.GoodsSpecifications, 0.00, 1.0, obj.GoodsPrice, obj.WholeSalePrice, obj.Remark, Guid.NewGuid().ToString(), obj.Id });
            }

            //if (ds != null)
            //{
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        if (ds.Tables[0].Rows[i][0].ToString().Equals(obj.SerialNumber))
            //        {
            //            MessageBox.Show("货号为" + obj.SerialNumber + "的" + obj.GoodsName + "已经在第" + (i + 1) + "行，请不要重复添加!");
            //            return;
            //        }
            //    }
            //    ds.Tables[0].Rows.Add(obj.SerialNumber, obj.GoodsName, obj.GoodsUnit, obj.GoodsSpecifications, 0, obj.GoodsPrice, 0.00, obj.Remark, Guid.NewGuid().ToString(), obj.Id);
            //}
            //else
            //{ 

            //}

        }
        /// <summary>
        /// 分店id
        /// </summary>
        private string branchId = "";
        /// <summary>
        /// 添加分店信息
        /// </summary>
        /// <param name="zcBranchTotal"></param>
        public void addBranchToTalInfo(ZcBranchTotal zcBranchTotal)
        {
            textBox4.Text = zcBranchTotal.BranchCode;
            textBox8.Text = zcBranchTotal.BranchName;
            textBox6.Text = zcBranchTotal.Id;
            branchId = zcBranchTotal.Id;
        }

        public void AddGoods(List<ZcGoodsMaster> list)
        {



            for (int i = 0; i < list.Count; i++)
            {
                bool isExist = false;
                ZcGoodsMaster zcGoodsMaster = list[i];
                for (int j = 0; j < dataGridView1.RowCount; j++)
                {
                    if (dataGridView1[0, j].Value.ToString().Equals(zcGoodsMaster.SerialNumber))
                    {
                        MessageBox.Show("货号为" + zcGoodsMaster.SerialNumber + "的" + zcGoodsMaster.GoodsName + "已经在第" + (j + 1) + "行，请不要重复添加!");
                        return;
                    }

                }
                if (!isExist)
                {
                    if (isNew)
                    {
                        dataGridView1.Rows.Add(new Object[] { zcGoodsMaster.SerialNumber, zcGoodsMaster.GoodsName, zcGoodsMaster.GoodsUnit, zcGoodsMaster.GoodsSpecifications, 1.0, zcGoodsMaster.GoodsPrice, zcGoodsMaster.GoodsPrice * 1.0, zcGoodsMaster.Remark, zcGoodsMaster.Id });
                    }
                    else
                    {
                        ZcRequireItemService zcRequireItemService = new ZcRequireItemService();

                        ZcRequireItem zcRequireItem = new ZcRequireItem();
                        zcRequireItem.id = Guid.NewGuid().ToString();
                        zcRequireItem.createTime = DateTime.Now;
                        zcRequireItem.updateTime = DateTime.Now;
                        zcRequireItem.nums = "1.0";
                        double money = zcGoodsMaster.GoodsPrice * 1.0;
                        zcRequireItem.money = money.ToString();
                        zcRequireItem.remark = zcGoodsMaster.Remark;
                        zcRequireItem.requireId = requireIdBox.Text.ToString();
                        zcRequireItem.goodsFileId = zcGoodsMaster.Id;

                        zcRequireItemService.addOneZcRequireItem(zcRequireItem);
                        dataGridView1.DataSource = GetGoodData(requireIdBox.Text);
                        dataGridView1.DataMember = "zc_goods_master";
                    }


                    //dataGridView1.Rows.Add(new Object[] { zcGoodsMaster.SerialNumber, zcGoodsMaster.GoodsName, zcGoodsMaster.GoodsUnit, zcGoodsMaster.GoodsSpecifications,1.0, zcGoodsMaster.GoodsPrice, zcGoodsMaster.GoodsPrice*1.0, zcGoodsMaster.Remark, zcGoodsMaster.Id });
                    moneyChange();
                }

            }


        }
        /// <summary>
        /// 添加商品
        /// </summary>a
        /// <param name="obj"></param>
        /// <param name="num">商品份数</param>                                                                                                                                                                                                                                                                                                                                              
        public void AddGoods(ZcGoodsMaster obj, string num)
        {
            DataSet ds = (DataSet)dataGridView1.DataSource;



            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    if (ds.Tables[0].Rows[i][0].ToString().ToString().Equals(obj.SerialNumber))
            //    {
            //        string nums = dataGridView1[5, i].Value.ToString();
            //        dataGridView1[5, i].Value = Convert.ToInt32(nums) + Convert.ToInt32(num);
            //        return;
            //    }
            //}
            //ds.Tables[0].Rows.Add(obj.SerialNumber, obj.GoodsName, obj.GoodsUnit, obj.GoodsSpecifications, num, obj.GoodsPrice, (Convert.ToInt32(num) * obj.GoodsPrice).ToString("0.00"), obj.Remark, Guid.NewGuid().ToString(), obj.Id);
            ///Calculate();
        }



        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void BranchZcRequire_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {


        }

       

        private void button9_Click(object sender, EventArgs e)
        {
            BranchMain branchMain = new BranchMain();
            branchMain.Show();
            this.Close();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 保存商品信息表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_3(object sender, EventArgs e)
        {


        }

        private void button7_Click(object sender, EventArgs e)
        {
            RGBuyerChoose rGBuyerChoose = new RGBuyerChoose();
            rGBuyerChoose.ShowDialog();//选择采购员
        }

        private void button11_Click(object sender, EventArgs e)
        {
            RGSupplierChoose rGSupplierChoose = new RGSupplierChoose(this);
            rGSupplierChoose.ShowDialog();//选择供应商
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RGROperatorChoose rGROperatorChoose = new RGROperatorChoose();
            rGROperatorChoose.Show();

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {   //显示当前分店和登录的用户
            label12.Text = LoginUserInfo.branchName;
            label1.Text = LoginUserInfo.name;
            textBox3.Text = LoginUserInfo.branchName;
            textBox2.Text = LoginUserInfo.name;
            textBox9.Text = LoginUserInfo.name;
            textBox5.Text = LoginUserInfo.branchId;
            //label10.Text = "新建";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timesPanel_Paint_2(object sender, PaintEventArgs e)
        {

            //显示时间
            Times times = new Times();
            times.TopLevel = false;
            this.timesPanel.Controls.Add(times);
            times.Show();
        }


        private void button4_Click_1(object sender, EventArgs e)
        {
            GoodsChoose goodsChoose = new GoodsChoose(this);
            goodsChoose.ShowDialog();

        }
        /// <summary>
        /// 编辑
        /// </summary>
        public void editing() {
            ZcRequire zcRequire = new ZcRequire();
            zcRequire.branchId = textBox5.Text.ToString();
            if (textBox6.Text.ToString().Equals(""))
            {
                MessageBox.Show("请填写发货分店");
                return;
            }
            zcRequire.calloutBranchId = textBox6.Text.ToString();
            
            zcRequire.checkMan = textBox11.Text.ToString();
            zcRequire.reason = textBox10.Text.ToString();
            
            ///判断当前要货单编号是否存在 如果存在就取出当前要货单编号 如果不存在就新的id
            if (isNew == true)
            {
                zcRequire.id = Guid.NewGuid().ToString();
            }
            else
            {
                zcRequire.id = requireIdBox.Text.ToString();
            }

            zcRequire.money = textBox14.Text;
            zcRequire.nums = textBox13.Text;
            zcRequire.remark = textBox7.Text;

            ///要货单状态：待提交
            zcRequire.status =Int32.Parse( Constant.CHECK_STATUS_UNDO);
            //string temp = "";
            //if (zcRequire.status == Constant.CHECK_STATUS_UNDO)
            //{
            //    temp = "待提交";
            //}
            //label10.Text = temp;
            zcRequire.updateTime = DateTime.Now;
            ///是否需要保存以后创建时间不再变化还是每次都new
            //if(timesLabel.Text.ToString()!=null){
            // string str=timesLabel.Text.ToString();
            //  DateTime time = Convert.ToDateTime(str);
            // zcRequire.createTime = time;
            //}

            ZcRequireService zcRequireService = new ZcRequireService();
            ZcRequireItemService zcRequireItemService = new ZcRequireItemService();

            //zcRequire.createTime = DateTime.Now;
          // ZcUserInfo zcUserInfo =  zcRequireService.FindByUserId();

            ZcUserInfo zcUserInfo = GetUserInfoData(LoginUserInfo.id);
            zcRequire.userId = zcUserInfo.Id;



            bool addSuccess = false;

            List<ZcRequireItem> list = new List<ZcRequireItem>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                ZcRequireItem zcRequireItem = new ZcRequireItem();
                zcRequireItem.nums = dataGridView1.Rows[i].Cells[4].Value.ToString();
                float goodPrice = float.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                float money = goodPrice * float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                zcRequireItem.money = money.ToString();
                zcRequireItem.createTime = DateTime.Now;
                zcRequireItem.requireId = zcRequire.id;
                zcRequireItem.updateTime = DateTime.Now;
                zcRequireItem.remark = dataGridView1.Rows[i].Cells[7].Value.ToString();
                zcRequireItem.goodsFileId = dataGridView1.Rows[i].Cells[0].Value.ToString();

                if (isNew == true)
                {
                    zcRequireItem.id = Guid.NewGuid().ToString();
                }
                else {
                    zcRequireItem.id = dataGridView1.Rows[i].Cells[8].Value.ToString(); 
                }
                
                list.Add(zcRequireItem);

            }
           

            ///TODO 
            ///上传的数据service Dao

            if (list.Count == 0)
            {
               DialogResult result =  MessageBox.Show("您没有选择商品，是否继续", "继续", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            if (isNew == true)
            {


                zcRequire.yhdNumber = "YHD" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                zcRequire.createTime = DateTime.Now;
                textBox1.Text = zcRequire.yhdNumber;
                zcRequireService.addZcRequireInfo(zcRequire);

                //ZcRequire zcRequireBefore = new ZcRequire();
                //zcRequireBefore.id = "";
                //zcRequireItemService.DeleteRequireItemById(zcRequireBefore);
                addSuccess = zcRequireItemService.addZcRequireItem(list);
                
                ///TODO 上传保存数据 

                //if (PingTask.IsConnected)
                //{
                //    ZcRequireService zcRequireService = new ZcRequireService();
                //    zcRequireService.AddZcRequireIndo(zcRequire);
                //    ZcRequireItemService zcRequireItemService = new ZcRequireItemService();
                //    zcRequireItemService.addRequireItem(list);
                //    //直接上传
                //}
                //else
                //{
                //    UploadInfo uploadInfo = new UploadInfo();
                //    uploadInfo.Id = zcRequire.id;
                //    uploadInfo.CreateTime = DateTime.Now;
                //    uploadInfo.UpdateTime = DateTime.Now;
                //    uploadInfo.Type = Constant.ZC_REQUIRE;
                //    UploadDao uploadDao = new UploadDao();
                //    uploadDao.AddUploadInfo(uploadInfo);
                //    //网络不通

                //}
            }
            else
            {
                zcRequire.checkDate = checkDatePageLoad;
                zcRequire.yhdNumber = textBox1.Text.ToString();
                zcRequireService.updateRequire(zcRequire);
                addSuccess = zcRequireItemService.updateZcRequireItem(list);

                ///TODO 上传更新数据
            }

            if (addSuccess == true)
            {
                MessageBox.Show("" + button1.Text.ToString() + "成功");

                commitButton.Enabled = true;

            }
            else
            {
                MessageBox.Show(button1.Text.ToString() + "失败");
            }
        }

        /// <summary>
        /// 保存商品和要货单信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_4(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0)
            {
                return;
            }

            DialogResult result = MessageBox.Show("确定" + button1.Text.ToString() + "吗？", button1.Text.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                editing();
                buttonClickNumber++;
               
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0)
            {
                return;
            }
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择至少一行数据");
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("确定删除吗？", "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (isNew == true)
                    {
                        int index = dataGridView1.CurrentRow.Index;
                        dataGridView1.Rows.RemoveAt(index);
                    }
                    else
                    {

                        for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                        {
                            string id = dataGridView1.SelectedRows[i].Cells[8].Value.ToString();
                            DataSet ds = (DataSet)dataGridView1.DataSource;
                            ds.Tables[0].Rows.RemoveAt(dataGridView1.SelectedRows[i].Index);
                            //    dataGridView1.Rows.Remove(dataGridView1.Rows[dataGridView1.SelectedRows[i].Index]);

                            if (!"".Equals(id))
                            {
                                ZcRequireItemService zcRequireItemService = new ZcRequireItemService();
                                ZcRequireItem zcRequireItem = new ZcRequireItem();
                                zcRequireItem.id = id;
                                zcRequireItemService.deleteGoodsById(zcRequireItem);
                            }
                        }

                    }
                    moneyChange();
                }
                else
                {
                    return;
                }
                if (dataGridView1.RowCount > 0)
                {
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Selected = true;
                }
            }
        }


       
        /// <summary>
        /// 编辑，状态改为false
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            isNew = false;
            RGReturnList rGReturnList = new RGReturnList(this);
            rGReturnList.ShowDialog();
        }


        /// <summary>
        /// 改变数量和总合计
        /// </summary>
        private void moneyChange()
        {
            float totalMoney = 0;
            float goodsNums = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                float nums = dataGridView1.Rows[i].Cells[4].Value == null ? 0 : float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                float price = dataGridView1.Rows[i].Cells[5].Value == null ? 0 : float.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                float money = nums * price;
                dataGridView1.Rows[i].Cells[6].Value = money;
                goodsNums += nums;
                totalMoney += money;
            }
            textBox14.Text = totalMoney.ToString("0.00");
            textBox13.Text = goodsNums.ToString();

        }

        

        /// <summary>
        /// 更改的时候获取商品信息
        /// </summary>
        /// <param name="requireId"></param>
        /// <returns></returns>
        private DataSet GetGoodData(string requireId)
        {
            DataSet ds = new DataSet();
            OracleUtil oracleUtil = new OracleUtil();

            string sql = "select c.SERIALNUMBER, c.GOODS_NAME,  c.GOODS_UNIT,c.GOODS_SPECIFICATIONS,a.nums, c.GOODS_PRICE, a.money, a.remark, a.id    from zc_require_item a left join zc_require b on a.require_id = b.id "
                + "left join  zc_goods_master c on a.goods_file_id = c.ID where a.require_id = '" + requireId + "'";
            ds = oracleUtil.GetDataSet(sql, "zc_goods_master");
            return ds;
        }
        /// <summary>
        /// 获取传进页面的分店信息
        /// </summary>
        /// <param name="branchFindId"></param>
        /// <returns></returns>
        private ZcBranchTotal GetBranchData(string branchFindId)
        {
            ZcBranchTotal obj = new ZcBranchTotal();
            string sql = "select branch_code ,branch_name from zc_branch_total where id = '" + branchFindId + "'";
            OracleConnection conn = null;
            OracleCommand cmd = new OracleCommand();
            try
            {
                conn = OracleUtil.OpenConn();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    obj.BranchCode = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.BranchName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            finally
            {
                OracleUtil.CloseConn(conn);
            }
            return obj;
        }
        /// <summary>
        /// 根据传进的userid获取当前订单的用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        private ZcUserInfo GetUserInfoData(string userid)
        {
            ZcUserInfo obj = new ZcUserInfo();
           
            string sql = "select id, username from zc_user_info where user_id = '" + userid + "'";

            OracleConnection conn = null;
            OracleCommand cmd = new OracleCommand();
            try
            {
                conn = OracleUtil.OpenConn();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    obj.Id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.UserName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            finally
            {
                OracleUtil.CloseConn(conn);
            }
            return obj;
        }

        int buttonClickNumber = 0;
        DateTime ? checkDatePageLoad;
        /// <summary>
        /// 更改商品信息
        /// </summary>
        /// <param name="zcRequire"></param>
        public void AddZcRequireInfo(ZcRequire zcRequire)
        {
            // 如果需要编辑之后判断使用
            //if (zcRequire.status.Equals("1") || zcRequire.status.Equals("2") || zcRequire.status.Equals("4") || zcRequire.status.Equals("5"))
            //{   
            //    button1.Enabled = false;
            //    commitButton.Enabled = false;
            //    button2.Enabled = false;
            //    button11.Enabled = false;
            //    textBox7.ReadOnly = true;
            //    label8.Text = "当前日期";
            //    deleteButton.Enabled = false;
            //    button4.Enabled = false;
            //    for (int i = 0; i < dataGridView1.Rows.Count;i++ )
            //    {
            //        dataGridView1.Rows[i].ReadOnly=true;
            //    }


            //}
            button1.Text = "更新";
            if(zcRequire.status.Equals("2"))
            {
                commitgoodsbutton.Text = "确认收货";
            }
            
            textBox1.Text = zcRequire.yhdNumber;
            ///这边是创建时间
            // timesLabel.Text = zcRequire.createTime.ToString("yyyy-MM-dd HH:mm:ss");

            requireIdBox.Text = zcRequire.id;
            textBox5.Text = zcRequire.branchId;
            textBox6.Text = zcRequire.calloutBranchId;

            //如何获取用户名称

            textBox7.Text = zcRequire.remark;
            textBox11.Text = zcRequire.checkMan;
            textBox10.Text = zcRequire.reason.ToString();
            textBox13.Text = zcRequire.nums;
            textBox14.Text = zcRequire.money;

            //DataSet ds3 = GetUserInfoData(zcRequire.userId);
           ZcUserInfo userInfoName =  GetUserInfoData(zcRequire.userId);
           textBox9.Text = userInfoName.UserName;

            //DataSet ds1 = GetGoodData(zcRequire.branchId);
            textBox3.Text = LoginUserInfo.street;
            textBox2.Text = LoginUserInfo.branchName;

            ZcBranchTotal obj = GetBranchData(zcRequire.calloutBranchId);
            textBox4.Text = string.IsNullOrEmpty(obj.BranchCode) ? string.Empty : obj.BranchCode;
            textBox8.Text = string.IsNullOrEmpty(obj.BranchName) ? string.Empty : obj.BranchName;

            dataGridView1.DataSource = GetGoodData(zcRequire.id);
            dataGridView1.DataMember = "zc_goods_master";
            dataGridView1.AutoGenerateColumns = false;

            // BranchZcRequireService branchZcRequireService = new BranchZcRequireService();
            //List<ZcRequireItem> zcRequireItem = branchZcRequireService.findAllRequireItemByRequireId(zcRequire);
            ///按钮按的次数判断
            buttonClickNumber = 0;

            commitButton.Enabled = true;
            if (!zcRequire.checkDate.ToString().Equals(""))
            {
                checkDatePageLoad = DateTime.Parse(zcRequire.checkDate.ToString());
            }
            checkDatePageLoad = null;


        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 4)
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

        private bool isNew = true;

        public void cleanTheWindow() {
            isNew = true;
            //label10.Text = "新建";
            textBox1.Text = null;
            textBox2.Text = LoginUserInfo.branchCode;
            textBox3.Text = LoginUserInfo.branchName;
            textBox4.Text = null;
            textBox8.Text = null;
            textBox6.Text = null;
            textBox5.Text = LoginUserInfo.branchId;
            textBox7.Text = null;
            label1.Text = LoginUserInfo.name;
            label12.Text = LoginUserInfo.branchName;
            textBox9.Text = LoginUserInfo.name;
            textBox13.Text = "0";
            textBox14.Text = "0.00";
            dataGridView1.DataSource = null;
            dataGridView1.DataMember = null;
            ; //显示时间
            Times times = new Times();
            times.TopLevel = false;
            this.timesPanel.Controls.Add(times);
            times.Show();
            timesLabel = null;
        }
        /// <summary>
        /// 新建赋空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createbutton_Click(object sender, EventArgs e)
        {
            cleanTheWindow();           
        }
        
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                moneyChange();
            }
        }
        /// <summary>
        /// 提交要货单(1.直接写好保存，提交 2.写好，保存，修改再点提交3.编辑选择，提交4.编辑选择之后，更改提交)  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commitButton_Click(object sender, EventArgs e)
        {
            //if (buttonClickNumber==0)
            //{
                
               
            //    DialogResult result = MessageBox.Show("是否更新并提交？", "提交", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (result == DialogResult.Yes)
            //    {
            //        editing();
            //        button1_Click_4(sender, e);
            //        ZcRequire zcRequireCommit = new ZcRequire();
            //        zcRequireCommit.yhdNumber = textBox1.Text.ToString();
            //        zcRequireCommit.status = Constant.CHECK_STATUS_FINISH;
            //        ZcRequireService zcRequireService = new ZcRequireService();
            //        zcRequireService.updateRequireByYhdNumber(zcRequireCommit);
            //        MessageBox.Show("更新并提交成功");
            //        BranchZcRequire bz = new BranchZcRequire();
            //        bz.ShowDialog();
                   
            //    }
               

            //}
            
            DialogResult result2 = MessageBox.Show("是否"+button1.Text.ToString()+"并且提交？", "提交", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result2 == DialogResult.Yes)
            {
                editing();
                button1_Click_4(sender, e);
                ZcRequire zcRequireCommit = new ZcRequire();
                zcRequireCommit.yhdNumber = textBox1.Text.ToString();
                zcRequireCommit.status = Int32.Parse(Constant.CHECK_STATUS_WAITCHECK);
                ZcRequireService zcRequireService = new ZcRequireService();
                zcRequireService.updateRequireByYhdNumber(zcRequireCommit);
                MessageBox.Show("提交成功");
                cleanTheWindow();

            }
        }
        /// <summary>
        /// 删除要货单和本单所有的商品信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click_2(object sender, EventArgs e)
        {
            if (isNew == true)
            {
                MessageBox.Show("请选择一条订单");
            }
            else
            {
                ZcRequire zcRequire = new ZcRequire();
                zcRequire.id = requireIdBox.Text.ToString();
                ZcRequireService zcRequireService = new ZcRequireService();
                zcRequireService.deleteZcRequireById(zcRequire);
                ZcRequireItemService zcRequireItemService = new ZcRequireItemService();
                zcRequireItemService.DeleteRequireItemById(zcRequire);
            }
        }

        
        /// <summary>
        /// 确认收货，需要改变状态，查询的框只有已经审核完毕的单据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commitgoodsbutton_Click(object sender, EventArgs e)
        {
            if (commitgoodsbutton.Text.Equals("收货单据"))
            {
                ChooserCommitList rGReturnList = new ChooserCommitList();
                rGReturnList.ShowDialog();
            }
            else if (commitgoodsbutton.Text.Equals("确认收货"))
            {
                ///TODO
                ///更改 审核时间，审核人，审核状态，更新时间
                ZcRequire ZcRequire = new ZcRequire();
                ZcRequire.id = requireIdBox.Text.ToString();
                ZcRequire.checkMan =  textBox11.Text.ToString();
                ZcRequire.checkDate = checkDatePageLoad;
                ZcRequire.status = Int32.Parse(Constant.CHECK_STATUS_FINISH.ToString()); 
                ZcRequire.reason = textBox10.Text.ToString();
                ZcRequireService zcRequireService = new ZcRequireService();
                zcRequireService.commitCheckedOrder(ZcRequire);
                MessageBox.Show("确认收货成功");
                commitgoodsbutton.Text = "收货单据";

                cleanTheWindow();

            }
            
            
        }
        /// <summary>
        /// 金额的格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView view = (DataGridView)sender;
            if (view.Columns[e.ColumnIndex].DataPropertyName == "money")
            {
                
                string money = Convert.ToString(e.Value);
                string showMoney = string.Format(money,"0.00");
                e.Value = showMoney;
            }

        }

       
       

       

        
    }
}
