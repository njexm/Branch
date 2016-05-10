using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.util;
using log4net;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.dao.branch
{


    public class BranchZcRequireDao : MysqlDBHelper
    {

        //private ILog log = LogManager.GetLogger(typeof(BranchZcRequireDao));
        ///// <summary>
        ///// 添加主订单信息
        ///// </summary>
        ///// <param name="zcRequire"></param>
        //public void addZcRequireInfo(ZcRequire zcRequire)
        //{

        //    string sql = "insert into Zc_Require values(@id,@createTime,@updateTime,@yhdNumber,@status,@userId,@nums,@money,@checkMan,@remark,@checkDate,@branchId,@calloutBranchId); ";
        //    MySqlConnection conn = null;
        //    MySqlTransaction tran = null;
        //    MySqlCommand cmt = new MySqlCommand();

        //    conn = GetConnection();
        //    tran = conn.BeginTransaction();
        //    cmt.CommandText = sql;
        //    cmt.Connection = conn;
        //    try
        //    {
        //        cmt.Parameters.AddWithValue("@id", zcRequire.id);
        //        cmt.Parameters.AddWithValue("@createTime", zcRequire.createTime);
        //        cmt.Parameters.AddWithValue("@updateTime", zcRequire.updateTime);
        //        cmt.Parameters.AddWithValue("@yhdNumber", zcRequire.yhdNumber);
        //        cmt.Parameters.AddWithValue("@status", zcRequire.status);
        //        cmt.Parameters.AddWithValue("@userId", zcRequire.userId);
        //        cmt.Parameters.AddWithValue("@nums", zcRequire.nums);
        //        cmt.Parameters.AddWithValue("@money", zcRequire.money);
        //        cmt.Parameters.AddWithValue("@checkMan", zcRequire.checkMan);
        //        cmt.Parameters.AddWithValue("@remark", zcRequire.remark);
        //        cmt.Parameters.AddWithValue("@checkDate", zcRequire.checkDate);
        //        cmt.Parameters.AddWithValue("@branchId", zcRequire.branchId);
        //        cmt.Parameters.AddWithValue("@calloutBranchId", zcRequire.calloutBranchId);
        //        cmt.ExecuteNonQuery();
        //        tran.Commit();

        //    }
        //    catch (Exception ex)
        //    {
        //        tran.Rollback();
        //        log.Error("添加出错", ex);

        //    }
        //    finally
        //    {
        //        cmt.Dispose();
        //        tran.Dispose();
        //        CloseConnection(conn);
        //    }

        //}

        //public void updateRequire(ZcRequire zcRequire)
        //{
        //    string sql = "update  Zc_Require set create_time = @createTime,update_Time = @updateTime,yhd_Number@yhdNumber,status = @status,user_Id = @userId,nums =@nums,money = @money,check_Man =@checkMan,remark = @remark,check_Date = @checkDate,branch_Id = @branchId,callout_Branch_Id = @calloutBranchId where id = @id";
        //    MySqlConnection conn = null;
        //    MySqlTransaction tran = null;
        //    MySqlCommand cmt = new MySqlCommand();

        //    conn = GetConnection();
        //    tran = conn.BeginTransaction();
        //    cmt.CommandText = sql;
        //    cmt.Connection = conn;
        //    try
        //    {

        //        cmt.Parameters.AddWithValue("@createTime", zcRequire.createTime);
        //        cmt.Parameters.AddWithValue("@updateTime", zcRequire.updateTime);
        //        cmt.Parameters.AddWithValue("@yhdNumber", zcRequire.yhdNumber);
        //        cmt.Parameters.AddWithValue("@status", zcRequire.status);
        //        cmt.Parameters.AddWithValue("@userId", zcRequire.userId);
        //        cmt.Parameters.AddWithValue("@nums", zcRequire.nums);
        //        cmt.Parameters.AddWithValue("@money", zcRequire.money);
        //        cmt.Parameters.AddWithValue("@checkMan", zcRequire.checkMan);
        //        cmt.Parameters.AddWithValue("@remark", zcRequire.remark);
        //        cmt.Parameters.AddWithValue("@checkDate", zcRequire.checkDate);
        //        cmt.Parameters.AddWithValue("@branchId", zcRequire.branchId);
        //        cmt.Parameters.AddWithValue("@calloutBranchId", zcRequire.calloutBranchId);
        //        cmt.Parameters.AddWithValue("@id", zcRequire.id);
        //        cmt.ExecuteNonQuery();
        //        tran.Commit();

        //    }
        //    catch (Exception ex)
        //    {
        //        tran.Rollback();
        //        log.Error("更新出错", ex);

        //    }
        //    finally
        //    {
        //        cmt.Dispose();
        //        tran.Dispose();
        //        CloseConnection(conn);
        //    }
        //}
        ///// <summary>
        ///// 添加商品信息
        ///// </summary>
        //public void addZcRequireItem(List<ZcRequireItem> list)
        //{
        //    string sql = "insert into Zc_Require_item values(@id,@createTime,@updateTime,@requireId,@goodsFileId,@nums,@money,@remark); ";
        //    MySqlConnection conn = null;
        //    MySqlTransaction tran = null;
        //    MySqlCommand cmt = new MySqlCommand();


        //    try
        //    {
        //        conn = GetConnection();
        //        tran = conn.BeginTransaction();
        //        cmt.CommandText = sql;
        //        cmt.Connection = conn;
        //        foreach (ZcRequireItem obj in list)
        //        {
        //            cmt.Parameters.Add(new MySqlParameter("@id", obj.id));
        //            cmt.Parameters.Add(new MySqlParameter("@createTime", obj.createTime));
        //            cmt.Parameters.Add(new MySqlParameter("@updateTime", obj.updateTime));
        //            cmt.Parameters.Add(new MySqlParameter("@requireId", obj.requireId));
        //            cmt.Parameters.Add(new MySqlParameter("@goodsFileId", obj.goodFileId));
        //            cmt.Parameters.Add(new MySqlParameter("@nums", obj.nums));
        //            cmt.Parameters.Add(new MySqlParameter("@money", obj.money));
        //            cmt.Parameters.Add(new MySqlParameter("@remark", obj.remark));


        //            cmt.ExecuteNonQuery();
        //            cmt.Parameters.Clear();

        //        }
        //        tran.Commit();
        //    }
        //    catch (Exception ex)
        //    {

        //        tran.Rollback();
        //        log.Error("添加出现异常", ex);

        //    }
        //    finally
        //    {
        //        cmt.Dispose();
        //        tran.Dispose();
        //        CloseConnection(conn);
        //    }
        //}

        ///// <summary>
        ///// 添加单个的商品信息
        ///// </summary>
        //public void addOneZcRequireItem(ZcRequireItem obj)
        //{
        //    string sql = "insert into Zc_Require_item values(@id,@createTime,@updateTime,@requireId,@goodsFileId,@nums,@money,@remark); ";
        //    MySqlConnection conn = null;
        //    MySqlTransaction tran = null;
        //    MySqlCommand cmt = new MySqlCommand();


        //    try
        //    {
        //        conn = GetConnection();
        //        tran = conn.BeginTransaction();
        //        cmt.CommandText = sql;
        //        cmt.Connection = conn;

        //        cmt.Parameters.Add(new MySqlParameter("@id", obj.id));
        //        cmt.Parameters.Add(new MySqlParameter("@createTime", obj.createTime));
        //        cmt.Parameters.Add(new MySqlParameter("@updateTime", obj.updateTime));
        //        cmt.Parameters.Add(new MySqlParameter("@requireId", obj.requireId));
        //        cmt.Parameters.Add(new MySqlParameter("@goodsFileId", obj.goodFileId));
        //        cmt.Parameters.Add(new MySqlParameter("@nums", obj.nums));
        //        cmt.Parameters.Add(new MySqlParameter("@money", obj.money));
        //        cmt.Parameters.Add(new MySqlParameter("@remark", obj.remark));


        //        cmt.ExecuteNonQuery();
        //        cmt.Parameters.Clear();


        //        tran.Commit();
        //    }
        //    catch (Exception ex)
        //    {

        //        tran.Rollback();
        //        log.Error("添加出现异常", ex);

        //    }
        //    finally
        //    {
        //        cmt.Dispose();
        //        tran.Dispose();
        //        CloseConnection(conn);
        //    }
        //}

        ///// <summary>
        ///// 查找所有分店信息
        ///// </summary>
        //public List<ZcBranchTotal> findAllBranchInfo()
        //{
        //    List<ZcBranchTotal> list = new List<ZcBranchTotal>();
        //    MySqlConnection conn = null;
        //    MySqlCommand cmt = new MySqlCommand();
        //    string sql = "select * from Zc_Branch_Total";



        //    try
        //    {
        //        conn = GetConnection();
        //        cmt.CommandText = sql;
        //        cmt.Connection = conn;
        //        var reader = cmt.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            ZcBranchTotal zcBranchTotal = new ZcBranchTotal();
        //            zcBranchTotal.Id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
        //            zcBranchTotal.CreateTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
        //            zcBranchTotal.UpdateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
        //            zcBranchTotal.BranchCode = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
        //            zcBranchTotal.BranchName = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
        //            zcBranchTotal.DelFlag = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
        //            zcBranchTotal.Money = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
        //            zcBranchTotal.CustomerId = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
        //            zcBranchTotal.ZoningId = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
        //            list.Add(zcBranchTotal);

        //        }

        //    }
        //    finally
        //    {
        //        cmt.Dispose();
        //        CloseConnection(conn);
        //    }
        //    return list;
        //}
        ///// <summary>
        ///// 根据传进的参数查询查询分店信息
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public ZcBranchTotal findZcBranchTotalById(string conditions)
        //{
        //    string sql = "select * from Zc_Branch_Total zbt where 1=1";
        //    ZcBranchTotal obj = new ZcBranchTotal();


        //    sql += conditions;


        //    MySqlConnection conn = null;
        //    MySqlCommand cmd = new MySqlCommand();
        //    try
        //    {
        //        conn = GetConnection();
        //        cmd.Connection = conn;
        //        cmd.CommandText = sql;





        //        MySqlDataReader reader = cmd.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            obj.Id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
        //            obj.CreateTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
        //            obj.UpdateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
        //            obj.BranchCode = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
        //            obj.BranchName = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
        //            obj.DelFlag = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
        //            obj.Money = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
        //            obj.CustomerId = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
        //            obj.ZoningId = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("查找关键词错误", ex);
        //    }
        //    finally
        //    {
        //        cmd.Dispose();
        //        CloseConnection(conn);
        //    }
        //    return obj;
        //}
        ///// <summary>
        ///// 查询所有商品信息
        ///// </summary>
        ///// <returns></returns>
        ///*public List<ZcGoodsMaster> findAllGoodsInfo() {
        //     List<ZcGoodsMaster> list = new List<ZcGoodsMaster>();
        //     MySqlConnection conn = null;
        //     MySqlCommand cmt = new MySqlCommand();
        //     string sql = "select * from zc_goods_master";



        //     try
        //     {
        //         conn = GetConnection();
        //         cmt.CommandText = sql;
        //         cmt.Connection = conn;
        //         var reader = cmt.ExecuteReader();
        //         while (reader.Read())
        //         {
        //             ZcGoodsMaster zcGoodsMaster = new ZcGoodsMaster();
        //             zcGoodsMaster.Id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
        //             zcGoodsMaster.CreateTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
        //             zcGoodsMaster.UpdateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
        //             zcGoodsMaster.DistributionPrice = reader.IsDBNull(3) ?default(float) : reader.GetFloat(3);
        //             zcGoodsMaster.EarlyWarningDays = reader.IsDBNull(4) ? default(float) : reader.GetFloat(4);
        //             zcGoodsMaster.EarlyWarningDays2 = reader.IsDBNull(5) ? default(float) : reader.GetFloat(5);
        //             zcGoodsMaster.GoodsCode = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
        //             zcGoodsMaster.GoodsName = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
        //             zcGoodsMaster.GoodsOrigin = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
        //             zcGoodsMaster.GoodsPrice = reader.IsDBNull(9) ? default(float) : reader.GetFloat(9);
        //             zcGoodsMaster.GoodsProperty = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
        //             zcGoodsMaster.GoodsPurchasePrice = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
        //             zcGoodsMaster.GoodsSpecifications = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
        //             zcGoodsMaster.GoodsType = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
        //             zcGoodsMaster.GoodsUnit = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
        //             zcGoodsMaster.GrossMargin = reader.IsDBNull(15) ? default(float) : reader.GetFloat(15);
        //             zcGoodsMaster.InputTax = reader.IsDBNull(16) ? default(float) : reader.GetFloat(16);
        //             zcGoodsMaster.JoinRate = reader.IsDBNull(17) ? default(float) : reader.GetFloat(17);
        //             zcGoodsMaster.LowestPrice = reader.IsDBNull(18) ? default(float) : reader.GetFloat(18);
        //             zcGoodsMaster.ManagementInventory = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
        //             zcGoodsMaster.MemberPrice = reader.IsDBNull(20) ? default(float) : reader.GetFloat(20);
        //             zcGoodsMaster.MemberPrice2 = reader.IsDBNull(21) ? default(float) : reader.GetFloat(21);
        //             zcGoodsMaster.MemberPrice3 = reader.IsDBNull(22) ? default(float) : reader.GetFloat(22);
        //             zcGoodsMaster.MemberPrice4 = reader.IsDBNull(23) ? default(float) : reader.GetFloat(23);
        //             zcGoodsMaster.MemberPrice5 = reader.IsDBNull(24) ? default(float) : reader.GetFloat(24);
        //             zcGoodsMaster.OutTax = reader.IsDBNull(25) ? default(float) : reader.GetFloat(25);
        //             zcGoodsMaster.PointOrNot = reader.IsDBNull(26) ? string.Empty : reader.GetString(26);
        //             zcGoodsMaster.PointsValue = reader.IsDBNull(27) ? default(float) : reader.GetFloat(27);
        //             zcGoodsMaster.ProcurementStatus = reader.IsDBNull(28) ? string.Empty : reader.GetString(28);
        //             zcGoodsMaster.PurchaseSpecifications = reader.IsDBNull(29) ? string.Empty : reader.GetString(29);
        //             zcGoodsMaster.Remark = reader.IsDBNull(30) ? string.Empty : reader.GetString(30);
        //             zcGoodsMaster.ValidityPeriod = reader.IsDBNull(31) ? default(float) : reader.GetFloat(31);
        //             zcGoodsMaster.ValucationMethod = reader.IsDBNull(32) ? string.Empty : reader.GetString(32);
        //             zcGoodsMaster.WholeSalePrice = reader.IsDBNull(33) ? default(float) : reader.GetFloat(33);
        //             zcGoodsMaster.WholeSalePrice2 = reader.IsDBNull(34) ? default(float) : reader.GetFloat(34);
        //             zcGoodsMaster.WholeSalePrice3 = reader.IsDBNull(35) ? default(float) : reader.GetFloat(35);
        //             zcGoodsMaster.WholeSalePrice4 = reader.IsDBNull(36) ? default(float) : reader.GetFloat(36);
        //             zcGoodsMaster.WholeSalePrice5 = reader.IsDBNull(37) ? default(float) : reader.GetFloat(37);
        //             zcGoodsMaster.WholeSalePrice6 = reader.IsDBNull(38) ? default(float) : reader.GetFloat(38);
        //             zcGoodsMaster.WholeSalePrice7 = reader.IsDBNull(39) ? default(float) : reader.GetFloat(39);
        //             zcGoodsMaster.WholeSalePrice8 = reader.IsDBNull(40) ? default(float) : reader.GetFloat(40);
        //             zcGoodsMaster.DelFlag = reader.IsDBNull(41) ? string.Empty : reader.GetString(41);
        //             zcGoodsMaster.GoodsBrandId = reader.IsDBNull(42) ? string.Empty : reader.GetString(42);
        //             zcGoodsMaster.GoodsClassId = reader.IsDBNull(43) ? string.Empty : reader.GetString(43);
        //             zcGoodsMaster.GoodsSupplierId = reader.IsDBNull(44) ? string.Empty : reader.GetString(44);
        //             zcGoodsMaster.GoodsState = reader.IsDBNull(45) ? string.Empty : reader.GetString(45);
        //             zcGoodsMaster.SerialNumber = reader.IsDBNull(46) ? string.Empty : reader.GetString(46);
        //             zcGoodsMaster.GoodsDiscountRate = reader.IsDBNull(47) ? default(float) : reader.GetFloat(47);
        //             zcGoodsMaster.GoodsPyCode = reader.IsDBNull(48) ? string.Empty : reader.GetString(48);
        //             zcGoodsMaster.ZcUserInfoId = reader.IsDBNull(49) ? string.Empty : reader.GetString(49);
        //             zcGoodsMaster.Store = reader.IsDBNull(50) ? string.Empty : reader.GetString(50);
        //             zcGoodsMaster.GoodsFileUserId = reader.IsDBNull(51) ? string.Empty : reader.GetString(51);
        //             zcGoodsMaster.GoodsTypeId = reader.IsDBNull(52) ? string.Empty : reader.GetString(52);
        //             zcGoodsMaster.GoodsAttribute = reader.IsDBNull(53) ? string.Empty : reader.GetString(53);
        //             zcGoodsMaster.ProductGoodsId = reader.IsDBNull(54) ? string.Empty : reader.GetString(54);
        //             zcGoodsMaster.WasteRate = reader.IsDBNull(55) ? string.Empty : reader.GetString(55);
                   
        //             list.Add(zcGoodsMaster);

        //         }

        //     }
        //     finally
        //     {
        //         cmt.Dispose();
        //         CloseConnection(conn);
        //     }
        //     return list;
        // }*/
        ///// <summary>
        ///// 根据id删除商品信息 
        ///// </summary>
        ///// <param name="list"></param>
        //public void DeleteRequireItemById(ZcRequire zcRequire)
        //{
        //    MySqlConnection conn = null;
        //    MySqlTransaction tran = null;
        //    MySqlCommand cmd = new MySqlCommand();
        //    string sql = "delete from zc_Require_Item where require_id = @require_id";
        //    try
        //    {
        //        conn = GetConnection();
        //        tran = conn.BeginTransaction();
        //        cmd.CommandText = sql;
        //        cmd.Connection = conn;
        //        cmd.Parameters.AddWithValue("require_id", zcRequire.id);
        //        cmd.ExecuteNonQuery();
        //        tran.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        tran.Rollback();
        //        log.Error("根据订单号删除订单商品信息失败", ex);
        //    }
        //    finally
        //    {
        //        cmd.Dispose();
        //        tran.Dispose();
        //        CloseConnection(conn);
        //    }
        //}
        ///// <summary>
        ///// 根据要货单主表id查询出要货单商品信息表的信息集合
        ///// </summary>
        ///// <returns></returns>
        //public List<ZcRequireItem> findAllRequireItemByRequireId(ZcRequire zcRequire)
        //{

        //    string sql = "select * from Zc_Require_item left join zc_goods_master on Zc_Require_item.goods_file_id =zc_goods_master.id  where require_id = @requireId ";
        //    MySqlConnection conn = null;

        //    MySqlCommand cmt = new MySqlCommand();
        //    List<ZcRequireItem> list = new List<ZcRequireItem>();

        //    try
        //    {

        //        conn = GetConnection();
        //        cmt.CommandText = sql;
        //        cmt.Connection = conn;
        //        cmt.Parameters.AddWithValue("@requireId", zcRequire.id);
        //        var reader = cmt.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            ZcRequireItem zcRequireItem = new ZcRequireItem();
        //            zcRequireItem.id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
        //            zcRequireItem.createTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
        //            zcRequireItem.updateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
        //            zcRequireItem.requireId = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
        //            zcRequireItem.remark = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
        //            zcRequireItem.nums = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
        //            zcRequireItem.money = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
        //            zcRequireItem.goodFileId = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
        //            list.Add(zcRequireItem);


        //        }



        //    }
        //    catch (Exception ex)
        //    {

        //        log.Error("添加出错", ex);

        //    }
        //    finally
        //    {
        //        cmt.Dispose();

        //        CloseConnection(conn);

        //    }
        //    return list;
        //}

        ///// <summary>
        ///// 根据商品信息id删除商品信息
        ///// </summary>
        ///// <param name="zcRequireItem"></param>
        //public void deleteGoodsById(ZcRequireItem zcRequireItem)
        //{
        //    MySqlConnection conn = null;
        //    MySqlTransaction tran = null;
        //    MySqlCommand cmd = new MySqlCommand();
        //    string sql = "delete from zc_Require_Item where id = @id";
        //    try
        //    {
        //        conn = GetConnection();
        //        tran = conn.BeginTransaction();
        //        cmd.CommandText = sql;
        //        cmd.Connection = conn;
        //        cmd.Parameters.AddWithValue("id", zcRequireItem.id);
        //        cmd.ExecuteNonQuery();
        //        tran.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        tran.Rollback();
        //        log.Error("根据商品id删除订单商品信息失败", ex);
        //    }
        //    finally
        //    {
        //        cmd.Dispose();
        //        tran.Dispose();
        //        CloseConnection(conn);
        //    }
        //}

        ///// <summary>
        ///// 根据要货单单号更新要货单信息，（提交）
        ///// </summary>
        ///// <param name="zcRequire"></param>
        //public void updateRequireByYhdNumber(ZcRequire zcRequire)
        //{

        //    string sql = "update  Zc_Require set status ='" + zcRequire.status + "' where yhd_number = '" + zcRequire .yhdNumber+ "'";
            
        //    MySqlConnection conn = null;
        //    MySqlTransaction tran = null;
        //    MySqlCommand cmd = new MySqlCommand();
        //    try
        //    {
        //        conn = GetConnection();
        //        tran = conn.BeginTransaction();
        //        cmd.CommandText = sql;
        //        cmd.Connection = conn;
        //        cmd.ExecuteNonQuery();
        //        tran.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        tran.Rollback();
        //        log.Error("根据订单号更新订单状态发生异常", ex);
        //    }
        //    finally
        //    {
        //        tran.Dispose();
        //        cmd.Dispose();
        //        CloseConnection(conn);
        //    }

        //}

    }
}
