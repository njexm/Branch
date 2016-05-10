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



    class ZcRequireItemDao
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(ZcRequireItemDao));

        /// <summary>
        /// 添加商品信息
        /// </summary>
        public void addZcRequireItem(List<ZcRequireItem> list)
        {
            string sql = "insert into Zc_Require_item (id,createTime,updateTime,require_Id,goods_File_Id,nums,money,remark) values(:id,:createTime,:updateTime,:require_Id,:goods_File_Id,:nums,:money,:remark) ";
            OracleConnection conn = null;
            OracleTransaction tran = null;
            OracleCommand cmt = new OracleCommand();


            try
            {
                conn = OracleUtil.OpenConn();
                tran = conn.BeginTransaction();
                cmt.CommandText = sql;
                cmt.Connection = conn;
                foreach (ZcRequireItem obj in list)
                {
                    cmt.Parameters.Add(new OracleParameter(":id", obj.id));
                    cmt.Parameters.Add(new OracleParameter(":createTime", DateTime.Now));
                    cmt.Parameters.Add(new OracleParameter(":updateTime", DateTime.Now));
                    cmt.Parameters.Add(new OracleParameter(":require_Id", obj.requireId));
                    cmt.Parameters.Add(new OracleParameter(":goods_File_Id", obj.goodsFileId));
                    cmt.Parameters.Add(new OracleParameter(":nums", obj.nums));
                    cmt.Parameters.Add(new OracleParameter(":money", obj.money));
                    cmt.Parameters.Add(new OracleParameter(":remark", obj.remark));


                    cmt.ExecuteNonQuery();
                    cmt.Parameters.Clear();

                }
                tran.Commit();
            }
            catch (Exception ex)
            {

                tran.Rollback();
                log.Error("添加出现异常", ex);

            }
            finally
            {
                cmt.Dispose();
                tran.Dispose();
                OracleUtil.CloseConn(conn);
            }
        }

        /// <summary>
        /// 添加单个的商品信息
        /// </summary>
        public void addOneZcRequireItem(ZcRequireItem obj)
        {
            string sql = "insert into Zc_Require_item (id,createTime,updateTime,require_Id,goods_File_Id,nums,money,remark) values(:id,:createTime,:updateTime,:require_Id,:goods_File_Id,:nums,:money,:remark) ";
            OracleConnection conn = null;
            OracleTransaction tran = null;
            OracleCommand cmt = new OracleCommand();


            try
            {
                conn = OracleUtil.OpenConn();
                tran = conn.BeginTransaction();
                cmt.CommandText = sql;
                cmt.Connection = conn;

                cmt.Parameters.Add(new OracleParameter(":id", obj.id));
                cmt.Parameters.Add(new OracleParameter(":createTime", obj.createTime));
                cmt.Parameters.Add(new OracleParameter(":updateTime", obj.updateTime));
                cmt.Parameters.Add(new OracleParameter(":require_Id", obj.requireId));
                cmt.Parameters.Add(new OracleParameter(":goods_File_Id", obj.goodsFileId));
                cmt.Parameters.Add(new OracleParameter(":nums", obj.nums));
                cmt.Parameters.Add(new OracleParameter(":money", obj.money));
                cmt.Parameters.Add(new OracleParameter(":remark", obj.remark));


                cmt.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {

                tran.Rollback();
                log.Error("添加出现异常", ex);

            }
            finally
            {
                cmt.Dispose();
                tran.Dispose();
                OracleUtil.CloseConn(conn);
            }
        }

        /// <summary>
        /// 根据id删除商品信息 
        /// </summary>
        /// <param name="list"></param>
        public void DeleteRequireItemById(ZcRequire zcRequire)
        {
            OracleConnection conn = null;
            OracleTransaction tran = null;
            OracleCommand cmd = new OracleCommand();
            string sql = "delete from zc_Require_Item where require_id = :require_id";
            try
            {
                conn = OracleUtil.OpenConn();
                tran = conn.BeginTransaction();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.Parameters.Add(":require_id", zcRequire.id);
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("根据订单号删除订单商品信息失败", ex);
            }
            finally
            {
                cmd.Dispose();
                tran.Dispose();
                OracleUtil.CloseConn(conn);
            }
        }

        /// <summary>
        /// 根据要货单主表id查询出要货单商品信息表的信息集合
        /// </summary>
        /// <returns></returns>
        public List<ZcRequireItem> findAllRequireItemByRequireId(ZcRequire zcRequire)
        {

            string sql = "select * from Zc_Require_item left join zc_goods_master on Zc_Require_item.goods_file_id =zc_goods_master.id  where require_id = :require_Id ";
            OracleConnection conn = null;

            OracleCommand cmt = new OracleCommand();
            List<ZcRequireItem> list = new List<ZcRequireItem>();

            try
            {

                conn = OracleUtil.OpenConn();
                cmt.CommandText = sql;
                cmt.Connection = conn;
                cmt.Parameters.Add(":require_Id", zcRequire.id);
                var reader = cmt.ExecuteReader();
                while (reader.Read())
                {
                    ZcRequireItem zcRequireItem = new ZcRequireItem();
                    zcRequireItem.id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    zcRequireItem.createTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    zcRequireItem.updateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    zcRequireItem.requireId = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    zcRequireItem.remark = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    zcRequireItem.nums = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    zcRequireItem.money = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    zcRequireItem.goodsFileId = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                    list.Add(zcRequireItem);


                }



            }
            catch (Exception ex)
            {

                log.Error("查询出错", ex);

            }
            finally
            {
                cmt.Dispose();

                OracleUtil.CloseConn(conn);

            }
            return list;
        }


        /// <summary>
        /// 根据商品信息id删除商品信息
        /// </summary>
        /// <param name="zcRequireItem"></param>
        public void deleteGoodsById(ZcRequireItem zcRequireItem)
        {
            OracleConnection conn = null;
            OracleTransaction tran = null;
            OracleCommand cmd = new OracleCommand();
            string sql = "delete from zc_Require_Item where id = :id";
            try
            {
                conn = OracleUtil.OpenConn();
                tran = conn.BeginTransaction();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.Parameters.Add(":id", zcRequireItem.id);
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("根据商品id删除订单商品信息失败", ex);
            }
            finally
            {
                cmd.Dispose();
                tran.Dispose();
                OracleUtil.CloseConn(conn);
            }
        }

        public bool updateZcRequireItem(List<ZcRequireItem> list)
        {
            string sql = "update  Zc_Require_item set updateTime = :updateTime,nums =:nums, money=:money,remark = :remark where id = :id";

            OracleConnection conn = null;
            OracleTransaction tran = null;
            OracleCommand cmt = new OracleCommand();
            try
            {
                conn = OracleUtil.OpenConn();
                tran = conn.BeginTransaction();
                cmt.CommandText = sql;
                cmt.Connection = conn;
                foreach (ZcRequireItem zcRequireItem in list)
                {
                    cmt.Parameters.Add(":updateTime", zcRequireItem.updateTime);
                    cmt.Parameters.Add(":nums", zcRequireItem.nums);
                    cmt.Parameters.Add(":money", zcRequireItem.money);
                    cmt.Parameters.Add(":remark", zcRequireItem.remark);
                    cmt.Parameters.Add(":id", zcRequireItem.id);
                    cmt.ExecuteNonQuery();
                    cmt.Parameters.Clear();
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("更新出错", ex);
                return false;

            }
            finally
            {
                cmt.Dispose();
                tran.Dispose();
                OracleUtil.CloseConn(conn);
            }
            return true;
        }
      
    }
}
