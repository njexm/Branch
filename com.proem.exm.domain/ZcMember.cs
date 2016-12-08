using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.domain
{
    /// <summary>
    /// 会员
    /// </summary>
    public class ZcMember
    {
        public string id { get; set; }

        public DateTime createTime { get; set; }

        public DateTime updateTime { get; set; }

        /// <summary>
        /// 消费次数
        /// </summary>
        public string costTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime lastCostDate { get; set; }

        /// <summary>
        /// 剩余积分
        /// </summary>
        public string leftScore { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string memberSex { get; set; }

        /// <summary>
        /// 会员地址
        /// </summary>
        public string memberAddress { get; set; }

        /// <summary>
        /// 会员生日
        /// </summary>
        public DateTime memberBirthDay { get; set; }

        /// <summary>
        /// 等级门店
        /// </summary>
        public string memberBranch { get; set; }

        /// <summary>
        /// 会员邮箱
        /// </summary>
        public string memberEmail { get; set; }

        /// <summary>
        /// 会员身份证
        /// </summary>
        public string memberIdfine { get; set; }

        /// <summary>
        /// 会员等级
        /// </summary>
        public string memberLevel { get; set; }

        /// <summary>
        /// 会员电话
        /// </summary>
        public string memberMobile { get; set; }

        /// <summary>
        /// 会员名
        /// </summary>
        public string memberName { get; set; }

        /// <summary>
        /// 会员手机
        /// </summary>
        public string memberPhone { get; set; }

        /// <summary>
        /// 积分有效期
        /// </summary>
        public DateTime scoreDate { get; set; }

        /// <summary>
        /// 累计消费
        /// </summary>
        public string totalCost { get; set; }

        /// <summary>
        /// 总积分
        /// </summary>
        public string totalScore { get; set; }

        /// <summary>
        /// 会员姓名
        /// </summary>
        public string tureName { get; set; }

        /// <summary>
        /// 已用积分
        /// </summary>
        public string usedScore { get; set; }
    }
}
