using Branch.com.proem.exm.dao.branch;
using Branch.com.proem.exm.dao.master;
using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.service;
using Branch.com.proem.exm.service.branch;
using Branch.com.proem.exm.service.master;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Branch.com.proem.exm.util
{
    /// <summary>
    /// 数据断网上传
    /// </summary>
    public class DataUploadTask
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DataUploadTask));

        public const int SLEEPINGTIME = 1000;

        public static void Run()
        {
            while (true)
            { 
                ///数据上传
                if (PingTask.IsConnected)
                {
                    Upload();
                }
                ///线程sleep
                Thread.Sleep(SLEEPINGTIME);

            }
        }

        /// <summary>
        /// 网络连通时，上传数据
        /// </summary>
        public static void Upload()
        {
            UploadDao dao = new UploadDao();
            int count = dao.QueryCount();
            if(count != 0)
            {
                List<UploadInfo> list = dao.FindAll();
                foreach(UploadInfo obj in list)
                {
                    //if(obj.Type == Constant.DAILY_RECEIVE_GOODS)
                    //{
                    //    BranchDailyReceiveGoodsDao dailyDao = new BranchDailyReceiveGoodsDao();
                    //    DailyReceiveGoods drg = dailyDao.FindById(obj.Id);
                    //    drg.CreateTime = DateTime.Now;
                    //    drg.UpdateTime = DateTime.Now;
                    //    DailyReceiveGoodsService service = new DailyReceiveGoodsService();
                    //    bool flag = service.AddDailyReceiveGoods(drg);
                    //    if(flag)
                    //    {
                    //        dao.DeleteByIdAndType(obj.Id, obj.Type);
                    //    }
                    //}
                    //else 
                    if (obj.Type == Constant.ZC_ORDER_TRANSIT_UPDATE)
                    {
                        ZcOrderTransitService service = new ZcOrderTransitService();
                        bool flag = service.UpdateStatus(obj.Id, Constant.ORDER_STATUS_RECEIPT);
                        if(flag)
                        {
                            dao.DeleteByIdAndType(obj.Id, obj.Type);
                        }
                    }
                    else if (obj.Type == Constant.PAY_INFO)
                    {
                        BranchPayInfoService service = new BranchPayInfoService();
                        PayInfo payInfo = service.FindById(obj.Id);
                        PayInfoService masterService = new PayInfoService();
                        bool flag = masterService.AddPayInfoI(payInfo);
                        if (flag)
                        {
                            dao.DeleteByIdAndType(obj.Id, obj.Type);
                        }
                    }
                    else if(obj.Type == Constant.PAY_INFO_ITEM){
                        BranchPayInfoItemService branchservice = new BranchPayInfoItemService();
                        PayInfoItem item = branchservice.FindById(obj.Id);
                        PayInfoItemService service = new PayInfoItemService();
                        bool flag = service.AddPayInfoItemI(item);
                        if (flag)
                        {
                            dao.DeleteByIdAndType(obj.Id, obj.Type);
                        }
                    }
                    else if (obj.Type == Constant.ZC_ORDER_HISTORY)
                    {
                        BranchZcOrderHistoryService branchService = new BranchZcOrderHistoryService();
                        ZcOrderHistory history = branchService.FindById(obj.Id);
                        ZcOrderHistoryService service = new ZcOrderHistoryService();
                        bool flag = service.AddZcOrderHistoryI(history);
                        if (flag)
                        {
                            dao.DeleteByIdAndType(obj.Id, obj.Type);
                        }
                    }
                    else if (obj.Type == Constant.ZC_ORDER_HISTORY_ITEM)
                    {
                        BranchZcOrderHistoryItemService branchService = new BranchZcOrderHistoryItemService();
                        ZcOrderHistoryItem item = branchService.FindById(obj.Id);
                        ZcOrderHistoryItemService service = new ZcOrderHistoryItemService();
                        bool flag = service.AddZcOrderHistoryItemI(item);
                        if (flag)
                        {
                            dao.DeleteByIdAndType(obj.Id, obj.Type);
                        }
                    }
                    else if (obj.Type == Constant.ZC_ORDER_TRANSIT_DELETE)
                    {
                        ZcOrderTransitService service = new ZcOrderTransitService();
                        bool flag = service.DeleteById(obj.Id);
                        if(flag)
                        {
                            dao.DeleteByIdAndType(obj.Id, obj.Type);
                        }
                    }
                    else if (obj.Type == Constant.ZC_ORDER_TRANSIT_ITEM_DELETE)
                    {
                        ZcOrderTransitItemService service = new ZcOrderTransitItemService();
                        bool flag = service.DeleteByOrderIdI(obj.Id);
                        if (flag)
                        {
                            dao.DeleteByIdAndType(obj.Id, obj.Type);
                        }
                    }
                    else if (obj.Type == Constant.ZC_ORDER_REFUSE)
                    {
                        BranchZcOrderRefuseService branchService = new BranchZcOrderRefuseService();
                        ZcOrderRefuse refuse = branchService.FindById(obj.Id);
                        ZcOrderRefuseService service = new ZcOrderRefuseService();
                        bool flag = service.AddZcOrderRefuseI(refuse);
                        if (flag)
                        {
                            dao.DeleteByIdAndType(obj.Id, obj.Type);
                        }
                    }
                    else if (obj.Type == Constant.ZC_ORDER_REFUSE_ITEM)
                    {
                        BranchZcOrderRefuseItemService branchService = new BranchZcOrderRefuseItemService();
                        ZcOrderRefuseItem item = branchService.FindById(obj.Id);
                        ZcOrderRefuseItemService service = new ZcOrderRefuseItemService();
                        bool flag = service.AddZcOrderRefuseItemI(item);
                        if (flag)
                        {
                            dao.DeleteByIdAndType(obj.Id, obj.Type);
                        }
                    }
                    else if (obj.Type == Constant.ZC_ORDER_REFUND)
                    {
                        BranchZcOrderRefundService branchService = new BranchZcOrderRefundService();
                        ZcOrderRefund refund = branchService.FindById(obj.Id);
                        RefundInfoService service = new RefundInfoService();
                        bool flag = service.AddZcOrderRefundI(refund);
                        if (flag)
                        {
                            dao.DeleteByIdAndType(obj.Id, obj.Type);
                        }
                    }
                    else if (obj.Type == Constant.ZC_ORDER_REFUND_ITEM)
                    {
                        BranchZcOrderRefundItemService branchService = new BranchZcOrderRefundItemService();
                        ZcOrderRefundItem item = branchService.FindById(obj.Id);
                        RefundInfoService service = new RefundInfoService();
                        bool flag = service.AddZcOrderRefundItemI(item);
                        if (flag)
                        {
                            dao.DeleteByIdAndType(obj.Id, obj.Type);
                        }
                    }
                    else if (obj.Type == Constant.ZC_ORDER_HISTORY_UPDATE)
                    {
                        BranchZcOrderHistoryService branchService = new BranchZcOrderHistoryService();
                        ZcOrderHistory history = branchService.FindById(obj.Id);
                        ZcOrderHistoryService service = new ZcOrderHistoryService();
                        bool flag = service.UpdateOrderStatusByIdsI(obj.Id, history.OrderStatus);
                        if (flag)
                        {
                            dao.DeleteByIdAndType(obj.Id, obj.Type);
                        }
                    }
                    else if(obj.Type == Constant.ZC_RESALE){
                        BranchResaleService branchService = new BranchResaleService();
                        Resale resale = branchService.FindById(obj.Id);
                        ResaleService service = new ResaleService();
                        bool flag = service.AddResaleI(resale);
                        if (flag)
                        {
                            dao.DeleteByIdAndType(obj.Id, obj.Type);
                        }
                    }
                    else if(obj.Type == Constant.ZC_RESALE_ITME){
                        BranchResaleItemService branchService = new BranchResaleItemService();
                        ResaleItem item = branchService.FindById(obj.Id);
                        ResaleItemService service = new ResaleItemService();
                        bool flag = service.AddResaleItemI(item);
                        if (flag)
                        {
                            dao.DeleteByIdAndType(obj.Id, obj.Type);
                        }
                    }
                    else if(obj.Type == Constant.ZC_STORE_HOSUE){
                        BranchZcStoreHouseService branchService = new BranchZcStoreHouseService();
                        ZcStoreHouse house = branchService.FindById(obj.Id);
                        ZcStoreHouseService service = new ZcStoreHouseService();
                        bool flag = service.AddZcStoreHouseI(house);
                        if (flag)
                        {
                            dao.DeleteByIdAndType(obj.Id, obj.Type);
                        }
                    }
                    else if(obj.Type == Constant.ZC_STOREHOUSE_UPDATE){
                        BranchZcStoreHouseService branchService = new BranchZcStoreHouseService();
                        ZcStoreHouse house = branchService.FindById(obj.Id);
                        ZcStoreHouseService service = new ZcStoreHouseService();
                        bool flag = service.UpdateStoreHouseI(house);
                        if (flag)
                        {
                            dao.DeleteByIdAndType(obj.Id, obj.Type);
                        }
                    }
                    else if(obj.Type == Constant.ZC_BRANCH_IN){
                        BranchInDao branchdao = new BranchInDao();
                        BranchIn branchIn = branchdao.FindById(obj.Id);
                        MasterBranchInDao masterDao = new MasterBranchInDao();
                        bool flag = masterDao.addObjI(branchIn);
                        if (flag)
                        {
                            dao.DeleteByIdAndType(obj.Id, obj.Type);
                        }
                    }
                    else if (obj.Type == Constant.ZC_BRANCH_IN_ITEM)
                    {
                        BranchInItemDao branchDao = new BranchInItemDao();
                        BranchInItem item = branchDao.FindById(obj.Id);
                        MasterBranchInItemDao masterItemDao = new MasterBranchInItemDao();
                        bool flag = masterItemDao.addObj(item);
                        if (flag)
                        {
                            dao.DeleteByIdAndType(obj.Id, obj.Type);
                        }
                    }
                    else if (obj.Type == Constant.ZC_BRANCH_DIFF)
                    {
                        BranchDiffDao branchDao = new BranchDiffDao();
                        BranchDiff diff = branchDao.FindById(obj.Id);
                        MasterBranchDiffDao matserDao = new MasterBranchDiffDao();
                        bool flag = matserDao.addObjI(diff);
                        if (flag)
                        {
                            dao.DeleteByIdAndType(obj.Id, obj.Type);
                        }
                    }
                    else if (obj.Type == Constant.ZC_BRANCH_DIFF_ITEM)
                    {
                        BranchDiffItemDao branchDao = new BranchDiffItemDao();
                        BranchDiffItem item = branchDao.FindById(obj.Id);
                        MasterBranchDiffItemDao masterDao = new MasterBranchDiffItemDao();
                        bool flag = masterDao.addObj(item);
                        if (flag)
                        {
                            dao.DeleteByIdAndType(obj.Id, obj.Type);
                        }
                    }

                }
            }
        }
    }
}
