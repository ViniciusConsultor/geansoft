using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Gean.Wrapper.Chess;

namespace Gean.UI.ChessControl
{
    public class ChessGrid : ChessSquare
    {
        public ChessGrid(int squareX, int squareY, Point location, Size size)
            : base(squareX, squareY)
        {
            this.OwnedRectangle = new Rectangle(location, size);
        }

        #region Inner Rectangle
        /// <summary>
        /// 该实体棋格的实际矩形
        /// </summary>
        public Rectangle OwnedRectangle { get; private set; }
        public Point Location 
        {
            get { return this.OwnedRectangle.Location; }
        }
        public Size Size
        {
            get { return this.OwnedRectangle.Size; }
        }
        public int Height
        {
            get { return this.OwnedRectangle.Height; }
        }
        public int Width
        {
            get { return this.OwnedRectangle.Width; }
        }

        public bool Contains(Point pt)
        {
            return this.OwnedRectangle.Contains(pt);
        }
        public bool Contains(int x, int y)
        {
            return this.OwnedRectangle.Contains(x, y);
        }
        #endregion
    }
}
