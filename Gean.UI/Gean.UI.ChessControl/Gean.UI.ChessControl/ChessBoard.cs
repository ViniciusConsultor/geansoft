using System;
using System.Drawing;
using System.Windows.Forms;
using Gean.Wrapper.Chess;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Collections.Specialized;

namespace Gean.UI.ChessControl
{
    /// <summary>
    /// 国际象棋棋盘控件
    /// </summary>
    public class ChessBoard : Control, IChessBoard
    {

        #region ctor

        /// <summary>
        /// 构造函数
        /// </summary>
        public ChessBoard()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.Chocolate;
            this.BackgroundImage = ChessBoardService.BoardImage;
            this.Rectangles = new Rectangle[8, 8];
            this.KeyChessPosition = new ChessPosition(1, 1);
            this.ViewKeyRectangle = false;
            this.Number = 1;

            ChessBoard.GetRectangleSize(this.Size, out _XofPanel, out _YofPanel, out _rectangleWidth, out _rectangleHeight);

            ChessBoardService.BoardImageChangedEvent += new ChessBoardService.BoardImageChangedEventHandler(ChessBoardHelper_BoardImageChangedEvent);
            ChessBoardService.GridImagesChangedEvent += new ChessBoardService.GridImagesChangedEventHandler(ChessBoardHelper_GridImagesChangedEvent);
            ChessBoardService.ChessmanImagesChangedEvent += new ChessBoardService.ChessmanImagesChangedEventHandler(ChessBoardHelper_ChessmanImagesChangedEvent);
        }

        #endregion

        #region Private and Protected

        /// <summary>获取此棋盘上所拥有的所有的棋格矩形(8*8)</summary>
        protected virtual Rectangle[,] Rectangles { get; private set; }

        /// <summary>获取此棋盘上所拥有的棋子集合，默认初始化为通常的32枚棋子</summary>
        protected virtual List<Chessman> Chessmans
        {
            get;
            set;
        }
        private List<Chessman> _chessmans = new List<Chessman>();

        protected virtual List<int> ChessmanDot { get; private set; } 

        protected virtual ChessPosition KeyChessPosition { get; private set; }

        protected virtual ChessPosition[] EnableMoveinPosition { get; private set; }

        protected virtual bool ViewKeyRectangle { get; private set; }

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

        #endregion

        #region Properties

        /// <summary>
        /// 回合编号
        /// </summary>
        public int Number { get; private set; }
        /// <summary>
        /// 获取此棋盘所拥有(包含)的ChessGame
        /// </summary>
        public virtual ChessGame OwnedChessGame
        {
            get { return this._ownedChessGame; }
            set
            {
                ChessGame oldGame = this._ownedChessGame;
                this._ownedChessGame = value;
                OnChessGameChanged(new ChessGameChangedEventArgs(oldGame, value));
            }
        }
        private ChessGame _ownedChessGame;
        /// <summary>
        /// 获取此棋盘当前的战方
        /// </summary>
        public virtual Enums.ChessmanSide CurrChessSide { get; private set; }
        /// <summary>
        /// 获取已选择的棋子所在的棋格坐标
        /// </summary>
        public virtual ChessPosition SelectedChessPosition { get; private set; }
        /// <summary>
        /// 获取与设置棋子将要被移到的棋格坐标
        /// </summary>
        public virtual ChessPosition TargetChessPosition { get; set; }

        #endregion

        #region Method

        /// <summary>
        /// 载入新棋局
        /// </summary>
        public virtual void LoadGame()
        {
            this._ownedChessGame = new ChessGame();
            this.CurrChessSide = Enums.ChessmanSide.White;
            this.InitializeChessmans();
            this.RegisterChessmans(this.Chessmans);
        }
        /// <summary>
        /// 载入新棋局
        /// </summary>
        /// <param name="chessmans">指定的棋子集合，一般是指残局或中盘棋局</param>
        public virtual void LoadGame(IEnumerable<Chessman> chessmans)
        {
            this._ownedChessGame = new ChessGame();
            this.CurrChessSide = Enums.ChessmanSide.White;
            this.InitializeChessmans(chessmans);
            this.RegisterChessmans(this.Chessmans);
        }

        #endregion

        #region Chessman List Protected Method

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

