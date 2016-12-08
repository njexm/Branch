using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.domain
{
    /// <summary>
    /// 数据字典
    /// </summary>
    public class ZcCode
    {
        public string id { get; set; }

        public DateTime createTime { get; set; }

        public DateTime updateTime { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string codeName { get; set; }

        /// <summary>
        /// 类型      
        /// </summary>
        public string codeType { get; set; }

        public string codeValue { get; set; }

        public string codeDescription { get; set; }

        /// <summary>
        /// 上级单位
        /// </summary>
        public string parent { get; set; }

        /// <summary>
        /// 状态
        /// 默认关闭 closed
        /// </summary>
        public string state { get; set; }
    }
}
