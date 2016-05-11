using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.util;
using log4net;
using Oracle.ManagedDataAccess.Client;

namespace Branch.com.proem.exm.dao.master
{
    public class ZcStoreHouseDao
    {
        private ILog log = LogManager.GetLogger(typeof(ZcStoreHouseDao));

        /// <summary>
        /// 根据分店信息下载数据
        /// </summary>
        /// <returns></returns>
        public List<ZcStoreHouse> FindByBranchId()
        {
            List<ZcStoreHouse> list = new List<ZcStoreHouse>();
            OracleConnection conn = null;
            try
            {
                conn = OracleUtil.OpenConn();
                string sql = "select a.ID, a.CREATETIME, a.UPDATETIME, a.STATUS, a.STORE, a.STOREMONEY, a.BRANCH_ID, a.CREATEUSER_ID, a.GOODSFILE_ID, a.weight  from ZC_STOREHOUSE a left join ZC_BRANCH_INFO b "
                +"on a.BRANCH_ID = b.id LEFT JOIN ZC_BRANCH_TOTAL c "
                +"on b.branchtotal_id = c.id where c.id = '"+LoginUserInfo.branchId+"'";
                OracleCommand cmd = new OracleCommand(sql, conn);
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    ZcStoreHouse obj = new ZcStoreHouse();
                    obj.Id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.CreateTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.UpdateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.Status = reader.IsDBNull(3) ? default(int) : reader.GetInt32(3);
                    obj.Store = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.BranchId = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    obj.CreateUserId = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.GoodsFileId = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    obj.weight = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                    list.Add(obj);
                }
            }
            catch(Exception ex)
            {
                log.Error("下载分店相关库存信息异常", ex);
            }
            finally
            {
                OracleUtil.CloseConn(conn);
            }
            return list;
        }

        /// <summary>
        /// 根据收货信息查询库存中是否存在改商品
        /// </summary>
        /// <returns></returns>
        public ZcStoreHouse FindByBranchIdAndGoodFilesId(ZcRequireItem zcRequireItem)
        {
            ZcStoreHouse obj = new ZcStoreHouse();
            OracleConnection conn = null;
            try
            {
                conn = OracleUtil.OpenConn();
                string sql = "select ID, CREATETIME, UPDATETIME, STATUS, STORE, STOREMONEY,  CREATEUSER_ID  from ZC_STOREHOUSE a  where GOODSFILE_ID ='" + zcRequireItem.goodsFileId + "' and  BRANCH_ID = '"+LoginUserInfo.branchId+"' ";
               
                OracleCommand cmd = new OracleCommand(sql, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    
                    obj.Id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.CreateTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.UpdateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.Status = reader.IsDBNull(3) ? default(int) : reader.GetInt32(3);
                    obj.Store = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.CreateUserId = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    
                }
            }
            catch (Exception ex)
            {
                log.Error("确认收货放入库存", ex);
            }
            finally
            {
                OracleUtil.CloseConn(conn);
            }
            return obj;
        }

        /// <summary>
        /// 添加确认收货到库存
        /// </summary>
        /// <param name="obj"></param>
        public void AddZcStoreHouse(ZcStoreHouse obj)
        {
            string sql = "insert into ZC_STOREHOUSE (id, CREATETIME, UPDATETIME, status, store, storemoney,branch_id,createuser_id,goodsfile_id,weight) values (:id, :createTime, :updateTime, :status, :store, :storemoney,:branch_id,:createuser_id,:goodsfile_id,:weight)";
            OracleConnection conn = null;
            OracleTransaction tran = null;
            OracleCommand cmd = new OracleCommand();
            try
            {
                conn = OracleUtil.OpenConn();
                tran = conn.BeginTransaction();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.Parameters.Add(":id", obj.Id);
                cmd.Parameters.Add(":createTime", obj.CreateTime);
                cmd.Parameters.Add(":updateTime", obj.UpdateTime);
                cmd.Parameters.Add(":status", obj.Status);
                cmd.Parameters.Add(":storemoney", obj.StoreMoney);
                cmd.Parameters.Add(":branch_id", obj.BranchId);
                cmd.Parameters.Add(":createuser_id", obj.CreateUserId);
                cmd.Parameters.Add(":goodsfile_id", obj.GoodsFileId);
                cmd.Parameters.Add(":weight", obj.weight);
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
               
            }
            finally
            {
                tran.Dispose();
                cmd.Dispose();
                OracleUtil.CloseConn(conn);
            }

        }

        /// <summary>
        /// 如果查询到存在，更新库存量 和库存的价格，更新时间（状态是什么鬼，不知道要不要更新）
        /// </summary>
        /// <param name="obj"></param>
        public void updateZcStoreHouse(ZcStoreHouse obj)
        {
            string sql = "update  ZC_STOREHOUSE set  UPDATETIME = :UPDATETIME, store = :store, storemoney = :storemoney where id = :id";
            OracleConnection conn = null;
            OracleTransaction tran = null;
            OracleCommand cmd = new OracleCommand();
            try
            {
                conn = OracleUtil.OpenConn();
                tran = conn.BeginTransaction();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                
                cmd.Parameters.Add(":updateTime", DateTime.Now);
                cmd.Parameters.Add(":store", obj.Store);
                cmd.Parameters.Add(":storemoney", obj.StoreMoney);
                cmd.Parameters.Add(":id", obj.Id);
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();

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
