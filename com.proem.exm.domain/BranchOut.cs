using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.domain
{
    /// <summary>
    /// 亭点出库单
    /// </summary>
    public class BranchOut
    {

        public string id { get; set; }

        public DateTime createTime { get; set; }

        public DateTime updateTime { get; set; }

        /// <summary>
        /// 出库单号
        /// </summary>
        public string OutOdd { get; set; }

        /// <summary>
        /// 差异单id
        /// </summary>
        public string branchDiff_id { get; set; }

        public string nums { get; set; }

        public string weight { get; set; }

        public string money { get; set; }

        /// <summary>
        /// 出库分店Id
        /// </summary>
        public string branch_id { get; set; }

        /// <summary>
        /// 发往分店Id
        /// </summary>
        public string branch_to_id { get; set; }

        /// <summary>
        /// 制单人
        /// </summary>
        public string user_id { get; set; }

        /// <summary>
        /// 亭点出库单状态
        /// </summary>
        public string status { get; set; }

        public string remark { get; set; }
    }
}
