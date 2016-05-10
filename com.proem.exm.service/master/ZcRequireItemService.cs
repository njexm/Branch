using Branch.com.proem.exm.dao.master;
using Branch.com.proem.exm.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.service.master
{
    class ZcRequireItemService
    {
        ZcRequireItemDao zcRequireItemDao = new ZcRequireItemDao();
        /// <summary>
        /// 添加要货单详细信息表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool addZcRequireItem(List<ZcRequireItem> list)
        {
            if(list!=null){
                zcRequireItemDao.addZcRequireItem(list);
                return true;
            }
            return false;
            
        }

         /// <summary>
        /// 添加单个的商品信息
        /// </summary>
        public void addOneZcRequireItem(ZcRequireItem obj)
        {
            zcRequireItemDao.addOneZcRequireItem(obj);
        }

         /// <summary>
        /// 根据要货单id删除商品信息 
        /// </summary>
        /// <param name="list"></param>
        public void DeleteRequireItemById(ZcRequire zcRequire)
        {
            zcRequireItemDao.DeleteRequireItemById(zcRequire);
        }

         /// <summary>
        /// 根据要货单主表id查询出要货单商品信息表的信息集合
        /// </summary>
        /// <returns></returns>
        public List<ZcRequireItem> findAllRequireItemByRequireId(ZcRequire zcRequire)
        {
            return zcRequireItemDao.findAllRequireItemByRequireId(zcRequire);
        }

         /// <summary>
        /// 根据商品信息id删除商品信息
        /// </summary>
        /// <param name="zcRequireItem"></param>
        public void deleteGoodsById(ZcRequireItem zcRequireItem)
        {
            zcRequireItemDao.deleteGoodsById(zcRequireItem);
        }

        /// <summary>
        /// 商品信息的更新
        /// </summary>
        /// <param name="zcRequireItem"></param>
        public bool updateZcRequireItem(List<ZcRequireItem> list)
        {   
           bool addSuccess =  zcRequireItemDao.updateZcRequireItem(list);

           return addSuccess;
            
        }
    }
}
