using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Branch.com.proem.exm.domain
{
    /// <summary>
    /// 支付信息主表
    /// </summary>
    public class PayInfo
    {
        public string Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 会员卡号
        /// </summary>
        public string MemberId { get; set; }

        /// <summary>
        /// 付款金额
        /// </summary>
        public string Money { get; set; }

        /// <summary>
        /// 分店
        /// </summary>
        public string BranchId { get; set; }

        /// <summary>
        /// 业务员
        /// </summary>
        public string salesmanId { get; set; }

    }
}
