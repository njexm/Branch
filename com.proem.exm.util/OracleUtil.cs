using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using log4net;

namespace Branch.com.proem.exm.util
{
    class OracleUtil
    {

        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(OracleUtil));

        /// <summary>
        /// open 一个 conn
        /// </summary>
        /// <returns></returns>
        public static OracleConnection OpenConn()
        {
            OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleDB"].ConnectionString);
             conn.Open();
             return conn;
         }

        /// <summary>
        /// 关闭conn 释放资源
        /// </summary>
        /// <param name="conn"></param>
        public static void CloseConn(OracleConnection conn)
         {
             if (conn == null) { return; }
             try
             {
                 if (conn.State != ConnectionState.Closed)
                 {
                     conn.Close();
                 }
             }
             catch (Exception e)
             {
                 Console.WriteLine(e.Message);
             }
             finally
             {
                 conn.Dispose();
             }
         }

        /// <summary>
        /// 返回一个DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql, string table)
        {
            DataSet ds = new DataSet();
            OracleConnection conn = null;
            OracleCommand cmd = null;
            OracleDataAdapter da = null;
            try
            {
                conn = OpenConn();
                cmd = new OracleCommand(sql, conn);
                da = new OracleDataAdapter(cmd);
                da.Fill(ds, table);
            }
            catch (Exception ex)
            {
                log.Error("获取" + table + "dataset异常", ex);
            }
            finally
            {
                da.Dispose();
                cmd.Dispose();
                CloseConn(conn);
            }
            return ds;
        }
        
    }
}
