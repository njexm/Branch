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
    /// <summary>
    /// 数据字典
    /// </summary>
    public class CodeDao
    {
        private readonly ILog log = LogManager.GetLogger(typeof(CodeDao));

        public List<ZcCode> getAll() {
            List<ZcCode> list = new List<ZcCode>();
            string sql = "select id, createTime, updateTime, CODEDESCRIPTION, CODENAME, CODETYPE, CODEVALUE, PARENT, STATE from zc_code ";
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
                    ZcCode obj = new ZcCode();
                    obj.id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.createTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.updateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.codeDescription = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    obj.codeName = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.codeType = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    obj.codeValue = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.parent = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    obj.state = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
                log.Error("获取全部数据字典信息失败", ex);
            }
            finally
            { 
                if(reader!= null){
                    reader.Close();
                }
                cmd.Dispose();
                if(conn != null){
                    conn.Close();
                }
            }
            return list;
        }

    }
}
