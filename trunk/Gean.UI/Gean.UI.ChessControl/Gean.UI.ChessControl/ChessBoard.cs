﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Gean.Wrapper.Chess;

namespace Gean.UI.ChessControl
{
    /// <summary>
    /// 棋盘控件
    /// </summary>
    public class ChessBoard : Control
    {
        #region ctor

        public ChessBoard()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.SaddleBrown;
        }

        #endregion

        #region PUBLIC

        /// <summary>
        /// 设置棋格的背景图片
        /// </summary>
        /// <param name="white">白棋格的背景图片</param>
        /// <param name="black">黑棋格的背景图片</param>
        public void SetGridImage(Image white, Image black)
        {
            this._whiteGridImage = white;
            this._blackGridImage = black;
            if (white != null && black != null)
            {
                this._hasGridImage = true;
            }
        }

        
        public void ChessGameBinding()
        {
            this._ChessGame = new ChessGame();
            this._ChessGame.GameStartedEvent += new ChessGame.GameStartedEventHandler(ChessGame_GameStartedEvent);
            this._ChessGame.LoadOpennings();
        }

        void ChessGame_GameStartedEvent(object sender, ChessGame.ChessGameEventArgs e)
        {
            ChessmanBase[] mans = e.ChessGame.GetLivingChessmans();
            Graphics g = this.CreateGraphics();
            foreach (ChessmanBase man in mans)
            {
                ChessSquare square = man.Squares.Peek();
                foreach (ChessGrid grid in this._Grids)
                {
                    if (grid.Square.X == square.X && grid.Square.Y == square.Y)
                    {
                        Point pt = new Point(grid.Location.X, grid.Location.Y + 35);
                        g.DrawString(man.ToSimpleString(), new Font("Arial Black", 15F), Brushes.Red, pt);
                    }
                }
            }
        }

        #endregion

        #region private

        private ChessGrid[,] _Grids = new ChessGrid[8, 8];
        private ChessGrid _sourceGrid = null;
        private ChessGrid _targetGrid = null;

        private bool _hasGridImage = false;
        private Image _whiteGridImage;
        private Image _blackGridImage;

        private Rectangle _currRect = Rectangle.Empty;

        private ChessGame _ChessGame = null;

        /// <summary>
        /// 格子宽度
        /// </summary>
        private int _GridWidth = 0;
        /// <summary>
        /// 格子高度
        /// </summary>
        private int _GridHeight = 0;

        #endregion

