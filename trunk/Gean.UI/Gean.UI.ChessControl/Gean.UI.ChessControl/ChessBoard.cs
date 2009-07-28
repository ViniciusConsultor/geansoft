using System;
using System.Drawing;
using System.Windows.Forms;
using Gean.Wrapper.Chess;
using System.Collections.Generic;

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
            this.BackColor = Color.Peru;
        }

        #endregion

        #region = PUBLIC =

        /// <summary>
        /// 载入棋局
        /// </summary>
        public ChessGame LoadGame()
        {
            this._ownedChessGame = new ChessGame();
            this._ownedChessGame.GameStartedEvent += new ChessGame.GameStartedEventHandler(ChessGame_GameStartedEvent);
            this._ownedChessGame.LoadOpennings();
            return this._ownedChessGame;
        }

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

        /// <summary>
        /// 设置默认的棋子图片
        /// </summary>
        public void SetChessmanImage()
        {
            if (this._ownedChessGame == null)
                return;
            foreach (Chessman man in this._ownedChessGame.Chessmans)
            {
                switch (man.ChessmanType)
                {
                    #region case
                    case Enums.ChessmanType.Rook:
                        switch (man.ChessmanSide)
                        {
                            case Enums.ChessmanSide.White:
                                man.BackgroundImage = ChessmanResource.white_rook;
                                break;
                            case Enums.ChessmanSide.Black:
                                man.BackgroundImage = ChessmanResource.black_rook;
                                break;
                            case Enums.ChessmanSide.None:
                            default:
                                break;
                        }
                        break;
                    case Enums.ChessmanType.Knight:
                        switch (man.ChessmanSide)
                        {
                            case Enums.ChessmanSide.White:
                                man.BackgroundImage = ChessmanResource.white_knight;
                                break;
                            case Enums.ChessmanSide.Black:
                                man.BackgroundImage = ChessmanResource.black_knight;
                                break;
                            case Enums.ChessmanSide.None:
                            default:
                                break;
                        }
                        break;
                    case Enums.ChessmanType.Bishop:
                        switch (man.ChessmanSide)
                        {
                            case Enums.ChessmanSide.White:
                                man.BackgroundImage = ChessmanResource.white_bishop;
                                break;
                            case Enums.ChessmanSide.Black:
                                man.BackgroundImage = ChessmanResource.black_bishop;
                                break;
                            case Enums.ChessmanSide.None:
                            default:
                                break;
                        }
                        break;
                    case Enums.ChessmanType.Queen:
                        switch (man.ChessmanSide)
                        {
                            case Enums.ChessmanSide.White:
                                man.BackgroundImage = ChessmanResource.white_queen;
                                break;
                            case Enums.ChessmanSide.Black:
                                man.BackgroundImage = ChessmanResource.black_queen;
                                break;
                            case Enums.ChessmanSide.None:
                            default:
                                break;
                        }
                        break;
                    case Enums.ChessmanType.King:
                        switch (man.ChessmanSide)
                        {
                            case Enums.ChessmanSide.White:
                                man.BackgroundImage = ChessmanResource.white_king;
                                break;
                            case Enums.ChessmanSide.Black:
                                man.BackgroundImage = ChessmanResource.black_king;
                                break;
                            case Enums.ChessmanSide.None:
                            default:
                                break;
                        }
                        break;
                    case Enums.ChessmanType.Pawn:
                        switch (man.ChessmanSide)
                        {
                            case Enums.ChessmanSide.White:
                                man.BackgroundImage = ChessmanResource.white_pawn;
                                break;
                            case Enums.ChessmanSide.Black:
                                man.BackgroundImage = ChessmanResource.black_pawn;
                                break;
                            case Enums.ChessmanSide.None:
                            default:
                                break;
                        }
                        break;
                    case Enums.ChessmanType.None:
                    default:
                        break;
                    #endregion
                }
            }
        }
        /// <summary>
        /// 设置棋子的背景图片
        /// </summary>
        /// <param name="images">背景图片集合</param>
        public void SetChessmanImage(ChessmanImages images)
        {
            foreach (Chessman man in this._ownedChessGame.Chessmans)
            {
                man.BackgroundImage = images[man.ChessmanSide, man.ChessmanType];
            }
        }

        #endregion

        #region Event Method

        private void ChessGame_GameStartedEvent(object sender, ChessGame.ChessGameEventArgs e)
        {
            this.Invalidate();
        }

        #endregion

        #region private

        /// <summary>
        /// 棋盘上所有的棋格(8*8)
        /// </summary>
        private ChessGrid[,] _chessGrids = new ChessGrid[8, 8];
        /// <summary>
        /// 格子宽度
        /// </summary>
        private int _chessGridWidth = 0;
        /// <summary>
        /// 格子高度
        /// </summary>
        private int _chessGridHeight = 0;
        /// <summary>
        /// 本棋盘拥有的棋局
        /// </summary>
        private ChessGame _ownedChessGame = null;

        private Image _whiteGridImage;
        private Image _blackGridImage;
        private bool _hasGridImage = false;

        private MoveableRectangle _moveableRectangle = null;
        private ChessGrid _sourceGrid = null;
        private ChessGrid _targetGrid = null; 

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
        protected virtual void PaintChessboard(Graphics g)
        {
            int offsetBoardX = 0;
            int offsetBoardY = 0;

            //根据当前的Board的大小形状确定棋格的高宽，坐标并返回这些值
            ChessBoard.GetGridSize(this.Size, out offsetBoardX, out offsetBoardY, out _chessGridWidth, out _chessGridHeight);
            //绘制所有的棋格
            ChessBoard.PaintChessGrid(g, this._chessGrids, _chessGridWidth, _chessGridHeight, offsetBoardX, offsetBoardY, this._whiteGridImage, this._blackGridImage, this._hasGridImage);
            //绘制棋子
            ChessBoard.PaintChessmanImage(g, this._ownedChessGame, this._chessGrids, this._chessGridWidth);

            if (_targetGrid != null)
            {
                g.FillRectangle(Brushes.Tomato, _targetGrid.OwnedRectangle);
                g.DrawRectangle(Pens.Black, _targetGrid.OwnedRectangle);
            }
#if DEBUG
            string ver = this.GetType().Assembly.ToString();
            int m = ver.IndexOf("Version");
            int n = ver.IndexOf("Culture") - m - 2;
            ver = ver.Substring(m, n);
            g.DrawString("Debug: " + ver, new Font("Arial", 8F), Brushes.White, this.Location);
#endif
        }

        private static void PaintChessGrid(Graphics g, ChessGrid[,] grids, int width, int height, int offsetBoardX, int offsetBoardY, Image white, Image black, bool hasImage)
        {
            for (int x = 1; x <= grids.GetLength(0); x++)
            {
                for (int y = 1; y <= grids.GetLength(1); y++)
                {
                    ChessGrid rid = ChessBoard.GetRectangle(x, y, width, height, offsetBoardX, offsetBoardY, x, y);
                    grids[x - 1, y - 1] = rid;
                    if ((x + y) % 2 == 0)
                    {
                        if (hasImage)
                            g.DrawImage(white, rid.OwnedRectangle);
                        else
                            g.FillRectangle(Brushes.WhiteSmoke, rid.OwnedRectangle);
                        g.DrawRectangle(Pens.Black, rid.OwnedRectangle);
#if DEBUG
                        g.DrawString(rid.ToString(), new Font("Arial", 8F), Brushes.Black, rid.RectangleLocation);
#endif
                    }
                    else
                    {
                        if (hasImage)
                            g.DrawImage(black, rid.OwnedRectangle);
                        else
                            g.FillRectangle(Brushes.Gray, rid.OwnedRectangle);
                        g.DrawRectangle(Pens.Black, rid.OwnedRectangle);
#if DEBUG
                        g.DrawString(rid.ToString(), new Font("Arial", 8F), Brushes.White, rid.RectangleLocation);
#endif
                    }
                }
            }
        }

        private static void PaintChessmanImage(Graphics g, ChessGame game, ChessGrid[,] grids, int width)
        {
            if (game != null)
            {
                foreach (Chessman man in game.Chessmans)
                {
                    if (man.IsKilled)
                        continue;
                    ChessGrid point = man.ChessGrids.Peek();
                    ChessGrid rid = grids[point.PointX - 1, 8 - point.PointY];
                    int offset = (int)(width * 0.2);//棋子填充比棋格小20%，以保证美观
                    Rectangle rect = new Rectangle();
                    rect.Location = new Point(rid.OwnedRectangle.X + offset, rid.OwnedRectangle.Y + offset);
                    rect.Size = new Size(rid.OwnedRectangle.Width - offset * 2, rid.OwnedRectangle.Height - offset * 2);
                    if (man.BackgroundImage != null)
                        g.DrawImage(man.BackgroundImage, rect);
#if DEBUG
                    else
                        g.DrawString(man.ToSimpleString(), new Font("Arial Black", 15F), Brushes.Red, rect);
#endif
                }
            }
        }

        private static ChessGrid GetRectangle(int x, int y, int width, int height, int offsetBoardX, int offsetBoardY, int pointX, int pointY)
        {
            Point point = new Point((x - 1) * width + offsetBoardX, (y - 1) * height + offsetBoardY);
            Size size = new Size(width, height);
            return new ChessGrid(pointX, 9 - pointY, point, size); //new Chesspoint(pointX, 9 - pointY));
        }

        private static void GetGridSize(Size size, out int offsetBoardX, out int offsetBoardY, out int width, out int height)
        {
            if (size.Height <= size.Width)
            {
                width = (int)(size.Height / 10);
                height = width;
                offsetBoardX = (int)((size.Width - (width * 8)) / 2);
                offsetBoardY = width;
            }
            else
            {
                width = (int)(size.Width / 10);
                height = width;
                offsetBoardX = width;
                offsetBoardY = (int)((size.Height - (width * 8)) / 2);
            }
        }

        #endregion

        #region OnMouse

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                for (int x = 0; x < _chessGrids.GetLength(0); x++)
                {
                    for (int y = 0; y < _chessGrids.GetLength(1); y++)
                    {
                        _sourceGrid = _chessGrids[x, y];
                        if (_sourceGrid.RectangleContains(e.Location) && _sourceGrid.OwnedChessman != null)
                        {
                            _moveableRectangle = new MoveableRectangle(_sourceGrid.OwnedRectangle, _sourceGrid.OwnedChessman.BackgroundImage);
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
                _moveableRectangle = null;
                for (int x = 0; x < _chessGrids.GetLength(0); x++)
                {
                    for (int y = 0; y < _chessGrids.GetLength(1); y++)
                    {
                        if (_chessGrids[x, y].RectangleContains(e.Location))
                        {
                            _targetGrid = _chessGrids[x, y];
                            _targetGrid.MoveIn(_sourceGrid.OwnedChessman);
                            _sourceGrid.MoveOut();
                            this.InvalidateBoard(_targetGrid.OwnedRectangle);
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
            if (_moveableRectangle != null)
            {
                int offset = _chessGridWidth / 2;
                Point newPoint = new Point(e.Location.X - offset, e.Location.Y - offset);
                Image img = _moveableRectangle.Image;
                _moveableRectangle = new MoveableRectangle(
                    new Rectangle(newPoint, new Size(_chessGridWidth, _chessGridHeight)), img);
                this.InvalidateBoard(_moveableRectangle.Rectangle);
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
            this.InvalidateBoard(null, rectangle);
        }
        /// <summary>
        /// 使控件指定的 "rectangle" 以外的区域无效，重新绘制更新区域
        /// </summary>
        /// <param name="image">指定的 "rectangle" 的背景图片</param>
        /// <param name="rectangle">指定的 "rectangle"</param>
        private void InvalidateBoard(Image image, Rectangle rectangle)
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
            Rectangle left = new Rectangle(0, pt.Y, pt.X, _chessGridHeight + 1);
            Rectangle right = new Rectangle(pt.X + _chessGridWidth + 1, pt.Y, Width - pt.Y - _chessGridWidth, _chessGridHeight + 1);
            Rectangle bottom = new Rectangle(0, pt.Y + _chessGridHeight + 1, Width, Height - pt.Y - _chessGridHeight);
            this.Invalidate(top, false);
            this.Invalidate(left, false);
            this.Invalidate(right, false);
            this.Invalidate(bottom, false);
        }

        #endregion

        #region event

        /// <summary>
        /// 当下棋(移动棋子)后执行
        /// </summary>
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