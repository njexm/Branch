using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.util;
using log4net;
using MySql.Data.MySqlClient;

namespace Branch.com.proem.exm.dao.branch
{
    public class BranchZcStoreHouseDao : MysqlDBHelper
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(BranchZcStoreHouseDao));

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="obj"></param>
        public void AddZcStoreHouse(List<ZcStoreHouse> list)
        {
            MySqlConnection conn = null;
            MySqlTransaction tran = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();
                string sql = "insert into zc_storehouse (id, createTime, updateTime, status, store, storemoney, branch_id, createuser_id,goodsFile_id ,weight) "
                    +" values(@id, @create, @update, @status, @store, @storeMoney, @branchId, @createUserId, @goodsFileId, @weight)";
                cmd.Connection = conn;
                cmd.CommandText = sql;
                foreach (ZcStoreHouse obj in list)
                {
                    cmd.Parameters.AddWithValue("@id", obj.Id);
                    cmd.Parameters.AddWithValue("@create", obj.CreateTime);
                    cmd.Parameters.AddWithValue("@update", obj.UpdateTime);
                    cmd.Parameters.AddWithValue("@status", obj.Status);
                    cmd.Parameters.AddWithValue("@store", obj.Store);
                    cmd.Parameters.AddWithValue("@storeMoney", obj.StoreMoney);
                    cmd.Parameters.AddWithValue("@branchId", obj.BranchId);
                    cmd.Parameters.AddWithValue("@createUserId", obj.CreateUserId);
                    cmd.Parameters.AddWithValue("@goodsFileId", obj.GoodsFileId);
                    cmd.Parameters.AddWithValue("@weight", obj.weight);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("添加zc_storehouse数据异常", ex);
            }
            finally
            {
                cmd.Dispose();
                tran.Dispose();
                CloseConnection(conn);
            }
        }


        /// <summary>
        /// 根据商品id和分店id获取该商品对应分店库存
        /// </summary>
        /// <param name="goodsFileId">商品id</param>
        /// <param name="p">分店id</param>
        /// <returns></returns>
        public ZcStoreHouse FindByGoodsFileIdAndBranchId(string goodsFileId, string p)
        {
            ZcStoreHouse obj = null;
            string sql = "select a.id, a.createTime, a.updateTime, a.status, a.store,a.storemoney ,a.branch_id, a.createUser_id,a.goodsfile_id, a.weight " 
                +" from zc_storehouse a left join zc_branch_info b on a.BRANCH_ID = b.ID left join zc_branch_total c on b.BRANCHTOTAL_ID = c.ID "
                +" where a.goodsFile_id = '"+goodsFileId+"' and c.id = '"+p+"'";
            MySqlConnection conn = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read()){
                    obj = new ZcStoreHouse();
                    obj.Id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.CreateTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.UpdateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.Status = reader.IsDBNull(3) ? default(int) : reader.GetInt32(3);
                    obj.Store = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.StoreMoney = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    obj.BranchId = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.CreateUserId = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    obj.GoodsFileId = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                    obj.weight = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                }
            }
            catch (Exception ex)
            {
                log.Error("根据商品id和分店id查找该商品对应分店库存发生错误", ex);
            }
            finally
            {
                CloseConnection(conn);
            }
            return obj;
        }

        /// <summary>
        /// 更新库存
        /// </summary>
        /// <param name="storeList"></param>
        public void updateStoreHouse(List<ZcStoreHouse> storeList)
        {
            string sql = "update zc_storehouse set updateTime = @updateTime, store = @store, storeMoney = @storeMoney, weight = @weight where id = @id";
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = null;
            MySqlTransaction tran = null;
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                for (int i = 0; i < storeList.Count; i++) 
                {
                    ZcStoreHouse obj = storeList[i];
                    cmd.Parameters.AddWithValue("@updateTime", obj.UpdateTime);
                    cmd.Parameters.AddWithValue("@store", obj.Store);
                    cmd.Parameters.AddWithValue("@storeMoney", obj.StoreMoney);
                    cmd.Parameters.AddWithValue("@weight", obj.weight);
                    cmd.Parameters.AddWithValue("@id", obj.Id);
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("批量更新商品库存信息失败", ex);
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        public ZcStoreHouse FindById(string p)
        {
            ZcStoreHouse obj = new ZcStoreHouse();
            string sql = "select a.ID, a.CREATETIME, a.UPDATETIME, a.STATUS, a.STORE, a.STOREMONEY, a.BRANCH_ID, a.CREATEUSER_ID, a.GOODSFILE_ID, a.weight  from ZC_STOREHOUSE a where a.id = '"+p+"'";
            MySqlConnection conn = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read()){
                    obj.Id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.CreateTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.UpdateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.Status = reader.IsDBNull(3) ? default(int) : reader.GetInt32(3);
                    obj.Store = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.BranchId = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    obj.CreateUserId = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    obj.GoodsFileId = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    obj.weight = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                }
            }
            catch (Exception ex)
            {
                log.Error("根据id查询库存信息发生错误", ex);
            }
            finally
            {
                CloseConnection(conn);
            }
            return obj;
        }

        public void AddZcStoreHouse(ZcStoreHouse obj)
        {
            MySqlConnection conn = null;
            MySqlTransaction tran = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();
                string sql = "insert into zc_storehouse (id, createTime, updateTime, status, store, storemoney, branch_id, createuser_id,goodsFile_id ,weight) "
                    + " values(@id, @create, @update, @status, @store, @storeMoney, @branchId, @createUserId, @goodsFileId, @weight)";
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id", obj.Id);
                cmd.Parameters.AddWithValue("@create", obj.CreateTime);
                cmd.Parameters.AddWithValue("@update", obj.UpdateTime);
                cmd.Parameters.AddWithValue("@status", obj.Status);
                cmd.Parameters.AddWithValue("@store", obj.Store);
                cmd.Parameters.AddWithValue("@storeMoney", obj.StoreMoney);
                cmd.Parameters.AddWithValue("@branchId", obj.BranchId);
                cmd.Parameters.AddWithValue("@createUserId", obj.CreateUserId);
                cmd.Parameters.AddWithValue("@goodsFileId", obj.GoodsFileId);
                cmd.Parameters.AddWithValue("@weight", obj.weight);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("添加zc_storehouse数据异常", ex);
            }
            finally
            {
                cmd.Dispose();
                tran.Dispose();
                CloseConnection(conn);
            }
        }
    }
}
