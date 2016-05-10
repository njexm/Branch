using Branch.com.proem.exm.dao.master;
using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.service.master
{
    class ZcRequireService : MysqlDBHelper
    {
        ZcRequireDao zcRequireDao = new ZcRequireDao();
        /// <summary>
        /// 添加主订单信息
        /// </summary>
        /// <param name="zcRequire"></param>
        public void addZcRequireInfo(ZcRequire zcRequire)
        {
            zcRequireDao.addZcRequireInfo(zcRequire);
        }

         /// <summary>
        /// 更新要货单信息
        /// </summary>
        /// <param name="zcRequire"></param>
        public void updateRequire(ZcRequire zcRequire)
        {
            zcRequireDao.updateRequire(zcRequire); 
        }
        /// <summary>
        /// 查询所有分店信息
        /// </summary>
        /// <returns></returns>
        public List<ZcBranchTotal> findAllBranchInfo()
        {
            return zcRequireDao.findAllBranchInfo();
        }
        /// <summary>
        /// 根据传进的参数查询分店信息
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public ZcBranchTotal findZcBranchTotalByParams(string conditions)
        {

            return zcRequireDao.findZcBranchTotalById(conditions);


        }
       /// <summary>
       /// 根据要货单单号更新商品信息
       /// </summary>
       /// <param name="zcRequire"></param>
        public void updateRequireByYhdNumber(ZcRequire zcRequire)
        {
            zcRequireDao.updateRequireByYhdNumber(zcRequire);
        }

         /// <summary>
        /// 根据id删除主表信息
        /// </summary>
        public void deleteZcRequireById(ZcRequire zcRequire)
        {
            zcRequireDao.deleteZcRequireById(zcRequire);
        }
        /// <summary>
        /// 根据useid查询ZcUserInfo的id
        /// </summary>
        /// <returns></returns>
        public ZcUserInfo FindByUserId() {
           return zcRequireDao.FindByUserId();
        }

        public void commitCheckedOrder(ZcRequire zcRequire ) {
            zcRequireDao.updateRequireById(zcRequire);
        }
    }
}
