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
    /// <summary>
    /// 分店zc_resale表操作
    /// </summary>
    public class BranchResaleDao : MysqlDBHelper
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(BranchResaleDao));

        /// <summary>
        /// 新增流水信息
        /// </summary>
        /// <param name="obj"></param>
        public void AddResale(Resale obj)
        {
            string sql = "insert into zc_resale values (@id, @createTime, @updateTime, @nums , @money,@discount_money, @preferential_money, @actual_money , @branchId, @saleman_id, @memberId , @order_id, @number, @payInfoId)";
            MySqlConnection conn = null;
            MySqlTransaction tran = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id", obj.Id);
                cmd.Parameters.AddWithValue("@createTime", obj.CreateTime);
                cmd.Parameters.AddWithValue("@updateTime", obj.UpdateTime);
                cmd.Parameters.AddWithValue("@nums", obj.Nums);
                cmd.Parameters.AddWithValue("@money", obj.Money);
                cmd.Parameters.AddWithValue("@discount_money", obj.DiscountMoney);
                cmd.Parameters.AddWithValue("@preferential_money", obj.PreferentialMoney);
                cmd.Parameters.AddWithValue("@actual_money", obj.ActualMoney);
                cmd.Parameters.AddWithValue("@branchId", obj.BranchId);
                cmd.Parameters.AddWithValue("@saleman_id", obj.SaleManId);
                cmd.Parameters.AddWithValue("@memberId", obj.memberId);
                cmd.Parameters.AddWithValue("@order_id", obj.OrderId);
                cmd.Parameters.AddWithValue("@number", obj.WaterNumber);
                cmd.Parameters.AddWithValue("@payInfoId", obj.PayInfoId);
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("保存零售信息到本地数据库失败", ex);
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        /// <summary>
        /// 根据会员id查询流水条数
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int FindCountByMemberId(string p)
        {
            int count = 0;
            string sql = "select count(1) from zc_resale where member_id = '"+p+"'";
            MySqlConnection conn = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    count = reader.IsDBNull(0) ? default(int) : reader.GetInt32(0);
                }
            }
            catch (Exception ex)
            {
                log.Error("根据会员id查询流水条数发生错误", ex);
            }
            finally 
            {
                CloseConnection(conn);
            }
            return count;
        }

        /// <summary>
        /// 根据会员id查询销售流水信息列表
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<Resale> FindByMemberId(string p)
        {
            List<Resale> list = new List<Resale>();
            string sql = "select id, createTime, updateTime, nums, money, discount_money, preferential_money, actual_money, branch_id, saleman_id, member_id, "
                + "order_id, water_number, payInfo_id from zc_resale where member_id = '"+p+"'";
            MySqlConnection conn = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Resale obj = new Resale();
                    obj.Id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.CreateTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.UpdateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.Nums = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    obj.Money = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.DiscountMoney = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    obj.PreferentialMoney = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.ActualMoney = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    obj.BranchId = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                    obj.SaleManId = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                    obj.memberId = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                    obj.OrderId = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                    obj.WaterNumber = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                    obj.PayInfoId = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                log.Error("根据会员id查询销售流水信息列表发生错误", ex);
            }
            finally
            {
                CloseConnection(conn);
            }
            return list;
        }
    }
}
