using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.util;
using Oracle.ManagedDataAccess.Client;
using Branch.com.proem.exm.dao.branch;

namespace Branch.com.proem.exm.dao.master
{
    public class PayInfoDao
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(PayInfoDao));

        /// <summary>
        /// 添加付款信息
        /// </summary>
        /// <param name="payinfo"></param>
        public void AddPayInfo(PayInfo obj)
        {
            OracleConnection conn = null;
            OracleTransaction tran = null;
            OracleCommand cmd = new OracleCommand();
            string sql = "insert into zc_payinfo (id, createTime, updateTime, member_id, money, branch_id, saleman_id) "
                + " values (:id, :createTime, :updateTime, :member_id, :money, :branch_id, :saleman_id)";
            try
            {
                conn = OracleUtil.OpenConn();
                tran = conn.BeginTransaction();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.Parameters.Add(":id", obj.Id);
                cmd.Parameters.Add(":createTime", obj.CreateTime);
                cmd.Parameters.Add(":updateTime", obj.UpdateTime);
                cmd.Parameters.Add(":member_id", obj.MemberId);
                cmd.Parameters.Add(":money", obj.Money);
                cmd.Parameters.Add(":branch_id", obj.BranchId);
                cmd.Parameters.Add(":saleman_id", obj.salesmanId);
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UploadDao uploadDao = new UploadDao();
                UploadInfo uploadInfo = new UploadInfo();
                uploadInfo.Id = obj.Id;
                uploadInfo.CreateTime = DateTime.Now;
                uploadInfo.UpdateTime = DateTime.Now;
                uploadInfo.Type = Constant.PAY_INFO;
                uploadDao.AddUploadInfo(uploadInfo);
                log.Error("添加付款信息发生异常", ex);
            }
            finally
            {
                tran.Dispose();
                cmd.Dispose();
                OracleUtil.CloseConn(conn);
            }
        }

        public bool AddPayInfoI(PayInfo obj)
        {
            bool flag = false;
            OracleConnection conn = null;
            OracleTransaction tran = null;
            OracleCommand cmd = new OracleCommand();
            string sql = "insert into zc_payinfo (id, createTime, updateTime, member_id, money, branch_id, saleman_id) "
                + " values (:id, :createTime, :updateTime, :member_id, :money, :branch_id, :saleman_id)";
            try
            {
                conn = OracleUtil.OpenConn();
                tran = conn.BeginTransaction();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.Parameters.Add(":id", obj.Id);
                cmd.Parameters.Add(":createTime", obj.CreateTime);
                cmd.Parameters.Add(":updateTime", obj.UpdateTime);
                cmd.Parameters.Add(":member_id", obj.MemberId);
                cmd.Parameters.Add(":money", obj.Money);
                cmd.Parameters.Add(":branch_id", obj.BranchId);
                cmd.Parameters.Add(":saleman_id", obj.salesmanId);
                cmd.ExecuteNonQuery();
                tran.Commit();
                flag = true;
            }
            catch (Exception ex)
            {
                tran.Rollback();

                flag = false;
                log.Error("添加付款信息发生异常", ex);
            }
            finally
            {
                tran.Dispose();
                cmd.Dispose();
                OracleUtil.CloseConn(conn);
            }
            return flag;
        }
    }
}
