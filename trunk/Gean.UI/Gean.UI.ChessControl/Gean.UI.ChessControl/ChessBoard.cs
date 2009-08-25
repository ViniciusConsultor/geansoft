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

        #region protected

        protected virtual Image OccImage { get; private set; }

        /// <summary>
        /// 获取此棋盘上所拥有的所有的棋格矩形(8*8)
        /// </summary>
        protected virtual Rectangle[,] Rectangles { get; private set; }

        protected virtual ChessPosition[] EnableMoveInPosition { get; private set; }

        /// <summary>
        /// 获取已选择的棋子所在的棋格坐标
        /// </summary>
        protected virtual ChessPosition SelectedChessPosition { get; private set; }

        /// <summary>
        /// 获取已移动到的有效棋子所在的棋格坐标
        /// </summary>
        protected virtual ChessPosition MouseMovedChessPosition { get; private set; }

        /// <summary>
        /// 获取与设置棋子将要被移到的棋格坐标
        /// </summary>
        protected virtual ChessPosition TargetChessPosition { get; set; }

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

        #region Property

        /// <summary>
        /// 当前棋盘所拥有的棋子集合
        /// </summary>
        public virtual ChessmanCollection Chessmans { get; set; }
        /// <summary>
        /// 获取此棋盘所拥有(包含)的ChessGame
        /// </summary>
        public virtual ChessGame ChessGame
        {
            get { return this._ChessGame; }
            set
            {
                ChessGame oldGame = this._ChessGame;
                this._ChessGame = value;
                OnChessGameChanged(new ChessGameChangedEventArgs(oldGame, value));
            }
        }
        private ChessGame _ChessGame;
        /// <summary>
        /// 回合编号
        /// </summary>
        public int Number { get { return _ChessGame.Number; } }
        /// <summary>
        /// 获取此棋盘当前的战方
        /// </summary>
        public virtual Enums.ChessmanSide CurrChessSide { get { return _ChessGame.ChessmanSide; } }

        #endregion

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

            ChessBoard.GetRectangleSize(this.Size, out _XofPanel, out _YofPanel, out _rectangleWidth, out _rectangleHeight);

            ChessBoardService.BoardImageChangedEvent += new ChessBoardService.BoardImageChangedEventHandler(ChessBoardHelper_BoardImageChangedEvent);
            ChessBoardService.GridImagesChangedEvent += new ChessBoardService.GridImagesChangedEventHandler(ChessBoardHelper_GridImagesChangedEvent);
            ChessBoardService.ChessmanImagesChangedEvent += new ChessBoardService.ChessmanImagesChangedEventHandler(ChessBoardHelper_ChessmanImagesChangedEvent);
        }

        #endregion

        #region LoadGame Method

        /// <summary>
        /// 载入新棋局
        /// </summary>
        public virtual void LoadGame()
        {
            this._ChessGame = new ChessGame();
            this.InitializeChessmans();
        }
        /// <summary>
        /// 载入新棋局
        /// </summary>
        /// <param name="chessmans">指定的棋子集合，一般是指残局或中盘棋局</param>
        public virtual void LoadGame(FENBuilder fenBuilder)
        {
            this._ChessGame = new ChessGame();
            this.InitializeChessmans(fenBuilder.ToChessmans());
        }

        #endregion

        #region Initialize Chessmans

        /// <summary>
        /// 初始化默认棋子集合（32个棋子）
        /// </summary>
        protected virtual void InitializeChessmans()
        {
            this.Chessmans = ChessmanCollection.OpeningChessmansCreator();
            this.Invalidate();
        }

        /// <summary>
        /// 初始化棋子集合
        /// </summary>
        /// <param name="images">一个指定棋子集合</param>
        protected virtual void InitializeChessmans(IEnumerable<Chessman> chessmans)
        {
            this.Chessmans = new ChessmanCollection();
            this.Chessmans.AddRange(chessmans);
            this.Invalidate();
        }

        #endregion

        #region Override: OnResize, OnCreateControl

        //1.
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.OccImage = new Bitmap(this.Width, this.Height);
            ChessBoard.GetRectangleSize(this.Size, out _XofPanel, out _YofPanel, out _rectangleWidth, out _rectangleHeight);
        }

        //2. 该方法仅会在最初执行一次
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        #endregion

        #region Override: OnPaint……

        //3.
        protected override void OnPaintBackground(PaintEventArgs pe)
        {
            base.OnPaintBackground(pe);
            Graphics g = pe.Graphics;
            this.Paint_ChessBoardGrid();
            if (this._ChessGame != null && this.Chessmans != null)
            {
                this.Paint_ChessmanImage();
                this.Paint_EnableMoveInPosition();
                this.Paint_MouseMovedPosition();
                this.Paint_SelectedPosition();
                this.Paint_SelectedMovedChessman();
            }
            g.DrawImage(this.OccImage, new Point(0, 0));
            g.Flush();
        }

        //4.
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            //Graphics g = pe.Graphics;
            //this.Paint_ChessBoardGrid();
            //if (this._ChessGame != null && this.Chessmans != null)
            //{
            //    this.Paint_ChessmanImage();
            //    this.Paint_EnableMoveInPosition();
            //    this.Paint_MouseMovedPosition();
            //    this.Paint_SelectedPosition();
            //    this.Paint_SelectedMovedChessman();
            //}
            //g.DrawImage(this.OccImage, new Point(0, 0));
            //g.Flush();
        }

        #endregion

        #region Override: OnKey……

        /* OnKeyUp
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (this._ChessGame == null) return;

            switch (e.KeyCode)
            {
                #region 方向控制
                case Keys.Down:
                    if (this.KeySelectPosition.Y > 1)
                    {
                        this.KeySelectPosition = new ChessPosition(this.KeySelectPosition.X, this.KeySelectPosition.Y - 1);
                        this.SetSelectedPointByKey();
                    }
                    break;
                case Keys.Up:
                    if (this.KeySelectPosition.Y < 8)
                    {
                        this.KeySelectPosition = new ChessPosition(this.KeySelectPosition.X, this.KeySelectPosition.Y + 1);
                        this.SetSelectedPointByKey();
                    }
                    break;
                case Keys.Left:
                    if (this.KeySelectPosition.X > 1)
                    {
                        this.KeySelectPosition = new ChessPosition(this.KeySelectPosition.X - 1, this.KeySelectPosition.Y);
                        this.SetSelectedPointByKey();
                    }
                    break;
                case Keys.Right:
                    if (this.KeySelectPosition.Y < 8)
                    {
                        this.KeySelectPosition = new ChessPosition(this.KeySelectPosition.X + 1, this.KeySelectPosition.Y);
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
        */

        #endregion

        #region Override: OnMouse……

        /// <summary>
        /// 鼠标左键是否被按下
        /// </summary>
        private bool _isMouseDown = false;
        /// <summary>
        /// 被选择的棋子
        /// </summary>
        protected Chessman SelectChessman;
        /// <summary>
        /// 被选择的棋子的图像
        /// </summary>
        protected Image SelectChessmanImage;
        protected Rectangle SelectMovedRectangle;


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this._isMouseDown = true;
            if (this._ChessGame == null) return;
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
                            ChessPosition position = this._ChessGame[x, y];
                            if (!this.ChessGame.HasChessman(position))
                                return;//找到矩形，但矩形中无棋子，退出
                            if (this.Chessmans.TryGetChessman(position.Dot, out this.SelectChessman))
                            {
                                if (this.SelectChessman.ChessmanSide != this.CurrChessSide)
                                {
                                    return;//找到棋子，但棋子战方不符
                                }
                                this.SelectedChessPosition = new ChessPosition(x, y);
                                this.EnableMoveInPosition = this.SelectChessman.GetEnablePositions();
                                this.SelectChessmanImage = ChessBoardService.GetChessmanImage(this.SelectChessman.ChessmanType);
                                this.Refresh();
                            }
                            else
                            {
                                return;
                            }
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

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this._isMouseDown = false;
            if (this._ChessGame == null) return;
            this.SelectedChessPosition = ChessPosition.Empty;
            this.EnableMoveInPosition = null;
            this.SelectChessman = null;
            this.SelectChessmanImage = null;
            this.SelectMovedRectangle = Rectangle.Empty;
            this.Cursor = Cursors.Default;
            this.Refresh();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this._ChessGame == null) return;
            for (int x = 1; x <= 8; x++)
            {
                for (int y = 1; y <= 8; y++)
                {
                    if (!this.Rectangles[x - 1, y - 1].Contains(e.Location))
                    {
                        continue;
                    }
                    if (_isMouseDown)
                    {//移动棋子
                        this.Cursor = Cursors.Hand;
                        this.SelectMovedRectangle = new Rectangle(
                            new Point(e.X - _currManRect.Width / 2, e.Y - _currManRect.Height / 2), _currManRect.Size);
                        this.Refresh();
                    }
                    else
                    {
                        ChessPosition position = this._ChessGame[x, y];
                        if (!this.ChessGame.HasChessman(position))
                        {
                            this.MouseMovedChessPosition = null;
                            this.Refresh();
                            return;//找到矩形，但矩形中无棋子，退出
                        }
                        Chessman selectChessman;
                        if (this.Chessmans.TryGetChessman(position.Dot, out selectChessman))
                        {
                            if (selectChessman.ChessmanSide != this.CurrChessSide)
                            {
                                return;//找到棋子，但棋子战方不符
                            }
                            this.MouseMovedChessPosition = new ChessPosition(x, y);
                            this.Refresh();
                            return;
                        }
                    }//if (_isMouseDown)
                }//for y
            }//for x
        }

        #endregion

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

        #region public: MoveIn

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

        #region protected virtual: === Paint ===

        #region Paint_ChessBoardGrid
        /// <summary>绘制棋盘
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="rectangles">64个棋格矩形集合</param>
        /// <param name="hasGridImage">是否有棋格矩形背景图片</param>
        /// <param name="XofPanel">棋盘左上角X坐标</param>
        /// <param name="YofPanel">棋盘左上角Y坐标</param>
        /// <param name="rectangleWidth">矩形宽度</param>
        /// <param name="rectangleHeight">矩形高度</param>
        protected virtual void Paint_ChessBoardGrid()
        {
            Graphics g = Graphics.FromImage(this.OccImage);
            for (int x = 1; x <= 8; x++)
            {
                for (int y = 1; y <= 8; y++)
                {
                    _currRect = ChessBoard.GetRectangle(x, y, _XofPanel, _YofPanel, _rectangleWidth, _rectangleHeight);
                    this.Rectangles[x - 1, y - 1] = _currRect;
                    if ((x + y) % 2 != 0)
                    {
                        g.DrawImage(ChessBoardService.WhiteGridImage, _currRect);
                        g.DrawRectangle(Pens.Black, _currRect);
                    }
                    else
                    {
                        g.DrawImage(ChessBoardService.BlackGridImage, _currRect);
                        g.DrawRectangle(Pens.Black, _currRect);
                    }
                }
            }
            _currRect = Rectangle.Empty;
        }
        /// <summary>
        /// 仅供棋盘绘制时声明的棋子背景图片Image变量。防止反复在OnPaint事件中声明造成内存碎片。
        /// </summary>
        private Image _currManImage;
        /// <summary>
        /// 仅供棋盘绘制时声明的矩形变量。防止反复在OnPaint事件中声明造成内存碎片。
        /// </summary>
        private Rectangle _currRect = Rectangle.Empty;
        #endregion

        #region Paint_ChessmanImage
        /// <summary>1. 用指定的棋子图片集合绘制棋子
        /// </summary>
        /// <param name="g">Graphics</param>
        protected virtual void Paint_ChessmanImage()
        {
            Graphics g = Graphics.FromImage(this.OccImage);
            foreach (Chessman man in this.Chessmans)
            {
                if (man.IsCaptured)
                    continue;
                _currManRect = ChessBoard.GetChessmanRectangle(this, man.CurrPosition);
                _currManImage = ChessBoardService.GetChessmanImage(man.ChessmanType);
                g.DrawImage(_currManImage, _currManRect);

            }
            //_currManRect = Rectangle.Empty;
            _currManImage = null;
        }
        /// <summary>
        /// 仅供棋盘绘制时声明棋子背景图片的矩形变量。防止反复在OnPaint事件中声明造成内存碎片。
        /// </summary>
        private Rectangle _currManRect;
        #endregion

        #region Paint_EnableMoveInPosition
        /// <summary>2. 绘制能移动到的矩形位置
        /// </summary>
        /// <param name="g">Graphics</param>
        protected virtual void Paint_EnableMoveInPosition()
        {
            if (this.SelectedChessPosition == null)
                return;
            if (this.EnableMoveInPosition == null)
                return;
            Graphics g = Graphics.FromImage(this.OccImage);
            Rectangle rect = Rectangle.Empty;
            foreach (ChessPosition position in this.EnableMoveInPosition)
            {
                rect = this.Rectangles[position.X, position.Y];
                for (int i = 0; i <= 1; i++)
                {
                    g.DrawRectangle(Pens.Green, Rectangle.Inflate(rect, i, i));
                }
            }
        }
        #endregion

        #region Paint_MouseMovedPosition
        /// <summary>3. 绘制鼠标移动到的矩形位置
        /// </summary>
        /// <param name="g">Graphics</param>
        protected virtual void Paint_MouseMovedPosition()
        {
            if (this.MouseMovedChessPosition == null)
                return;
            Graphics g = Graphics.FromImage(this.OccImage);
            Rectangle rect = Rectangle.Empty;
            rect = this.Rectangles[this.MouseMovedChessPosition.X, this.MouseMovedChessPosition.Y];
            for (int i = -1; i <= 1; i++)
            {
                g.DrawRectangle(Pens.Red, Rectangle.Inflate(rect, i, i));
            }
        }
        #endregion

        #region Paint_SelectedPosition
        /// <summary>4. 绘制已选择的矩形位置
        /// </summary>
        /// <param name="g">Graphics</param>
        protected virtual void Paint_SelectedPosition()
        {
            if (this.SelectedChessPosition == null)
                return;
            Graphics g = Graphics.FromImage(this.OccImage);
            Rectangle rect = Rectangle.Empty;
            rect = this.Rectangles[this.SelectedChessPosition.X, this.SelectedChessPosition.Y];
            for (int i = -1; i <= 1; i++)
            {
                g.DrawRectangle(Pens.Yellow, Rectangle.Inflate(rect, i, i));
            }
        }
        #endregion

        #region Paint_SelectedPosition
        /// <summary>5. 绘制移动的棋子
        /// </summary>
        /// <param name="g">Graphics</param>
        private void Paint_SelectedMovedChessman()
        {
            if (this.SelectChessmanImage == null)
                return;
            Graphics g = Graphics.FromImage(this.OccImage);
            g.DrawImage(this.SelectChessmanImage, this.SelectMovedRectangle);
        }

        #endregion

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

        #region static

        /// <summary>
        /// 获取棋子将绘制的矩形(棋子填充比棋格小15%，以保证美观)
        /// </summary>
        /// <param name="board">传递引用的ChessBoard类型</param>
        /// <param name="chessGrid">ChessGrid</param>
        private static Rectangle GetChessmanRectangle(ChessBoard board, ChessPosition pos)
        {
            int offset = (int)(board._rectangleWidth * 0.15);//棋子填充比棋格小20%，以保证美观
            Rectangle boardRect = board.Rectangles[pos.X, pos.Y];
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
