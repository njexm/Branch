using Branch.com.proem.exm.dao.branch;
using Branch.com.proem.exm.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Branch.com.proem.exm.service.branch
{
    class BranchZcGoodsMasterService
    {
        public void AddZcGoodsMaster(List<ZcGoodsMaster> list)
        {
            BranchZcGoodsMasterDao dao = new BranchZcGoodsMasterDao();
            dao.AddZcGoodsMaster(list);
        }

        public void DeleteAll()
        {
            BranchZcGoodsMasterDao dao = new BranchZcGoodsMasterDao();
            dao.DeleteAll("zc_goods_master");
        }

        /// <summary>
        /// 根据货号获取商品信息
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public ZcGoodsMaster FindBySerialNumber(string serial)
        {
            BranchZcGoodsMasterDao dao = new BranchZcGoodsMasterDao();
            return dao.FindBySerialNumber(serial);
        }

        /// <summary>
        /// 判断商品是不是称重商品
        /// </summary>
        /// <param name="id">商品id</param>
        /// <returns></returns>
        public bool IsWeightGoods(string id)
        {
            BranchZcGoodsMasterDao dao = new BranchZcGoodsMasterDao();
            return dao.IsWeightGoods(id);
        }
    }
}
