using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.domain
{
    /// <summary>
    /// 实体类 对应zc_orders_digits
    /// </summary>
    public class OrderDigits
    {
        public string Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public string CountOdsi { get; set; }

        public string DigitsAmount { get; set; }

        public string MoneyAccuracy { get; set; }

        public string orderAmount { get; set; }

        public string Type { get; set; }
    }
}
