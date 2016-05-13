using Branch.com.proem.exm.dao.branch;
using Branch.com.proem.exm.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.service.branch
{
    public class BranchResaleService
    {
        public void AddResale(Resale obj) {
            BranchResaleDao dao = new BranchResaleDao();
            dao.AddResale(obj);
        }

        /// <summary>
        /// 根据会员id查询流水数量
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int FindCountByMemberId(string p)
        {
            BranchResaleDao dao = new BranchResaleDao();
            return dao.FindCountByMemberId(p);
        }

        /// <summary>
        /// 根据会员id获取销售流水信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<Resale> FindByMemberId(string p)
        {
            BranchResaleDao dao = new BranchResaleDao();
            return dao.FindByMemberId(p);
        }
    }
}
