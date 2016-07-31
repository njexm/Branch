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
    public class MasterBranchInItemDao
    {
        private readonly ILog log = LogManager.GetLogger(typeof(MasterBranchInItemDao));

        public void addList(List<BranchInItem> list)
        {
            string sql = "insert into zc_branch_in_item (id, createTime, updateTime, branchIn_id, nums, weight, money, goodsFile_id, price) "
                + " values(:id, :createTime, :updateTime, :branchIn_id, :nums, :weight, :money, :goodsFile_id, :price)";
            OracleConnection conn = null;
            OracleTransaction tran = null;
            OracleCommand cmd = new OracleCommand();
            try
            {
                conn = OracleUtil.OpenConn();
                tran = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                for (int i = 0; i < list.Count; i++)
                {
                    BranchInItem obj = list[i];
                    cmd.Parameters.Add(":id", obj.id);
                    cmd.Parameters.Add(":createTime", obj.createTime);
                    cmd.Parameters.Add(":updateTime", obj.updateTime);
                    cmd.Parameters.Add(":branchIn_id", obj.branchIn_id);
                    cmd.Parameters.Add(":nums", obj.nums);
                    cmd.Parameters.Add(":weight", obj.weight);
                    cmd.Parameters.Add(":money", obj.money);
                    cmd.Parameters.Add(":goodsFile_id", obj.goodsFile_id);
                    cmd.Parameters.Add(":price", obj.price);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UploadDao dao = new UploadDao();
                List<UploadInfo> uplist = new List<UploadInfo>();
                for (int i = 0; i<list.Count; i++)
                {
                    UploadInfo uploadInfo = new UploadInfo();
                    uploadInfo.Id = list[i].id;
                    uploadInfo.Type = Constant.ZC_BRANCH_IN_ITEM;
                    uploadInfo.CreateTime = DateTime.Now;
                    uploadInfo.UpdateTime = DateTime.Now;
                    uplist.Add(uploadInfo);
                }
                dao.AddUploadInfo(uplist);
                log.Error("上传分店入库单明细失败", ex);
            }
            finally
            {
                cmd.Dispose();
                OracleUtil.CloseConn(conn);
            }
        }

        public bool addObj(BranchInItem obj)
        {
            bool flag = true;
            string sql = "insert into zc_branch_in_item (id, createTime, updateTime, branchIn_id, nums, weight, money, goodsFile_id, price) "
                + " values(:id, :createTime, :updateTime, :branchIn_id, :nums, :weight, :money, :goodsFile_id, :price)";
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
                cmd.Parameters.Add(":branchIn_id", obj.branchIn_id);
                cmd.Parameters.Add(":nums", obj.nums);
                cmd.Parameters.Add(":weight", obj.weight);
                cmd.Parameters.Add(":money", obj.money);
                cmd.Parameters.Add(":goodsFile_id", obj.goodsFile_id);
                cmd.Parameters.Add(":price", obj.price);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                flag = false;
                log.Error("上传分店入库单明细失败", ex);
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
