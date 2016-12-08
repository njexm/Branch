using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.domain
{
    /// <summary>
    /// 促销主表
    /// </summary>
    public class Promotion
    {
        public string id { get; set; }

        public DateTime createTime { get; set; }

        public DateTime updateTime { get; set; }

        public string branchIds { get; set; }

        public DateTime check_date { get; set; }

        public string check_man { get; set; }

        public int check_state { get; set; }

        public string create_man { get; set; }
        /// <summary>
        /// 会员等级
        /// </summary>
        public string member_level { get; set; }
        /// <summary>
        /// 促销开始时间
        /// </summary>
        public DateTime promotionBeginDate { get; set; }
        /// <summary>
        /// 促销时段
        /// </summary>
        public string promotion_days { get; set; }
        /// <summary>
        /// 促销结束时间
        /// </summary>
        public DateTime promotionEndDate { get; set; }
        /// <summary>
        /// 促销编号
        /// </summary>
        public string promotion_number { get; set; }

        public string promotion_remark { get; set; }

        public string promotion_title{get;set;}
        /// <summary>
        /// 促销结束时间
        /// </summary>
        public DateTime stop_date { get; set; }

        public string stop_man { get; set; }
        /// <summary>
        /// 促销模式
        /// </summary>
        public string zccode_modeId { get; set; }
        /// <summary>
        /// 促销范围
        /// </summary>
        public string zccode_scopeId { get; set; }
    }
}
