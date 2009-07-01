using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Pdf
{
    /// <summary>
    /// 一个指定页面大小的类
    /// </summary>
    public class PdfPageSize
    {
        public uint Width { get; private set; }
        public uint Height { get; private set; }
        public uint Left { get; private set; }
        public uint Right { get; private set; }
        public uint Top { get; private set; }
        public uint Bottom { get; private set; }

        public PdfPageSize()
        {
            this.Width = 0;
            this.Height = 0;
            this.Left = 0;
            this.Right = 0;
            this.Top = 0;
            this.Bottom = 0;
        }

        public PdfPageSize(uint width, uint height)
        {
            Width = width;
            Height = height;
        }
        public void SetMargins(uint L, uint T, uint R, uint B)
        {
            Left = L;
            Right = R;
            Top = T;
            Bottom = B;
        }
    }
}
