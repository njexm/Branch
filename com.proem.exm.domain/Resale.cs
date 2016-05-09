using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.domain
{
    /// <summary>
    /// 零售信息
    /// </summary>
    public class Resale
    {
        public string Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        
        //数量
        public string Nums { get; set; }
        //合计金额
        public string Money { get; set; }

        /// <summary>
        /// 折扣金额
        /// </summary>
        public string DiscountMoney { get; set; }

        /// <summary>
        /// 折让金额
        /// </summary>
        public string PreferentialMoney { get; set; }

        /// <summary>
        /// 实付金额
        /// </summary>
        public string ActualMoney { get; set; }

        //分店Id
        public string BranchId { get; set; }

        /// <summary>
        /// 业务员
        /// </summary>
        public string SaleManId { get; set; }

        /// <summary>
        /// 会员信息
        /// </summary>
        public string memberId { get; set; }

        /// <summary>
        /// 订单号
        /// null 代表零售流水
        /// !null 订单提货
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 流水单号 格式：LS + yyyyMMddhhmmss + street
        /// </summary>
        public string WaterNumber { get; set; }
    }
}
