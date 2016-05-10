using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.domain
{   
    /// <summary>
    /// 订单信息表
    /// </summary>
    public class ZcRequire
    {
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
        /// 单号
        /// </summary>
        public string yhdNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 状态是否被审核
        /// </summary>
        public int status
        {
            get;
            set;
        }
        /// <summary>
        /// 用户的编号
        /// </summary>
        public string userId
        {
            get;
            set;
        }
        /// <summary>
        /// 商品总数量
        /// </summary>
        public string nums
        {
            get;
            set;
        }

        /// <summary>
        /// 商品总计价格
        /// </summary>
        public string money
        {
            get;
            set;
        }
        /// <summary>
        /// 审核人
        /// </summary>
        public string checkMan
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark
        {
            get;
            set;
        }
        /// <summary>
        /// 检查时间
        /// </summary>
        public Nullable<DateTime> checkDate
        {
            get;
            set;
        }
        /// <summary>
        /// 分店信息
        /// </summary>
        public string branchId
        {
            get;
            set;
        }
        /// <summary>
        /// 仓库分店信息
        /// </summary>
        public string calloutBranchId
        {
            get;
            set;
        }
        /// <summary>
        /// 审核不通过的原因
        /// </summary>
        public string reason
        {
            get;
            set;
        }
    }
}
