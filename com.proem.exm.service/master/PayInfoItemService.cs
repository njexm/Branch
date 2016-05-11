using Branch.com.proem.exm.dao.master;
using Branch.com.proem.exm.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.service.master
{
    public class PayInfoItemService
    {

        public void AddPayInfoItem(List<PayInfoItem> list)
        {
            PayInfoItemDao dao = new PayInfoItemDao();
            dao.AddPayInfoItem(list);
        }

        public bool AddPayInfoItemI(PayInfoItem obj) 
        {
            PayInfoItemDao dao = new PayInfoItemDao();
            return dao.AddPayInfoItemI(obj);
        }
    }
}
