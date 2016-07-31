using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.domain
{
    /// <summary>
    /// 亭点入库单
    /// </summary>
    public class BranchIn
    {
        public string id { get; set;}

        public DateTime createTime { get; set; }

        public DateTime updateTime { get; set; }

        /// <summary>
        /// 入库单号
        /// </summary>
        public string InOdd { get; set; }

        /// <summary>
        /// 配送出库单id
        /// </summary>
        public string dispatching_id { get; set; }

        public string nums { get; set; }

        public string weight { get; set; }

        public string money { get; set; }

        /// <summary>
        /// 分店Id
        /// </summary>
        public string branch_id { get; set; }

        /// <summary>
        /// 来货分店
        /// </summary>
        public string branch_from_id { get; set; }

        /// <summary>
        /// 制单人
        /// </summary>
        public string user_id { get; set; }
    }
}
