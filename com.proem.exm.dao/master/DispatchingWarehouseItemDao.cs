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
    public class DispatchingWarehouseItemDao
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(DispatchingWarehouseItemDao));

        public List<DispatchingWarehouseItem> getBy(string dispatchingWarehouseId) 
        {
            List<DispatchingWarehouseItem> list = new List<DispatchingWarehouseItem>();
            string sql = "select id, createTime, updateTime, CASH_DATE, DISPATCHINGWAREHOUSE_ID, GOODS_NAME, GOODS_PRICE, GOODS_SPECIFICATIONS, SERIALNUMBER, "
                + " TOTAL_MONEY, WEIGHT, BRANCH_TOTAL_ID, GOODSFILE_ID, NUMS from zc_dispatching_Warehouse_items where 1=1 and DISPATCHINGWAREHOUSE_ID = '"+dispatchingWarehouseId+"'";
            OracleConnection conn = null;
            OracleCommand cmd = new OracleCommand();
            OracleDataReader reader = null;
            try
            {
                conn = OracleUtil.OpenConn();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    DispatchingWarehouseItem obj = new DispatchingWarehouseItem();
                    obj.id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.createTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.updateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.cash_date = reader.IsDBNull(3) ? default(DateTime) : reader.GetDateTime(3);
                    obj.dispatchingWarehouseId = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.goods_name = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    obj.goodsPrice = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.goods_specifications = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    obj.serialNumber = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                    obj.money = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                    obj.weight = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                    obj.branch_total_id = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                    obj.goodsFile_id = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                    obj.nums = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                log.Error("查询当天的配送出库单明细失败", ex);
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
