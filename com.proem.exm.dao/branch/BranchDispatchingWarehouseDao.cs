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
    public class BranchDispatchingWarehouseDao
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(BranchDispatchingWarehouseDao));

        /// <summary>
        /// 新增配送出货单
        /// </summary>
        /// <param name="list"></param>
        public void addObj(List<DispatchingWarehouse> list) {
            string sql = "insert into zc_dispatching_Warehouse (id, createTime, updateTime, street, branch_total_id, dispatcher_date, nums, "
                + "weight, money, dispatcherOdd, statue, checkMan, checkDate, createMan, remark, branch_id, type) values"
                + " (@id, @createTime, @updateTime, @street, @branch_total_id, @dispatcher_date, @nums, "
                + "@weight, @money, @dispatcherOdd, @statue, @checkMan, @checkDate, @createMan, @remark, @branch_id, @type)";
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
                    DispatchingWarehouse obj = list[i];
                    cmd.Parameters.AddWithValue("@id", obj.id);
                    cmd.Parameters.AddWithValue("@createTime", obj.createTime);
                    cmd.Parameters.AddWithValue("@updateTime", obj.updateTime);
                    cmd.Parameters.AddWithValue("@street", obj.street);
                    cmd.Parameters.AddWithValue("@branch_total_id", obj.branch_total_id);
                    cmd.Parameters.AddWithValue("@dispatcher_date", obj.dispatcher_date);
                    cmd.Parameters.AddWithValue("@nums", obj.nums);
                    cmd.Parameters.AddWithValue("@weight", obj.weight);
                    cmd.Parameters.AddWithValue("@money", obj.money);
                    cmd.Parameters.AddWithValue("@dispatcherOdd", obj.dispatcherOdd);
                    cmd.Parameters.AddWithValue("@statue", obj.statue);
                    cmd.Parameters.AddWithValue("@checkMan", obj.checkMan);
                    cmd.Parameters.AddWithValue("@checkDate", obj.checkDate);
                    cmd.Parameters.AddWithValue("@createMan", obj.createMan);
                    cmd.Parameters.AddWithValue("@remark", obj.remark);
                    cmd.Parameters.AddWithValue("@branch_id", obj.branch_id);
                    cmd.Parameters.AddWithValue("@type", obj.type);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("向zc_dispatching_Warehouse添加数据失败", ex);
            }
            finally 
            {
                cmd.Dispose();
                dbHelper.CloseConnection(conn);
            }
        }


        public List<string> getCountToday()
        {
            List<string> list = new List<string>();
            string startTime = DateTime.Now.ToString("yyyy-MM-dd 00:00:01");
            string endTime = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
            string sql = "select id from zc_dispatching_Warehouse where 1=1 and createTime>=to_date('"+startTime+"', 'yyyy-MM-dd HH24:mi:ss') createTime<=to_date('"+endTime+"', 'yyyy-MM-dd HH24:mi:ss') ";
            MysqlDBHelper dbHelper = new MysqlDBHelper();
            MySqlConnection conn = null;
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader = null;
            try
            {
                conn = dbHelper.GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                while(reader.Read()){
                    string id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    list.Add(id);
                }
            }
            catch (Exception ex)
            {
                log.Error("查询今天有无总部配送出库单", ex);
            }
            finally
            {
                if(reader != null){
                    reader.Close();
                }
                cmd.Dispose();
                dbHelper.CloseConnection(conn);
            }
            return list;
        }
    } 
}