        #region PaintChessboard

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            this.PaintChessboard(pevent.Graphics);
        }

        /// <summary>
        /// 画一个棋盘，该方法仅在 OnPaintBackground(PaintEventArgs pevent) 中被使用。
        /// </summary>
        private void PaintChessboard(Graphics g)
        {
            int offsetX = 0;
            int offsetY = 0;
            this.GetGridSize(this.Size, out _GridWidth, out _GridHeight, out offsetX, out offsetY);

            for (int x = 1; x <= this._Grids.GetLength(0); x++)
            {
                for (int y = 1; y <= this._Grids.GetLength(1); y++)
                {
                    ChessGrid rid = this.GetRectangle(x, y, _GridWidth, _GridHeight, offsetX, offsetY, x, y);
                    this._Grids[x - 1, y - 1] = rid;
                    if ((x + y) % 2 == 0)
                    {
                        if (this._hasGridImage)
                            g.DrawImage(this._whiteGridImage, rid.InnerRect);
                        else
                            g.FillRectangle(Brushes.WhiteSmoke, rid.InnerRect);
                        g.DrawRectangle(Pens.Black, rid.InnerRect);
#if DEBUG
                        g.DrawString(rid.Square.ToString(), new Font("Arial", 8F), Brushes.Black, rid.Location);
#endif
                    }
                    else
                    {
                        if (this._hasGridImage)
                            g.DrawImage(this._blackGridImage, rid.InnerRect);
                        else
                            g.FillRectangle(Brushes.Gray, rid.InnerRect);
                        g.DrawRectangle(Pens.Black, rid.InnerRect);
#if DEBUG
                        g.DrawString(rid.Square.ToString(), new Font("Arial", 8F), Brushes.White, rid.Location);
#endif
                    }
                }
            }
            if (_targetGrid != null)
            {
                g.FillRectangle(Brushes.Tomato, _targetGrid.InnerRect);
                g.DrawRectangle(Pens.Black, _targetGrid.InnerRect);
            }
#if DEBUG
            string ver = this.GetType().Assembly.ToString();
            int m = ver.IndexOf("Version");
            int n = ver.IndexOf("Culture") - m - 2;
            ver = ver.Substring(m, n);
            g.DrawString("Debug: " + ver, new Font("Arial", 8F), Brushes.White, this.Location);
#endif
        }

        private ChessGrid GetRectangle(int x, int y, int width, int height, int offsetX, int offsetY, int squareX, int squareY)
        {
            Point point = new Point((x - 1) * width + offsetX, (y - 1) * height + offsetY);
            Size size = new Size(width, height);
            return new ChessGrid(point, size, new ChessSquare(squareX, 9 - squareY));
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

        #endregion

        #region OnMouse

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                _targetGrid = null;
                for (int x = 0; x < _Grids.GetLength(0); x++)
                {
                    for (int y = 0; y < _Grids.GetLength(1); y++)
                    {
                        if (_Grids[x, y].Contains(e.Location))
                        {
                            _sourceGrid = _Grids[x, y];
                            _currRect = _Grids[x, y].InnerRect;
                            return;
                        }
                    }//for y
                }//for x
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                _currRect = Rectangle.Empty;
                for (int x = 0; x < _Grids.GetLength(0); x++)
                {
                    for (int y = 0; y < _Grids.GetLength(1); y++)
                    {
                        if (_Grids[x, y].Contains(e.Location))
                        {
                            _targetGrid = _Grids[x, y];
                            this.InvalidateBoard(_targetGrid.InnerRect);
                            //注册棋子移动事件
                            OnChessPlayed(new ChessPlayedEventArgs(_sourceGrid, _targetGrid));
                            return;
                        }
                        else
                        {
                            this.Invalidate();
                        }
                    }//for y
                }//for x
            }//if
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_currRect != Rectangle.Empty)
            {
                int offset = _GridWidth / 2;
                Point newPoint = new Point(e.Location.X - offset, e.Location.Y - offset);
                _currRect = new Rectangle(newPoint, new Size(_GridWidth, _GridHeight));
                this.InvalidateBoard(_currRect);
            }
        }

        #endregion

        #region private Method

        /// <summary>
        /// 使控件指定的 "rectangle" 以外的区域无效，重新绘制更新区域
        /// </summary>
        /// <param name="rectangle">指定的 "rectangle"</param>
        private void InvalidateBoard(Rectangle rectangle)
        {
            this.InvalidateBoard(rectangle, null);
        }
        /// <summary>
        /// 使控件指定的 "rectangle" 以外的区域无效，重新绘制更新区域
        /// </summary>
        /// <param name="rectangle">指定的 "rectangle"</param>
        /// <param name="image">指定的 "rectangle" 的背景图片</param>
        private void InvalidateBoard(Rectangle rectangle, Image image)
        {
            Graphics g = this.CreateGraphics();
            if (image == null)
            {
                g.FillRectangle(Brushes.Tomato, rectangle);
                g.DrawRectangle(Pens.Black, rectangle);
            }
            else
            {
                g.DrawImage(image, rectangle);
            }

            // 使控件的 "rectangle" 以外的区域无效，重新绘制更新区域
            Point pt = rectangle.Location;
            Rectangle top = new Rectangle(0, 0, Width, pt.Y);
            Rectangle left = new Rectangle(0, pt.Y, pt.X, _GridHeight + 1);
            Rectangle right = new Rectangle(pt.X + _GridWidth + 1, pt.Y, Width - pt.Y - _GridWidth, _GridHeight + 1);
            Rectangle bottom = new Rectangle(0, pt.Y + _GridHeight + 1, Width, Height - pt.Y - _GridHeight);
            this.Invalidate(top, false);
            this.Invalidate(left, false);
            this.Invalidate(right, false);
            this.Invalidate(bottom, false);
        }

        #endregion

        #region event

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

        #endregion
    }
}