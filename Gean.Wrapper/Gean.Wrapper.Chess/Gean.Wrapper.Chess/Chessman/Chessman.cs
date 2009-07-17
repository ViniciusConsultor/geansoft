using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 棋子抽象类(王,后,车,象,马,兵)
    /// </summary>
    public abstract class Chessman
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">棋子类型</param>
        public Chessman(Enums.ChessmanType type, Enums.ChessmanSide side)
        {
            this.ChessmanType = type;
            this.ChessmanSide = side;
            this.Squares = new SquareCollection();
            this.InitializeComponent();
        }

        #region override
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Chessman: ");
            sb.Append(this.ChessmanSide.ToString());
            sb.Append(',');
            sb.Append(this.ChessmanType.ToString());
            return sb.ToString();
        }
        public override bool Equals(object obj)
        {
            Chessman man = obj as Chessman;
            if (man.ChessmanType != this.ChessmanType)
            {
                return false;
            }
            if (man.ChessmanSide != this.ChessmanSide)
            {
                return false;
            }
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked(17 * this.ChessmanSide.GetHashCode() ^ this.ChessmanType.GetHashCode());
        }
        #endregion

        /// <summary>
        /// 棋子的战方
        /// </summary>
        public Enums.ChessmanSide ChessmanSide { get; protected set; }
        /// <summary>
        /// 棋子的类型
        /// </summary>
        public Enums.ChessmanType ChessmanType { get; private set; }
        /// <summary>
        /// 该棋子走过的路线
        /// </summary>
        public SquareCollection Squares { get; set; }

        /// <summary>
        /// 获取或设置该棋子是否已被杀死
        /// </summary>
        public virtual bool IsKilled
        {
            get { return this._isKilled; }
            set
            {
                OnKilling(new ChessmanKillEventArgs(this));//注册棋子即将被杀死的事件
                this._isKilled = value;
                OnKilled(new ChessmanKillEventArgs(this));//注册棋子被杀死后的事件
            }
        }
        private bool _isKilled = false;

        public abstract void InitializeComponent();
        public abstract string ToSimpleString();

        #region custom event

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
        /// 包含棋子杀死事件的数据
        /// </summary>
        public class ChessmanKillEventArgs : ChessmanEventArgs
        {
            public ChessmanKillEventArgs(Chessman man)
                : base(man)
            {
            }
        }

        #endregion
    }
}