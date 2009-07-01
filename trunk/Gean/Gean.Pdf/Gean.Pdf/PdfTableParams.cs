using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Pdf
{
    /// <summary>
    /// 一个描述表格特征的表格(Table)类
    /// </summary>
    public class PdfTableParams
    {
        public uint X { get; set; }
        public uint Y { get; set; }
        public uint Width { get; set; }
        public uint Height { get; set; }

        public uint RowCount { get; set; }
        public uint ColumnCount { get; set; }

        public uint ColumnWidth { get; set; }
        public uint RowHeight { get; set; }

        public uint[] ColumnWidths;
        /// <summary>
        /// 构造一个简单的表格
        /// </summary>
        /// <param name="colCount">列数</param>
        /// <param name="colWidths">各列的宽度</param>
        public PdfTableParams(uint colCount, params uint[] colWidths)
        {
            X = Y = RowCount = ColumnWidth = RowHeight = Height = 0;
            Width = 0;
            ColumnCount = colCount;
            ColumnWidths = new uint[ColumnCount];
            ColumnWidths = colWidths;
            this.SetTableWidth();
        }
        /// <summary>
        /// 构造一个简单的表格
        /// </summary>
        /// <param name="colCount">列数</param>
        public PdfTableParams(uint colCount)
        {
            X = Y = RowCount = ColumnWidth = RowHeight = Height = 0;
            Width = 0;
            ColumnCount = colCount;
            ColumnWidths = null;
            ColumnWidth = 0;
        }
        private void SetTableWidth()
        {
            for (uint i = 0; i < ColumnCount; i++)
            {
                Width += ColumnWidths[i];
            }
        }
    }
}
