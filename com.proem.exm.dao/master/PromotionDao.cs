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
    public class PromotionDao
    {
        private readonly ILog log = LogManager.GetLogger(typeof(PromotionDao));

        public List<Promotion> getUpdateBy(DateTime updateTime) 
        {
            List<Promotion> list = new List<Promotion>();
            string sql = "select id, createTime, updateTime, branchIds, check_date, check_state, check_man ,member_level, promotion_begin_date, "
                + "promotion_days, promotion_end_date,promotion_number, promotion_remark, promotion_title, stop_date, STOP_MAN, ZCCODE_MODEID, ZCCODE_SCOPEID, create_man "
                + " from Zc_SalesPromotion where updateTime >=to_date('"+updateTime.ToString("yyyy-MM-dd HH:mm:ss")+"', 'yyyy-MM-dd HH24:mi:ss') and branchIds like '%"+LoginUserInfo.street+"%'";
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
                    Promotion obj = new Promotion();
                    obj.id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.createTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.updateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.branchIds = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    obj.check_date = reader.IsDBNull(4) ? default(DateTime) : reader.GetDateTime(4);
                    obj.check_state = reader.IsDBNull(5) ? default(int) : reader.GetInt32(5);
                    obj.check_man = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.member_level = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    obj.promotionBeginDate = reader.IsDBNull(8) ? default(DateTime) : reader.GetDateTime(8);
                    obj.promotion_days = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                    obj.promotionEndDate = reader.IsDBNull(10) ? default(DateTime) : reader.GetDateTime(10);
                    obj.promotion_number = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                    obj.promotion_remark = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                    obj.promotion_title = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                    obj.stop_date = reader.IsDBNull(14) ? default(DateTime) : reader.GetDateTime(14);
                    obj.stop_man = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                    obj.zccode_modeId = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                    obj.zccode_scopeId = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                    obj.create_man = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                log.Error("获取上次数据拉取后的促销方案", ex);
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
