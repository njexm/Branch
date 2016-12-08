using Branch.com.proem.exm.dao.branch;
using Branch.com.proem.exm.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Branch.com.proem.exm.util
{
    /// <summary>
    /// 促销方案工具类
    /// </summary>
    public class SalesPromotionUtil
    {
        public static List<Promotion> promotionList = new List<Promotion>();

        /// <summary>
        /// 初始化加载促销方案
        /// </summary>
        public static void initPromotion(ZcMember zcMember)
        {
            promotionList.Clear();
            string day = "0"; 
            DayOfWeek dayOfWeek = DateTime.Now.DayOfWeek;
            if (DayOfWeek.Sunday.Equals(dayOfWeek))
            {
                day = "7";
            }
            else 
            {
                day = ((int)dayOfWeek).ToString();
            }
            BranchPromotionDao promotionDao = new BranchPromotionDao();
            promotionList = promotionDao.queryListBy(day, zcMember);
        }

        /// <summary>
        /// 获取促销信息
        /// </summary>
        /// <returns></returns>
        public static List<PromotionItem> getPromotionItem(string goodsFileId)
        {
            BranchPromotionItemDao branchDao = new BranchPromotionItemDao();
            List<PromotionItem> list = new List<PromotionItem>();
            if (promotionList != null && promotionList.Count > 0)
            {
                for (int i = 0; i < promotionList.Count; i++ )
                {
                    Promotion promotion = promotionList[i];
                    List<PromotionItem> itemList = branchDao.FindBy(promotion.id, goodsFileId);
                    if(itemList != null && itemList.Count > 0){
                        list.AddRange(itemList);
                    }
                }
            }
            return list;
        }
    }
}
