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
    public class BranchPromotionDao  : MysqlDBHelper
    {
        private readonly ILog log = LogManager.GetLogger(typeof(BranchPromotionDao));

        public void addPromotion(List<Promotion> list)
        {
            string sql = "insert into Zc_SalesPromotion (id, createTime, updateTime, branchIds, check_date,check_man, check_state,create_man , member_level, promotion_begin_date, "
                + "promotion_days, promotion_end_date,promotion_number, promotion_remark, promotion_title, stop_date, STOP_MAN, ZCCODE_MODEID, ZCCODE_SCOPEID) "
                + " values (@id, @createTime, @updateTime, @branchIds, @check_date,@check_man, @check_state, @create_man, @member_level, @promotion_begin_date, "
                + "@promotion_days, @promotion_end_date,@promotion_number, @promotion_remark, @promotion_title, @stop_date, @STOP_MAN, @ZCCODE_MODEID, @ZCCODE_SCOPEID)";
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
                    Promotion obj = list[i];
                    cmd.Parameters.AddWithValue("@id", obj.id);
                    cmd.Parameters.AddWithValue("@createTime", obj.createTime);
                    cmd.Parameters.AddWithValue("@updateTime", obj.updateTime);
                    cmd.Parameters.AddWithValue("@branchIds", obj.branchIds);
                    cmd.Parameters.AddWithValue("@check_date", obj.check_date);
                    cmd.Parameters.AddWithValue("@check_man", obj.check_man);
                    cmd.Parameters.AddWithValue("@check_state", obj.check_state);
                    cmd.Parameters.AddWithValue("@create_man", obj.create_man);
                    cmd.Parameters.AddWithValue("@member_level", obj.member_level);
                    cmd.Parameters.AddWithValue("@promotion_begin_date", obj.promotionBeginDate);
                    cmd.Parameters.AddWithValue("@promotion_days", obj.promotion_days);
                    cmd.Parameters.AddWithValue("@promotion_end_date", obj.promotionEndDate);
                    cmd.Parameters.AddWithValue("@promotion_number", obj.promotion_number);
                    cmd.Parameters.AddWithValue("@promotion_remark", obj.promotion_remark);
                    cmd.Parameters.AddWithValue("@promotion_title", obj.promotion_title);
                    cmd.Parameters.AddWithValue("@stop_date", obj.stop_date);
                    cmd.Parameters.AddWithValue("@STOP_MAN", obj.stop_man);
                    cmd.Parameters.AddWithValue("@ZCCODE_MODEID", obj.zccode_modeId);
                    cmd.Parameters.AddWithValue("@ZCCODE_SCOPEID", obj.zccode_scopeId);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("新增数据字典错误", ex);
            }
            finally
            {
                cmd.Dispose();
                CloseConnection(conn);
            }
        }

        public int getCountBy(string id)
        {
            int count = 0;
            MySqlConnection conn = null;
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader = null;
            string sql = "select count(1) from Zc_SalesPromotion where id = '"+id+"'";
            try
            {
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                if(reader.Read()){
                    count = reader.IsDBNull(0) ? default(int) : reader.GetInt32(0);
                }
            }
            catch (Exception ex)
            {
                log.Error("根据id获取记录数", ex);
            }
            finally
            {
                if(reader != null){
                    reader.Close();
                }
                cmd.Dispose();
                CloseConnection(conn);
            }
            return count;
        }

        public void deleteById(string p)
        {
            string sql = "delete from Zc_SalesPromotion where id = '"+p+"'";
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
                log.Error("根据id删除促销主表失败", ex);
            }
            finally
            {
                cmd.Dispose();
                CloseConnection(conn);
            }
        }


        public List<Promotion> queryListBy(string dayOfWeek, ZcMember zcMember)
        {
            List<Promotion> list = new List<Promotion>();
            string sql = "select id, createTime, updateTime, branchIds, check_date,check_man, check_state,create_man , member_level, promotion_begin_date, "
                + " promotion_days, promotion_end_date,promotion_number, promotion_remark, promotion_title, stop_date, STOP_MAN, ZCCODE_MODEID, ZCCODE_SCOPEID "
                + " from Zc_SalesPromotion where 1=1 and check_state ='2' and branchIds like '%" + LoginUserInfo.street + "%' and promotion_days like '%"+dayOfWeek+"%' "
                + " and promotion_begin_date <= @beginDate and promotion_end_date >= @endDate ";
            if(zcMember != null && !string.IsNullOrEmpty(zcMember.memberLevel)){
                sql += " and member_level = '"+zcMember.memberLevel+"'";
            }
            MySqlConnection conn = null;
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader = null;
            try
            {
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@beginDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@endDate", DateTime.Now);
                reader = cmd.ExecuteReader();
                while(reader.Read()){
                    Promotion obj = new Promotion();
                    obj.id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.createTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.updateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.branchIds = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    obj.check_date = reader.IsDBNull(4) ? default(DateTime) : reader.GetDateTime(4);
                    obj.check_man = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    obj.check_state = reader.IsDBNull(6) ? default(int) : reader.GetInt32(6);
                    obj.create_man = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    obj.member_level = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                    obj.promotionBeginDate = reader.IsDBNull(9) ? default(DateTime) : reader.GetDateTime(9);
                    obj.promotion_days = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                    obj.promotionEndDate = reader.IsDBNull(11) ? default(DateTime) : reader.GetDateTime(11);
                    obj.promotion_number = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                    obj.promotion_remark = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                    obj.promotion_title = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                    obj.stop_date = reader.IsDBNull(15) ? default(DateTime) : reader.GetDateTime(15);
                    obj.stop_man = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                    obj.zccode_modeId = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                    obj.zccode_scopeId = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                    
                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                log.Error("根据条件查询促销信息失败", ex);
            }
            finally
            { 
                
                if(reader != null){
                    reader.Close();
                }
                if(conn != null){
                    CloseConnection(conn);
                }
            }
            return list;
        }
    }
}
