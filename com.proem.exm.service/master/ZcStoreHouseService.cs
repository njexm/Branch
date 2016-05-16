using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.dao.master;

namespace Branch.com.proem.exm.service.master
{
    public class ZcStoreHouseService
    {
        public List<ZcStoreHouse> FindByBranchId()
        {
            ZcStoreHouseDao dao = new ZcStoreHouseDao();
            return dao.FindByBranchId();
        }

        /// <summary>
        /// 根据收货信息查询库存中是否存在改商品
        /// </summary>
        /// <returns></returns>
        public ZcStoreHouse FindByBranchIdAndGoodFilesId(ZcRequireItem zcRequireItem)
        {
            ZcStoreHouseDao dao = new ZcStoreHouseDao();
            return dao.FindByBranchIdAndGoodFilesId(zcRequireItem);
        }

         /// <summary>
        /// 添加确认收货到库存
        /// </summary>
        /// <param name="obj"></param>
        public void AddZcStoreHouse(ZcStoreHouse obj) {

            ZcStoreHouseDao dao = new ZcStoreHouseDao();
            dao.AddZcStoreHouse(obj);
        }

         /// <summary>
        /// 如果查询到存在，更新库存量 和库存的价格，更新时间（状态是什么鬼，不知道要不要更新）
        /// </summary>
        /// <param name="obj"></param>
        public void updateZcStoreHouse(ZcStoreHouse obj) {
            ZcStoreHouseDao dao = new ZcStoreHouseDao();
            dao.updateZcStoreHouse(obj);
        }

        /// <summary>
        /// 更新库存
        /// 上传
        /// </summary>
        /// <param name="storeList"></param>
        public void updateStoreHouse(List<ZcStoreHouse> storeList)
        {
            ZcStoreHouseDao dao = new ZcStoreHouseDao();
            dao.updateZcStoreHouse(storeList);
        }

        public bool AddZcStoreHouseI(ZcStoreHouse obj)
        {
            ZcStoreHouseDao dao = new ZcStoreHouseDao();
            return dao.AddZcStoreHouseI(obj);
        }

        public bool UpdateStoreHouseI(ZcStoreHouse house)
        {
            ZcStoreHouseDao dao = new ZcStoreHouseDao();
            return dao.UpdateStoreHouseI(house);
        }

        public void AddZcStoreHouseII(ZcStoreHouse obj)
        {
            ZcStoreHouseDao dao = new ZcStoreHouseDao();
             dao.AddZcStoreHouseII(obj);
        }
    }
}
