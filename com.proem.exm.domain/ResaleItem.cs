using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.domain
{
    /// <summary>
    /// 销售流水从表
    /// </summary>
    public class ResaleItem
    {
        public string Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 零售主表id
        /// </summary>
        public string ResaleId { get; set; }

        /// <summary>
        /// 商品id
        /// </summary>
        public string GoodsFileId { get; set; }
        
        /// <summary>
        /// 数量
        /// </summary>
        public string Nums { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public string weight { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public string Money { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public string DiscountMoney { get; set; }

        /// <summary>
        /// 实付金额
        /// </summary>
        public string ActualMoney { get; set; }
    }
}
