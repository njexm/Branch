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
    /// 分店zc_resale_item操作
    /// </summary>
    public class BranchResaleItemDao : MysqlDBHelper
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(BranchResaleItemDao));

        /// <summary>
        /// 新增流水明细信息
        /// </summary>
        /// <param name="list"></param>
        public void AddResaleItem(List<ResaleItem> list){
            string sql = "insert into zc_resale_item values (@id, @createTime, @updateTime, @resaleId, @GoodsFileId, @nums , @weight,@money, @discount_amount, @actual_money)";
            MySqlConnection conn = null;
            MySqlTransaction tran = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                foreach(ResaleItem obj in list){
                    cmd.Parameters.AddWithValue("@id", obj.Id);
                    cmd.Parameters.AddWithValue("@createTime", obj.CreateTime);
                    cmd.Parameters.AddWithValue("@updateTime", obj.UpdateTime);
                    cmd.Parameters.AddWithValue("@resaleId", obj.ResaleId);
                    cmd.Parameters.AddWithValue("@GoodsFileId", obj.GoodsFileId);
                    cmd.Parameters.AddWithValue("@nums", obj.Nums);
                    cmd.Parameters.AddWithValue("@weight", obj.weight);
                    cmd.Parameters.AddWithValue("@money", obj.Money);
                    cmd.Parameters.AddWithValue("@discount_amount", obj.DiscountMoney);
                    cmd.Parameters.AddWithValue("@actual_money", obj.ActualMoney);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("新增零售详细信息失败", ex);
            }
            finally 
            {
                CloseConnection(conn);
            }
        }


        public ResaleItem FindById(string p)
        {
            ResaleItem obj = new ResaleItem();
            string sql = "select id, createTime, updateTime, resale_id, goodsFile_id, nums, weight, money, discount_amount, actual_money from zc_resale_item where id = '"+p+"'";
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = null;
            try
            {
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read()){
                    obj.Id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.CreateTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.UpdateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.ResaleId = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    obj.GoodsFileId = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.Nums = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    obj.weight = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.Money = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    obj.DiscountMoney = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                    obj.ActualMoney = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                }
            }
            catch (Exception ex)
            {
                log.Error("根据id查询销售流水明细发生错误", ex);
            }
            finally
            {
                CloseConnection(conn);
            }
            return obj;
        }
    }
}
