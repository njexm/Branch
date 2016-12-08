using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.util
{
    /// <summary>
    /// 打印的商品信息
    /// </summary>
    public class GoodsPrint
    {
        /// <summary>
        /// 货号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public float Nums { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 条形码
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// 商品id
        /// </summary>
        public string GoodsFileId { get; set; }

        /// <summary>
        /// 是否是促销商品
        /// true 是
        /// false 否
        /// </summary>
        public bool isPromotion { get; set; }

        /// <summary>
        /// 特价价格
        /// </summary>
        public float p_price { get; set; }

        /// <summary>
        /// 特价数量
        /// </summary>
        public float p_nums { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public float money { get; set; }



    }
}
