using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.domain
{
    /// <summary>
    /// 分店配送收货差异单明细
    /// </summary>
    public class BranchDiffItem
    {
        public string id { get; set; }

        public string branchDiff_id { get; set; }

        public DateTime createTime { get; set; }

        public DateTime updateTime { get; set; }

        public string nums { get; set; }

        public string weight { get; set; }

        public string money { get; set; }

        public string goodsFile_id { get; set; }

        /// <summary>
        /// 配送单价
        /// </summary>
        public string price { get; set; }
    }
}