        /// <summary>
        /// 注册所有棋子当相应的棋格
        /// </summary>
        /// <param name="chessmans"></param>
        protected virtual void RegisterChessmans(IEnumerable<Chessman> chessmans)
        {
            foreach (Chessman man in chessmans)
            {
                //ChessPosition point = man.ChessPositions.Peek();
                //this._ownedChessGame.MoveIn(0, this._ownedChessGame, man, Enums.Action.Opennings);
            }
        }

        #endregion

        #region override

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ChessBoard.GetRectangleSize(this.Size, out _XofPanel, out _YofPanel, out _rectangleWidth, out _rectangleHeight);
        }
        
        protected override void OnPaintBackground(PaintEventArgs pe)
        {
            base.OnPaintBackground(pe);
            Graphics g = pe.Graphics;
            ChessBoard.PaintChessBoardGrid(g, this.Rectangles, _XofPanel, _YofPanel, _rectangleWidth, _rectangleHeight);
            if (this._ownedChessGame != null && this.Chessmans != null)
            {
                ChessBoard.PaintChessmanImage(g, this.Chessmans, this);
                ChessBoard.PaintEnableMoveInChessPoint(g, this);
                ChessBoard.PaintSelectedChessPoint(g, this);
            }
            g.Flush();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (this._ownedChessGame == null) return;

            switch (e.KeyCode)
            {
                #region 方向控制
                case Keys.Down:
                    if (this.KeyChessPosition.Y > 1)
                    {
                        this.KeyChessPosition = new ChessPosition(this.KeyChessPosition.X, this.KeyChessPosition.Y - 1);
                        this.SetSelectedPointByKey();
                    }
                    break;
                case Keys.Up:
                    if (this.KeyChessPosition.Y < 8)
                    {
                        this.KeyChessPosition = new ChessPosition(this.KeyChessPosition.X, this.KeyChessPosition.Y + 1);
                        this.SetSelectedPointByKey();
                    }
                    break;
                case Keys.Left:
                    if (this.KeyChessPosition.X > 1)
                    {
                        this.KeyChessPosition = new ChessPosition(this.KeyChessPosition.X - 1, this.KeyChessPosition.Y);
                        this.SetSelectedPointByKey();
                    }
                    break;
                case Keys.Right:
                    if (this.KeyChessPosition.Y < 8)
                    {
                        this.KeyChessPosition = new ChessPosition(this.KeyChessPosition.X + 1, this.KeyChessPosition.Y);
                        this.SetSelectedPointByKey();
                    }
                    break;
                #endregion

                #region 选择
                case Keys.Space:
                    this.ViewKeyRectangle = true;
                    this.SetSelectedPointByKey();
                    break;
                case Keys.Enter:
                    break;
                case Keys.Escape:
                    this.ViewKeyRectangle = false;
                    break;
                #endregion

                #region 棋子快捷键
                case Keys.P:
                    break;
                case Keys.R:
                    break;
                case Keys.N:
                    break;
                case Keys.B:
                    break;
                case Keys.K:
                    break;
                case Keys.Q:
                    break;
                #endregion

                default:
                    break;
            }
            this.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this._ownedChessGame == null) return;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    #region 选择ChessPosition,并MoveIn
                    for (int x = 1; x <= 8; x++)
                    {
                        for (int y = 1; y <= 8; y++)
                        {
                            if (!this.Rectangles[x - 1, y - 1].Contains(e.Location))
                                continue;

                            ChessPosition position = this._ownedChessGame[x, y];
                            if (this.SelectedChessPosition == ChessPosition.Empty)//选择棋子
                            {
                                //if (Chessman.IsNullOrEmpty(position.Occupant))
                                    return;//找到矩形，但矩形中无棋子，退出
                                //if (position.Occupant.ChessmanSide != this.CurrChessSide)
                                    return;//棋子的战方与当前棋局要求的战方不符，退出

                                //鼠标到达的棋格符合所有规则，记录下棋格的坐标，等待移动
                                this.SelectedChessPosition = new ChessPosition(x, y);

                                this.EnableMoveinPosition = this.SelectedChessPosition.GetPositions(null, this.CurrChessSide);
                                this.Invalidate();
                            }
                            else//移动棋子
                            {
                                //目标棋格有棋子
                                if (!Chessman.IsNullOrEmpty(null))
                                {
                                    //目标棋格棋子的战方不符合规则
                                    //if (position.Occupant.ChessmanSide == this.CurrChessSide)
                                    //{
                                    //    return;
                                    //}
                                }
                                this.TargetChessPosition = new ChessPosition(x, y);
                                this.MoveIn(this.SelectedChessPosition, this.TargetChessPosition);
                            }//this.SelectedChessPosition == ?
                        }
                    }
                    #endregion
                    break;
                case MouseButtons.Middle:
                case MouseButtons.Right:
                case MouseButtons.None:
                case MouseButtons.XButton1:
                case MouseButtons.XButton2:
                default:
                    break;
            }
        }

        #endregion

        #region private

        #region private Invalidate Method

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
            g.FillRectangle(Brushes.Transparent, rectangle);
            if (image != null)
            {
                g.DrawImage(image, rectangle);
            }

            // 使控件的 "rectangle" 以外的区域无效，重新绘制更新区域
            Point pt = rectangle.Location;
            Rectangle top = new Rectangle(0, 0, this.Width, pt.Y);
            Rectangle left = new Rectangle(0, pt.Y, pt.X, rectangle.Height + 1);
            Rectangle right = new Rectangle
                (pt.X + rectangle.Width + 1, pt.Y, this.Width - pt.Y - rectangle.Width, rectangle.Height + 1);
            Rectangle bottom = new Rectangle
                (0, pt.Y + rectangle.Height + 1, this.Width, this.Height - pt.Y - rectangle.Height);
            this.Invalidate(top, false);
            this.Invalidate(left, false);
            this.Invalidate(right, false);
            this.Invalidate(bottom, false);
        }

        #endregion

        /// <summary>
        /// 设置通过键盘选择的棋格
        /// </summary>
        protected virtual void SetSelectedPointByKey()
        {
            ChessPosition rid = this._ownedChessGame[this.KeyChessPosition.X, this.KeyChessPosition.Y];
            Chessman man = null;// rid.Occupant;
            if (man == null)
                return;
            if (man.ChessmanSide != this.CurrChessSide)
                return;
            this.SelectedChessPosition = this.KeyChessPosition;
        }

        /// <summary>
        /// 棋子移动的方法
        /// </summary>
        public virtual void MoveIn(ChessPosition srcPos, ChessPosition tgtPos)
        {
            //ChessGrid srcGrid = this._ownedChessGame[srcPos.X + 1, srcPos.Y + 1];
            //ChessGrid tgtGrid = this._ownedChessGame[tgtPos.X + 1, tgtPos.Y + 1];
            Chessman man = null;// srcGrid.Occupant;
            Enums.Action action = Enums.Action.General;

            if (TryMoveIn(man, srcPos, srcPos, out action))
            {
                ////核心行棋动作
                //ChessStep chessStep = tgtPos.MoveIn(this.Number, this._ownedChessGame, man, action);
                //OnPlay(new PlayEventArgs(chessStep));//注册行棋事件
                ////转换战方
                //this.CurrChessSide = Enums.GetOtherSide(this.CurrChessSide);
                ////刷新
                //this.Invalidate();
                //this.SelectedChessPosition = ChessPosition.Empty;
                //if (this.CurrChessSide == Enums.ChessmanSide.Black)
                //    this.Number++;
            }
        }

        /// <summary>
        /// 获取指定的棋子是否能够从指定的源棋格移动到指定的目标棋格，并返回该步棋的Enums.Action值
        /// </summary>
        /// <param name="chessman">指定的棋子</param>
        /// <param name="sourceGrid">指定的棋子所在的源棋格</param>
        /// <param name="targetGrid">指定的目标棋格</param>
        /// <param name="action">该步棋的Enums.Action值</param>
        /// <returns></returns>
        public static bool TryMoveIn(Chessman chessman, ChessPosition sourcePos, ChessPosition targetPos, out Enums.Action action)
        {
            if (chessman == null) throw new ArgumentNullException("Chessman cannot NULL.");
            if (sourcePos == null) throw new ArgumentNullException("Source ChessGrid cannot NULL.");
            if (targetPos == null) throw new ArgumentNullException("Target ChessGrid cannot NULL.");

            action = Enums.Action.Invalid;



            //if (!Chessman.IsNullOrEmpty(targetPos.Occupant))
            //    action = Enums.Action.Capture;
            //else
            //    action = Enums.Action.General;
            return true;
        }
        #endregion

        #region Image Changed Event

        protected virtual void ChessBoardHelper_BoardImageChangedEvent(ChessBoardService.BoardImageChangedEventArgs e)
        {
            this.BackgroundImage = e.BoardImage;
            this.Invalidate();
        }
        protected virtual void ChessBoardHelper_GridImagesChangedEvent(ChessBoardService.GridImagesChangedEventArgs e)
        {
            this.Invalidate();
        }
        protected virtual void ChessBoardHelper_ChessmanImagesChangedEvent(ChessBoardService.ChessmanImagesChangedEventArgs e)
        {
            this.Invalidate();
        }

        #endregion

        #region static Method

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
                        g.DrawImage(ChessBoardService.WhiteGridImage, _currRect);
                        g.DrawRectangle(Pens.Black, _currRect);
