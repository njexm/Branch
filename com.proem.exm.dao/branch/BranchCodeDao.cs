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
    /// 数据字典分店数据库操作类
    /// </summary>
    public class BranchCodeDao : MysqlDBHelper
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(BranchCodeDao));

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="list"></param>
        public void addCode(List<ZcCode> list) 
        {
            string sql = "insert into zc_code (id, createTime, updateTime, codeDescription, codeName, codeType, codeValue, parent, state) "
                + " values (@id, @createTime, @updateTime, @codeDescription, @codeName, @codeType, @codeValue, @parent, @state)";
            MySqlConnection conn = null;
            MySqlCommand cmd = new MySqlCommand();
            MySqlTransaction tran = null;
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                for (int i = 0; i < list.Count; i++ )
                {
                    ZcCode obj = list[i];
                    cmd.Parameters.AddWithValue("@id", obj.id);
                    cmd.Parameters.AddWithValue("@createTime", obj.createTime);
                    cmd.Parameters.AddWithValue("@updateTime", obj.updateTime);
                    cmd.Parameters.AddWithValue("@codeDescription", obj.codeDescription);
                    cmd.Parameters.AddWithValue("@codeName", obj.codeName);
                    cmd.Parameters.AddWithValue("@codeType", obj.codeType);
                    cmd.Parameters.AddWithValue("@codeValue", obj.codeValue);
                    cmd.Parameters.AddWithValue("@parent", obj.parent);
                    cmd.Parameters.AddWithValue("@state", obj.state);
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

        /// <summary>
        /// 根据id获取数据字典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ZcCode FindById(string id) {
            ZcCode obj = null;
            string sql = "select id, createTime, updateTime, codeDescription, codeName, codeType, codeValue, parent, state from zc_code where id = '"+id+"'";
            MySqlConnection conn = null;
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader = null;
            try
            {
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                if(reader.Read()){
                    obj = new ZcCode();
                    obj.id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.createTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.updateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.codeDescription = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    obj.codeName = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.codeType = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    obj.codeValue = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.parent = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    obj.state = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                }
            }
            catch (Exception ex)
            {
                log.Error("根据id获取数据字典", ex);
            }
            finally
            {
                cmd.Dispose();
                if(reader!= null){
                    reader.Close();
                }
                if(conn != null){
                    conn.Close();
                }
            }
            return obj;
        }
    }
}
