using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 棋子抽象类(王,后,车,象,马,兵)
    /// </summary>
    public abstract class ChessmanBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sourceGrid">该棋子的初始棋格</param>
        /// <param name="type">棋子类型</param>
        public ChessmanBase(ChessboardGrid sourceGrid, Enums.ChessmanType type)
        {
            //设置该棋子的名字的前缀是实例该棋子时，该棋子的坐标，后缀是该棋子
            this.ChessmanType = type;
            this._name = sourceGrid.ToString() + " - " + this.ChessmanType.ToString();
            this.InitializeComponent();
        }
        public override string ToString()
        {
            return "Chessman: " + this._name;
        }

        /// <summary>
        /// 获取或设置该棋子的名字。
        /// 该棋子的名字组成部份：前缀是实例该棋子时，该棋子的坐标，后缀是该棋子。
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        private string _name;

        public ChessboardGrid GridOwner { get; private set; }

        /// <summary>
        /// 棋子的战方
        /// </summary>
        public Enums.ChessmanSide ChessmanSide { get; protected set; }
        /// <summary>
        /// 棋子的类型
        /// </summary>
        public Enums.ChessmanType ChessmanType { get; private set; }

        /// <summary>
        /// 获取或设置该棋子是否已被杀死
        /// </summary>
        public bool IsKilled
        {
            get { return this._isKilled; }
            set 
            {
                OnKilling(new ChessmanKillEventArgs(this));//注册棋子即将被杀死的事件
                this._isKilled = value;
                OnKilled(new ChessmanKillEventArgs(this));//注册棋子被杀死后的事件
            }
        }
        private bool _isKilled;

        /// <summary>
        /// 将该棋子移入指定的棋格中
        /// </summary>
        /// <param name="grid">指定的棋格</param>
        /// <returns></returns>
        public virtual bool MoveToGrid(ChessboardGrid grid)
        {
            if (grid.IsUsable(this))
            {
                ChessmanMoveEventArgs e = new ChessmanMoveEventArgs(this.GridOwner, grid, this);
                OnMoving(e);//注册移动前事件
                this.GridOwner = grid;
                this.GridOwner.ChessmanOwner = this;
                OnMoved(e);//注册移动后事件
                return true;
            }
            return false;
        }

        public abstract void InitializeComponent();
        public abstract string ToSimpleString();

        #region custom event

        /// <summary>
        /// 在棋子被移动后发生
        /// </summary>
        public event MovedEventHandler MovedEvent;
        protected virtual void OnMoved(ChessmanMoveEventArgs e)
        {
            if (MovedEvent != null)
                MovedEvent(this, e);
        }
        public delegate void MovedEventHandler(object sender, ChessmanMoveEventArgs e);
        
        /// <summary>
        /// 在棋子被移动前发生
        /// </summary>
        public event MovingEventHandler MovingEvent;
        protected virtual void OnMoving(ChessmanMoveEventArgs e)
        {
            if (MovingEvent != null)
                MovingEvent(this, e);
        }
        public delegate void MovingEventHandler(object sender, ChessmanMoveEventArgs e);

        /// <summary>
        /// 在该棋子被杀死后发生
        /// </summary>
        public event KilledEventHandler KilledEvent;
        protected virtual void OnKilled(ChessmanKillEventArgs e)
        {
            if (KilledEvent != null)
                KilledEvent(this, e);
        }
        public delegate void KilledEventHandler(object sender, ChessmanKillEventArgs e);

        /// <summary>
        /// 在该棋子正在被杀死（也可理解为，即将被杀死时）发生
        /// </summary>
        public event KillingEventHandler KillingEvent;
        protected virtual void OnKilling(ChessmanKillEventArgs e)
        {
            if (KillingEvent != null)
                KillingEvent(this, e);
        }
        public delegate void KillingEventHandler(object sender, ChessmanKillEventArgs e);


        /// <summary>
        /// 包含棋子移动事件的数据
        /// </summary>
        public class ChessmanMoveEventArgs : ChessmanEventArgs
        {
            public ChessboardGrid OldGrid { get; set; }
            public ChessboardGrid NewGrid { get; set; }
            public ChessmanMoveEventArgs(ChessboardGrid oldGrid, ChessboardGrid newGrid, ChessmanBase chessman)
                :base(chessman)
            {
                this.OldGrid = oldGrid;
                this.NewGrid = newGrid;
            }
        }
        /// <summary>
        /// 包含棋子杀死事件的数据
        /// </summary>
        public class ChessmanKillEventArgs : ChessmanEventArgs
        {
            public ChessmanKillEventArgs(ChessmanBase man)
                : base(man)
            {

            }
        }

        #endregion

    }

}
