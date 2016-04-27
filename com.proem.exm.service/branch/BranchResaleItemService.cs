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
        public void AddResale(List<ResaleItem> list) 
        {
            BranchResaleItemDao dao = new BranchResaleItemDao();
            dao.AddResaleItem(list);
        }

    }
}
