using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gean.UI.ChessControl
{
    public class ChessBoard : Control
    {
        public ChessBoard()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.NavajoWhite;
        }

        private ChessGrid _currRect = null;
        private ChessGrid _targetRect = null;
        private int _rectWidth = 0;
        private int _rectHeight = 0;

        public ChessGrid[,] Rectangles
        {
            get { return _rectangles; }
        }
        private ChessGrid[,] _rectangles = new ChessGrid[8, 8];

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            this.CreatBoard(pevent.Graphics);
        }

        private void CreatBoard(Graphics g)
        {
            int offsetX = 0;
            int offsetY = 0;
            this.GetGridSize(this.Size, out _rectWidth, out _rectHeight, out offsetX, out offsetY);

            for (int x = 1; x <= this._rectangles.GetLength(0); x++)
            {
                for (int y = 1; y <= this._rectangles.GetLength(1); y++)
                {
                    ChessGrid rect = this.GetRectangle(x, y, _rectWidth, _rectHeight, offsetX, offsetY);
                    this._rectangles[x - 1, y - 1] = rect;
                    if ((x + y) % 2 == 0)
                    {
                        g.FillRectangle(Brushes.Gray, rect.InnerRect);
                        g.DrawRectangle(Pens.Black, rect.InnerRect);
                    }
                    else
                    {
                        g.FillRectangle(Brushes.WhiteSmoke, rect.InnerRect);
                        g.DrawRectangle(Pens.Black, rect.InnerRect);
                    }
                }
            }
            if (_targetRect != null)
            {
                g.FillRectangle(Brushes.Tomato, _targetRect.InnerRect);
                g.DrawRectangle(Pens.Black, _targetRect.InnerRect);
            }
        }

        private void GetGridSize(Size size, out int width, out int height, out int offsetX, out int offsetY)
        {
            if (size.Height <= size.Width)
            {
                width = (int)(size.Height / 10);
                height = width;
                offsetX = (int)((size.Width - (width * 8)) / 2);
                offsetY = width;
            }
            else
            {
                width = (int)(size.Width / 10);
                height = width;
                offsetX = width;
                offsetY = (int)((size.Height - (width * 8)) / 2);
            }
        }

        private ChessGrid GetRectangle(int x, int y, int width, int height, int offsetX, int offsetY)
        {
            Point point = new Point((x - 1) * width + offsetX, (y - 1) * height + offsetY);
            Size size = new Size(width, height);
            return new ChessGrid(point, size);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _targetRect = null;
            for (int x = 0; x < _rectangles.GetLength(0); x++)
            {
                for (int y = 0; y < _rectangles.GetLength(1); y++)
                {
                    if (_rectangles[x, y].Contains(e.Location))
                        _currRect = _rectangles[x, y];
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _currRect = null;
            for (int x = 0; x < _rectangles.GetLength(0); x++)
            {
                for (int y = 0; y < _rectangles.GetLength(1); y++)
                {
                    if (_rectangles[x, y].Contains(e.Location))
                    {
                        _targetRect = _rectangles[x, y];
                        Graphics g = this.CreateGraphics();
                        g.FillRectangle(Brushes.Tomato, _rectangles[x, y].InnerRect);
                        g.DrawRectangle(Pens.Black, _rectangles[x, y].InnerRect);
                        this.InvalidateRectangles(_rectangles[x, y].Location);
                    }
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_currRect != null)
            {
                int offset = _rectWidth / 2;
                Graphics g = this.CreateGraphics();
                Point newPoint = new Point(e.Location.X - offset, e.Location.Y - offset);
                _currRect = new ChessGrid(newPoint, new Size(_rectWidth, _rectHeight));
                g.FillRectangle(Brushes.GreenYellow, _currRect.InnerRect);
                g.DrawRectangle(Pens.Black, _currRect.InnerRect);
                this.InvalidateRectangles(newPoint);
            }
        }

        protected virtual void InvalidateRectangles(Point point)
        {
            Rectangle top = new Rectangle(0, 0, Width, point.Y);
            Rectangle left = new Rectangle(0, point.Y, point.X, _rectHeight + 1);
            Rectangle right = new Rectangle(point.X + _rectWidth + 1, point.Y, Width - point.Y - _rectWidth, _rectHeight + 1);
            Rectangle bottom = new Rectangle(0, point.Y + _rectHeight + 1, Width, Height - point.Y - _rectHeight);
            this.Invalidate(top, false);
            this.Invalidate(left, false);
            this.Invalidate(right, false);
            this.Invalidate(bottom, false);
        }
    }

}
