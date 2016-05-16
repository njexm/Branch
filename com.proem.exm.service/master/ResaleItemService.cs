using Branch.com.proem.exm.dao.master;
using Branch.com.proem.exm.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.service.master
{
    public class ResaleItemService
    {
        public void AddResaleItem(List<ResaleItem> list) 
        {
            ResaleItemDao dao = new ResaleItemDao();
            dao.AddResaleItem(list);
        }

        public bool AddResaleItemI(ResaleItem item)
        {
            ResaleItemDao dao = new ResaleItemDao();
            return dao.AddResaleItemI(item);
        }
    }
}
