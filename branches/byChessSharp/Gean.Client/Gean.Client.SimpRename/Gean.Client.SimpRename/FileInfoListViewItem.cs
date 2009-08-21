using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Gean.Client.SimpRename
{
    class FileInfoListViewItem : ListViewItem
    {
        protected FileInfo FileInfo { get; set; }

        public string FullName
        {
            get { return this.FileInfo.FullName; }
        }
        public string FileName
        {
            get { return this.FileInfo.Name; }
        }
        public string DirectoryName
        {
            get { return this.FileInfo.DirectoryName; }
        }
        public string ExtensionName
        {
            get { return this.FileInfo.Extension; }
        }

        public FileInfoListViewItem(string filename, string ruleString, int serialNum) :
            this(new FileInfo(filename), ruleString, serialNum) { }

        public FileInfoListViewItem(FileInfo fileInfo, string ruleString, int serialNum)
        {
            this.FileInfo = fileInfo;
            this.RuleString = ruleString;
            this.CurrSerialNumber = serialNum;
            this.Text = this.FileInfo.Name;
            if (!string.IsNullOrEmpty(ruleString))
                this.PreviewFileName = Helper.BuildNewFileName(this.RuleString, this.CurrSerialNumber, this.ExtensionName);
            this.Checked = true;

            ListViewSubItem[] items = Helper.BuildSubItems(this.FileInfo, this.PreviewFileName);
            this.SubItems.AddRange(items);
        }

        public string RuleString { get; private set; }
        /// <summary>
        /// 获取该ListViewItem绑定的文件的改名编号
        /// </summary>
        public int CurrSerialNumber { get; private set; }
        /// <summary>
        /// 获取根据规则能够生成的文件名(预览，未实际改名)
        /// </summary>
        public string PreviewFileName { get; private set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("|SourceFile info:  |").Append(this.FullName).Append("\r\n");
            sb.Append("|Preview FileName: |").Append(this.PreviewFileName);
            return sb.ToString();
        }

        /// <summary>
        /// 当更改了当前实例的改名规则或当前绑定编号的前提下，预览文件名必然也会发生变化。
        /// 本方法实现重新生成预览文件名，并重新绑定相应的ListViewSubItem。
        /// </summary>
        public void ResetListViewSubItem(string rule, int serialNum)
        {
            this.RuleString = rule;
            this.CurrSerialNumber = serialNum;
            this.PreviewFileName = Helper.BuildNewFileName(rule, serialNum, this.ExtensionName);

            ListViewSubItem item = new ListViewSubItem();
            item.Name = "Target";
            item.Text = this.PreviewFileName;
            this.SubItems[this.SubItems.IndexOfKey("Target")] = item;
        }
        /*上述方法如果以通过改变ListViewSubitem内部值的方法，但却无法引发刷新(界面显示的变化)，why?
        this.TargetNumber = targetNum;
        string modifiedName = this.GetModifiedName();
        this.SubItems["Target"].Text = modifiedName;*/

        static class Helper
        {
            /// <summary>
            /// 转义字符
            /// </summary>
            const string ESC = "\\";
            /// <summary>
            /// 数字替代符
            /// </summary>
            const string NUM_RULE = "#";
            /// <summary>
            /// 字符替代符
            /// </summary>
            const string CHAR_RULE = "?";

            /// <summary>
            /// 根据指定的System.IO.FileInfo生成相应的SubItem集合，一般用来添加到ListView的一个Item中去
            /// </summary>
            /// <param name="fileInfo">指定的System.IO.FileInfo</param>
            /// <param name="previewFileName">根据规则生成的预览的文件名</param>
            /// <returns></returns>
            public static ListViewSubItem[] BuildSubItems(FileInfo fileInfo, string previewFileName)
            {
                List<ListViewSubItem> list = new List<ListViewSubItem>();

                ListViewSubItem item = new ListViewSubItem();
                item.Name = "Target";
                item.Text = previewFileName;
                list.Add(item);

                list.Add(Helper.GetAttributeViewItem(fileInfo.Attributes, FileAttributes.ReadOnly));
                list.Add(Helper.GetAttributeViewItem(fileInfo.Attributes, FileAttributes.Hidden));
                list.Add(Helper.GetAttributeViewItem(fileInfo.Attributes, FileAttributes.System));
                list.Add(Helper.GetAttributeViewItem(fileInfo.Attributes, FileAttributes.Archive));
                list.Add(Helper.GetAttributeViewItem(fileInfo.Attributes, FileAttributes.Compressed));

                return list.ToArray();
            }

            /// <summary>
            /// 根据指定的规则、编号生成新的文件名(使用正则的方式)。
            /// 已进行单元测试。
            /// </summary>
            /// <param name="rule">规则字符串</param>
            /// <param name="serial">当前编号</param>
            /// <param name="extensionName">当前扩展名</param>
            /// <returns></returns>
            public static string BuildNewFileName(string rule, int serial, string extensionName)
            {
                string tgstring = rule;
                Regex regex = new Regex(@"##*");
                
                MatchCollection matches = regex.Matches(rule);
                if (matches == null || matches.Count <= 0)
                {
                    return string.Empty;
                }
                GroupCollection groups = matches[matches.Count - 1].Groups;
                
                string replaceString = Helper.IntToString(groups[0].Length, serial);

                //无法使用正则的替换功能，因为会经常出现“abc__##__###__####__xyz”这种现象时，
                //我们只会将最后的4位的替换符替换成预览名的。
                tgstring = tgstring.Remove(groups[0].Index, groups[0].Length);
                tgstring = tgstring.Insert(groups[0].Index, replaceString) + extensionName;

                return tgstring;
            }

            /// <summary>
            /// 将指定的一个整数转换成指定位数的字符串
            /// </summary>
            /// <param name="digit">指定位数，如：000005是6位</param>
            /// <param name="number">指定的一个整数</param>
            /// <returns></returns>
            static string IntToString(int digit, int number)
            {
                string value = string.Empty;
                int n = Helper.GetDigit(number);
                if (n > digit)
                {
                    return number.ToString();
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < digit - n; i++)
                    {
                        sb.Append('0');
                    }
                    sb.Append(number.ToString());
                    return sb.ToString();
                }
            }

            /// <summary>
            /// 判断一个指定的整数的位数。如：324有3位数，34530有5位数
            /// </summary>
            /// <param name="value">一个指定的整数</param>
            /// <returns></returns>
            static int GetDigit(long value)
            {
                int numlen = 0;
                long flag = 0;
                long x;
                do
                {
                    if (value == 0) //判断是否为0
                    {
                        numlen++;
                        break;
                    }
                    //例: 123/10=12, 12%10=2 这样就可以把某位数取出
                    x = (long)value / (long)Math.Pow(10, numlen);
                    flag = x % 10;

                    if (flag == 0 && x < 10)
                    {
                        flag = -1;
                    }
                    else
                    {
                        numlen++;
                    }
                } while (flag != -1);

                return numlen;
            }

            static ListViewSubItem GetAttributeViewItem(FileAttributes currAttribute, FileAttributes fileAttributes)
            {
                ListViewSubItem subitem = new ListViewSubItem();
                if ((currAttribute & fileAttributes) == fileAttributes)
                    subitem.Text = "*";
                else
                    subitem.Text = string.Empty;
                return subitem;
            }
        }
    }
}
