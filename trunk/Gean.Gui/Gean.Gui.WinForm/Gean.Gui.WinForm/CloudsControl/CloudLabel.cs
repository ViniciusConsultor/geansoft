using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Gean.Gui.WinForm
{
    public class CloudLabel
    {
        internal SizeF SizeF { get; set; }
        internal PointF AbsolutePointF { get; set; }
        internal PointF RelativePointF { get; set; }
        internal int LineNumber { get; private set; }
        public int Number { get; private set; }
        public IGenerator Cloud { get; set; }
        public string Text
        {
            get
            {
                if (this.Cloud == null)
                {
                    return "";
                }
                return this.Cloud.Generator();
            }
        }

        public CloudLabel(IGenerator cloud, int number, int width, int height) :
            this(cloud, number, new SizeF(width, height)) { }

        public CloudLabel(IGenerator cloud, int number, SizeF sizeF)
        {
            this.Cloud = cloud;
            this.Number = number;
            this.SizeF = sizeF;
        }

        public CloudLabel(IGenerator cloud, int number) :
            this(cloud, number, SizeF.Empty)
        {
        }
    }
}
