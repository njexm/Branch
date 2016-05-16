using Branch.com.proem.exm.dao.branch;
using Branch.com.proem.exm.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.service.branch
{
    public class BranchResaleItemService
    {
        public void AddResaleItem(List<ResaleItem> list) 
        {
            BranchResaleItemDao dao = new BranchResaleItemDao();
            dao.AddResaleItem(list);
        }

        /// <summary>
        /// 根据id查询流水销售明细
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public ResaleItem FindById(string p)
        {
            BranchResaleItemDao dao = new BranchResaleItemDao();
            return dao.FindById(p);
        }
    }
}
