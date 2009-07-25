using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 一种国际象棋棋格的坐标表示方法。类似Point的一个类，比Point多一些与返回字母的方法。
    /// </summary>
    public class ChessSquare
    {
        /// <summary>
        /// 返回一个为空的值。该变量为只读。
        /// </summary>
        public static readonly ChessSquare Empty = null;

        /// <summary>
        /// 一种国际象棋棋格的表示方法
        /// </summary>
        /// <param name="x">棋格的横坐标</param>
        /// <param name="y">棋格的纵坐标</param>
        public ChessSquare(int x, int y)
        {
            #region Exception
            if (!ChessSquare.Check(x, y))
            {
                throw new ArgumentOutOfRangeException("坐标值超限！");
            }
            #endregion

            this.X = x;
            this.Y = y;
            this.CharX = Utility.IntToChar(x);
            this.SquareSide = ChessSquare.GetSquareSide(x, y);
        }
        /// <summary>
        /// 一种国际象棋棋格的表示方法
        /// </summary>
        /// <param name="c">棋格的横坐标的字符</param>
        /// <param name="y">棋格的纵坐标</param>
        public ChessSquare(char c, int y) : this(Utility.CharToInt(c), y) { }

        /// <summary>
        /// 棋格的横坐标
        /// </summary>
        public int X { get; internal set; }
        /// <summary>
        /// 棋格的横坐标的字母表示法。
        /// </summary>
        public char CharX { get; private set; }
        /// <summary>
        /// 棋格的纵坐标
        /// </summary>
        public int Y { get; internal set; }
        /// <summary>
        /// 黑格,白格
        /// </summary>
        public Enums.ChessSquareSide SquareSide { get; internal set; }

        /// <summary>
        /// 获取该棋格坐标点的有效性
        /// </summary>
        public bool IsAvailability
        {
            get { return ChessSquare.Check(this.X, this.Y); }
        }

        /// <summary>
        /// 获取或设置当前格子中拥有的棋子
        /// </summary>
        public ChessmanBase OwnedChessman
        {
            get { return this._ownedChessman; }
            set
            {
                OnPlayBefore(new PlayEventArgs(value));//注册落子前事件
                this._ownedChessman = value;
                OnPlayAfter(new PlayEventArgs(value));//注册落子后事件
            }
        }
        private ChessmanBase _ownedChessman = null;

        /// <summary>
        /// 用指定的值设置棋格的横坐标
        /// </summary>
        /// <param name="x">一个整数值，不能小于1，且不能大于8</param>
        public void SetX(int x)
        {
            this.X = x;
        }
        /// <summary>
        /// 用指定的值设置棋格的横坐标
        /// </summary>
        /// <param name="c">一个字母，是一个不能小于a,大于h的字母</param>
        public void SetX(char c)
        {
            this.X = Utility.CharToInt(c);
        }
        /// <summary>
        /// 用指定的值设置棋格的纵坐标
        /// </summary>
        /// <param name="x">一个整数值，不能小于1，且不能大于8</param>
        public void SetY(int i)
        {
            this.Y = i;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.CharX).Append(this.Y);
            return sb.ToString();
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            ChessSquare square = (ChessSquare)obj;
            if (!square.SquareSide.Equals(this.SquareSide))
                return false;
            if (!square.X.Equals(this.X))
                return false;
            if (!square.Y.Equals(this.Y))
                return false;
            if (!UtilityEquals.PairEquals(this.OwnedChessman, square.OwnedChessman))
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked((X.GetHashCode() + Y.GetHashCode() + SquareSide.GetHashCode()) * 3);
        }

        /// <summary>
        /// 根据坐标值判断该格是黑格还是白格
        /// </summary>
        private static Enums.ChessSquareSide GetSquareSide(int x, int y)
        {
            if ((y % 2) == 0)
            {
                if ((x % 2) == 0)
                    return Enums.ChessSquareSide.White;
                else
                    return Enums.ChessSquareSide.Black;
            }
            else
            {
                if ((x % 2) == 0)
                    return Enums.ChessSquareSide.Black;
                else
                    return Enums.ChessSquareSide.White;
            }
        }

        /// <summary>
        /// 检查指定的一双整数值是否符合棋盘坐标值的限制
        /// </summary>
        public static bool Check(int x, int y)
        {
            if (((x >= 1 && x <= 8)) && ((y >= 1 && y <= 8)))
                return true;
            else
                return false;
        }

        public static ChessSquare Parse(string square)
        {
            if (string.IsNullOrEmpty(square))
                throw new ArgumentOutOfRangeException("Square IsNullOrEmpty!");
            if (square.Length != 2)
                throw new ArgumentOutOfRangeException("\"" + square + "\" is OutOfRange!");

            int x = Utility.CharToInt(square[0]);
            int y = Convert.ToInt32(square.Substring(1));
            return new ChessSquare(x, y);
        }

        #region custom event

        /// <summary>
        /// 在该棋格中拥有的棋子发生变化的时候(变化前、即将发生变化)发生。
        /// (包括该在棋格落子和移走该棋格拥有的棋子)
        /// </summary>
        public event PlayBeforeEventHandler PlayBeforeEvent;
        protected virtual void OnPlayBefore(PlayEventArgs e)
        {
            if (PlayBeforeEvent != null)
                PlayBeforeEvent(this, e);
        }
        public delegate void PlayBeforeEventHandler(object sender, PlayEventArgs e);

        /// <summary>
        /// 在该棋格中拥有的棋子发生变化后发生(包括该在棋格落子和移走该棋格拥有的棋子)
        /// </summary>
        public event PlayAfterEventHandler PlayAfterEvent;
        protected virtual void OnPlayAfter(PlayEventArgs e)
        {
            if (PlayAfterEvent != null)
                PlayAfterEvent(this, e);
        }
        public delegate void PlayAfterEventHandler(object sender, PlayEventArgs e);

        public class PlayEventArgs : ChessmanEventArgs
        {
            public PlayEventArgs(ChessmanBase man)
                : base(man)
            {

            }
        }

        #endregion
    }
}
