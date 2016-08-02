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
    public class BranchOutDao
    {
        private readonly ILog log = LogManager.GetLogger(typeof(BranchOutDao));

        public void addObj(BranchOut obj) 
        {
            string sql = "insert into zc_branch_out (id, createTime, updateTime, OutOdd, branchDiff_id, nums, weight, money, branch_id, branch_to_id, user_id, status, remark) "
                + " values (@id, @createTime, @updateTime, @OutOdd, @branchDiff_id, @nums, @weight, @money, @branch_id, @branch_to_id, @user_id, @status, @remark)";
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
                cmd.Parameters.AddWithValue("@OutOdd", obj.OutOdd);
                cmd.Parameters.AddWithValue("@branchDiff_id", obj.branchDiff_id);
                cmd.Parameters.AddWithValue("@nums", obj.nums);
                cmd.Parameters.AddWithValue("@weight", obj.weight);
                cmd.Parameters.AddWithValue("@money", obj.money);
                cmd.Parameters.AddWithValue("@branch_id", obj.branch_id);
                cmd.Parameters.AddWithValue("@branch_to_id", obj.branch_to_id);
                cmd.Parameters.AddWithValue("@user_id", obj.user_id);
                cmd.Parameters.AddWithValue("@status", obj.status);
                cmd.Parameters.AddWithValue("@remark", obj.remark);
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("新增分店出库单失败", ex);
            }
            finally
            {
                cmd.Dispose();
                dbHelper.CloseConnection(conn);
            }
        }

        public void deleteById(string id)
        {
            string sql = "delete from zc_branch_out where 1=1 and id ='" + id + "'";
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
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("根据Id删除分店出库单失败", ex);
            }
            finally
            {
                cmd.Dispose();
                dbHelper.CloseConnection(conn);
            }
        }

        public void updateObj(BranchOut obj)
        {
            string sql = "update zc_branch_out set updateTime = @updateTime, nums= @nums,money=@money, branch_to_id = @branch_to_id, remark= @remark where 1=1 and id=@id";
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
                cmd.Parameters.AddWithValue("@updateTime", obj.updateTime);
                cmd.Parameters.AddWithValue("@nums", obj.nums);
                cmd.Parameters.AddWithValue("@money", obj.money);
                cmd.Parameters.AddWithValue("@branch_to_id", obj.branch_to_id);
                cmd.Parameters.AddWithValue("@remark", obj.remark);
                cmd.Parameters.AddWithValue("@id", obj.id);
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("更新分店出库单失败", ex);
            }
            finally
            {
                cmd.Dispose();
                dbHelper.CloseConnection(conn);
            }
        }

        public void updateStatus(string branchId, string p)
        {
            string sql = "update zc_branch_out set status = '" + p + "' where 1=1 and id='" + branchId + "'";
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
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("审核分店出库单失败", ex);
            }
            finally
            {
                cmd.Dispose();
                dbHelper.CloseConnection(conn);
            }
        }

        public BranchOut FindById(string branchId)
        {
            BranchOut obj = new BranchOut();
            string sql = "select id, createTime, updateTime, OutOdd, branchDiff_id, nums, weight, money, branch_id, branch_to_id, user_id, status, remark from zc_branch_out where id='" + branchId + "'";
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
                    obj.OutOdd = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    obj.branchDiff_id = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.nums = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    obj.weight = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.money = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    obj.branch_id = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                    obj.branch_to_id = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                    obj.user_id = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                    obj.status = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                    obj.remark = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                }
            }
            catch (Exception ex)
            {
                log.Error("根据id单查询分店出库单失败", ex);
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
