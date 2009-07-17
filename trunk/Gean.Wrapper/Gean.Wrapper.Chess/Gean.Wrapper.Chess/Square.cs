using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 一种国际象棋棋格的表示方法。类似Point的一个类，多一些与返回字母的方法。
    /// </summary>
    public struct Square
    {
        /// <summary>
        /// 棋格的横坐标
        /// </summary>
        public int X
        {
            get { return this._x; }
        }
        private int _x;

        /// <summary>
        /// 棋格的横坐标的字母表示法。
        /// </summary>
        public char CharX
        {
            get { return Utility.IntToChar(this._x); }
        }

        /// <summary>
        /// 棋格的纵坐标
        /// </summary>
        public int Y
        {
            get { return this._y; }
        }
        private int _y;

        /// <summary>
        /// 一种国际象棋棋格的表示方法
        /// </summary>
        /// <param name="x">棋格的横坐标</param>
        /// <param name="y">棋格的纵坐标</param>
        public Square(int x, int y)
        {
            #region Exception
            if (!Square.Check(x, y))
            {
                throw new ArgumentOutOfRangeException("坐标值超限！");
            }
            #endregion

            this._x = 0; this._y = 0;
            this._x = x;
            this._y = y;
        }

        public string Name { get { return this.ToString(); } }

        /// <summary>
        /// 获取该棋格坐标点的有效性
        /// </summary>
        public bool IsAvailability
        {
            get
            {
                return Square.Check(this._x, this._y);
            }
        }

        /// <summary>
        /// 按指定的宽度返回该Grid描述的棋格的矩形。
        /// </summary>
        /// <param name="width">指定的宽度</param>
        /// <returns></returns>
        public Rectangle GetRectangle(int width)
        {
            Point point = new Point((_x - 1) * width, (8 - _y) * width);
            Size size = new Size(width, width);
            return new Rectangle(point, size);
        }

        /// <summary>
        /// 用指定的值设置棋格的横坐标
        /// </summary>
        /// <param name="x">一个整数值，不能小于1，且不能大于8</param>
        public void SetX(int x)
        {
            this._x = x;
        }
        /// <summary>
        /// 用指定的值设置棋格的横坐标
        /// </summary>
        /// <param name="c">一个字母，是一个不能小于a,大于h的字母</param>
        public void SetX(char c)
        {
            this._x = Utility.CharToInt(c);
        }
        /// <summary>
        /// 用指定的值设置棋格的纵坐标
        /// </summary>
        /// <param name="x">一个整数值，不能小于1，且不能大于8</param>
        public void SetY(int i)
        {
            this._y = i;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.CharX).Append(this._y);
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            Square grid = (Square)obj;
            if (!grid.X.Equals(this.X))
                return false;
            if (!grid.Y.Equals(this.Y))
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked((this._x.GetHashCode() ^ this._y.GetHashCode()) * 27);
        }

        public static bool operator !=(Square a, Square b)
        {
            return !a.Equals(b);
        }
        public static bool operator ==(Square a, Square b)
        {
            return a.Equals(b);
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
    }
}
