using Branch.com.proem.exm.util;
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

namespace Branch.com.proem.exm.window.receive
{
    public partial class DispatchChoose : Form
    {
        private readonly ILog log = LogManager.GetLogger(typeof(DispatchChoose));

        private DeliveryGoods deliveryGoods;

        public DispatchChoose()
        {
            InitializeComponent();
        }

        public DispatchChoose(DeliveryGoods deliveryGoods)
        {
            InitializeComponent();
            this.deliveryGoods = deliveryGoods;
        }

        private void DispatchChoose_Load(object sender, EventArgs e)
        {
            DateTime first = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:01"));
            DateTime last = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            string sql = "select id, dispatcherOdd as odd, createTime, money from zc_dispatching_Warehouse where 1=1 and createTime >= :first and createTime <=:last";
            MysqlDBHelper dbHelper = new MysqlDBHelper();         
            MySqlConnection conn = null;
            MySqlCommand cmd = new MySqlCommand();
            DataSet ds = new DataSet();
            try
            {
                conn = dbHelper.GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue(":first", first);
                cmd.Parameters.AddWithValue(":last", last);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds, "zc_dispatching_Warehouse");
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "zc_dispatching_Warehouse";
            }
            catch (Exception ex)
            {
                log.Error("加载今天的配送出库单失败", ex);
            }
            finally
            {
                cmd.Dispose();
                dbHelper.CloseConnection(conn);
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //行号
            using (SolidBrush b = new SolidBrush(this.dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 5, e.RowBounds.Location.Y + 4);
            }
        }

        private void DispatchChoose_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter){
                okButton_Click(this, EventArgs.Empty);
            }
            if(e.KeyCode == Keys.Escape){
                cancelButton_Click(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            if(dataGridView1.DataSource == null || dataGridView1.RowCount == 0){
                return;
            }
            string id = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            deliveryGoods.loadById(id);
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
