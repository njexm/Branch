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

namespace Branch.com.proem.exm.window.branchOut
{
    public partial class BranchOutChoose : Form
    {
        private readonly ILog log = LogManager.GetLogger(typeof(BranchOutChoose));

        public BranchOutChoose()
        {
            InitializeComponent();
        }

        private BranchOutForm branchOutForm;

        public BranchOutChoose(BranchOutForm branchOutForm)
        {
            InitializeComponent();
            this.branchOutForm = branchOutForm;
        }

        private void queryButton_Click(object sender, EventArgs e)
        {
            string startTime = startDateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss ");
            string endTime = endDateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss ");
            string odd = oddTextBox.Text;
            string sql = "select id, createTime, OutOdd as odd, nums, money, status from zc_branch_out where createTime>='" + startTime + "' and createTime<='" + endTime + "' and OutOdd like '%" + odd + "%'";
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
                da.Fill(ds, "zc_branch_out");
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "zc_branch_out";
            }
            catch (Exception ex)
            {
                log.Error("条件查询单据", ex);
            }
            finally
            {
                dbHelper.CloseConnection(conn);
                cmd.Dispose();
            }
        }

        private void okbutton_Click(object sender, EventArgs e)
        {
            if(dataGridView1.DataSource == null || dataGridView1.RowCount == 0){
                return;
            }
            string id = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            string status = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            branchOutForm.openBranchOut(id);
            this.Close();
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BranchOutChoose_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter){
                okbutton_Click(this, EventArgs.Empty);
            }
            if(e.KeyCode == Keys.Escape){
                cancelbutton_Click(this, EventArgs.Empty);
            }
            if(e.KeyCode == Keys.S){
                queryButton_Click(this, EventArgs.Empty);
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

        private void BranchOutChoose_Load(object sender, EventArgs e)
        {
            queryButton_Click(this, EventArgs.Empty);
        }
    }
}
