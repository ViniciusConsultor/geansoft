using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Gean.UI.ChessControl
{
    public class ChessGrid
    {
        public Rectangle InnerRect
        {
            get { return _innerRect; }
        }
        private Rectangle _innerRect = new Rectangle();

        public int SquareX { get; private set; }
        public int SquareY { get; private set; }

        public ChessGrid(int x, int y, int width, int height, int squareX, int squareY)
            : this(x, y, width, height)
        {
            this.SetSquare(squareX, squareY);
        }
        public ChessGrid(int x, int y, int width, int height)
            : this(new Point(x, y), new Size(width, height)) { }
        public ChessGrid(Point location, Size size)
        {
            _innerRect.Location = location;
            _innerRect.Size = size;
        }
        public ChessGrid(Point location, Size size, int squareX, int squareY)
            : this(location, size)
        {
            this.SetSquare(squareX, squareY);
        }

        private void SetSquare(int squareX, int squareY)
        {
            this.SquareX = squareX;
            this.SquareY = 9 - squareY;
        }

        #region Inner Rectangle
        public Point Location 
        {
            get { return this._innerRect.Location; }
            set { this._innerRect.Location = value; }
        }
        public int Bottom
        {
            get { return this._innerRect.Bottom; }
        }
        public int Height
        {
            get { return this._innerRect.Height; }
            set { this._innerRect.Height = value; }
        }
        public bool IsEmpty
        {
            get { return this._innerRect.IsEmpty; } 
        }
        public int Left
        {
            get { return this._innerRect.Left; }
        }
        public int Right
        {
            get { return this._innerRect.Right; }
        }
        public Size Size
        {
            get { return this._innerRect.Size; }
            set { this._innerRect.Size = value; }
        }
        public int Top
        {
            get { return this._innerRect.Top; }
        }
        public int Width
        {
            get { return this._innerRect.Width; }
            set { this._innerRect.Width = value; }
        }
        public int X
        {
            get { return this._innerRect.X; }
            set { this._innerRect.X = value; }
        }
        public int Y
        {
            get { return this._innerRect.Y; }
            set { this._innerRect.Y = value; }
        }

        public bool Contains(Point pt)
        {
            return this._innerRect.Contains(pt);
        }
        public bool Contains(Rectangle rect)
        {
            return this.Contains(rect);
        }
        public bool Contains(int x, int y)
        {
            return this._innerRect.Contains(x, y);
        }
        /// <summary>
        /// 将此 Gean.UI.ChessControl.Rectangle 放大指定量。
        /// </summary>
        /// <param name="size">此矩形的放大量。</param>
        public void Inflate(Size size)
        {
            this._innerRect.Inflate(size);
        }
        /// <summary>
        /// 将此 Gean.UI.ChessControl.Rectangle 放大指定量。
        /// </summary>
        /// <param name="width">水平放大量</param>
        /// <param name="height">垂直放大量</param>
        public void Inflate(int width, int height)
        {
            this._innerRect.Inflate(width, height);
        }
        /// <summary>
        /// 将此矩形的位置调整指定的量。
        /// </summary>
        /// <param name="pos">该位置的偏移量。</param>
        public void Offset(Point pos)
        {
            this._innerRect.Offset(pos);
        }
        /// <summary>
        /// 将此矩形的位置调整指定的量。
        /// </summary>
        /// <param name="x">水平偏移量。</param>
        /// <param name="y">垂直偏移量。</param>
        public void Offset(int x, int y)
        {
            this._innerRect.Offset(x, y);
        }
        #endregion

        public override bool Equals(object obj)
        {
            ChessGrid rid = (ChessGrid)obj;
            if (!this._innerRect.Equals(rid._innerRect))
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked(7 * (_innerRect.GetHashCode()));
        }
        public override string ToString()
        {
            return _innerRect.ToString();
        }

    }
}
