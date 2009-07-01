using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Pdf
{
    /// <summary>
    /// 一个供PDF使用的Color类型
    /// </summary>
    public class PdfColor
    {
        public string Red { get; private set; }
        public string Green { get; private set; }
        public string Blue { get; private set; }
        public PdfColor(uint R, uint G, uint B)
        {
            double r = R / 255;
            double g = G / 255;
            double b = B / 255;
            this.Red = Math.Round(r, 3).ToString();
            this.Green = Math.Round(g, 3).ToString();
            this.Blue = Math.Round(b, 3).ToString(); 
        }
    }
}
