using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.util;
using log4net;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.dao.master
{
    public class OrderDigitsDao
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(OrderDigitsDao));

        public List<OrderDigits> FindAll()
        {
            List<OrderDigits> list = new List<OrderDigits>();
            string sql = "select id,createTime, updateTime, countodsi, digitsamount, money_accuracy, orderamount,type from zc_order_digits";
            OracleConnection conn = null;
            OracleCommand cmd = new OracleCommand();
            try
            {
                conn = OracleUtil.OpenConn();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                OracleDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    OrderDigits obj = new OrderDigits();
                    obj.Id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.CreateTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.UpdateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.CountOdsi = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    obj.DigitsAmount = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.MoneyAccuracy = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    obj.orderAmount = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.Type = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                log.Error("查询精度表发生错误", ex);
            }
            finally 
            {
                OracleUtil.CloseConn(conn);
            }
            return list;
        }
    }
}
