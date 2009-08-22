using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 棋子抽象类(王,后,车,象,马,兵)
    /// </summary>
    public abstract class Chessman
    {
        /// <summary>
        /// 表示棋子为空(null)时。此变量为只读。
        /// </summary>
        public static readonly Chessman Empty = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">棋子类型</param>
        internal Chessman(Enums.ChessmanType type, Enums.ChessmanSide side)
        {
            this.ChessmanType = type;
            this.ChessmanSide = side;
            this.IsCaptured = false;
            //this.ChessPositions = new Stack<ChessPosition>();
        }

        #region override
        public override string ToString()
        {
            return Enums.ChessmanTypeToString(this.ChessmanType);
            //StringBuilder sb = new StringBuilder();
            //sb.Append("Chessman: ");
            //sb.Append(this.ChessmanSide.ToString());
            //sb.Append(',');
            //sb.Append(this.ChessmanType.ToString());
            //return sb.ToString();
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;

            Chessman man = obj as Chessman;
            if (man.ChessmanType != this.ChessmanType) 
                return false;
            if (man.ChessmanSide != this.ChessmanSide) 
                return false;
            //if (!UtilityEquals.EnumerableEquals(this.ChessPositions, man.ChessPositions)) 
            //    return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked(7 * (this.ChessmanSide.GetHashCode() + this.ChessmanType.GetHashCode()));
        }
        #endregion

        /// <summary>
        /// 棋子的战方
        /// </summary>
        public Enums.ChessmanSide ChessmanSide { get; protected set; }
        /// <summary>
        /// 棋子的类型
        /// </summary>
        public Enums.ChessmanType ChessmanType { get; protected set; }
        /// <summary>
        /// 棋子所在位置
        /// </summary>
        public ChessPosition CurrPosition { get; protected set; }
        /// <summary>
        /// 该棋子的棋步<see cref="ChessStep"/>的集合
        /// </summary>
        //public Stack<ChessPosition> ChessPositions { get; set; }

        /// <summary>
        /// 获取或设置该棋子是否已被杀死
        /// </summary>
        public virtual bool IsCaptured { get; internal set; }

        public abstract ChessPosition[] GetEnablePositions();

        /// <summary>
        /// 指示指定的 Chessman 对象是 null 还是 Chessman.Empty。
        /// </summary>
        /// <param name="chessman">指定的 Chessman 对象</param>
        public static bool IsNullOrEmpty(Chessman chessman)
        {
            if (chessman == null)
                return true;
            if (chessman == Chessman.Empty)
                return true;
            return false;
        }

        /// <summary>
        /// 根据指定的棋字战方、棋格方获取开局的棋子坐标
        /// </summary>
        protected static ChessPosition GetOpenningsPosition(Enums.ChessmanSide side, Enums.ChessGridSide gridSide, int left, int right)
        {
            ChessPosition point = ChessPosition.Empty;
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    {
                        switch (gridSide)
                        {
                            case Enums.ChessGridSide.Black:
                                point = new ChessPosition(left, 1);
                                break;
                            case Enums.ChessGridSide.White:
                                point = new ChessPosition(right, 1);
                                break;
                        }
                        break;
                    }
                case Enums.ChessmanSide.Black:
                    {
                        switch (gridSide)
                        {
                            case Enums.ChessGridSide.Black:
                                point = new ChessPosition(right, 8);
                                break;
                            case Enums.ChessGridSide.White:
                                point = new ChessPosition(left, 8);
                                break;
                        }
                        break;
                    }
            }
            return point;
        }

        internal static Chessman CreatMan(Enums.ChessmanType manType, Enums.ChessmanSide manSide, ChessPosition chessPosition)
        {
            throw new NotImplementedException();
        }
    }
}