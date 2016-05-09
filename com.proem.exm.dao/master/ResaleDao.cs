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
    public class ResaleDao
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(ResaleDao));

        /// <summary>
        /// 上传流水信息
        /// </summary>
        /// <param name="obj"></param>
        public void AddResale(Resale obj) 
        {
            string sql = "insert into zc_resale (id, createTime, updateTime, nums, money, discount_money, preferential_money, actual_money, branch_id, saleman_id ,member_id, order_id, water_number) "
                + " values (:id, :createTime, :updateTime, :nums, :money,:discount_money,:preferential_money, :actual_money, :branchId,:saleman_id, :memberId, :order_id, :water_number)";
            OracleConnection conn = null;
            OracleTransaction tran = null;
            OracleCommand cmd = new OracleCommand();
            try
            {
                conn = OracleUtil.OpenConn();
                tran = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.Add(":id", obj.Id);
                cmd.Parameters.Add(":createTime", obj.CreateTime);
                cmd.Parameters.Add(":updateTime", obj.UpdateTime);
                cmd.Parameters.Add(":nums", obj.Nums);
                cmd.Parameters.Add(":money", obj.Money);
                cmd.Parameters.Add(":discount_money", obj.DiscountMoney);
                cmd.Parameters.Add(":preferential_money", obj.PreferentialMoney);
                cmd.Parameters.Add(":actual_money", obj.ActualMoney);
                cmd.Parameters.Add(":branchId", obj.BranchId);
                cmd.Parameters.Add(":saleman_id", obj.SaleManId);
                cmd.Parameters.Add(":memberId", obj.memberId);
                cmd.Parameters.Add(":order_id", obj.OrderId);
                cmd.Parameters.Add(":water_number", obj.WaterNumber);
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UploadInfo uploadInfo = new UploadInfo();
                uploadInfo.Id = obj.Id;
                uploadInfo.CreateTime = DateTime.Now;
                uploadInfo.UpdateTime = DateTime.Now;
                uploadInfo.Type = Constant.ZC_RESALE;
                UploadDao uploadDao = new UploadDao();
                uploadDao.AddUploadInfo(uploadInfo);
                log.Error("上传零售信息发生异常", ex);
            }
            finally
            {
                OracleUtil.CloseConn(conn);
            }
        }
    }
}
