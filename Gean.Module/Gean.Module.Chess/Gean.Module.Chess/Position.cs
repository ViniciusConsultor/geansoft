﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Gean.Module.Chess
{
    /*   FEN Dot
     * 
     *   8 |     1   2   3   4   5   6   7   8
     *   7 |     9  10  11  12  13  14  15  16
     *   6 |    17  18  19  20  21  22  23  24
     *   5 |    25  26  27  28  29  30  31  32
     *   4 |    33  34  35  36  37  38  39  40
     *   3 |    41  42  43  44  45  46  47  48
     *   2 |    49  50  51  52  53  54  55  56
     *   1 |    57  58  59  60  61  62  63  64
     *          ------------------------------
     *           1   2   3   4   5   6   7   8
     *           a   b   c   d   e   f   g   h
     *   
     *   this.Dot = (8 * (7 - _y)) + (_x + 1);
     */

    /// <summary>
    /// 一个描述棋盘位置的类型
    /// </summary>
    [Serializable]
    public class Position
    {

        #region Static

        /// <summary>
        /// 值为空时。该成员为只读状态。
        /// </summary>
        public static readonly Position Empty = null;

        /// <summary>
        /// 根据棋盘点的字符串值(e4,a8,h1...)解析Chess Position
        /// </summary>
        /// <param name="value">棋盘点的字符串值(e4,a8,h1...)</param>
        public static Position Parse(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException();
            if (value.Length != 2)
                return Empty;

            char horizontal = value[0];
            int vertical = int.Parse(value[1].ToString());

            return new Position(horizontal, vertical);
        }

        /// <summary>
        /// 根据指定的相应的FEN的点获取相应的棋盘位置
        /// </summary>
        /// <param name="dot"></param>
        /// <returns></returns>
        public static Position GetPositionByDot(int dot)
        {
            if (dot < 1 || dot > 64)
            {
                return Empty;
            }
            int x;
            int y;
            Position.CalculateXY(dot, out x, out y);
            return new Position(x, y);
        }

        /// <summary>
        /// 将指定的相应的FEN的点转换为横坐标(1-8)与纵坐标(1-8)
        /// </summary>
        /// <param name="x">横坐标(1-8)</param>
        /// <param name="y">纵坐标(1-8)</param>
        public static void CalculateXY(int dot, out int x, out int y)
        {
            x = dot % 8;
            y = 8 - ((dot - 1) / 8);
            if (x == 0) x = 8;
        }

        /// <summary>
        /// 将指定的横坐标与指定的纵坐标转换成相应的FEN的点
        /// </summary>
        /// <param name="x">横坐标(1-8)</param>
        /// <param name="y">纵坐标(1-8)</param>
        public static int CalculateDot(int x, int y)
        {
            return (8 * (7 - y)) + (x + 1);
        }

        #endregion

        #region ctor

        /// <summary>
        /// 一个描述棋盘位置的类型
        /// </summary>
        /// <param name="horizontal">横坐标(a-h)</param>
        /// <param name="vertical">纵坐标(1-8)</param>
        public Position(char horizontal, int vertical)
        {
            _horizontal = horizontal;
            _vertical = vertical;
            _x = Utility.CharToInt(horizontal) - 1;
            _y = vertical - 1;
            this.Dot = Position.CalculateDot(_x, _y);
        }

        /// <summary>
        /// 一个描述棋盘位置的类型
        /// </summary>
        /// <param name="x">横坐标(1-8)</param>
        /// <param name="y">纵坐标(1-8)</param>
        public Position(int x, int y)
        {
            _x = x - 1;
            _y = y - 1;
            _horizontal = Utility.IntToChar(x);
            _vertical = y;
            this.Dot = Position.CalculateDot(_x, _y);
        }

        #endregion

        #region Property

        /// <summary>
        /// 获取或设置当前位置的棋盘横坐标(a-h)
        /// </summary>
        public char Horizontal { get { return _horizontal; } }
        /// <summary>
        /// 当前位置的棋盘横坐标(a-h)
        /// </summary>
        [NonSerialized]
        private char _horizontal;

        /// <summary>
        /// 获取或设置当前位置的棋盘纵坐标(1-8)
        /// </summary>
        public int Vertical { get { return _vertical; } }
        /// <summary>
        /// 当前位置的棋盘纵坐标(1-8)
        /// </summary>
        [NonSerialized]
        private int _vertical;

        /// <summary>
        /// 获取或设置当前位置的计算机X坐标(0-7)
        /// </summary>
        public int X { get { return _x; } }
        /// <summary>
        /// 当前位置的计算机X坐标(0-7)
        /// </summary>
        [NonSerialized]
        private int _x;

        /// <summary>
        /// 获取或设置当前位置的计算机Y坐标(0-7)
        /// </summary>
        public int Y { get { return _y; } }
        /// <summary>
        /// 当前位置的计算机Y坐标(0-7)
        /// </summary>
        [NonSerialized]
        private int _y;

        /// <summary>
        /// 获取该位置对应FEN的点(1-64)
        /// </summary>
        public int Dot { get; private set; }

        #endregion

        #region Override

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;

            Position point = (Position)obj;
            if (this.X != point.X) return false;
            if (this.Y != point.Y) return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked
                (3 * (
                this._x.GetHashCode() +
                this._y.GetHashCode()
                ));
        }
        public override string ToString()
        {
            return string.Format("{0}{1}", this.Horizontal, this.Vertical);
        }

        #endregion

        #region Shift

        /// <summary>
        /// 向北移一格
        /// </summary>
        internal Position ShiftNorth()
        {
            if (_y == 7)
                return Position.Empty;
            return new Position(_horizontal, _y + 2);
        }
        /// <summary>
        /// 向南移一格
        /// </summary>
        internal Position ShiftSouth()
        {
            if (_y == 0)
                return Position.Empty;
            return new Position(_horizontal, _y);
        }
        /// <summary>
        /// 向西移一格
        /// </summary>
        internal Position ShiftWest()
        {
            if (_x == 0)
                return Position.Empty;
            return new Position(_x, _vertical);
        }
        /// <summary>
        /// 向东移一格
        /// </summary>
        internal Position ShiftEast()
        {
            if (_x == 7)
                return Position.Empty;
            return new Position(_x + 2, _vertical);
        }
        /// <summary>
        /// 向西北移一格
        /// </summary>
        internal Position ShiftWestNorth()
        {
            if (_x == 0 || _y == 7)
                return Position.Empty;
            return new Position(_x, _y + 2);
        }
        /// <summary>
        /// 向东北移一格
        /// </summary>
        internal Position ShiftEastNorth()
        {
            if (_x == 7 || _y == 7)
                return Position.Empty;
            return new Position(_x + 2, _y + 2);
        }
        /// <summary>
        /// 向西南移一格
        /// </summary>
        internal Position ShiftWestSouth()
        {
            if (_x == 0 || _y == 0)
                return Position.Empty;
            return new Position(_x, _y);
        }
        /// <summary>
        /// 向东南移一格
        /// </summary>
        internal Position ShiftEastSouth()
        {
            if (_x == 7 || _y == 0)
                return Position.Empty;
            return new Position(_x + 2, _y);
        }

        #endregion

    }
}