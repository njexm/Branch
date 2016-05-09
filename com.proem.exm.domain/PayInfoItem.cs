using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.domain
{
    /// <summary>
    /// 支付明细
    /// </summary>
    public class PayInfoItem
    {
        public string Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 支付主表id
        /// </summary>
        public string PayInfoId { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string PayMode { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public string Money { get; set; }
    }
}
