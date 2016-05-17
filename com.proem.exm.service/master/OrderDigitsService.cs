using Branch.com.proem.exm.dao.master;
using Branch.com.proem.exm.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.service.master
{
    public class OrderDigitsService
    {
        public List<OrderDigits> FindAll()
        {
            OrderDigitsDao dao = new OrderDigitsDao();
            return dao.FindAll();
        }
    }
}
