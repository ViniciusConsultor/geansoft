using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Gean.Wrapper.Chess;

namespace Gean.UI.ChessControl
{
    class MoveableRectangle
    {
        public Rectangle Rectangle { get; private set; }
        public Image Image { get; private set; }

        public MoveableRectangle(Rectangle rect, Image img)
        {
            this.Rectangle = rect;
            this.Image = img;
        }
    }
}
