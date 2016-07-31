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
    public class MasterBranchDiffDao
    {
        private readonly ILog log = LogManager.GetLogger(typeof(MasterBranchDiffDao));

        public void addObj(BranchDiff obj)
        {
            string sql = "insert into zc_branch_diff (id, createTime, updateTime, DiffOdd, branchIn_id, nums, weight, money, branch_id, branch_from_id, user_id) "
                + " values (:id, :createTime, :updateTime, :DiffOdd, :branchIn_id, :nums, :weight, :money, :branch_id, :branch_from_id, :user_id)";
            OracleConnection conn = null;
            OracleTransaction tran = null;
            OracleCommand cmd = new OracleCommand();
            try
            {
                conn = OracleUtil.OpenConn();
                tran = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.Add(":id", obj.id);
                cmd.Parameters.Add(":createTime", obj.createTime);
                cmd.Parameters.Add(":updateTime", obj.updateTime);
                cmd.Parameters.Add(":DiffOdd", obj.DiffOdd);
                cmd.Parameters.Add(":branchIn_id", obj.branchIn_id);
                cmd.Parameters.Add(":nums", obj.nums);
                cmd.Parameters.Add(":weight", obj.weight);
                cmd.Parameters.Add(":money", obj.money);
                cmd.Parameters.Add(":branch_id", obj.branch_id);
                cmd.Parameters.Add(":branch_from_id", obj.branch_from_id);
                cmd.Parameters.Add(":user_id", obj.user_id);
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UploadInfo uploadInfo = new UploadInfo();
                uploadInfo.Id = obj.id;
                uploadInfo.Type = Constant.ZC_BRANCH_DIFF;
                uploadInfo.CreateTime = DateTime.Now;
                uploadInfo.UpdateTime = DateTime.Now;
                UploadDao dao = new UploadDao();
                dao.AddUploadInfo(uploadInfo);
                log.Error("上传分店差异单失败", ex);
            }
            finally
            {
                cmd.Dispose();
                OracleUtil.CloseConn(conn);
            }
        }

        public bool addObjI(BranchDiff obj)
        {
            bool flag = true;
            string sql = "insert into zc_branch_diff (id, createTime, updateTime, DiffOdd, branchIn_id, nums, weight, money, branch_id, branch_from_id, user_id) "
                + " values (:id, :createTime, :updateTime, :DiffOdd, :branchIn_id, :nums, :weight, :money, :branch_id, :branch_from_id, :user_id)";
            OracleConnection conn = null;
            OracleTransaction tran = null;
            OracleCommand cmd = new OracleCommand();
            try
            {
                conn = OracleUtil.OpenConn();
                tran = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.Add(":id", obj.id);
                cmd.Parameters.Add(":createTime", obj.createTime);
                cmd.Parameters.Add(":updateTime", obj.updateTime);
                cmd.Parameters.Add(":DiffOdd", obj.DiffOdd);
                cmd.Parameters.Add(":branchIn_id", obj.branchIn_id);
                cmd.Parameters.Add(":nums", obj.nums);
                cmd.Parameters.Add(":weight", obj.weight);
                cmd.Parameters.Add(":money", obj.money);
                cmd.Parameters.Add(":branch_id", obj.branch_id);
                cmd.Parameters.Add(":branch_from_id", obj.branch_from_id);
                cmd.Parameters.Add(":user_id", obj.user_id);
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                flag = false;
                tran.Rollback();
                log.Error("上传分店差异单失败", ex);
            }
            finally
            {
                cmd.Dispose();
                OracleUtil.CloseConn(conn);
            }
            return flag;
        }
    }
}
