using Branch.com.proem.exm.dao.branch;
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
    /// 支付明细表
    /// </summary>
    public class PayInfoItemDao
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(PayInfoItemDao));

        /// <summary>
        /// 上传支付明细
        /// </summary>
        /// <param name="list"></param>
        public void AddPayInfoItem(List<PayInfoItem > list)
        {
            string sql = "insert into zc_payinfo_item (id, createTime, updateTime, payInfo_id, pay_mode, money) "
                + " values(:id, :createTime, :updateTime, :payInfo_id, :pay_mode, :money)";
            OracleConnection conn = null;
            OracleTransaction tran = null;
            OracleCommand cmd = new OracleCommand();
            try
            {
                conn = OracleUtil.OpenConn();
                tran = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                foreach(PayInfoItem obj in list)
                {
                    cmd.Parameters.Add(":id", obj.Id);
                    cmd.Parameters.Add(":createTime", obj.CreateTime);
                    cmd.Parameters.Add(":updateTime", obj.UpdateTime);
                    cmd.Parameters.Add(":payInfo_id", obj.PayInfoId);
                    cmd.Parameters.Add(":pay_mode", obj.PayMode);
                    cmd.Parameters.Add(":money", obj.Money);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UploadDao uploadDao = new UploadDao();
                List<UploadInfo> uploadList = new List<UploadInfo>();
                foreach (PayInfoItem obj in list)
                {
                    UploadInfo uploadInfo = new UploadInfo();
                    uploadInfo.Id = obj.Id;
                    uploadInfo.CreateTime = DateTime.Now;
                    uploadInfo.UpdateTime = DateTime.Now;
                    uploadInfo.Type = Constant.PAY_INFO_ITEM;
                    uploadList.Add(uploadInfo);
                }
                uploadDao.AddUploadInfo(uploadList);
                log.Error("上传支付明细失败", ex);
            }
            finally
            {
                OracleUtil.CloseConn(conn);
            }
        }

        /// <summary>
        /// 上传付款明细
        /// 重载
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool AddPayInfoItemI(PayInfoItem obj)
        {
            bool flag = false;
            OracleConnection conn = null;
            OracleTransaction tran = null;
            OracleCommand cmd = new OracleCommand();
            string sql = "insert into zc_payinfo_item (id, createTime, updateTime, payInfo_id, pay_mode, money) "
                + " values(:id, :createTime, :updateTime, :payInfo_id, :pay_mode, :money)";
            try
            {
                conn = OracleUtil.OpenConn();
                tran = conn.BeginTransaction();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.Parameters.Add(":id", obj.Id);
                cmd.Parameters.Add(":createTime", obj.CreateTime);
                cmd.Parameters.Add(":updateTime", obj.UpdateTime);
                cmd.Parameters.Add(":payInfo_id", obj.PayInfoId);
                cmd.Parameters.Add(":pay_mode", obj.PayMode);
                cmd.Parameters.Add(":money", obj.Money);
                cmd.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                tran.Commit();
                flag = true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                flag = false;
                log.Error("添加付款明细发生异常", ex);
            }
            finally
            {
                OracleUtil.CloseConn(conn);
            }
            return flag;
        }
    }
}
