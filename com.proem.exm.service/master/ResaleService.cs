using Branch.com.proem.exm.dao.master;
using Branch.com.proem.exm.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.service.master
{
    public class ResaleService
    {
        public void AddResale(Resale obj)
        {
            ResaleDao dao = new ResaleDao();
            dao.AddResale(obj);
        }

        public bool AddResaleI(Resale resale)
        {
            ResaleDao dao = new ResaleDao();
            return dao.AddResaleI(resale);
        }
    }
}
