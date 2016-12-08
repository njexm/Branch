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
    public class ZcMemberDao
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(ZcMemberDao));

        public List<ZcMember> FindUpdateBy(DateTime updateTime)
        {
            List<ZcMember> list = new List<ZcMember>();
            string sql = "select id, CREATETIME, UPDATETIME, COSTTIME, LASTCOSTDATE, LEFTSCORE, MEBERSEX, MEMBERADDRESS, MEMBERBIRTHDAY, MEMBERBYBRANCH, MEMBEREMAIL, MEMBERIDFINE, MEMBERLEVEL, MEMBERMOBILE, MEMBERNAME, MEMBERPHONE, SCOREDATE, TOTALCOST, TOTALSCORE, TURENAME, USEDSCORE from zc_member where updateTime > :lastUpdateTime";
            OracleConnection conn = null;
            OracleDataReader reader = null;
            OracleCommand cmd = new OracleCommand();
            try
            {
                conn = OracleUtil.OpenConn();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.Add(":lastUpdateTime", updateTime);
                reader = cmd.ExecuteReader();
                while(reader.Read()){
                    ZcMember obj = new ZcMember();
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
                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                log.Error("获取会员表更新数据失败", ex);
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
    }
}
