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
    public class PromotionItemDao
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(PromotionItemDao));

        public List<PromotionItem> getUpdateBy(DateTime updateTime)
        {
            List<PromotionItem> list = new List<PromotionItem>();
            string sql = "select id, createTime, updateTime, add_money, add_more_money, all_discount, bargain_price, begin_time_frame, "
                + "discount, end_time_frame, free_goodsIds, full_buy_count, full_buy_money, group_number, limit_number, nums, reduce_money, "
                + "salespromotion_id, brand_classify_id, class_classify_id, free_goodsId, goodsFile_id from Zc_SalesPromotionItem where 1=1 "
                + " and salespromotion_id in (select id from Zc_SalesPromotion where updateTime >= to_date('"+updateTime.ToString("yyyy-MM-dd HH:mm:ss")+"', 'yyyy-MM-dd HH24:mi:ss'))";
            OracleConnection conn = null;
            OracleCommand cmd = new OracleCommand();
            OracleDataReader reader = null;
            try
            {
                conn = OracleUtil.OpenConn();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PromotionItem obj = new PromotionItem();
                    obj.id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.createTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.updateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.add_money = reader.IsDBNull(3) ? default(float) : reader.GetFloat(3);
                    obj.add_more_money = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.all_discount = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    obj.bargain_price = reader.IsDBNull(6) ? default(float) : reader.GetFloat(6);
                    obj.begin_time_frame = reader.IsDBNull(7) ? default(DateTime) : reader.GetDateTime(7);
                    obj.discount = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                    obj.end_time_frame = reader.IsDBNull(9) ? default(DateTime) : reader.GetDateTime(9);
                    obj.free_goodsIds = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                    obj.full_buy_count = reader.IsDBNull(11) ? default(float) : reader.GetFloat(11);
                    obj.full_buy_money = reader.IsDBNull(12) ? default(float) : reader.GetFloat(12);
                    obj.group_number = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                    obj.limit_number = reader.IsDBNull(14) ? default(float) : reader.GetFloat(14);
                    obj.nums = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                    obj.reduce_money = reader.IsDBNull(16) ? default(float) : reader.GetFloat(16);
                    obj.salespromotion_id = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                    obj.brand_classify_id = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                    obj.class_classify_id = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                    obj.free_goodsId = reader.IsDBNull(20) ? string.Empty : reader.GetString(20);
                    obj.goodsFile_id = reader.IsDBNull(21) ? string.Empty : reader.GetString(21);
                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                log.Error("获取上次数据拉取后的促销方案明细", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                cmd.Dispose();
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return list;
        }
    }
}
