using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.domain
{
    /// <summary>
    /// 要货单信息详细表
    /// </summary>
    public class ZcRequireItem
    {
        /// <summary>
        /// 要货商品信息实体类
        /// </summary>
        
        
            /// <summary>
            /// 要货信息表的id
            /// </summary>
            public string id
            {
                get;
                set;
            }
            /// <summary>
            /// 要货信息表的id
            /// </summary>
            public DateTime createTime
            {
                get;
                set;
            }
            /// <summary>
            /// 要货商品详细表的更新时间
            /// </summary>
            public DateTime updateTime
            {
                get;
                set;
            }
            /// <summary>
            /// 要货表的ID
            /// </summary>
            public string requireId
            {
                get;
                set;
            }
            /// <summary>
            /// 商品信息
            /// </summary>
            public string goodsFileId
            {
                get;
                set;
            }
            /// <summary>
            /// 商品数量
            /// </summary>
            public string nums
            {
                get;
                set;
            }

            /// <summary>
            /// 商品价格
            /// </summary>
            public string money
            {
                get;
                set;
            }
            /// <summary>
            /// 商品备注
            /// </summary>
            public string remark
            {
                get;
                set;
            }
        

    }
}