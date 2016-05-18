using Branch.com.proem.exm.dao.branch;
using Branch.com.proem.exm.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.service.branch
{
    public class BranchOrderDigitsService
    {
        public void AddDigits(List<OrderDigits> list)
        {
            BranchOrderDigitsDao dao = new BranchOrderDigitsDao();
            dao.AddOrderDigits(list);
        }

        /// <summary>
        /// 获取精度
        /// </summary>
        /// <returns></returns>
        public OrderDigits FindDigits()
        {
            BranchOrderDigitsDao dao = new BranchOrderDigitsDao();
            return dao.GetDigits();
        }

        internal void DeleteAll()
        {
            BranchOrderDigitsDao dao = new BranchOrderDigitsDao();
            dao.DeleteAll("zc_order_digits");
        }
    }
}
