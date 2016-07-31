using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.domain
{
    public class DispatchingWarehouseItem
    {
        public string id { get; set; }

        public DateTime createTime { get; set; }

        public DateTime updateTime { get; set; }

        public string dispatchingWarehouseId { get; set; }

        public string goods_name { get; set; }

        public string serialNumber { get; set; }

        public string branch_total_id { get; set; }

        public DateTime cash_date { get; set; }

        public string weight { get; set; }

        public string goodsPrice { get; set; }

        public string money { get; set; }

        public string goods_specifications { get; set; }

        public string goodsFile_id { get; set; }

        public string nums { get; set; }

    }
}
