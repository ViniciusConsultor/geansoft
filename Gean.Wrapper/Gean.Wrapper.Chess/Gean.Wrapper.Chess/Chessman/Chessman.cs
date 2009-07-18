﻿using System;
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
        internal Chessman(Enums.ChessmanType type, Enums.ChessmanSide side)
        {
            this.ChessmanType = type;
            this.ChessmanSide = side;
            this.Squares = new SquareCollection();
            this.InitializeComponent();
        }

        /// <summary>
        /// 简单工厂模式，创建不同类型的棋子类
        /// </summary>
        public static Chessman Create
            (Enums.ChessmanType manType, Enums.ChessmanSide manSide, Enums.ChessboardGridSide gridSide)
        {
            switch (manType)
            {
                case Enums.ChessmanType.Rook:
                    return new ChessmanRook(manSide, gridSide);
                case Enums.ChessmanType.Knight:
                    return new ChessmanRook(manSide, gridSide);
                case Enums.ChessmanType.Bishop:
                    return new ChessmanRook(manSide, gridSide);
                case Enums.ChessmanType.Queen:
                    return new ChessmanRook(manSide, gridSide);
                case Enums.ChessmanType.King:
                    return new ChessmanRook(manSide, gridSide);
                case Enums.ChessmanType.Pawn:
                    return new ChessmanRook(manSide, gridSide);
                case Enums.ChessmanType.Nothing:
                    return new ChessmanRook(manSide, gridSide);
                default:
                    throw new ArgumentOutOfRangeException(manType.ToString());
            }
        }

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
        /// 表示棋子为空(null)时。此变量为只读。
        /// </summary>
        public static readonly Chessman Empty = null;

        /// <summary>
        /// 棋子的战方
        /// </summary>
        public Enums.ChessmanSide ChessmanSide { get; protected set; }
        /// <summary>
        /// 棋子的类型
        /// </summary>
        public Enums.ChessmanType ChessmanType { get; private set; }
        /// <summary>
        /// 该棋子走过的路线(坐标的集合)
        /// </summary>
        public SquareCollection Squares { get; set; }

        /// <summary>
        /// 获取或设置该棋子是否已被杀死
        /// </summary>
        public virtual bool IsKilled { get; internal set; }

        public abstract void InitializeComponent();
        public abstract string ToSimpleString();

        /// <summary>
        /// 根据指定的棋字战方、棋格方获取开局的棋子坐标
        /// </summary>
        internal static Square GetOpenningsSquare(Enums.ChessmanSide side, Enums.ChessboardGridSide gridSide, int left, int right)
        {
            Square square = new Square();
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    {
                        switch (gridSide)
                        {
                            case Enums.ChessboardGridSide.Black:
                                square = new Square(left, 1);
                                break;
                            case Enums.ChessboardGridSide.White:
                                square = new Square(right, 1);
                                break;
                        }
                        break;
                    }
                case Enums.ChessmanSide.Black:
                    {
                        switch (gridSide)
                        {
                            case Enums.ChessboardGridSide.Black:
                                square = new Square(right, 8);
                                break;
                            case Enums.ChessboardGridSide.White:
                                square = new Square(left, 8);
                                break;
                        }
                        break;
                    }
            }
            return square;
        }
    }
}