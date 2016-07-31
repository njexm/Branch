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
    public class DispatchingWarehouseDao
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(DispatchingWarehouseDao));

        /// <summary>
        /// 查询当天所有配送出库单
        /// </summary>
        /// <returns></returns>
        public List<DispatchingWarehouse> getAll() 
        {
            string startTime = DateTime.Now.ToString("yyyy-MM-dd 00:00:01");
            string endTime = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
            List<DispatchingWarehouse> list = new List<DispatchingWarehouse>();
            string sql = "select id, createTime, updateTime, BRANCH_CODE, BRANCH_TOTAL_ID, DISPATCHER_DATE, DISPATCHER_MONEY, DISPATCHER_NUMS, DISPATCHER_WEIGHT, DISPATCHER_ODD, STATUE, CHECKMAN, CHECKDATE,"
                + " CREATEMAN, REMARK, BRANCH_ID, dispatching_Type from zc_dispatching_Warehouse where 1=1 and BRANCH_TOTAL_ID = '"+LoginUserInfo.branchId+"' and createTime>=to_date('" + startTime + "', 'yyyy-MM-dd HH24:mi:ss') and createTime <=to_date('" + endTime + "', 'yyyy-MM-dd HH24:mi:ss') ";
            OracleConnection conn = null;
            OracleCommand cmd = new OracleCommand();
            OracleDataReader reader = null;
            try
            {
                conn = OracleUtil.OpenConn();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                while(reader.Read()){
                    DispatchingWarehouse obj = new DispatchingWarehouse();
                    obj.id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.createTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.updateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.street = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    obj.branch_total_id = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.dispatcher_date = reader.IsDBNull(5) ? default(DateTime) : reader.GetDateTime(5);
                    obj.money = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.nums = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    obj.weight = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                    obj.dispatcherOdd = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                    obj.statue = reader.IsDBNull(10) ? default(int) : reader.GetInt32(10);
                    obj.checkMan = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                    obj.checkDate = reader.IsDBNull(12) ? default(DateTime) : reader.GetDateTime(12);
                    obj.createMan = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                    obj.remark = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                    obj.branch_id = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                    obj.type = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                log.Error("查询当天的配送出库单失败", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                OracleUtil.CloseConn(conn);
                cmd.Dispose();
            }
            return list;
        }
    }
}
