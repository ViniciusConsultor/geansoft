﻿using System;
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
        #region ctor

        /// <summary>
        /// 构造函数
        /// </summary>
        protected Chessman() { }

        #endregion

        #region Property

        /// <summary>
        /// 棋子的类型
        /// </summary>
        public virtual Enums.ChessmanType ChessmanType { get; protected set; }
        /// <summary>
        /// 棋子的战方
        /// </summary>
        public virtual Enums.ChessmanSide ChessmanSide { get; protected set; }
        /// <summary>
        /// 获取或设置该棋子是否已被杀死
        /// </summary>
        public virtual bool IsCaptured { get; internal set; }
        /// <summary>
        /// 棋子所在位置
        /// </summary>
        public virtual ChessPosition CurrPosition { get; protected set; }

        #endregion

        #region abstract

        protected abstract ChessPosition SetCurrPosition(ChessPosition position);
        public abstract ChessPosition[] GetEnablePositions();

        #endregion

        #region Override

        /// <summary>
        /// 重写。生成该棋子的字符表示。
        /// </summary>
        /// <returns>大写表示为白棋，小写表示黑棋</returns>
        public override string ToString()
        {
            return Enums.FromChessmanType(this.ChessmanType);
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;

            Chessman man = obj as Chessman;
            if (this.ChessmanType.Equals(man.ChessmanType))
                return false;
            if (this.ChessmanSide.Equals(man.ChessmanSide))
                return false;
            if (this.IsCaptured.Equals(man.IsCaptured))
                return false;
            if (this.CurrPosition.Equals(man.CurrPosition))
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked(3 * (
                this.ChessmanSide.GetHashCode() +
                this.ChessmanType.GetHashCode() +
                this.IsCaptured.GetHashCode() +
                this.CurrPosition.GetHashCode()
                ));
        }

        #endregion

        #region static

        /// <summary>
        /// 表示棋子为空(null)时。此变量为只读。
        /// </summary>
        public static readonly Chessman Empty = null;
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

        /// <summary>根据指定的棋子战方与指定的棋子X坐标获取开局的棋子坐标</summary>
        /// <param name="side">指定的棋子战方</param>
        /// <param name="x">指定的棋子X坐标</param>
        protected static ChessPosition GetOpenningsPosition(Enums.ChessmanSide side, int x)
        {
            ChessPosition point = ChessPosition.Empty;
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    point = new ChessPosition(x, 1);
                    break;
                case Enums.ChessmanSide.Black:
                    point = new ChessPosition(x, 8);
                    break;
            }
            return point;
        }

        #endregion
    }
}