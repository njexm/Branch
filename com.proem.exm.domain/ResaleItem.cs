using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.domain
{
    /// <summary>
    /// 零售从表
    /// </summary>
    public class ResaleItem
    {
        public string Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 商品id
        /// </summary>
        public string GoodsFileId { get; set; }
        /// <summary>
        /// 零售主表id
        /// </summary>
        public string ResaleId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string Nums { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public string Money { get; set; }
    }
}