#if DEBUG
                        g.DrawString(string.Format("{0},{1}", x, y), new Font("Arial", 6.5F), Brushes.Black, _currRect.Location);
#endif
                    }
                    else
                    {
                        g.DrawImage(ChessBoardService.BlackGridImage, _currRect);
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
                if (man.IsCaptured)
                    continue;
                _currManRect = ChessBoard.GetChessmanRectangle(board, man.CurrPosition);
                _currManImage = ChessBoardService.GetChessmanImage(man.ChessmanSide, man.ChessmanType);
                g.DrawImage(_currManImage, _currManRect);

            }
            _currManRect = Rectangle.Empty;
            _currManImage = null;
        }

        private static void PaintSelectedChessPoint(Graphics g, ChessBoard chessBoard)
        {
            if (chessBoard.SelectedChessPosition == null)
                return;
            Rectangle rect = Rectangle.Empty;
            if (chessBoard.ViewKeyRectangle)
            {
                rect = chessBoard.Rectangles[chessBoard.KeyChessPosition.X, chessBoard.KeyChessPosition.Y];
                g.DrawRectangle(Pens.White, rect);
            }
            rect = chessBoard.Rectangles[chessBoard.SelectedChessPosition.X, chessBoard.SelectedChessPosition.Y];
            for (int i = -1; i <= 1; i++)
            {
                g.DrawRectangle(Pens.Yellow, Rectangle.Inflate(rect, i, i));
            }
        }

        private static void PaintEnableMoveInChessPoint(Graphics g, ChessBoard chessBoard)
        {
            if (chessBoard.SelectedChessPosition == null)
                return;
            if (chessBoard.EnableMoveinPosition == null)
                return;
            Rectangle rect = Rectangle.Empty;
            foreach (ChessPosition position in chessBoard.EnableMoveinPosition)
            {
                rect = chessBoard.Rectangles[position.X, position.Y];
                for (int i = 0; i <= 1; i++)
                {
                    g.DrawRectangle(Pens.Green, Rectangle.Inflate(rect, i, i));
                }
            }
        }

        /// <summary>
        /// 获取棋子将绘制的矩形(棋子填充比棋格小15%，以保证美观)
        /// </summary>
        /// <param name="board">传递引用的ChessBoard类型</param>
        /// <param name="chessGrid">ChessGrid</param>
        private static Rectangle GetChessmanRectangle(ChessBoard board, ChessPosition pos)
        {
            int offset = (int)(board._rectangleWidth * 0.15);//棋子填充比棋格小20%，以保证美观
            Rectangle boardRect = board.Rectangles[pos.X - 1, pos.Y - 1];
            Rectangle rtnRect = new Rectangle();
            rtnRect.Location = new Point(boardRect.X + offset, boardRect.Y + offset);
            rtnRect.Size = new Size(boardRect.Width - offset * 2, boardRect.Height - offset * 2);
            return rtnRect;
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

        #region Custom EVENT

        /// <summary>
        /// 当棋盘拥有的棋局发生变化后发生
        /// </summary>
        public event ChessGameChangedEventHandler ChessGameChangedEvent;
        protected virtual void OnChessGameChanged(ChessGameChangedEventArgs e)
        {
            if (ChessGameChangedEvent != null)
                ChessGameChangedEvent(this, e);
        }
        public delegate void ChessGameChangedEventHandler(object sender, ChessGameChangedEventArgs e);
        public class ChessGameChangedEventArgs : ChangedEventArgs<ChessGame>
        {
            public ChessGameChangedEventArgs(ChessGame oldGame,ChessGame newGame)
                : base(oldGame, newGame)
            { 
            }
        }

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

        #endregion

    }

}
