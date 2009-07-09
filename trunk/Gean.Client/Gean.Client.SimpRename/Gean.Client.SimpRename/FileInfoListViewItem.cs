using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace Gean.Client.SimpRename
{
    class FileInfoListViewItem : ListViewItem
    {
        protected FileInfo FileInfo { get; set; }
        private const string RULE = "#";

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

        public FileInfoListViewItem(string filename, string ruleString, int targetNum) :
            this(new FileInfo(filename), ruleString, targetNum) { }

        public FileInfoListViewItem(FileInfo fileInfo, string ruleString, int targetNum)
        {
            Debug.Assert(!string.IsNullOrEmpty(ruleString), "Rename RULE is None!");
            this.FileInfo = fileInfo;
            this.RuleString = ruleString;
            this.TargetNumber = targetNum;
            this.Text = this.FileInfo.Name;
            this.Checked = true;
            this.SubItems.AddRange(this.SubItemCreator());
        }

        public string RuleString { get; private set; }
        public int TargetNumber { get; private set; }
        public string TargetFileName { get; private set; }

        private ListViewSubItem[] SubItemCreator()
        {
            List<ListViewSubItem> list = new List<ListViewSubItem>();

            ListViewSubItem item = new ListViewSubItem();
            item.Name = "Target";
            item.Text = this.GetTargetName();
            list.Add(item);

            list.Add(Helper.GetAttributeViewItem(this.FileInfo.Attributes, FileAttributes.ReadOnly));
            list.Add(Helper.GetAttributeViewItem(this.FileInfo.Attributes, FileAttributes.Hidden));
            list.Add(Helper.GetAttributeViewItem(this.FileInfo.Attributes, FileAttributes.System));
            list.Add(Helper.GetAttributeViewItem(this.FileInfo.Attributes, FileAttributes.Archive));
            list.Add(Helper.GetAttributeViewItem(this.FileInfo.Attributes, FileAttributes.Compressed));

            return list.ToArray();
        }

        public void RenamePreview(int targetNum)
        {
            this.TargetNumber = targetNum;
            this.SubItems["Target"].Text = this.GetTargetName();
        }

        private string GetTargetName()
        {
            string before = this.RuleString.Substring(0, RuleString.LastIndexOf(RULE) - 1);

            string serialPlace;
            serialPlace = this.RuleString.Substring(this.RuleString.LastIndexOf(RULE) - 1);
            serialPlace = serialPlace.Substring(0, serialPlace.LastIndexOf(RULE) + 1);

            string replaceString = Helper.IntToString(serialPlace.Length, this.TargetNumber);
            string end = this.RuleString.Substring(before.Length + serialPlace.Length);

            StringBuilder sb = new StringBuilder();
            sb.Append(before).Append(replaceString).Append(end).Append(this.ExtensionName);

            this.TargetFileName = sb.ToString();
            return this.TargetFileName;
        }

        static class Helper
        {
            /// <summary>
            /// 将指定的一个整数转换成指定位数的字符串
            /// </summary>
            /// <param name="digit">指定位数，如：000005是6位</param>
            /// <param name="number">指定的一个整数</param>
            /// <returns></returns>
            internal static string IntToString(int digit, int number)
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
            /// 判断一个整数的位数。如：324有3位数，34530有5位数
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            internal static int GetDigit(long value)
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
                    x = (long)value / (long)Math.Pow(10, numlen);//例子123/10=12,12%10=2这样就可以把某位数取出
                    flag = x % 10;
                    if (flag == 0 && x < 10)
                    {
                        flag = -1;
                    }
                    else
                    {
                        numlen++;
                    }
                }
                while (flag != -1);
                return numlen;
            }

            internal static ListViewSubItem GetAttributeViewItem(FileAttributes currAttribute, FileAttributes fileAttributes)
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
