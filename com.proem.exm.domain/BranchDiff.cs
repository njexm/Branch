using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.domain
{
    /// <summary>
    /// 分店配送收货差异单
    /// </summary>
    public class BranchDiff
    {
        public string id { get; set; }

        public DateTime createTime { get; set; }

        public DateTime updateTime { get; set; }

        /// <summary>
        /// 差异单号
        /// </summary>
        public string DiffOdd { get; set; }

        /// <summary>
        /// 分店入库单id
        /// </summary>
        public string branchIn_id { get; set; }

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
