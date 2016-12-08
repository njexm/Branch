using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.util;
using System;
using System.Collections.Generic;
using log4net;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Branch.com.proem.exm.dao.branch
{
    public class BranchZcClassifyInfoDao : MysqlDBHelper
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(BranchZcClassifyInfoDao));

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="list"></param>
        public void AddZcClassifyInfo(List<ZcClassIfyInfo> list)
        {
            string sql = "insert into zc_classify_info values (@id,@createTime,@updateTime,@child,@code,@level,@name,@type,@commission,@flag,@index,@parentId,@path,@typeId)";
            MySqlConnection conn = null;
            MySqlTransaction tran = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                foreach(ZcClassIfyInfo obj in list)
                {
                    cmd.Parameters.Add(new MySqlParameter("@id", obj.Id));
                    cmd.Parameters.Add(new MySqlParameter("@createTime", obj.CreateTime));
                    cmd.Parameters.Add(new MySqlParameter("@updateTime", obj.UpdateTime));
                    cmd.Parameters.Add(new MySqlParameter("@child", obj.ChildCount));
                    cmd.Parameters.Add(new MySqlParameter("@code", obj.ClassifyCode));
                    cmd.Parameters.Add(new MySqlParameter("@level", obj.ClassifyLevel));
                    cmd.Parameters.Add(new MySqlParameter("@name", obj.ClassifyName));
                    cmd.Parameters.Add(new MySqlParameter("@type", obj.ClassifyType));
                    cmd.Parameters.Add(new MySqlParameter("@commission", obj.Commission));
                    cmd.Parameters.Add(new MySqlParameter("@flag", obj.DelFlag));
                    cmd.Parameters.Add(new MySqlParameter("@index", obj.OrderIndex));
                    cmd.Parameters.Add(new MySqlParameter("@parentId", obj.ParentId));
                    cmd.Parameters.Add(new MySqlParameter("@path", obj.ParentPath));
                    cmd.Parameters.Add(new MySqlParameter("@typeId", obj.TypeId));
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("新增zcClassIfyInfo发生异常", ex);
            }
            finally
            {
                cmd.Dispose();
                tran.Dispose();
                CloseConnection(conn);
            }
        }


        public ZcClassIfyInfo getById(string p)
        {
            ZcClassIfyInfo obj = null;
            string sql = "select ID,CREATETIME,UPDATETIME,CHILDCOUNT,CLASSIFY_CODE,CLASSIFY_LEVEL, "
                    + " CLASSIFY_NAME, CLASSIFY_TYPE, COMMISSION, DELFLAG, ORDER_INDEX, PARENTID, PARENTPATH, TYPEID  from zc_classify_info where id='"+p+"'";
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
                    obj = new ZcClassIfyInfo();
                    obj.Id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.CreateTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.UpdateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.ChildCount = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    obj.ClassifyCode = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.ClassifyLevel = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    obj.ClassifyName = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.ClassifyType = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    obj.Commission = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                    obj.DelFlag = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                    obj.OrderIndex = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                    obj.ParentId = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                    obj.ParentPath = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                    obj.TypeId = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                }
            }
            catch (Exception ex)
            {
                log.Error("根据id获取分类信息失败", ex);
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
            return obj;
        }
    }
}
