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
    /// 会员数据操作类
    /// </summary>
    public class BranchZcMemberDao : MysqlDBHelper 
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(BranchZcMemberDao));

        public long getCountById(string p)
        {
            long count = 0;
            string sql = "select count(1) from zc_member where id = '"+p+"'";
            MySqlConnection conn = null;
            MySqlDataReader reader = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                if(reader.Read()){
                    count = reader.IsDBNull(0) ? default(long) : reader.GetInt64(0);
                }
            }
            catch (Exception ex)
            {
                log.Error("根据id获取数据记录数", ex);
            }
            finally
            {
                cmd.Dispose();
                if(reader != null){
                    reader.Close();
                }
                if(conn != null){
                    CloseConnection(conn);
                }
            }
            return count;
        }

        public void deleteById(string p)
        {
            string sql = "delete from zc_member where id = '" + p + "'";
            MySqlConnection conn = null;
            MySqlTransaction tran = null;
            MySqlCommand cmd = new MySqlCommand();
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
                log.Error("根据id获取删除记录", ex);
            }
            finally
            {
                cmd.Dispose();
                if (conn != null)
                {
                    CloseConnection(conn);
                }
            }
        }

        public void AddZcMember(List<domain.ZcMember> list)
        {
            string sql = "insert into zc_member (id, CREATETIME, UPDATETIME, COSTTIME, LASTCOSTDATE, LEFTSCORE, MEBERSEX, MEMBERADDRESS, MEMBERBIRTHDAY, MEMBERBYBRANCH, MEMBEREMAIL, MEMBERIDFINE, MEMBERLEVEL, MEMBERMOBILE, MEMBERNAME, MEMBERPHONE, SCOREDATE, TOTALCOST, TOTALSCORE, TURENAME, USEDSCORE) values "
                +" (@id, @CREATETIME, @UPDATETIME, @COSTTIME, @LASTCOSTDATE, @LEFTSCORE, @MEBERSEX, @MEMBERADDRESS, @MEMBERBIRTHDAY, @MEMBERBYBRANCH, @MEMBEREMAIL, @MEMBERIDFINE, @MEMBERLEVEL, @MEMBERMOBILE, @MEMBERNAME, @MEMBERPHONE, @SCOREDATE, @TOTALCOST, @TOTALSCORE, @TURENAME, @USEDSCORE)";
            MySqlConnection conn = null;
            MySqlTransaction tran = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                for (int i = 0; i < list.Count; i++ )
                {
                    ZcMember obj = list[i];
                    cmd.Parameters.AddWithValue("@id", obj.id);
                    cmd.Parameters.AddWithValue("@CREATETIME", obj.createTime);
                    cmd.Parameters.AddWithValue("@UPDATETIME", obj.updateTime);
                    cmd.Parameters.AddWithValue("@COSTTIME", obj.costTime);
                    cmd.Parameters.AddWithValue("@LASTCOSTDATE", obj.lastCostDate);
                    cmd.Parameters.AddWithValue("@LEFTSCORE", obj.leftScore);
                    cmd.Parameters.AddWithValue("@MEBERSEX", obj.memberSex);
                    cmd.Parameters.AddWithValue("@MEMBERADDRESS", obj.memberAddress);
                    cmd.Parameters.AddWithValue("@MEMBERBIRTHDAY", obj.memberBirthDay);
                    cmd.Parameters.AddWithValue("@MEMBERBYBRANCH", obj.memberBranch);
                    cmd.Parameters.AddWithValue("@MEMBEREMAIL", obj.memberEmail);
                    cmd.Parameters.AddWithValue("@MEMBERIDFINE", obj.memberIdfine);
                    cmd.Parameters.AddWithValue("@MEMBERLEVEL", obj.memberLevel);
                    cmd.Parameters.AddWithValue("@MEMBERMOBILE", obj.memberMobile);
                    cmd.Parameters.AddWithValue("@MEMBERNAME", obj.memberName);
                    cmd.Parameters.AddWithValue("@MEMBERPHONE", obj.memberPhone);
                    cmd.Parameters.AddWithValue("@SCOREDATE", obj.scoreDate);
                    cmd.Parameters.AddWithValue("@TOTALCOST", obj.totalCost);
                    cmd.Parameters.AddWithValue("@TOTALSCORE", obj.totalScore);
                    cmd.Parameters.AddWithValue("@TURENAME", obj.tureName);
                    cmd.Parameters.AddWithValue("@USEDSCORE", obj.usedScore);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("批量插入会员信息", ex);
            }
            finally
            {
                cmd.Dispose();
                if (conn != null)
                {
                    CloseConnection(conn);
                }
            }
        }

        public ZcMember FindById(string p)
        {
            ZcMember obj = null;
            string sql = "select id, CREATETIME, UPDATETIME, COSTTIME, LASTCOSTDATE, LEFTSCORE, MEBERSEX, MEMBERADDRESS, MEMBERBIRTHDAY, MEMBERBYBRANCH, MEMBEREMAIL, MEMBERIDFINE, MEMBERLEVEL, MEMBERMOBILE, MEMBERNAME, MEMBERPHONE, SCOREDATE, TOTALCOST, TOTALSCORE, TURENAME, USEDSCORE from zc_member where id = '"+p+"'";
            MySqlConnection conn = null;
            MySqlDataReader reader = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    obj = new ZcMember();
                    obj.id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.createTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.updateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.costTime = reader.IsDBNull(3) ? "0" : reader.GetString(3);
                    obj.lastCostDate = reader.IsDBNull(4) ? default(DateTime) : reader.GetDateTime(4);
                    obj.leftScore = reader.IsDBNull(5) ? "0" : reader.GetString(5);
                    obj.memberSex = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.memberAddress = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    obj.memberBirthDay = reader.IsDBNull(8) ? default(DateTime) : reader.GetDateTime(8);
                    obj.memberBranch = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                    obj.memberEmail = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                    obj.memberIdfine = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                    obj.memberLevel = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                    obj.memberMobile = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                    obj.memberName = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                    obj.memberPhone = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                    obj.scoreDate = reader.IsDBNull(16) ? default(DateTime) : reader.GetDateTime(16);
                    obj.totalCost = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                    obj.totalScore = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                    obj.tureName = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                    obj.usedScore = reader.IsDBNull(20) ? string.Empty : reader.GetString(20);
                }
            }
            catch (Exception ex)
            {
                log.Error("获取会员表数据", ex);
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
                    CloseConnection(conn);
                }
            }
            return obj;
        }
    }
}
