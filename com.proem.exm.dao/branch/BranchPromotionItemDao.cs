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
    public class BranchPromotionItemDao : MysqlDBHelper
    {
        private readonly ILog log = LogManager.GetLogger(typeof(BranchPromotionItemDao));

        public void addPromotionItem(List<PromotionItem> list) 
        {
            string sql = "insert into Zc_SalesPromotionItem (id, createTime, updateTime, add_money, add_more_money, all_discount, bargain_price, begin_time_frame, "
                + "discount, end_time_frame, free_goodsIds, full_buy_count, full_buy_money, group_number, limit_number, nums, reduce_money, "
                + "salespromotion_id, brand_classify_id, class_classify_id, free_goodsId, goodsFile_id) values (@id, @createTime, @updateTime, @add_money, @add_more_money, @all_discount, @bargain_price, @begin_time_frame, "
                + "@discount, @end_time_frame, @free_goodsIds, @full_buy_count, @full_buy_money, @group_number, @limit_number, @nums, @reduce_money, "
                + "@salespromotion_id, @brand_classify_id, @class_classify_id, @free_goodsId, @goodsFile_id) ";
            MySqlConnection conn = null;
            MySqlCommand cmd = new MySqlCommand();
            MySqlTransaction tran = null;
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                for (int i = 0; i < list.Count; i++)
                {
                    PromotionItem obj = list[i];
                    cmd.Parameters.AddWithValue("@id", obj.id);
                    cmd.Parameters.AddWithValue("@createTime", obj.createTime);
                    cmd.Parameters.AddWithValue("@updateTime", obj.updateTime);
                    cmd.Parameters.AddWithValue("@add_money", obj.add_money);
                    cmd.Parameters.AddWithValue("@add_more_money", obj.add_more_money);
                    cmd.Parameters.AddWithValue("@all_discount", obj.all_discount);
                    cmd.Parameters.AddWithValue("@bargain_price", obj.bargain_price);
                    cmd.Parameters.AddWithValue("@begin_time_frame", obj.begin_time_frame);
                    cmd.Parameters.AddWithValue("@discount", obj.discount);
                    cmd.Parameters.AddWithValue("@end_time_frame", obj.end_time_frame);
                    cmd.Parameters.AddWithValue("@free_goodsIds", obj.free_goodsIds);
                    cmd.Parameters.AddWithValue("@full_buy_count", obj.full_buy_count);
                    cmd.Parameters.AddWithValue("@full_buy_money", obj.full_buy_money);
                    cmd.Parameters.AddWithValue("@group_number", obj.group_number);
                    cmd.Parameters.AddWithValue("@limit_number", obj.limit_number);
                    cmd.Parameters.AddWithValue("@nums", obj.nums);
                    cmd.Parameters.AddWithValue("@reduce_money", obj.reduce_money);
                    cmd.Parameters.AddWithValue("@salespromotion_id", obj.salespromotion_id);
                    cmd.Parameters.AddWithValue("@brand_classify_id", obj.brand_classify_id);
                    cmd.Parameters.AddWithValue("@class_classify_id", obj.class_classify_id);
                    cmd.Parameters.AddWithValue("@free_goodsId", obj.free_goodsId);
                    cmd.Parameters.AddWithValue("@goodsFile_id", obj.goodsFile_id);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("新增促销明细错误", ex);
            }
            finally
            {
                cmd.Dispose();
                CloseConnection(conn);
            }
        }



        public void deleteByPromotionId(string p)
        {
            string sql = "delete from Zc_SalesPromotionItem where SALESPROMOTION_ID = '"+p+"'";
             MySqlConnection conn = null;
            MySqlCommand cmd = new MySqlCommand();
            MySqlTransaction tran = null;
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("根据促销主表删除明细失败", ex);
            }
            finally
            {
                cmd.Dispose();
                CloseConnection(conn);
            }
        }

        public List<PromotionItem> FindBy(string p, string goodsFileId)
        {
            List<PromotionItem> list = new List<PromotionItem>();
            string sql = "select id, createTime, updateTime, add_money, add_more_money, all_discount, bargain_price, begin_time_frame, "
                + "discount, end_time_frame, free_goodsIds, full_buy_count, full_buy_money, group_number, limit_number, nums, reduce_money, "
                + "salespromotion_id, brand_classify_id, class_classify_id, free_goodsId, goodsFile_id from Zc_SalesPromotionItem where 1=1 and salespromotion_id='" + p + "' and goodsFile_id = '" + goodsFileId + "' ";
            MySqlConnection conn = null;
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader = null;
            try
            {
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                while(reader.Read()){
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
                log.Error("获取促销明细失败", ex);
            }
            finally
            {
                cmd.Dispose();
                if(reader != null){
                    reader.Close();
                }
                if(conn != null){
                    conn.Close();
                }
            }
            return list;
        }

        public List<PromotionItem> FindByPromotionId(string p)
        {
            List<PromotionItem> list = new List<PromotionItem>();
            string sql = "select id, createTime, updateTime, add_money, add_more_money, all_discount, bargain_price, begin_time_frame, "
                + "discount, end_time_frame, free_goodsIds, full_buy_count, full_buy_money, group_number, limit_number, nums, reduce_money, "
                + "salespromotion_id, brand_classify_id, class_classify_id, free_goodsId, goodsFile_id from Zc_SalesPromotionItem where 1=1 and salespromotion_id='" + p + "' ";
            MySqlConnection conn = null;
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader = null;
            try
            {
                conn = GetConnection();
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
                log.Error("获取促销明细失败", ex);
            }
            finally
            {
                cmd.Dispose();
                if (reader != null)
                {
                    reader.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return list;
        }
    }
}
