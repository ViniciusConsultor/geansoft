using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Pdf
{

    /// <summary>
    /// Specify the page size in 1/72 inches units.
    /// </summary>
    public struct PdfPageSize
    {
        public uint xWidth;
        public uint yHeight;
        public uint leftMargin;
        public uint rightMargin;
        public uint topMargin;
        public uint bottomMargin;

        public PdfPageSize(uint width, uint height)
        {
            xWidth = width;
            yHeight = height;
            leftMargin = 0;
            rightMargin = 0;
            topMargin = 0;
            bottomMargin = 0;
        }
        public void SetMargins(uint L, uint T, uint R, uint B)
        {
            leftMargin = L;
            rightMargin = R;
            topMargin = T;
            bottomMargin = B;
        }
    }
}
