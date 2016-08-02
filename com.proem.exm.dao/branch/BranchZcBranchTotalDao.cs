using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using MySql.Data.MySqlClient;

namespace Branch.com.proem.exm.dao.branch
{
    public class BranchZcBranchTotalDao : MysqlDBHelper
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(BranchZcBranchTotalDao));

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="list"></param>
        public void AddZcBranchTotal(List<ZcBranchTotal> list)
        {
            string sql = "insert into ZC_BRANCH_TOTAL values (@id,@createTime,@updateTime,@code,@name,@flag, @money,@customerId,@zoningId)";
            MySqlConnection conn = null;
            MySqlTransaction tran = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                foreach(ZcBranchTotal obj in list)
                {
                    cmd.Parameters.Add(new MySqlParameter("@id", obj.Id));
                    cmd.Parameters.Add(new MySqlParameter("@createTime", obj.CreateTime));
                    cmd.Parameters.Add(new MySqlParameter("@updateTime", obj.UpdateTime));
                    cmd.Parameters.Add(new MySqlParameter("@code", obj.BranchCode));
                    cmd.Parameters.Add(new MySqlParameter("@name", obj.BranchName));
                    cmd.Parameters.Add(new MySqlParameter("@flag", obj.DelFlag));
                    cmd.Parameters.AddWithValue("@money", obj.Money);
                    cmd.Parameters.Add(new MySqlParameter("@customerId", obj.CustomerId));
                    cmd.Parameters.Add(new MySqlParameter("@zoningId", obj.ZoningId));
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("新增zcBranchTotal发生异常", ex);
            }
            finally
            {
                cmd.Dispose();
                tran.Dispose();
                CloseConnection(conn);
            }
        }


        public ZcBranchTotal FindById(string p)
        {
            ZcBranchTotal obj = new ZcBranchTotal();
            string sql = "select id,createTime, updateTime,branch_code, branch_name, DELFLAG, money, CUSTOMER_ID, ZONING_ID  from zc_branch_total where 1=1 and id='" + p + "'";
            MySqlConnection conn = null;
            MySqlDataReader reader = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = GetConnection();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                reader = cmd.ExecuteReader();
                if(reader.Read()){
                    obj.Id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.CreateTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.UpdateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.BranchCode = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    obj.BranchName = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.DelFlag = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    obj.Money = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.CustomerId = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    obj.ZoningId = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                }
            }
            catch (Exception ex)
            {
                log.Error("根据id查询分店信息失败", ex);
            }
            finally
            {
                if(reader != null){
                    reader.Close();
                }
                cmd.Dispose();
                CloseConnection(conn);
            }
            return obj;
        }
    }
}
