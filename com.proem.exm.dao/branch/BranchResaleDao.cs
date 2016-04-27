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
        /// 新增零售信息
        /// </summary>
        /// <param name="obj"></param>
        public void AddResale(Resale obj)
        {
            string sql = "insert into zc_resale values (@id, @createTime, @updateTime, @number, @nums , @money, @branchId, @memberId)";
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
                cmd.Parameters.AddWithValue("@number", obj.WaterNumber);
                cmd.Parameters.AddWithValue("@nums", obj.Nums);
                cmd.Parameters.AddWithValue("@money", obj.Money);
                cmd.Parameters.AddWithValue("@branchId", obj.BranchId);
                cmd.Parameters.AddWithValue("@memberId", obj.memberId);
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
    }
}
