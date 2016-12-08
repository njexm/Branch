using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.domain
{
    /// <summary>
    /// 促销从表
    /// </summary>
    public class PromotionItem
    {
        public string id { get; set; }

        public DateTime createTime { get; set; }

        public DateTime updateTime { get; set; }
        /// <summary>
        /// 增加金额
        /// </summary>
        public float add_money { get; set; }

        public string add_more_money { get; set; }
        /// <summary>
        /// 全场折扣
        /// </summary>
        public string all_discount { get; set; }
        /// <summary>
        /// 特价
        /// </summary>
        public float bargain_price { get; set; }
        /// <summary>
        /// 开始时间段
        /// </summary>
        public DateTime begin_time_frame { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public string discount { get; set; }
        /// <summary>
        /// 结束时间段
        /// </summary>
        public DateTime end_time_frame { get; set; }
        /// <summary>
        /// 赠送商品ids
        /// </summary>
        public string free_goodsIds { get; set; }
        /// <summary>
        /// 买满数量
        /// </summary>
        public float full_buy_count { get; set; }
        /// <summary>
        /// 买满金额
        /// </summary>
        public float full_buy_money { get; set; }

        /// <summary>
        /// 组号
        /// </summary>
        public string group_number { get; set; }
        /// <summary>
        /// 每单限量
        /// </summary>
        public float limit_number { get; set; }

        public string nums { get; set; }
        /// <summary>
        /// 减少金额
        /// </summary>
        public float reduce_money { get; set; }

        public string salespromotion_id { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string brand_classify_id { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string class_classify_id { get; set; }
      
        public string free_goodsId { get; set; }
        /// <summary>
        /// 商品信息
        /// </summary>
        public string goodsFile_id { get; set; }
    }
}
