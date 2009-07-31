using System;
using System.Drawing;
using System.Windows.Forms;
using Gean.Wrapper.Chess;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace Gean.UI.ChessControl
{
    /// <summary>
    /// 国际象棋棋盘控件
    /// </summary>
    public class ChessBoard : Control
    {
        /// <summary>
        /// 棋盘上所有的棋格(8*8)
        /// </summary>
        protected virtual Rectangle[,] OwnedRectangles { get; private set; }
        public ChessGame OwnedChessGame { get; private set; }
        public Enums.ChessmanSide CurrChessSide { get; private set; }

        /// <summary>
        /// 获取或设置鼠标置入棋格后棋格高亮显示。true高亮，false无高亮。
        /// </summary>
        public bool EnableGridHighlight { get; set; }
        /// <summary>
        /// 获取或设置选中棋子后，该棋子可能的行棋路径高亮显示。true高亮，false无高亮。
        /// </summary>
        public bool EnablePathHighlight { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ChessBoard()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.Chocolate;
            this.BackgroundImage = ChessBoardHelper.BoardImage;
            this.OwnedRectangles = new Rectangle[8, 8];

            ChessBoard.GetRectangleSize(this.Size, out _XofPanel, out _YofPanel, out _rectangleWidth, out _rectangleHeight);

            ChessBoardHelper.BoardImageChangedEvent += new ChessBoardHelper.BoardImageChangedEventHandler(ChessBoardHelper_BoardImageChangedEvent);
            ChessBoardHelper.GridImagesChangedEvent += new ChessBoardHelper.GridImagesChangedEventHandler(ChessBoardHelper_GridImagesChangedEvent);
            ChessBoardHelper.ChessmanImagesChangedEvent += new ChessBoardHelper.ChessmanImagesChangedEventHandler(ChessBoardHelper_ChessmanImagesChangedEvent);
        }

        /// <summary>
        /// 载入新棋局
        /// </summary>
        public void LoadGame()
        {
            this.OwnedChessGame = new ChessGame();
            this.CurrChessSide = Enums.ChessmanSide.White;
            this.InitializeChessmans();
            this.RegisterChessmans(this.Chessmans);
        }
        /// <summary>
        /// 载入新棋局
        /// </summary>
        /// <param name="chessmans">指定的棋子集合，可能是残局或中盘棋局</param>
        public void LoadGame(IEnumerable<Chessman> chessmans)
        {
            this.OwnedChessGame = new ChessGame();
            this.CurrChessSide = Enums.ChessmanSide.White;
            this.InitializeChessmans(chessmans);
            this.RegisterChessmans(this.Chessmans);
        }

        #region Chessman List

        internal virtual List<Chessman> Chessmans { get; private set; }

        /// <summary>
        /// 初始化默认棋子集合（32个棋子）
        /// </summary>
        protected virtual void InitializeChessmans()
        {
            this.Chessmans = new List<Chessman>(32);
            #region
            //兵
            for (int i = 1; i <= 8; i++)
            {
                this.Chessmans.Add(new ChessmanPawn(Enums.ChessmanSide.White, i));//白兵
                this.Chessmans.Add(new ChessmanPawn(Enums.ChessmanSide.Black, i));//黑兵
            }
            //王
            this.Chessmans.Add(new ChessmanKing(Enums.ChessmanSide.White));
            this.Chessmans.Add(new ChessmanKing(Enums.ChessmanSide.Black));
            //后
            this.Chessmans.Add(new ChessmanQueen(Enums.ChessmanSide.White));
            this.Chessmans.Add(new ChessmanQueen(Enums.ChessmanSide.Black));
            //车
            this.Chessmans.Add(new ChessmanRook(Enums.ChessmanSide.White, Enums.ChessGridSide.White));
            this.Chessmans.Add(new ChessmanRook(Enums.ChessmanSide.White, Enums.ChessGridSide.Black));
            this.Chessmans.Add(new ChessmanRook(Enums.ChessmanSide.Black, Enums.ChessGridSide.White));
            this.Chessmans.Add(new ChessmanRook(Enums.ChessmanSide.Black, Enums.ChessGridSide.Black));
            //马
            this.Chessmans.Add(new ChessmanKnight(Enums.ChessmanSide.White, Enums.ChessGridSide.White));
            this.Chessmans.Add(new ChessmanKnight(Enums.ChessmanSide.White, Enums.ChessGridSide.Black));
            this.Chessmans.Add(new ChessmanKnight(Enums.ChessmanSide.Black, Enums.ChessGridSide.White));
            this.Chessmans.Add(new ChessmanKnight(Enums.ChessmanSide.Black, Enums.ChessGridSide.Black));
            //象
            this.Chessmans.Add(new ChessmanBishop(Enums.ChessmanSide.White, Enums.ChessGridSide.White));
            this.Chessmans.Add(new ChessmanBishop(Enums.ChessmanSide.White, Enums.ChessGridSide.Black));
            this.Chessmans.Add(new ChessmanBishop(Enums.ChessmanSide.Black, Enums.ChessGridSide.White));
            this.Chessmans.Add(new ChessmanBishop(Enums.ChessmanSide.Black, Enums.ChessGridSide.Black));
            #endregion
            this.Invalidate();
        }

        /// <summary>
        /// 初始化棋子集合
        /// </summary>
        /// <param name="images">一个指定棋子集合</param>
        protected virtual void InitializeChessmans(IEnumerable<Chessman> chessmans)
        {
            this.Chessmans = new List<Chessman>(32);
            this.Chessmans.AddRange(chessmans);
            this.Chessmans.TrimExcess();
            this.Invalidate();
        }

        protected virtual void RegisterChessmans(IEnumerable<Chessman> chessmans)
        {
            foreach (Chessman man in chessmans)
            {
                ChessPoint point = man.ChessPoints.Peek();
                this.OwnedChessGame[point.X, point.Y].MoveIn(this.OwnedChessGame, man, Enums.Action.Opennings);
            }
        }

        #endregion

        /// <summary>
        /// 棋盘左上角X坐标
        /// </summary>
        protected int _XofPanel = 0;
        /// <summary>
        /// 棋盘左上角Y坐标
        /// </summary>
        protected int _YofPanel = 0;
        /// <summary>
        /// 矩形宽度
        /// </summary>
        protected int _rectangleWidth = 0;
        /// <summary>
        /// 矩形高度
        /// </summary>
        protected int _rectangleHeight = 0;

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ChessBoard.GetRectangleSize(this.Size, out _XofPanel, out _YofPanel, out _rectangleWidth, out _rectangleHeight);
        }
        protected override void OnPaintBackground(PaintEventArgs pe)
        {
            base.OnPaintBackground(pe);
            Graphics g = pe.Graphics;
            ChessBoard.PaintChessBoardGrid(g, this.OwnedRectangles, _XofPanel, _YofPanel, _rectangleWidth, _rectangleHeight);
            if (this.OwnedChessGame != null && this.Chessmans != null)
            {
                ChessBoard.PaintChessmanImage(g, this.Chessmans, this);
            }
            g.Flush();
        }

        #region OnMouse

        private ChessPoint _sourcePoint = ChessPoint.Empty;
        private ChessPoint _targetPoint = ChessPoint.Empty;
        private ChessGrid _tmpGrid;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this.OwnedChessGame == null) return;
            if (e.Button == MouseButtons.Left)
            {
                for (int x = 1; x <=8; x++)
                {
                    for (int y = 1; y <=8; y++)
                    {
                        if (!this.OwnedRectangles[x-1, y-1].Contains(e.Location))
                            continue;

                        this._tmpGrid = this.OwnedChessGame[x, y];
                        
                        if (Chessman.IsNullOrEmpty(_tmpGrid.OwnedChessman))
                            return;//找到矩形，但矩形中无棋子，退出
                        if (_tmpGrid.OwnedChessman.ChessmanSide != this.CurrChessSide)
                            return;//棋子的战方与当前棋局要求的战方不符，退出

                        //鼠标到达的棋格符合所有规则，记录下棋格的坐标，等待移动
                        _sourcePoint = new ChessPoint(x, y);
                        Console.WriteLine(_sourcePoint);
                    }
                }
            }//if
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (this.OwnedChessGame == null) return;
            if (this._sourcePoint.Equals(ChessPoint.Empty)) return;
            if (e.Button == MouseButtons.Left)
            {
                for (int x = 1; x <= 8; x++)
                {
                    for (int y = 1; y <= 8; y++)
                    {
                        if (!this.OwnedRectangles[x-1, y-1].Contains(e.Location))
                            continue;
                        
                        this._tmpGrid = this.OwnedChessGame[x, y];
                        //目标棋格有棋子
                        if (!Chessman.IsNullOrEmpty(_tmpGrid.OwnedChessman))
                        {
                            //目标棋格棋子的战方不符合规则
                            if (_tmpGrid.OwnedChessman.ChessmanSide == this.CurrChessSide)
                                return;
                        }
                        Enums.Action action = Enums.Action.General;
                        Chessman man = this.OwnedChessGame[this._sourcePoint.X, this._sourcePoint.Y].OwnedChessman;

                        if (ChessPath.TryMoveInGrid(man, null, null, out action))
                        {
                            //this._tmpGrid.MoveIn(this.OwnedChessGame, man, action);
                        }

                        if (!Chessman.IsNullOrEmpty(this._tmpGrid.OwnedChessman))
                            action = Enums.Action.Kill;
                        //核心，行棋动作
                        ChessStep chessStep = this._tmpGrid.MoveIn(this.OwnedChessGame, man, action);
                        OnPlay(new PlayEventArgs(chessStep));//注册行棋事件
                        this._sourcePoint = ChessPoint.Empty;
                        //转换战方
                        this.CurrChessSide = Enums.GetOtherSide(this.CurrChessSide);
                        //刷新
                        this.Invalidate();
                    }//for y
                }//for x
            }//if
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.OwnedChessGame == null) return;
            //if (_movedPoint != null)
            //{
            //    int offset = _chessGridWidth / 2;
            //    Point newPoint = new Point(e.Location.X - offset, e.Location.Y - offset);
            //    Image img = _movedPoint.Image;
            //    _movedPoint = new MoveableRectangle(
            //        new Rectangle(newPoint, new Size(_chessGridWidth, _chessGridHeight)), img);
            //    this.InvalidateBoard(_movedPoint.Rectangle);
            //}
            ChessBoard.SetRectangleCursor(this.OwnedChessGame, this, e.Location);
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
            //Point pt = rectangle.Location;
            //Rectangle top = new Rectangle(0, 0, Width, pt.Y);
            //Rectangle left = new Rectangle(0, pt.Y, pt.X, _chessGridHeight + 1);
            //Rectangle right = new Rectangle(pt.X + _chessGridWidth + 1, pt.Y, Width - pt.Y - _chessGridWidth, _chessGridHeight + 1);
            //Rectangle bottom = new Rectangle(0, pt.Y + _chessGridHeight + 1, Width, Height - pt.Y - _chessGridHeight);
            //this.Invalidate(top, false);
            //this.Invalidate(left, false);
            //this.Invalidate(right, false);
            //this.Invalidate(bottom, false);
        }

        #endregion



        protected virtual void ChessBoardHelper_BoardImageChangedEvent(ChessBoardHelper.BoardImageChangedEventArgs e)
        {
            this.BackgroundImage = e.BoardImage;
            this.Invalidate();
        }
        protected virtual void ChessBoardHelper_GridImagesChangedEvent(ChessBoardHelper.GridImagesChangedEventArgs e)
        {
            this.Invalidate();
        }
        protected virtual void ChessBoardHelper_ChessmanImagesChangedEvent(ChessBoardHelper.ChessmanImagesChangedEventArgs e)
        {
            this.Invalidate();
        }

        #region static

        private static Rectangle _enterRect = Rectangle.Empty;
        private static ChessGrid _enterGrid;
        private static void SetRectangleCursor(ChessGame game, ChessBoard board, Point point)
        {
            for (int x = 1; x <= 8; x++)
            {
                for (int y = 1; y <= 8; y++)
                {
                    _enterRect = board.OwnedRectangles[x - 1, y - 1];//循环矩形集合
                    if (!_enterRect.Contains(point))
                    {
                        board.Cursor = Cursors.Default;
                        continue;
                    }
                    else//找到相应的矩形
                    {
                        _enterGrid = game[x, y];
                        if (Chessman.IsNullOrEmpty(_enterGrid.OwnedChessman))
                            return;
                        board.Cursor = Cursors.Hand;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 仅供棋盘绘制时声明的矩形变量。防止反复在OnPaint事件中声明造成内存碎片。
        /// </summary>
        private static Rectangle _currRect = Rectangle.Empty;
        /// <summary>
        /// 仅供棋盘绘制时声明的棋子背景图片Image变量。防止反复在OnPaint事件中声明造成内存碎片。
        /// </summary>
        private static Image _currManImage;
        /// <summary>
        /// 仅供棋盘绘制时声明棋子背景图片的矩形变量。防止反复在OnPaint事件中声明造成内存碎片。
        /// </summary>
        private static Rectangle _currManRect;

        /// <summary>
        /// 绘制棋盘
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="rectangles">64个棋格矩形集合</param>
        /// <param name="hasGridImage">是否有棋格矩形背景图片</param>
        /// <param name="XofPanel">棋盘左上角X坐标</param>
        /// <param name="YofPanel">棋盘左上角Y坐标</param>
        /// <param name="rectangleWidth">矩形宽度</param>
        /// <param name="rectangleHeight">矩形高度</param>
        private static void PaintChessBoardGrid
            (Graphics g, Rectangle[,] rectangles, int XofPanel, int YofPanel, int rectangleWidth, int rectangleHeight)
        {
            for (int x = 1; x <= 8; x++)
            {
                for (int y = 1; y <= 8; y++)
                {
                    _currRect = ChessBoard.GetRectangle(x, y, XofPanel, YofPanel, rectangleWidth, rectangleHeight);
                    rectangles[x - 1, y - 1] = _currRect;
                    if ((x + y) % 2 != 0)
                    {
                        g.DrawImage(ChessBoardHelper.WhiteGridImage, _currRect);
                        g.DrawRectangle(Pens.Black, _currRect);
#if DEBUG
                        g.DrawString(string.Format("{0},{1}", x, y), new Font("Arial", 6.5F), Brushes.Black, _currRect.Location);
#endif
                    }
                    else
                    {
                        g.DrawImage(ChessBoardHelper.BlackGridImage, _currRect);
                        g.DrawRectangle(Pens.Black, _currRect);
#if DEBUG
                        g.DrawString(string.Format("{0},{1}", x, y), new Font("Arial", 6.5F), Brushes.White, _currRect.Location);
#endif
                    }
                }
            }
            _currRect = Rectangle.Empty;
        }

        /// <summary>
        /// 用指定的棋子图片集合绘制棋子
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="chessmans">指定的棋子图片集合</param>
        /// <param name="board">传递引用的ChessBoard类型</param>
        private static void PaintChessmanImage(Graphics g, List<Chessman> chessmans, ChessBoard board)
        {
            foreach (Chessman man in chessmans)
            {
                if (man.IsKilled)
                    continue;
                ChessPoint point = man.ChessPoints.Peek();
                ChessGrid chessGrid = board.OwnedChessGame[point.X, point.Y];
                ChessBoard.GetChessmanRectangle(board, chessGrid);
                _currManImage = ChessBoardHelper.GetChessmanImage(man.ChessmanSide, man.ChessmanType);
                g.DrawImage(_currManImage, _currManRect);

            }
            _currManRect = Rectangle.Empty;
            _currManImage = null;
        }

        /// <summary>
        /// 获取棋子将绘制的矩形(棋子填充比棋格小15%，以保证美观)
        /// </summary>
        /// <param name="board">传递引用的ChessBoard类型</param>
        /// <param name="chessGrid">ChessGrid</param>
        private static void GetChessmanRectangle(ChessBoard board, ChessGrid chessGrid)
        {
            int offset = (int)(board._rectangleWidth * 0.15);//棋子填充比棋格小20%，以保证美观
            Rectangle rectangle = board.OwnedRectangles[chessGrid.PointX - 1, chessGrid.PointY - 1];
            _currManRect.Location = new Point(rectangle.X + offset, rectangle.Y + offset);
            _currManRect.Size = new Size(rectangle.Width - offset * 2, rectangle.Height - offset * 2);
        }

        /// <summary>
        /// 根据棋盘棋格位置计算出实际棋格的绝对位置信息
        /// </summary>
        /// <param name="x">棋格X坐标</param>
        /// <param name="y">棋格Y坐标</param>
        /// <param name="XofPanel">棋盘左上角X坐标</param>
        /// <param name="YofPanel">棋盘左上角Y坐标</param>
        /// <param name="rectangleWidth">矩形宽度</param>
        /// <param name="rectangleHeight">矩形高度</param>
        /// <returns></returns>
        private static Rectangle GetRectangle
            (int x, int y, int XofPanel, int YofPanel, int rectangleWidth, int rectangleHeight)
        {
            Point point = new Point((x - 1) * rectangleWidth + XofPanel, (8 - y) * rectangleHeight + YofPanel);
            Size size = new Size(rectangleWidth, rectangleHeight);
            return new Rectangle(point, size);
        }

        /// <summary>
        /// 根据桌面的尺寸获取棋格的相关尺寸信息
        /// </summary>
        /// <param name="panelSize">桌面大小</param>
        /// <param name="offsetPanelX">棋盘在桌面的左上角的X坐标</param>
        /// <param name="offsetPanelY">棋盘在桌面的左上角的Y坐标</param>
        /// <param name="rectangleWidth">棋盘矩形的宽度</param>
        /// <param name="rectangleHeight">棋盘矩形的高度</param>
        private static void GetRectangleSize
            (Size panelSize, out int XofPanel, out int YofPanel, out int rectangleWidth, out int rectangleHeight)
        {
            if (panelSize.Height <= panelSize.Width)
            {
                rectangleWidth = (int)(panelSize.Height / 10);
                rectangleHeight = rectangleWidth;
                XofPanel = (int)((panelSize.Width - (rectangleWidth * 8)) / 2);
                YofPanel = rectangleWidth;
            }
            else
            {
                rectangleWidth = (int)(panelSize.Width / 10);
                rectangleHeight = rectangleWidth;
                XofPanel = rectangleWidth;
                YofPanel = (int)((panelSize.Height - (rectangleWidth * 8)) / 2);
            }
        }

        #endregion

        /// <summary>
        /// 在行棋后发生
        /// </summary>
        public event PlayEventHandler PlayEvent;
        protected virtual void OnPlay(PlayEventArgs e)
        {
            if (PlayEvent != null)
                PlayEvent(this, e);
        }
        public delegate void PlayEventHandler(object sender, PlayEventArgs e);
        public class PlayEventArgs : EventArgs
        {
            public ChessStep ChessStep { get; private set; }
            public PlayEventArgs(ChessStep chessStep)
            {
                this.ChessStep = chessStep;
            }
        }
    }

}
