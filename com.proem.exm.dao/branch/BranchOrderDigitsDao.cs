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
    public class BranchOrderDigitsDao : MysqlDBHelper
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(BranchOrderDigitsDao));

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="list"></param>
        public void AddOrderDigits(List<OrderDigits> list)
        {
            string sql = "insert into zc_order_digits (id, createTime, updateTime, countodsi, digitsAmount, money_accuracy, orderAmount, type)"
                + " values(@id, @createTime, @updateTime, @countodsi, @disgitsAmount, @money_accuracy, @orderAmount, @type)";
            MySqlConnection conn = null;
            MySqlTransaction tran = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                foreach (OrderDigits obj in list) {
                    cmd.Parameters.AddWithValue("@id", obj.Id);
                    cmd.Parameters.AddWithValue("@createTime", obj.CreateTime);
                    cmd.Parameters.AddWithValue("@updateTime", obj.UpdateTime);
                    cmd.Parameters.AddWithValue("@countodsi", obj.CountOdsi);
                    cmd.Parameters.AddWithValue("@disgitsAmount", obj.DigitsAmount);
                    cmd.Parameters.AddWithValue("@money_accuracy", obj.MoneyAccuracy);
                    cmd.Parameters.AddWithValue("@orderAmount", obj.orderAmount);
                    cmd.Parameters.AddWithValue("@type", obj.Type);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("新增精度表发生错误", ex);
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        /// <summary>
        /// 获取精度
        /// </summary>
        /// <returns></returns>
        public OrderDigits GetDigits()
        {
            OrderDigits obj = new OrderDigits();
            string sql = "select id,createTime, updateTime, countodsi, digitsamount, money_accuracy, orderamount,type from zc_order_digits where id = '123456789'";
            MySqlConnection conn = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    obj.Id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.CreateTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.UpdateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.CountOdsi = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    obj.DigitsAmount = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.MoneyAccuracy = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    obj.orderAmount = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.Type = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                }
            }
            catch (Exception ex)
            {
                log.Error("获取精度发生错误", ex);
            }
            finally
            {
                CloseConnection(conn);
            }
            return obj;
        }
    }
}
