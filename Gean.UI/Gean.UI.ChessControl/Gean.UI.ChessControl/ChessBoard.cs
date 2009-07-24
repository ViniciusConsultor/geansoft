using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
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

        private Rectangle _currRect = Rectangle.Empty;

        public Rectangle[,] Rectangles
        {
            get { return _rectangles; }
        }
        private Rectangle[,] _rectangles = new Rectangle[8, 8];

        int rectWidth = 0;
        int rectHeight = 0;

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            this.CreatBoard(pevent.Graphics);
        }

        private void CreatBoard(Graphics g)
        {
            int offsetX = 0;
            int offsetY = 0;
            this.GetGridSize(this.Size, out rectWidth, out rectHeight, out offsetX, out offsetY);

            for (int x = 1; x <= this._rectangles.GetLength(0); x++)
            {
                for (int y = 1; y <= this._rectangles.GetLength(1); y++)
                {
                    Rectangle rect = this.GetRectangle(x, y, rectWidth, rectHeight, offsetX, offsetY);
                    this._rectangles[x - 1, y - 1] = rect;
                    if ((x + y) % 2 == 0)
                    {
                        g.FillRectangle(Brushes.Gray, rect);
                        g.DrawRectangle(Pens.Black, rect);
                    }
                    else
                    {
                        g.FillRectangle(Brushes.WhiteSmoke, rect);
                        g.DrawRectangle(Pens.Black, rect);
                    }
                }
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

        private Rectangle GetRectangle(int x, int y, int width, int height, int offsetX, int offsetY)
        {
            Point point = new Point((x - 1) * width + offsetX, (y - 1) * height + offsetY);
            Size size = new Size(width, height);
            return new Rectangle(point, size);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
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
            _currRect = Rectangle.Empty;
            for (int x = 0; x < _rectangles.GetLength(0); x++)
            {
                for (int y = 0; y < _rectangles.GetLength(1); y++)
                {
                    if (_rectangles[x, y].Contains(e.Location))
                    {
                        Graphics g = this.CreateGraphics();
                        g.FillRectangle(Brushes.Maroon, _rectangles[x, y]);
                        g.DrawRectangle(Pens.Black, _rectangles[x, y]);
                        this.InvalidateRectangles(_rectangles[x, y].Location);
                    }
                }
            }

        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_currRect != Rectangle.Empty)
            {
                int offset = rectWidth / 2;
                Graphics g = this.CreateGraphics();
                Point newPoint = new Point(e.Location.X - offset, e.Location.Y - offset);
                _currRect = new Rectangle(newPoint, new Size(rectWidth, rectHeight));
                g.FillRectangle(Brushes.GreenYellow, _currRect);
                g.DrawRectangle(Pens.Black, _currRect);
                this.InvalidateRectangles(newPoint);
            }
        }

        private void InvalidateRectangles(Point point)
        {
            Rectangle top = new Rectangle(0, 0, Width, point.Y);
            Rectangle left = new Rectangle(0, point.Y, point.X, rectHeight + 1);
            Rectangle right = new Rectangle(point.X + rectWidth + 1, point.Y, Width - point.Y - rectWidth, rectHeight + 1);
            Rectangle bottom = new Rectangle(0, point.Y + rectHeight + 1, Width, Height - point.Y - rectHeight);
            this.Invalidate(top, false);
            this.Invalidate(left, false);
            this.Invalidate(right, false);
            this.Invalidate(bottom, false);
        }
    }

}
