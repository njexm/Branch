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
    public class BranchDispatchingWarehouseItemDao
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(BranchDispatchingWarehouseItemDao));

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="list"></param>
        public void addObj(List<DispatchingWarehouseItem> list) 
        {
            string sql = "insert into zc_dispatching_Warehouse_items (id, createTime, updateTime, dispatchingWarehouseId, goods_name, serialNumber, "
                + "branch_total_id, cash_date, weight, goodsPrice, money, goods_specifications, goodsFile_id, nums) values "
                + " (@id, @createTime, @updateTime, @dispatchingWarehouseId, @goods_name, @serialNumber, "
                + " @branch_total_id, @cash_date, @weight, @goodsPrice, @money, @goods_specifications, @goodsFile_id, @nums)";
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
                    DispatchingWarehouseItem obj = list[i];
                    cmd.Parameters.AddWithValue("@id", obj.id);
                    cmd.Parameters.AddWithValue("@createTime", obj.createTime);
                    cmd.Parameters.AddWithValue("@updateTime", obj.updateTime);
                    cmd.Parameters.AddWithValue("@dispatchingWarehouseId", obj.dispatchingWarehouseId);
                    cmd.Parameters.AddWithValue("@goods_name", obj.goods_name);
                    cmd.Parameters.AddWithValue("@serialNumber", obj.serialNumber);
                    cmd.Parameters.AddWithValue("@branch_total_id", obj.branch_total_id);
                    cmd.Parameters.AddWithValue("@cash_date", obj.cash_date);
                    cmd.Parameters.AddWithValue("@weight", obj.weight);
                    cmd.Parameters.AddWithValue("@goodsPrice", obj.goodsPrice);
                    cmd.Parameters.AddWithValue("@money", obj.money);
                    cmd.Parameters.AddWithValue("@goods_specifications", obj.goods_specifications);
                    cmd.Parameters.AddWithValue("@goodsFile_id", obj.goodsFile_id);
                    cmd.Parameters.AddWithValue("@nums", obj.nums);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("新增zc_dispatching_Warehouse_items失败", ex);
            }
            finally
            { 
                cmd.Dispose();
                dbHelper.CloseConnection(conn);
            }
        }
    }
}
