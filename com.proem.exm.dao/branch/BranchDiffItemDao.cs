using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.util;
using log4net;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.dao.branch
{
    public class BranchDiffItemDao
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(BranchDiffItemDao));

        public void addList(List<BranchDiffItem> list)
        {
            string sql = "insert into zc_branch_diff_item (id, createTime, updateTime, branchDiff_id, nums, weight, money, goodsFile_id, price) "
                + " values(@id, @createTime, @updateTime, @branchDiff_id, @nums, @weight, @money, @goodsFile_id, @price)";
            MySqlConnection conn = null;
            MySqlTransaction tran = null;
            MySqlCommand cmd = new MySqlCommand();
            MysqlDBHelper dbHelper = new MysqlDBHelper();
            try
            {
                conn = dbHelper.GetConnection();
                tran = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                for (int i = 0; i < list.Count; i++)
                {
                    BranchDiffItem obj = list[i];
                    cmd.Parameters.AddWithValue("@id", obj.id);
                    cmd.Parameters.AddWithValue("@createTime", obj.createTime);
                    cmd.Parameters.AddWithValue("@updateTime", obj.updateTime);
                    cmd.Parameters.AddWithValue("@branchDiff_id", obj.branchDiff_id);
                    cmd.Parameters.AddWithValue("@nums", obj.nums);
                    cmd.Parameters.AddWithValue("@weight", obj.weight);
                    cmd.Parameters.AddWithValue("@money", obj.money);
                    cmd.Parameters.AddWithValue("@goodsFile_id", obj.goodsFile_id);
                    cmd.Parameters.AddWithValue("@price", obj.price);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("新增分店差异单明细失败", ex);
            }
            finally
            {
                cmd.Dispose();
                dbHelper.CloseConnection(conn);
            }
        }

        public BranchDiffItem FindById(string p){
            BranchDiffItem obj = new BranchDiffItem();
            string sql = "select id, createTime, updateTime, branchDiff_id, nums, weight, money, goodsFile_id, price  from zc_branch_diff_item where 1=1 and id='" + p + "'";
            MySqlConnection conn = null;
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader = null;
            MysqlDBHelper dbHelper = new MysqlDBHelper();
            try
            {
                conn = dbHelper.GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                if(reader.Read()){
                    obj.id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.createTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.updateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.branchDiff_id = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    obj.nums = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.weight = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    obj.money = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.goodsFile_id = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    obj.price = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                }
            }
            catch (Exception ex)
            {
                log.Error("根据Id获取差异单明细失败", ex);
            }
            finally
            {
                if(reader!= null){
                    reader.Close();
                }
                cmd.Dispose();
                dbHelper.CloseConnection(conn);
            }
            return obj;
        }
    }
}
