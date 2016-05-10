using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.util;
using Branch.com.proem.exm.window.require;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Branch.com.proem.exm.window.retreat
{
    /// <summary>
    /// 选择操作员
    /// </summary>
    public partial class RGROperatorChoose : Form
    {
        public RGROperatorChoose()
        {
            InitializeComponent();
        }

         private RGReturnList rGReturnList;

        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="obj"></param>
         public RGROperatorChoose(RGReturnList obj)
        {
            InitializeComponent();
            this.rGReturnList = obj;
        }

         private ChooserCommitList chooserCommitList;

         /// <summary>
         /// 重载确认收货
         /// </summary>
         /// <param name="obj"></param>
         public RGROperatorChoose(ChooserCommitList obj)
         {
             InitializeComponent();
             this.chooserCommitList = obj;
         }
        private void chooseUserInfo() {
            ZcUserInfo zcUserInfo = new ZcUserInfo();
            zcUserInfo.Id = operatorDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            zcUserInfo.UserName = operatorDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            this.rGReturnList.addUserInfo(zcUserInfo);
        }
        private void okButton_Click(object sender, EventArgs e)
        {
            chooseUserInfo();
            this.Close();
        }

        private void leaveButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RGROperatorChoose_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Q)
            //{
            //    queryButton_Click(this, EventArgs.Empty);
            //}
            if (e.KeyCode == Keys.Y)
            {
                okButton_Click(this, EventArgs.Empty);
            }
            if (e.KeyCode == Keys.L)
            {
                leaveButton_Click(this, EventArgs.Empty);
            }
        }

        private void RGROperatorChoose_Load(object sender, EventArgs e)
        {
            DataSet ds = GetData();
            operatorDataGridView.DataSource = ds;
            operatorDataGridView.DataMember = "zc_user_info";
        }

        private void queryButton_Click(object sender, EventArgs e)
        {

            operatorDataGridView.DataSource = GetData(); ;
            operatorDataGridView.DataMember = "zc_user_info";
        }

        private DataSet GetData()
        {
            DataSet ds = new DataSet();
            OracleUtil oracleUtil = new OracleUtil();
            string searchString = operatorTextBox.Text;
            string sql = "select u.id,zbt.branch_code,zbt.branch_name,u.username from zc_user_info u left join zc_branch_total zbt on u.branch_name_id = zbt.id ";
            if (!string.IsNullOrEmpty(searchString.Trim()))
            {
                sql += " where u.username like '%" + searchString + "%' or zbt.branch_code like '%" + searchString + "%' or zbt.branch_name like '%" + searchString + "%'";
            }
            ds = oracleUtil.GetDataSet(sql, "zc_user_info");
            return ds;
        }

        private void operatorDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            okButton_Click(okButton,new EventArgs());
        }
    }
}
