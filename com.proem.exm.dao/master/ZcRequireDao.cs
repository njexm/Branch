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
    class ZcRequireDao 
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(ResaleDao));

        /// <summary>
        /// 添加主订单信息
        /// </summary>
        /// <param name="zcRequire"></param>
        public void addZcRequireInfo(ZcRequire zcRequire)
        {

            string sql = "insert into Zc_Require (ID,CREATETIME,UPDATETIME,YHD_NUMBER,STATUS,USER_ID,NUMS,MONEY,REMARK,BRANCH_ID,CALLOUT_BRANCH_ID,REASON) "
                        + " values (:id,:createTime,:updateTime,:yhd_Number,:status,:user_Id,:nums,:money,:remark,:branch_Id,:callout_Branch_Id,:reason)";
            OracleConnection conn = null;
            OracleTransaction tran = null;
            OracleCommand cmt = new OracleCommand();

            conn = OracleUtil.OpenConn();
            tran = conn.BeginTransaction();
            cmt.CommandText = sql;
            cmt.Connection = conn;
            try
            {
                cmt.Parameters.Add(":id", zcRequire.id);
                cmt.Parameters.Add(":createTime", DateTime.Now);
                cmt.Parameters.Add(":updateTime", DateTime.Now);
                cmt.Parameters.Add(":yhd_Number", zcRequire.yhdNumber);
                cmt.Parameters.Add(":status", zcRequire.status);
                cmt.Parameters.Add(":user_Id", zcRequire.userId);
                cmt.Parameters.Add(":nums", zcRequire.nums);
                cmt.Parameters.Add(":money", zcRequire.money);
                
                cmt.Parameters.Add(":remark", zcRequire.remark);
                
                cmt.Parameters.Add(":branch_Id", zcRequire.branchId);
                cmt.Parameters.Add(":callout_Branch_Id", zcRequire.calloutBranchId);
                cmt.Parameters.Add(":reason", zcRequire.reason);
                cmt.ExecuteNonQuery();
                tran.Commit();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("添加出错", ex);

            }
            finally
            {
                cmt.Dispose();
                tran.Dispose();
                OracleUtil.CloseConn(conn);
            }

        }
        /// <summary>
        /// 更新要货单信息
        /// </summary>
        /// <param name="zcRequire"></param>
        public void updateRequire(ZcRequire zcRequire)
        {
            string sql = "update  Zc_Require set UPDATETIME=:updateTime,YHD_NUMBER=:yhd_Number ,status = :status,user_Id =:user_Id ,nums = :nums,money = :money,check_Man =:check_Man,remark = :remark,check_Date = :check_Date,branch_Id = :branch_Id,callout_Branch_Id = :callout_Branch_Id , Reason = :Reason where id =:id";
            OracleConnection conn = null;
            OracleTransaction tran = null;
            OracleCommand cmt = new OracleCommand();

            conn = OracleUtil.OpenConn();
            tran = conn.BeginTransaction();
            cmt.CommandText = sql;
            cmt.Connection = conn;
            try
            {

               
                cmt.Parameters.Add(":updateTime", zcRequire.updateTime);
                cmt.Parameters.Add(":yhd_Number", zcRequire.yhdNumber);
                cmt.Parameters.Add(":status", zcRequire.status);
                cmt.Parameters.Add(":user_Id", zcRequire.userId);
                cmt.Parameters.Add(":nums", zcRequire.nums);
                cmt.Parameters.Add(":money", zcRequire.money);
                cmt.Parameters.Add(":check_Man", zcRequire.checkMan);
                cmt.Parameters.Add(":remark", zcRequire.remark);
                cmt.Parameters.Add(":check_Date", zcRequire.checkDate);
                cmt.Parameters.Add(":branch_Id", zcRequire.branchId);
                cmt.Parameters.Add(":callout_Branch_Id", zcRequire.calloutBranchId);
                cmt.Parameters.Add(":Reason", zcRequire.reason);                
                cmt.Parameters.Add(":id", zcRequire.id);
                cmt.ExecuteNonQuery();
                tran.Commit();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("更新出错", ex);

            }
            finally
            {
                cmt.Dispose();
                tran.Dispose();
                OracleUtil.CloseConn(conn);
            }
        }

        /// <summary>
        /// 查找所有分店信息
        /// </summary>
        public List<ZcBranchTotal> findAllBranchInfo()
        {
            List<ZcBranchTotal> list = new List<ZcBranchTotal>();
            OracleConnection conn = null;
            OracleCommand cmt = new OracleCommand();
            string sql = "select * from Zc_Branch_Total";



            try
            {
                conn = OracleUtil.OpenConn();
                cmt.CommandText = sql;
                cmt.Connection = conn;
                var reader = cmt.ExecuteReader();
                while (reader.Read())
                {
                    ZcBranchTotal zcBranchTotal = new ZcBranchTotal();
                    zcBranchTotal.Id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    zcBranchTotal.CreateTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    zcBranchTotal.UpdateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    zcBranchTotal.BranchCode = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    zcBranchTotal.BranchName = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    zcBranchTotal.DelFlag = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    zcBranchTotal.Money = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    zcBranchTotal.CustomerId = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    zcBranchTotal.ZoningId = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                    list.Add(zcBranchTotal);

                }

            }
            finally
            {
                cmt.Dispose();
                OracleUtil.CloseConn(conn);
            }
            return list;
        }

        /// <summary>
        /// 根据传进的参数查询查询分店信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ZcBranchTotal findZcBranchTotalById(string conditions)
        {
            string sql = "select * from Zc_Branch_Total zbt where 1=1";
            ZcBranchTotal obj = new ZcBranchTotal();


            sql += conditions;


            OracleConnection conn = null;
            OracleCommand cmd = new OracleCommand();
            try
            {
                conn = OracleUtil.OpenConn();
                cmd.Connection = conn;
                cmd.CommandText = sql;





                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
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
                log.Error("查找关键词错误", ex);
            }
            finally
            {
                cmd.Dispose();
                OracleUtil.CloseConn(conn);
            }
            return obj;
        }

        /// <summary>
        /// 根据要货单单号更新要货单信息，（提交）
        /// </summary>
        /// <param name="zcRequire"></param>
        public void updateRequireByYhdNumber(ZcRequire zcRequire)
        {

            string sql = "update  Zc_Require set status ='" + zcRequire.status + "' where yhd_number = '" + zcRequire.yhdNumber + "'";

            OracleConnection conn = null;
            OracleTransaction tran = null;
            OracleCommand cmd = new OracleCommand();
            try
            {
                conn = OracleUtil.OpenConn();
                tran = conn.BeginTransaction();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("根据订单号更新订单状态发生异常", ex);
            }
            finally
            {
                tran.Dispose();
                cmd.Dispose();
                OracleUtil.CloseConn(conn);
            }

        }

        /// <summary>
        /// 根据id删除主表信息
        /// </summary>
        public void deleteZcRequireById(ZcRequire zcRequire)
        {
            OracleConnection conn = null;
            OracleTransaction tran = null;
            OracleCommand cmd = new OracleCommand();
            string sql = "delete from zc_Require where id = :id";
            try
            {
                conn = OracleUtil.OpenConn();
                tran = conn.BeginTransaction();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.Parameters.Add(":id", zcRequire.id);
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("根据订单id删除订单信息失败", ex);
            }
            finally
            {
                cmd.Dispose();
                tran.Dispose();
                OracleUtil.CloseConn(conn);
            }
        }

        /// <summary>
        /// 根据userid查询ZcUserInfo的id
        /// </summary>
        /// <returns></returns>
        public ZcUserInfo FindByUserId()
        {
            ZcUserInfo obj = new ZcUserInfo();
            OracleConnection conn = null;
            try
            {
                conn = OracleUtil.OpenConn();
                string sql = "select ID, CREATETIME, UPDATETIME, EMAIL, MOBILEPHONE, SEXTYPE, USERNAME, ZIPCODE, ROLE_ID,  ZONING_ID, BRANCH_NAME_ID  from zc_user_info where USER_ID, = '" + LoginUserInfo.id + "'";
                OracleCommand command = new OracleCommand(sql);
                command.Connection = conn;
                var reader = command.ExecuteReader();

                while (reader.Read())
                {

                    obj.Id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.CreateTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.UpdateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.Email = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    obj.MobilePhone = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.SexType = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    obj.UserName = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.ZipCode = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    obj.RoleId = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);

                    obj.ZoningId = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                    obj.branchTotalId = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                    obj.UserId = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                OracleUtil.CloseConn(conn);
            }
            return obj;
        }

        /// <summary>
        /// 根据ID确认收货更新信息（库存，状态）
        /// </summary>
        /// <param name="zcRequire"></param>
        public void updateRequireById(ZcRequire zcRequire)
        {

            string sql = "update  Zc_Require  set updateTime='" + DateTime.Now + "',check_date = '" + zcRequire .checkDate+ "', status ='" + zcRequire.status + "',reason='"+zcRequire.reason+"'  where id = '" + zcRequire.id + "'";

            OracleConnection conn = null;
            OracleTransaction tran = null;
            OracleCommand cmd = new OracleCommand();
            try
            {
                conn = OracleUtil.OpenConn();
                tran = conn.BeginTransaction();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("根据id更新订单状态发生异常", ex);
            }
            finally
            {
                tran.Dispose();
                cmd.Dispose();
                OracleUtil.CloseConn(conn);
            }

        }

    }
}
