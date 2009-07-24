using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Gean.Wrapper.Chess;

namespace Gean.UI.ChessControl
{
    public class ChessBoard : Control
    {
        public ChessBoard()
        {
            this.ChessGame = new ChessGame();
            this.DoubleBuffered = true;
            this.BackColor = Color.NavajoWhite;
        }

        public ChessGame ChessGame { get; set; }
        public Rectangle[,] Rectangles
        {
            get { return _rectangles; }
        }
        private Rectangle[,] _rectangles = new Rectangle[8, 8];

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            this.CreatBoard(pevent.Graphics);
        }

        private void CreatBoard(Graphics g)
        {
            for (int x = 1; x <= this._rectangles.GetLength(0); x++)
            {
                for (int y = 1; y <= this._rectangles.GetLength(1); y++)
                {
                    int width = 0;
                    int height = 0;
                    int offsetX = 0;
                    int offsetY = 0;
                    this.GetGridSize(this.Size, out width, out height, out offsetX, out offsetY);
                    Rectangle rect = this.GetRectangle(x, y, width, height, offsetX, offsetY);
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
    }
}
