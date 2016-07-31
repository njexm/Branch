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
    public class BranchDiffDao
    {
        private readonly ILog log = LogManager.GetLogger(typeof(BranchDiffDao));

        public void addObj(BranchDiff obj)
        {
            string sql = "insert into zc_branch_diff (id, createTime, updateTime, DiffOdd, branchIn_id, nums, weight, money, branch_id, branch_from_id, user_id) "
                + " values (@id, @createTime, @updateTime, @DiffOdd, @branchIn_id, @nums, @weight, @money, @branch_id, @branch_from_id, @user_id)";
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
                cmd.Parameters.AddWithValue("@id", obj.id);
                cmd.Parameters.AddWithValue("@createTime", obj.createTime);
                cmd.Parameters.AddWithValue("@updateTime", obj.updateTime);
                cmd.Parameters.AddWithValue("@DiffOdd", obj.DiffOdd);
                cmd.Parameters.AddWithValue("@branchIn_id", obj.branchIn_id);
                cmd.Parameters.AddWithValue("@nums", obj.nums);
                cmd.Parameters.AddWithValue("@weight", obj.weight);
                cmd.Parameters.AddWithValue("@money", obj.money);
                cmd.Parameters.AddWithValue("@branch_id", obj.branch_id);
                cmd.Parameters.AddWithValue("@branch_from_id", obj.branch_from_id);
                cmd.Parameters.AddWithValue("@user_id", obj.user_id);
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("新增分店差异单失败", ex);
            }
            finally
            {
                cmd.Dispose();
                dbHelper.CloseConnection(conn);
            }
        }

        public BranchDiff FindById(string p)
        {
            BranchDiff obj = new BranchDiff();
            string sql = "select id, createTime, updateTime, DiffOdd, branchIn_id, nums, weight, money, branch_id, branch_from_id, user_id from zc_branch_diff where 1=1 and id = '" + p + "'";
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
                while (reader.Read())
                {
                    obj.id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.createTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.updateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.DiffOdd = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    obj.branchIn_id = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.nums = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    obj.weight = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.money = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    obj.branch_id = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                    obj.branch_from_id = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                    obj.user_id = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                }
            }
            catch (Exception ex)
            {
                log.Error("根据id单查询分店入库单条数失败", ex);
            }
            finally
            {
                if (reader.Read())
                {
                    reader.Close();
                }
                cmd.Dispose();
                dbHelper.CloseConnection(conn);
            }
            return obj;
        }
    }
}
