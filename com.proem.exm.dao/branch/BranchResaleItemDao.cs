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
        /// 新增零售信息
        /// </summary>
        /// <param name="list"></param>
        public void AddResaleItem(List<ResaleItem> list){
            string sql = "insert into zc_resale_item values (@id, @createTime, @updateTime, @GoodsFileId, @nums ,@money, @resaleId)";
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
                    cmd.Parameters.AddWithValue("@GoodsFileId", obj.GoodsFileId);
                    cmd.Parameters.AddWithValue("@nums", obj.Nums);
                    cmd.Parameters.AddWithValue("@money", obj.Money);
                    cmd.Parameters.AddWithValue("@resaleId", obj.ResaleId);
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

    }
}
