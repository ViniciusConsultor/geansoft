using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Pdf
{
    /// <summary>
    /// The Color to be used while drawing
    /// </summary>
    public struct PdfColorSpec
    {
        private double red1;
        private double green1;
        private double blue1;
        public string red;
        public string green;
        public string blue;
        public PdfColorSpec(uint R, uint G, uint B)
        {
            //Convert in the range 0.0 to 1.0
            red1 = R; green1 = G; blue1 = B;
            red1 = Math.Round((red1 / 255), 3);
            green1 = Math.Round((green1 / 255), 3);
            blue1 = Math.Round((blue1 / 255), 3);
            red = red1.ToString();
            green = green1.ToString();
            blue = blue1.ToString();
        }
    }
}
