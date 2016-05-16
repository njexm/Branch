using Branch.com.proem.exm.dao.branch;
using Branch.com.proem.exm.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.service.branch
{
    public class BranchPayInfoItemService
    {
        public void AddPayInfoItem(List<PayInfoItem> list) 
        {
            BranchPayInfoItemDao dao = new BranchPayInfoItemDao();
            dao.AddPayInfoItem(list);
        }


        public PayInfoItem FindById(string p)
        {
            BranchPayInfoItemDao dao = new BranchPayInfoItemDao();
            return dao.FindById(p);
        }
    }
}
