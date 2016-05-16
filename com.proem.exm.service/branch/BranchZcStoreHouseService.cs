using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.dao.branch;

namespace Branch.com.proem.exm.service.branch
{
    /// <summary>
    /// 本地库存Service
    /// </summary>
    public class BranchZcStoreHouseService
    {
        public void AddZcStoreHouse(List<ZcStoreHouse> list)
        {
            BranchZcStoreHouseDao dao = new BranchZcStoreHouseDao();
            dao.AddZcStoreHouse(list);
        }

        public void DeleteAll()
        {
            BranchZcStoreHouseDao dao = new BranchZcStoreHouseDao();
            dao.DeleteAll("zc_storehouse");
        }

        /// <summary>
        /// 根据商品id和分店id获取该商品对应分店库存
        /// </summary>
        /// <param name="goodsFileId">商品id</param>
        /// <param name="p">分店id</param>
        /// <returns></returns>
        public ZcStoreHouse FindByGoodsFileIdAndBranchId(string goodsFileId, string p)
        {
            BranchZcStoreHouseDao dao = new BranchZcStoreHouseDao();
            return dao.FindByGoodsFileIdAndBranchId(goodsFileId, p);
        }

        /// <summary>
        /// 更新库存信息
        /// </summary>
        /// <param name="storeList"></param>
        public void updateStoreHouse(List<ZcStoreHouse> storeList)
        {
            BranchZcStoreHouseDao dao = new BranchZcStoreHouseDao();
            dao.updateStoreHouse(storeList);
        }

        public ZcStoreHouse FindById(string p)
        {
            BranchZcStoreHouseDao dao = new BranchZcStoreHouseDao();
            return dao.FindById(p);
        }
    }
}
