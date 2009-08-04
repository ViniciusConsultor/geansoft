using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 一个描述附属条目(变招，评论)的抽象类型
    /// </summary>
    public abstract class BylawItem
    {
        protected BylawItem(string userId, string value)
        {
            this.UserID = userId;
            this.BylawValue = value;
        }

        /// <summary>
        /// 获取与设置作者的ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 获取与设置条目的实际内容
        /// </summary>
        public string BylawValue { get; set; }

        public override string ToString()
        {
            return Utility.BylawItemToString(this.UserID, this.BylawValue);
        }
        public override int GetHashCode()
        {
            return unchecked(3 * (UserID.GetHashCode() + BylawValue.GetHashCode()));
        }
        public override bool Equals(object obj)
        {
            BylawItem bi = (BylawItem)obj;
            if (!bi.UserID.Equals(this.UserID))
                return false;
            if (!bi.BylawValue.Equals(this.BylawValue))
                return false;
            return true;
        }
    }
}
