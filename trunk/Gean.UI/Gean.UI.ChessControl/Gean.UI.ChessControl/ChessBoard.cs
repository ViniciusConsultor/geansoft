using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Gean.UI.ChessControl
{
    public class ChessBoard : Control
    {
        public ChessBoard()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.SaddleBrown;
        }

        private ChessGrid _currGrid = null;
        private ChessGrid _targetGrid = null;
        private int _GridWidth = 0;
        private int _GridHeight = 0;

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
            this.GetGridSize(this.Size, out _GridWidth, out _GridHeight, out offsetX, out offsetY);

            for (int x = 1; x <= this._rectangles.GetLength(0); x++)
            {
                for (int y = 1; y <= this._rectangles.GetLength(1); y++)
                {
                    ChessGrid rid = this.GetRectangle
                        (x, y, _GridWidth, _GridHeight, offsetX, offsetY, x, y);
                    this._rectangles[x - 1, y - 1] = rid;
                    if ((x + y) % 2 == 0)
                    {
                        g.FillRectangle(Brushes.Gray, rid.InnerRect);
                        g.DrawRectangle(Pens.Black, rid.InnerRect);
                    }
                    else
                    {
                        g.FillRectangle(Brushes.WhiteSmoke, rid.InnerRect);
                        g.DrawRectangle(Pens.Black, rid.InnerRect);
                    }
#if DEBUG
                    g.DrawString(rid.SquareX + "," + rid.SquareY,
                        new Font("Tahoma", 8.25F), Brushes.Black, rid.Location);
#endif
                }
            }
            if (_targetGrid != null)
            {
                g.FillRectangle(Brushes.Tomato, _targetGrid.InnerRect);
                g.DrawRectangle(Pens.Black, _targetGrid.InnerRect);
            }
#if DEBUG
            Assembly ass = this.GetType().Assembly;
            g.DrawString("Debug: " + ass.ToString(), new Font("Consolas", 9.5F), Brushes.White, this.Location);
#endif
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

        private ChessGrid GetRectangle
            (int x, int y, int width, int height, int offsetX, int offsetY, int squareX, int squareY)
        {
            Point point = new Point((x - 1) * width + offsetX, (y - 1) * height + offsetY);
            Size size = new Size(width, height);
            return new ChessGrid(point, size, squareX, squareY);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _targetGrid = null;
            for (int x = 0; x < _rectangles.GetLength(0); x++)
            {
                for (int y = 0; y < _rectangles.GetLength(1); y++)
                {
                    if (_rectangles[x, y].Contains(e.Location))
                        _currGrid = _rectangles[x, y];
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            ChessGrid tempGrid = _currGrid;
            _currGrid = null;
            for (int x = 0; x < _rectangles.GetLength(0); x++)
            {
                for (int y = 0; y < _rectangles.GetLength(1); y++)
                {
                    if (_rectangles[x, y].Contains(e.Location))
                    {
                        _targetGrid = _rectangles[x, y];

                        this.DebugDisplay(_targetGrid);

                        //注册棋子移动事件
                        OnChessPlayed(new ChessPlayedEventArgs(tempGrid, _targetGrid));
                    }
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_currGrid != null)
            {
                int offset = _GridWidth / 2;
                Point newPoint = new Point(e.Location.X - offset, e.Location.Y - offset);
                _currGrid = new ChessGrid(newPoint, new Size(_GridWidth, _GridHeight));
                this.DebugDisplay(_currGrid);
            }
        }

#if DEBUG
        private void DebugDisplay(ChessGrid rid)
        {
            Graphics g = this.CreateGraphics();
            g.FillRectangle(Brushes.Tomato, rid.InnerRect);
            g.DrawRectangle(Pens.Black, rid.InnerRect);

            Point pt = rid.Location;
            Rectangle top = new Rectangle(0, 0, Width, pt.Y);
            Rectangle left = new Rectangle(0, pt.Y, pt.X, _GridHeight + 1);
            Rectangle right = new Rectangle(pt.X + _GridWidth + 1, pt.Y, Width - pt.Y - _GridWidth, _GridHeight + 1);
            Rectangle bottom = new Rectangle(0, pt.Y + _GridHeight + 1, Width, Height - pt.Y - _GridHeight);
            this.Invalidate(top, false);
            this.Invalidate(left, false);
            this.Invalidate(right, false);
            this.Invalidate(bottom, false);
        }
#endif

        public event ChessPlayedEventHandler ChessPlayedEvent;
        protected virtual void OnChessPlayed(ChessPlayedEventArgs e)
        {
            if (ChessPlayedEvent != null)
                ChessPlayedEvent(this, e);
        }
        public delegate void ChessPlayedEventHandler(object sender, ChessPlayedEventArgs e);
        public class ChessPlayedEventArgs : EventArgs
        {
            public ChessGrid OldGrid { get; private set; }
            public ChessGrid NewGrid { get; private set; }
            public ChessPlayedEventArgs(ChessGrid oldGrid, ChessGrid newGrid)
            {
                this.OldGrid = oldGrid;
                this.NewGrid = newGrid;
            }
        }
    }

}
