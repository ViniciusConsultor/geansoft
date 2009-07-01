using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Pdf
{
    /// <summary>
    /// Passed to Table class to create tables
    /// </summary>
    public struct PdfTableParams
    {
        public uint xPos;
        public uint yPos;
        public uint numRow;
        public uint columnWidth;
        public uint numColumn;
        public uint rowHeight;
        public uint tableWidth;
        public uint tableHeight;
        public uint[] columnWidths;
        /// <summary>
        /// Call this for columns of variable widhts, specify the widhts
        /// </summary>
        /// <param name="numColumns"></param>
        /// <param name="widths"></param>
        public PdfTableParams(uint numColumns, params uint[] widths)
        {
            xPos = yPos = numRow = columnWidth = rowHeight = tableHeight = 0;
            tableWidth = 0;
            numColumn = numColumns;
            columnWidths = new uint[numColumn];
            columnWidths = widths;
            SetTableWidth();
        }
        /// <summary>
        /// Call this for columns of equal widths, column width should be set
        /// </summary>
        /// <param name="numColumns"></param>
        public PdfTableParams(uint numColumns)
        {
            xPos = yPos = numRow = columnWidth = rowHeight = tableHeight = 0;
            tableWidth = 0;
            numColumn = numColumns;
            columnWidths = null;
            columnWidth = 0;
        }
        private void SetTableWidth()
        {
            for (uint i = 0; i < numColumn; i++)
                tableWidth += columnWidths[i];
        }
    }
}
