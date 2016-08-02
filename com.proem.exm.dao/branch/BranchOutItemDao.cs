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
    public class BranchOutItemDao
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(BranchOutItemDao));

        public void addObj(BranchOutItem obj) 
        {
            string sql = "insert into zc_branch_out_item (id, createTime, updateTime, branchOut_id, nums, weight, money, goodsFile_id, price) "
                + " values(@id, @createTime, @updateTime, @branchOut_id, @nums, @weight, @money, @goodsFile_id, @price)";
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
                cmd.Parameters.AddWithValue("@branchOut_id", obj.branchOut_id);
                cmd.Parameters.AddWithValue("@nums", obj.nums);
                cmd.Parameters.AddWithValue("@weight", obj.weight);
                cmd.Parameters.AddWithValue("@money", obj.money);
                cmd.Parameters.AddWithValue("@goodsFile_id", obj.goodsFile_id);
                cmd.Parameters.AddWithValue("@price", obj.price);
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("新增分店出库单明细失败", ex);
            }
            finally
            {
                cmd.Dispose();
                dbHelper.CloseConnection(conn);
            }
        }

        public void deleteByOutId(string branchOutId)
        {
            string sql = "delete from zc_branch_out_item where 1=1 and branchOut_id ='" + branchOutId + "'";
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
                log.Error("根据Id删除分店出库单明细失败", ex);
            }
            finally
            {
                cmd.Dispose();
                dbHelper.CloseConnection(conn);
            }
        }

        public void addList(List<BranchOutItem> list)
        {
            string sql = "insert into zc_branch_out_item (id, createTime, updateTime, branchOut_id, nums, weight, money, goodsFile_id, price) "
                + " values(@id, @createTime, @updateTime, @branchOut_id, @nums, @weight, @money, @goodsFile_id, @price)";
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
                for (int i = 0; i < list.Count; i++ )
                {
                    BranchOutItem obj = list[i];
                    cmd.Parameters.AddWithValue("@id", obj.id);
                    cmd.Parameters.AddWithValue("@createTime", obj.createTime);
                    cmd.Parameters.AddWithValue("@updateTime", obj.updateTime);
                    cmd.Parameters.AddWithValue("@branchOut_id", obj.branchOut_id);
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
                log.Error("新增分店出库单明细失败", ex);
            }
            finally
            {
                cmd.Dispose();
                dbHelper.CloseConnection(conn);
            }
        }

        public void updateList(List<BranchOutItem> list)
        {
            string sql = "update zc_branch_out_item set updateTime =@updateTime, nums= @nums, money =@money where 1=1 and id=@id";
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
                    BranchOutItem obj = list[i];
                    cmd.Parameters.AddWithValue("@updateTime", obj.updateTime);
                    cmd.Parameters.AddWithValue("@nums", obj.nums);
                    cmd.Parameters.AddWithValue("@money", obj.money);
                    cmd.Parameters.AddWithValue("@id", obj.id);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("更新分店出库单明细失败", ex);
            }
            finally
            {
                cmd.Dispose();
                dbHelper.CloseConnection(conn);
            }
        }

        public List<BranchOutItem> FindByOutId(string branchId)
        {
            List<BranchOutItem> list = new List<BranchOutItem>();
            string sql = "select id, createTime, updateTime, branchOut_id, nums, weight, money, goodsFile_id, price  from zc_branch_out_item where 1=1 and branchOut_id='" + branchId + "'";
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
                    BranchOutItem obj = new BranchOutItem();
                    obj.id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.createTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.updateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.branchOut_id = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    obj.nums = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.weight = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    obj.money = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.goodsFile_id = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    obj.price = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                log.Error("根据Id获取亭点出库单明细失败", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                cmd.Dispose();
                dbHelper.CloseConnection(conn);
            }
            return list;
        }

        public void deleteById(string id)
        {
            string sql = "delete from zc_branch_out_item where 1=1 and id ='" + id + "'";
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
                log.Error("根据Id删除分店出库单明细失败", ex);
            }
            finally
            {
                cmd.Dispose();
                dbHelper.CloseConnection(conn);
            }
        }

        public BranchOutItem FindById(string p)
        {
            BranchOutItem obj = new BranchOutItem();
            string sql = "select id, createTime, updateTime, branchOut_id, nums, weight, money, goodsFile_id, price  from zc_branch_out_item where 1=1 and id='" + p + "'";
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
                if (reader.Read())
                {
                    
                    obj.id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.createTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.updateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.branchOut_id = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    obj.nums = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.weight = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    obj.money = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.goodsFile_id = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    obj.price = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                }
            }
            catch (Exception ex)
            {
                log.Error("根据Id获取亭点出库单明细失败", ex);
            }
            finally
            {
                if (reader != null)
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
